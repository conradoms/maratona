using Maratona.Model;
using Maratona.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maratona.ViewModel
{
    public class GroceryListItemViewModel : BaseViewModel
    {
        public GroceryListItem groceryListItem { get; set; }

        public int Id { get; set; } = 0;

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

        public GroceryListItemViewModel()
        {
            SaveChangesCommand = new Command(ExecuteSaveChangesCommand);
        }

        private void ExecuteSaveChangesCommand()
        {
            using (var db = new DBAccess())
            {
                if (Id == 0)
                {
                    groceryListItem = new GroceryListItem()
                    {
                        Name = Nome,
                        Type = Type,
                        Amount = Amount
                    };
                    db.InsertItem(groceryListItem);
                }
            }
        }

        private bool CanExecuteSaveChangesCommand()
        {
            return !string.IsNullOrWhiteSpace(Nome); // && !string.IsNullOrWhiteSpace(Type) && Amount > 0);
        }
    }
}
