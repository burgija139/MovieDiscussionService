using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MovieDiscussionService_Contracts.Contracts;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Repositories;
using MovieDiscussionService_HealthMonitoringService.Proxies;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MovieDiscussionService_HealthMonitoringService
{
    public class HealthMonitoringWorker : RoleEntryPoint
    {
        private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        //private bool _running = true;
        private HealthMonitoringServiceProvider _service;
        private CloudStorageAccount _storageAccount;
        private CloudQueue _adminQueue;

        //private HttpListener _listener;

        public override bool OnStart()
        {
            Trace.WriteLine("HealthMonitoringWorker started.");

            _storageAccount = CloudStorageAccount.Parse(
                RoleEnvironment.GetConfigurationSettingValue("DataConnectionString"));

            var queueClient = _storageAccount.CreateCloudQueueClient();
            _adminQueue = queueClient.GetQueueReference("adminnotificationqueue");
            _adminQueue.CreateIfNotExists();

            string connectionString = Environment.GetEnvironmentVariable("DataConnectionString")
                                      ?? "UseDevelopmentStorage=true";
            _service = new HealthMonitoringServiceProvider(connectionString);

            return base.OnStart();
        }
        public override void Run()
        {
            Trace.TraceInformation("NotificationService Run starting");
            try
            {
                RunAsync(_cts.Token).Wait();
            }
            finally
            {
                _runCompleteEvent.Set();
            }
        }

        public override void OnStop()
        {
            Trace.TraceInformation("NotificationService OnStop");
            _cts.Cancel();
            _runCompleteEvent.WaitOne();
            base.OnStop();
        }

        private async Task RunAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Trace.WriteLine("Performing health checks...");

                    bool movieOk = CheckService("http://localhost:80");
                    Trace.WriteLine($"MovieDiscussionService status: {movieOk}");

                    bool notificationOk = false;
                    try
                    {
                        var msg = await _adminQueue.GetMessageAsync();
                        if (msg != null)
                        {
                            string json = msg.AsString;
                            var statusObj = JsonConvert.DeserializeObject<NotificationStatus>(json);
                            notificationOk = statusObj != null && statusObj.Service == "NotificationService";
                            await _adminQueue.DeleteMessageAsync(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Error reading admin queue: {ex.Message}");
                    }

                    Trace.WriteLine($"NotificationService status: {notificationOk}");

                    _service.AddRecord(new HealthCheckRecord("MovieDiscussionService")
                    {
                        Status = movieOk ? "OK" : "NOT_OK",
                        CheckTime = DateTime.UtcNow
                    });

                    _service.AddRecord(new HealthCheckRecord("NotificationService")
                    {
                        Status = notificationOk ? "OK" : "NOT_OK",
                        CheckTime = DateTime.UtcNow
                    });

                    if (!movieOk || !notificationOk)
                    {
                        foreach (var email in _service.GetAlertEmails())
                        {
                            await SendAlertEmail(email, movieOk, notificationOk);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"ERROR in RunAsync: {ex}");
                }

                await Task.Delay(3000, token);
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
        private async Task SendAlertEmail(string toEmail, bool movieOk, bool notificationOk)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
                var client = new SendGridClient(apiKey);

                var from = new EmailAddress("dusanloncar14@gmail.com", "Health Monitor");
                var subject = "⚠️ Service Health Alert";

                var plainTextContent = $"MovieDiscussionService: {(movieOk ? "OK" : "NOT_OK")}\n" +
                                       $"NotificationService: {(notificationOk ? "OK" : "NOT_OK")}";
                var htmlContent = $"<h3>Service Health Alert</h3>" +
                                  $"<p><b>MovieDiscussionService:</b> {(movieOk ? "✅ OK" : "❌ NOT_OK")}</p>" +
                                  $"<p><b>NotificationService:</b> {(notificationOk ? "✅ OK" : "❌ NOT_OK")}</p>";

                var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(toEmail), subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                Trace.TraceInformation($"Alert email sent to {toEmail}, SendGrid status={response.StatusCode}");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error sending alert email to {toEmail}: {ex}");
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
