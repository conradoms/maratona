using Maratona.Model;
using Maratona.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maratona.ViewModels
{
    public class GroceryListItemEditViewModel : BaseViewModel
    {
        public GroceryListItem GroceryListItem { get; set; }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set
            {
                SetProperty(ref _nome, value);
                //SaveChangesCommand.ChangeCanExecute();
            }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
                //SaveChangesCommand.ChangeCanExecute();
            }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set
            {
                SetProperty(ref _amount, value);
                //SaveChangesCommand.ChangeCanExecute();
            }
        }

        public Command SaveChangesCommand { get; }

        public Command CancelCommand { get; }

        public GroceryListItemEditViewModel(GroceryListItem groceryListItem)
        {
            SaveChangesCommand = new Command(ExecuteSaveChangesCommand);
            CancelCommand = new Command(ExecuteCancelCommand);
            GroceryListItem = groceryListItem;
            LoadData();
        }

        private void LoadData()
        {
            Id = GroceryListItem.Id;
            Nome = GroceryListItem.Name;
            Type = GroceryListItem.Type;
            Amount = GroceryListItem.Amount;
        }

        private void ExecuteSaveChangesCommand()
        {
            using (var db = new DBAccess())
            {
                GroceryListItem.Name = Nome;
                GroceryListItem.Type = Type;
                GroceryListItem.Amount = Amount;

                db.UpdateItem(GroceryListItem);
            }
        }

        private void ExecuteCancelCommand()
        {
            PopModalAsync();
        }

        private bool CanExecuteSaveChangesCommand()
        {
            return !string.IsNullOrWhiteSpace(Nome); // && !string.IsNullOrWhiteSpace(Type) && Amount > 0);
        }
    }
}
