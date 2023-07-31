using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutriGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminEditPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ObservableCollection<string> Items { get; set; }

        public AdminEditPage()
        {
            InitializeComponent();

            //Items = new ObservableCollection<string>
            //{
            //    "Item 1",
            //    "Item 2",
            //    "Item 3",
            //    "Item 4",
            //    "Item 5"
            //};

            //MyListView.ItemsSource = Items;
        }

        protected override async void OnAppearing()
        {
            var meal = await firebaseHelper.GetAll();
            
            MyListView.ItemsSource = meal;
            //MyListView.IsRefreshing = false;


        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var meal = await firebaseHelper.GetByIdMeal(id);
            if (meal == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            meal.Id = id;
            await Navigation.PushModalAsync(new FoodDetailEdit(meal));




        }

        //private async void DeleteTapp_Tapped(object sender, EventArgs e)
        //{

        //    var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
        //    if (response)
        //    {
        //        string id = ((TappedEventArgs)e).Parameter.ToString();
        //        bool isDelete = await firebaseHelper.DeleteMeal(id);
        //        if (isDelete)
        //        {
        //            await DisplayAlert("Information", "Meal has been deleted.", "Ok");

        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Meal deleted failed.", "Ok");
        //        }
        //    }
        //}

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this meal?", "Yes", "No");
                if (answer)
                {
                    string id = ((TappedEventArgs)e).Parameter.ToString();
                    var meal = await firebaseHelper.GetByIdMeal(id);

                    if (meal != null)
                    {
                        await firebaseHelper.DeleteMeal(id);
                        //await Navigation.PopAsync();
                        await DisplayAlert("Success", "Meal has been deleted", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

    }
}
