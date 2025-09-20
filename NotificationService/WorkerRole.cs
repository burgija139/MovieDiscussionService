using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure;
using SendGrid;
using SendGrid.Helpers.Mail;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Repositories;
using Newtonsoft.Json;
using System.Net.Mail;

namespace NotificationService
{
    public class NotificationService : RoleEntryPoint
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);

        private CloudStorageAccount _storageAccount;
        private CloudQueue _notificationsQueue;

        private CommentRepository _commentRepo;
        private FollowRepository _followRepo;
        // private NotificationLogRepository _logRepo;

        public override bool OnStart()
        {
            Trace.TraceInformation("NotificationService OnStart");

            _storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("DataConnectionString"));

            var queueClient = _storageAccount.CreateCloudQueueClient();
            _notificationsQueue = queueClient.GetQueueReference("notifications");
            _notificationsQueue.CreateIfNotExists();

            _commentRepo = new CommentRepository(_storageAccount);
            _followRepo = new FollowRepository(_storageAccount);
            //_logRepo = new NotificationLogRepository(_storageAccount);

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
                    CloudQueueMessage qMessage = await _notificationsQueue.GetMessageAsync();
                    if (qMessage != null)
                    {
                        string commentId = qMessage.AsString;
                        Trace.TraceInformation($"NotificationService: got commentId '{commentId}'");
                        string[] parts = qMessage.AsString.Split('|');
                        string discussionId = parts[0];
                        string comId = parts[1];



                        // 1) Čitaj komentar
                        var comment = _commentRepo.GetById(discussionId, comId);
                        if (comment == null)
                        {
                            Trace.TraceError($"Comment {commentId} not found!");
                            await _notificationsQueue.DeleteMessageAsync(qMessage);
                            continue;
                        }

                        // 2) Dohvati followere
                        var followers = _followRepo.GetFollowers(comment.PartitionKey)
                            .Where(f => !string.Equals(f, comment.AuthorEmail, StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        int sentCount = 0;

                        // 3) Pošalji mejlove
                        foreach (var follower in followers)
                        {
                            await SendEmail(follower, comment);
                            sentCount++;
                        }

                        // 4) Upisi log
                        /*var log = new NotificationLog
                        {
                            RowKey = Guid.NewGuid().ToString("N"),
                            PartitionKey = "Notification",
                            CommentId = commentId,
                            DateSent = DateTime.UtcNow,
                            SentCount = sentCount
                        };
                        _logRepo.Add(log);
                        */
                        // 5) Obriši poruku
                        await _notificationsQueue.DeleteMessageAsync(qMessage);

                        Trace.TraceInformation($"NotificationService: processed comment {commentId}, emails={sentCount}");
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), token);
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"Error in NotificationService: {ex.Message}");
                    await Task.Delay(TimeSpan.FromSeconds(5), token);
                }
            }
        }

        private async Task SendEmail(string toEmail, Comment comment)
        {
            try
            {
                // forsiraj TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var apiKey = CloudConfigurationManager.GetSetting("SendGridApiKey");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("dusanloncar14@gmail.com", "Movie Discussion Service");
                var subject = $"New comment in discussion {comment.PartitionKey}";
                var to = new EmailAddress(toEmail);
                var plainTextContent = $"{comment.AuthorEmail} commented: {comment.Text}";
                var htmlContent = $"<strong>{comment.AuthorEmail}</strong> commented:<br/>{comment.Text}";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
                Trace.TraceInformation($"SendGrid response: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error sending email to {toEmail} for comment {comment.RowKey}: {ex}");
                // možeš dodati await Task.Delay(...) ako želiš retry
            }
        }
    }

    // DTO za log
    /*public class NotificationLog : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public string CommentId { get; set; }
        public DateTime DateSent { get; set; }
        public int SentCount { get; set; }
    }

    // Repozitorijum za log
    public class NotificationLogRepository : TableRepository<NotificationLog>
    {
        public NotificationLogRepository(CloudStorageAccount account)
            : base(account, "NotificationLog") { }
    }*/
}
