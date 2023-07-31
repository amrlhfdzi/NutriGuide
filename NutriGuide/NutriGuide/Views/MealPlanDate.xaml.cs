using NutriGuide.ViewModel;
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
    public partial class MealPlanDate : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public MealPlanDate()
        {
            InitializeComponent();
            BindingContext = new ScheduleViewModel();

            // Set up command bindings
           
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            displayRecord.ItemsSource = await firebaseHelper.GetAllSchedule();
        }
    }
}
