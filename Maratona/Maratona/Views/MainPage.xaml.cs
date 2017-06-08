using Maratona.ViewModels;
using Xamarin.Forms;

namespace Maratona.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            //CreateLayout();
        }



        public void CreateLayout()
        {
            Content = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "StackLayout",
                        HorizontalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        Text = "stacks its children",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Label
                    {
                        Text = "vertically",
                        HorizontalOptions = LayoutOptions.End
                    },
                    new Label
                    {
                        Text = "by default,",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Label
                    {
                        Text = "but horizontal placement",
                        HorizontalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        Text = "can be controlled with",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Label
                    {
                        Text = "the HorizontalOptions property.",
                        HorizontalOptions = LayoutOptions.End
                    },
                    new Label
                    {
                        Text = "An Expand option allows one or more children " +
                               "to occupy the an area within the remaining " +
                               "space of the StackLayout after it's been sized " +
                               "to the height of its parent.",
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.End
                    },
                    new StackLayout
                    {
                        Spacing = 0,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Text = "Stacking",
                            },
                            new Label
                            {
                                Text = "can also be",
                                HorizontalOptions = LayoutOptions.CenterAndExpand
                            },
                            new Label
                            {
                                Text = "horizontal.",
                            },
                        }
                    }
                }
            };            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ViewModel.EditGroceryItemCommand.Execute(e.SelectedItem);
            }
        }
    }
}
