using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Maratona.Model;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Collections;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.IO;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using Maratona.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserApi))]
namespace Maratona.Services
{
    public class UserApi
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<GroceryListItem> groceryListItemsTable;

        public async Task Initialize()
        {
            MobileService = new MobileServiceClient("http://maratonasl.azurewebsites.net");

            var path = "syncgrocerylist.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<GroceryListItem>();

            await MobileService.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            groceryListItemsTable =  MobileService.GetSyncTable<GroceryListItem>();
        }

        public async Task<IEnumerable<GroceryListItem>> GetGroceryListItems()
        {
            //Initialize & Sync
            await Initialize();
            await SyncGroceryListItem();

            return await groceryListItemsTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task<GroceryListItem> AddGroceryListItem(GroceryListItem groceryListItem)
        {
            await Initialize();
            var gli = new GroceryListItem()
            {
                DateUtc = DateTime.UtcNow,
                Name = groceryListItem.Name,
                Type = groceryListItem.Type,
                Amount = groceryListItem.Amount
            };

            if (groceryListItem.Id == null)
            {
                await groceryListItemsTable.InsertAsync(gli);
            }
            else
            {
                await groceryListItemsTable.UpdateAsync(gli);
            }

            await SyncGroceryListItem();
            //return coffee
            return gli;
        }

        public async Task SyncGroceryListItem()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await groceryListItemsTable.PullAsync("allGroceryListItems", groceryListItemsTable.CreateQuery());

                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Deu Ruim!", "Não foi possível atualizar sua lista de compras mas não se preocupe, você ainda pode editar sua lista offline" + ex, "OK... Fazer o que?");
                Debug.WriteLine("Não foi possível atualizar sua lista de compras mas não se preocupe, você ainda pode editar sua lista offline" + ex);
            }
        }
    }
}
