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

namespace HealthMonitoringService
{
	public class HealthMonitoringWorker : RoleEntryPoint
	{
		private bool _running = true;

		public override bool OnStart()
		{
			// Mo�e� dodati inicijalizaciju konekcija ili logova
			Console.WriteLine("HealthMonitoringService Worker started.");
			return base.OnStart();
		}

		public override void Run()
		{
			while (_running)
			{
				try
				{
					// Placeholder logika
					Console.WriteLine("HealthMonitoringService radi proveru...");
					Console.WriteLine($"{DateTime.Now}: Worker radi posao...");
	
				}
				catch (Exception ex)
				{
					Console.WriteLine("Gre�ka u HealthMonitoringService: " + ex.Message);
				}
				Thread.Sleep(3000); // svake 3 sekunde
			}
		}

		public override void OnStop()
		{
			_running = false;
			base.OnStop();
		}
	}
}
