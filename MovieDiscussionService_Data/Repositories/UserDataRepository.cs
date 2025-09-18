using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MovieDiscussionService_Data.Interfaces;
using System;
using System.Linq;

namespace MovieDiscussionService_Data.Repositories
{
	public class UserDataRepository : IUserRepository
	{
		private CloudStorageAccount _storageAccount;
		private CloudTable _table;

		public UserDataRepository()
		{
			// Učitavanje connection stringa iz konfiguracije (app.config ili cloud settings)
			_storageAccount = CloudStorageAccount.Parse(
				CloudConfigurationManager.GetSetting("DataConnectionString"));

			var tableClient = new CloudTableClient(
				new Uri(_storageAccount.TableEndpoint.AbsoluteUri),
				_storageAccount.Credentials);

			_table = tableClient.GetTableReference("UserTable");
			_table.CreateIfNotExists();
		}

		// Vraća IQueryable svih korisnika u particiji "User"
		public IQueryable<User> RetrieveAllUsers()
		{
			var query = from u in _table.CreateQuery<User>()
						where u.PartitionKey == "User"
						select u;

			return query;
		}

		// Dodaje novog korisnika u tabelu
		public void AddUser(User newUser)
		{
			TableOperation insertOperation = TableOperation.Insert(newUser);
			_table.Execute(insertOperation);
		}

		// Možeš dodati i metodu za dohvat korisnika po emailu (RowKey)
		public User GetUserByEmail(string email)
		{
			TableOperation retrieveOperation = TableOperation.Retrieve<User>("User", email);
			var result = _table.Execute(retrieveOperation);
			return result.Result as User;
		}

		// Update korisnika(takodje placeholder)
		public void UpdateUser(User user)
		{
			TableOperation updateOperation = TableOperation.Replace(user);
			_table.Execute(updateOperation);
		}

		// Brisanje korisnika (za sada neka bude ovakav placeholder)
		public void DeleteUser(string email)
		{
			TableOperation retrieveOperation = TableOperation.Retrieve<User>("User", email);
			var result = _table.Execute(retrieveOperation);
			var user = result.Result as User;
			if (user != null)
			{
				TableOperation deleteOperation = TableOperation.Delete(user);
				_table.Execute(deleteOperation);
			}
		}
	}
}
