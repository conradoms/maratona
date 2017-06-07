using Maratona.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maratona.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryListItemPage : ContentPage
    {
        public GroceryListItemPage()
        {
            InitializeComponent();
            BindingContext = new GroceryListItemViewModel();
        }
    }
}