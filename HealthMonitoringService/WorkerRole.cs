using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MovieDiscussionService_Contracts.Contracts;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Repositories;
using MovieDiscussionService_HealthMonitoringService.Proxies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MovieDiscussionService_HealthMonitoringService
{
    public class HealthMonitoringWorker : RoleEntryPoint
    {
        private bool _running = true;
        private HealthMonitoringServiceProvider _service;
        // Dodaj u HealthMonitoringWorker klasu

        private CloudStorageAccount _storageAccount;
        private CloudQueue _adminQueue;

        private HttpListener _listener;

        public override bool OnStart()
        {
            Trace.WriteLine("HealthMonitoringWorker started.");

            // Inicijalizuj storage account pre nego što praviš queue

            _storageAccount = CloudStorageAccount.Parse(
                RoleEnvironment.GetConfigurationSettingValue("DataConnectionString"));

            var queueClient = _storageAccount.CreateCloudQueueClient();
            _adminQueue = queueClient.GetQueueReference("adminnotificationqueue");
            _adminQueue.CreateIfNotExists();

            // HTTP listener
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:50002/");
            _listener.Start();

            Thread listenerThread = new Thread(ListenForRequests);
            listenerThread.Start();

            string connectionString = Environment.GetEnvironmentVariable("DataConnectionString")
                                      ?? "UseDevelopmentStorage=true";
            _service = new HealthMonitoringServiceProvider(connectionString);

            return base.OnStart();
        }

        private void ListenForRequests()
        {
            while (_running)
            {
                try
                {
                    var context = _listener.GetContext();
                    ProcessRequest(context);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("HTTP Listener error: " + ex.Message);
                }
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/health-monitoring")
            {
                var records = _service.GetLastTwoHours();
                var json = System.Text.Json.JsonSerializer.Serialize(records);

                byte[] buffer = Encoding.UTF8.GetBytes(json);
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                context.Response.StatusCode = 404;
            }

            context.Response.OutputStream.Close();
        }

        public override void Run()
        {
            Trace.WriteLine("HealthMonitoringWorker Run() method started.");

            while (_running)
            {
                try
                {
                    Trace.WriteLine("Performing health checks...");

                    // 1. Proveri MovieDiscussionService putem HTTP
                    bool movieOk = CheckService("http://localhost:59271"); // web role endpoint
                    Trace.WriteLine($"MovieDiscussionService status: {movieOk}");

                    // 2. Proveri NotificationService putem AdminNotificationQueue
                    bool notificationOk = false;
                    try
                    {
                        var msg = _adminQueue.GetMessage(); // sync metoda da ne komplikujemo Task
                        if (msg != null)
                        {
                            string json = msg.AsString;
                            var statusObj = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationStatus>(json);

                            // možeš dodati checksum ili timestamp proveru
                            notificationOk = statusObj != null && statusObj.Service == "NotificationService";
                            // Poruku izbriši jer smo je pročitali
                            _adminQueue.DeleteMessage(msg);
                        }
                        else
                        {
                            notificationOk = false; // queue je prazan
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Error reading admin queue: {ex.Message}");
                        notificationOk = false;
                    }

                    Trace.WriteLine($"NotificationService status: {notificationOk}");

                    // 3. Upisi u tabelu HealthCheck
                    var movieRecord = new HealthCheckRecord("MovieDiscussionService")
                    {
                        Status = movieOk ? "OK" : "NOT_OK",
                        CheckTime = DateTime.UtcNow
                    };

                    var notificationRecord = new HealthCheckRecord("NotificationService")
                    {
                        Status = notificationOk ? "OK" : "NOT_OK",
                        CheckTime = DateTime.UtcNow
                    };

                    _service.AddRecord(movieRecord);
                    _service.AddRecord(notificationRecord);

                    Trace.WriteLine("Health checks completed and written to table.");
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"ERROR in Run method: {ex.Message}");
                    Trace.WriteLine($"Stack trace: {ex.StackTrace}");
                }

                Thread.Sleep(3000);
            }
        }
        private bool CheckService(string url)
        {
            try
            {
                Trace.WriteLine($"Checking service at: {url}");

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);
                    var response = client.GetAsync(url).Result;

                    Trace.WriteLine($"Service {url} responded with: {response.StatusCode}");
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Service {url} failed: {ex.Message}");
                return false;
            }
        }
        private class NotificationStatus
        {
            public string Service { get; set; }
            public string Status { get; set; }
            public DateTime Timestamp { get; set; }
        }

    }

}
