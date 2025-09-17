using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure;
using Microsoft.Azure;

namespace NotificationService
{
	public class NotificationService : RoleEntryPoint
	{
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();
		private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);

		private CloudStorageAccount _storageAccount;
		private CloudQueue _notificationsQueue;

		public override bool OnStart()
		{
			Trace.TraceInformation("NotificationService OnStart");

			// Inicijalizacija storage account-a iz konfiguracije
			_storageAccount = CloudStorageAccount.Parse(
				CloudConfigurationManager.GetSetting("DataConnectionString"));

			// Kreiranje/vezivanje na queue "notifications"
			var queueClient = _storageAccount.CreateCloudQueueClient();
			_notificationsQueue = queueClient.GetQueueReference("notifications");
			_notificationsQueue.CreateIfNotExists();

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
					// Pokušaj da uzmemo poruku iz queue-a
					CloudQueueMessage qMessage = await _notificationsQueue.GetMessageAsync();
					if (qMessage != null)
					{
						string payload = qMessage.AsString; // npr. commentId ili JSON sa podacima
						Trace.TraceInformation($"NotificationService: got message '{payload}'");

						// TODO: ovde pozovi logiku za dohvat pretplatnika i slanje emailova
						// npr. var sentCount = SendNotificationsForComment(payload);
						// i loguj slanje u tabelu (persist)

						// Kada obrada uspe, izbriši poruku iz reda
						await _notificationsQueue.DeleteMessageAsync(qMessage);
						Trace.TraceInformation($"NotificationService: processed message '{payload}'");
					}
					else
					{
						// nema poruka — malo odspavaj
						await Task.Delay(TimeSpan.FromSeconds(1), token);
					}
				}
				catch (StorageException se)
				{
					Trace.TraceError($"StorageException in NotificationService: {se.Message}");
					await Task.Delay(TimeSpan.FromSeconds(5), token);
				}
				catch (OperationCanceledException) when (token.IsCancellationRequested)
				{
					// graceful exit
				}
				catch (Exception ex)
				{
					Trace.TraceError($"Unhandled exception in NotificationService: {ex.Message}");
					await Task.Delay(TimeSpan.FromSeconds(5), token);
				}
			}
		}
	}
}
