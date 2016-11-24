using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using MSAApp.DataModels;
using System.Threading.Tasks;

namespace MSAApp
{
    class DatabaseHandler
    {
        private static DatabaseHandler instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Users> UsersTable;

        private DatabaseHandler()
        {
            this.client = new MobileServiceClient("http://FabrikamFoodApp.azurewebsites.net");
            this.UsersTable = this.client.GetTable<Users>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static DatabaseHandler DatabaseHandlerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseHandler();
                }

                return instance;
            }
        }

        public async Task AddUser(Users user)
        {
            await this.UsersTable.InsertAsync(user);           
        }

        public async Task<List<Users>> GetUsers()
        {
            return await this.UsersTable.ToListAsync();
        }
        public async Task UpdateInfo(Users user)
        {
            await this.UsersTable.UpdateAsync(user);
        }
    }
}
