using System;
using Maratona.Helpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Maratona.Model;
using Maratona.Services;
using MvvmHelpers;
using Maratona.Services.Interfaces;

namespace Maratona.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public string UserId { get; private set; }
        public string Token { get; private set; }
        public string UserProfileImage { get; set; }
        public string UserName { get; set; }
        public Command AddGroceryItemCommand { get; }
        public ObservableCollection<GroceryListItem> GroceryListItem { get; }
        
        public MainViewModel()
        {
            UserId = Settings.UserId;
            Token = Settings.AuthToken;
            UserProfileImage = Settings.UserImage;
            UserName = Settings.UserName;
            AddGroceryItemCommand = new Command(ExecuteAddGroceryItem);

            using (var data = new DBAccess())
            {
                GroceryListItem = new ObservableCollection<Model.GroceryListItem>(data.GetItems());
            }

            //GroceryListItem = new ObservableCollection<Model.GroceryListItem>();
            //loadDummyData();
            //loadAzureData();
        }

        //private async void loadAzureData()
        //{
        //    var items = await userApi.GetGroceryListItems();
        //    GroceryListItem.AddRange(items);
        //}

        private void loadDummyData()
        {
            GroceryListItem.Add(
                new Model.GroceryListItem
                {
                    Name = "Arroz",
                    Type = "Kg",
                    Amount = 5
                });

            GroceryListItem.Add(
                new Model.GroceryListItem
                {
                    Name = "Feijão",
                    Type = "Kg",
                    Amount = 2
                });

            GroceryListItem.Add(
                new Model.GroceryListItem
                {
                    Name = "Açucar",
                    Type = "Kg",
                    Amount = 1
                });

            GroceryListItem.Add(
                new Model.GroceryListItem
                {
                    Name = "Papel Higiênico",
                    Type = "Un",
                    Amount = 16
                });
        }

        private async void ExecuteAddGroceryItem()
        {
            await PushModalAsync<GroceryListItemViewModel>();
        }
    }
}
