using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerRoleService
{
	public class WorkerRole : RoleEntryPoint
	{
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();
		private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);

		public override bool OnStart()
		{
			Trace.TraceInformation("WorkerRoleService OnStart");

			// Ovde ide inicijalizacija ako treba (db konekcija, konfiguracija itd.)

			return base.OnStart();
		}

		public override void Run()
		{
			Trace.TraceInformation("WorkerRoleService Run starting");
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
			Trace.TraceInformation("WorkerRoleService OnStop");
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
					// Ovde ide poslovna logika za radnike
					Trace.TraceInformation("WorkerRoleService radi posao...");

					// Spavaj malo da ne guta CPU
					await Task.Delay(TimeSpan.FromSeconds(10), token);
				}
				catch (OperationCanceledException) when (token.IsCancellationRequested)
				{
					// Normalan izlaz
				}
				catch (Exception ex)
				{
					Trace.TraceError($"WorkerRoleService exception: {ex.Message}");
					await Task.Delay(TimeSpan.FromSeconds(5), token);
				}
			}
		}
	}
}
