using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutriGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealListPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MealListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MealListView.ItemsSource = await firebaseHelper.GetAll();
        }


    }
}