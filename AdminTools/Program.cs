using System;
using System.Linq;
using System.Diagnostics;
using System.Configuration;
using MovieDiscussionService_Data.Repositories;

namespace AdminToolsConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			string connectionString = "UseDevelopmentStorage=true";

			while (true)
			{
				Console.Clear();
				Console.WriteLine("🛠️  Admin Tools Console");
				Console.WriteLine("1. Manage Alert Emails");
				Console.WriteLine("2. Verify User");
				Console.WriteLine("3. Exit");

				var choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						ManageAlertEmails(connectionString);
						break;
					case "2":
						VerifyUser(connectionString);
						break;
					case "3":
						return;
				}
			}
		}

		static void ManageAlertEmails(string connectionString)
		{
			var repo = new AlertEmailRepository(connectionString);
			var emails = repo.GetAllAlertEmails();

			Console.WriteLine("📧 Alert Emails:");
			foreach (var email in emails)
			{
				Console.WriteLine($"- {email}");
			}

			Console.WriteLine("\n1. Add Email\n2. Remove Email\n3. Back");
			var choice = Console.ReadLine();

			if (choice == "1")
			{
				Console.Write("Enter email to add: ");
				var newEmail = Console.ReadLine();
				repo.AddAlertEmailAsync(newEmail).Wait();
				Console.WriteLine("✅ Email added!");
			}
			else if (choice == "2")
			{
				Console.Write("Enter email to remove: ");
				var emailToRemove = Console.ReadLine();
				repo.RemoveAlertEmailAsync(emailToRemove).Wait();
				Console.WriteLine("✅ Email removed!");
			}

			Console.ReadKey();
		}

		static void VerifyUser(string connectionString)
		{
			System.Configuration.ConfigurationManager.
				AppSettings["DataConnectionString"] = "UseDevelopmentStorage=true";

			Console.WriteLine("User Verification");
			Console.WriteLine("====================");

			// Učitaj sve korisnike
			var userRepo = new UserDataRepository();
			var allUsers = userRepo.RetrieveAllUsers();

			// Prikaz neverifikovanih korisnika
			var unverifiedUsers = allUsers.Where(u => !u.IsVerified).ToList();

			if (unverifiedUsers.Count == 0)
			{
				Console.WriteLine("No unverified users found.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Unverified users:");
			for (int i = 0; i < unverifiedUsers.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {unverifiedUsers[i].RowKey} - {unverifiedUsers[i].Name} {unverifiedUsers[i].Lastname}");
			}

			Console.Write("\nSelect user to verify (number) or '0' to cancel: ");
			if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= unverifiedUsers.Count)
			{
				var selectedUser = unverifiedUsers[choice - 1];

				Console.Write($"Verify user {selectedUser.RowKey}? (y/n): ");
				if (Console.ReadLine().ToLower() == "y")
				{
					selectedUser.IsVerified = true;
					userRepo.UpdateUser(selectedUser);
					Console.WriteLine("User verified successfully!");
				}
			}
			else
			{
				Console.WriteLine("Cancelled or invalid selection.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}
}