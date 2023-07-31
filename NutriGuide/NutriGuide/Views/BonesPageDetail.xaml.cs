using NutriGuide.Models;
using NutriGuide.ViewModel;
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
    public partial class BonesPageDetail : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ObservableCollection<string> Items { get; set; }

        public BonesPageDetail()
        {
            InitializeComponent();

            BindingContext = new BonesPageDetailViewModel();
        }

       

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var selectedItem = (Meal)e.SelectedItem;

            await Navigation.PushAsync(new FoodAndMealDetails(selectedItem));

            bonesPageDetail.SelectedItem = null;
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((MenuItem)sender).CommandParameter.ToString();
            var meal = await firebaseHelper.GetByIdMeal(id);
            if (meal == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            meal.Id = id;
            await Navigation.PushModalAsync(new FoodDetailEdit(meal));





        }

        //private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        //{
        //    string id = ((MenuItem)sender).CommandParameter.ToString();
        //    var meal = await firebaseHelper.GetById(id);
        //    if (meal == null)
        //    {
        //        await DisplayAlert("Warning", "Data not found.", "Ok");
        //    }
        //    meal.Id = id;
        //    await Navigation.PushModalAsync(new FoodDetailEdit(meal));
        //}

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var FoodList = await firebaseHelper.GetAllByName(searchValue, "Bones");
                bonesPageDetail.ItemsSource = null;
                bonesPageDetail.ItemsSource = FoodList;
            }
            else
            {
                OnAppearing();
            }
        }




    }
}
