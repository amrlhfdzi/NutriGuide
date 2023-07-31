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
    public partial class BrainPageDetail : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ObservableCollection<string> Items { get; set; }

        public BrainPageDetail()
        {
            InitializeComponent();

            BindingContext = new BrainPageDetailViewModel();
        }

       

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var selectedItem = (Meal)e.SelectedItem;

            await Navigation.PushAsync(new FoodAndMealDetails(selectedItem));

            brainPageDetail.SelectedItem = null;
        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var FoodList = await firebaseHelper.GetAllByName(searchValue, "Brain");
                brainPageDetail.ItemsSource = null;
                brainPageDetail.ItemsSource = FoodList;
            }
            else
            {
                OnAppearing();
            }
        }
    }
}
