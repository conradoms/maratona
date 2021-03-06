﻿using Maratona.Model;
using Maratona.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maratona.ViewModels
{
    public class GroceryListItemAddViewModel : BaseViewModel
    {
        public GroceryListItem GroceryListItem { get; set; }
        
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

        public GroceryListItemAddViewModel()
        {
            SaveChangesCommand = new Command(ExecuteSaveChangesCommand);
            CancelCommand = new Command(ExecuteCancelCommand);
        }

        private void ExecuteSaveChangesCommand()
        {
            using (var db = new DBAccess())
            {
                GroceryListItem = new GroceryListItem()
                {
                    Name = Nome,
                    Type = Type,
                    Amount = Amount
                };

                db.InsertItem(GroceryListItem);
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
