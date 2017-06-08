using Maratona.Services.Interfaces;
using SQLite.Net;
using System;
using System.IO;
using Xamarin.Forms;
using Maratona.Model;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Maratona.Services
{
    public class DBAccess : IDisposable
    {
        private SQLiteConnection SQLiteConnection;

        public DBAccess()
        {
            try
            {
                var config = DependencyService.Get<IConfig>();
                SQLiteConnection = new SQLiteConnection(config.Platform, Path.Combine(config.SQLiteDirectory, "Cadastro.db3"));
                SQLiteConnection.CreateTable<GroceryListItem>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void InsertItem(GroceryListItem groceryListItem)
        {
            SQLiteConnection.Insert(groceryListItem);
        }

        public void UpdateItem(GroceryListItem GroceryListItem)
        {
            SQLiteConnection.Update(GroceryListItem);
        }

        public void DeleteItem(GroceryListItem GroceryListItem)
        {
            SQLiteConnection.Delete(GroceryListItem);
        }

        public List<GroceryListItem> GetItems()
        {
            return SQLiteConnection.Table<GroceryListItem>().OrderBy(c => c.Name).ToList();
        }

        public void Dispose()
        {
            SQLiteConnection.Dispose();
        }
    }
}
