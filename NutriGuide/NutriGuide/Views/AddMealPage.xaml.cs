using NutriGuide.Models;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutriGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMealPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AddMealPage()
        {
            InitializeComponent();
        }

        void onDatePickerSelected(object sender, DateChangedEventArgs e)
        {
            var selectedDate = e.NewDate.ToString();
        }


        async void OnSaveRecord(object sender, EventArgs e)
        {
            var selectdate = selectDate.Date.ToString("dd/MM/yyyy");
            string title = Title.Text;
            string description = Description.Text;
            string hours = Hours.Text;
            string nutrient = Nutrient.Text;


            string lunchtitle = LunchTitle.Text;
            string lunchdescription = LunchDescription.Text;
            string lunchhours = LunchHours.Text;
            string lunchnutrient = LunchNutrient.Text;



            string dinnertitle = DinnerTitle.Text;
            string dinnerdescription = DinnerDescription.Text;
            string dinnerhours = DinnerHours.Text;
            string dinnernutrient = DinnerNutrient.Text;

            Schedule schedule = new Schedule();
            schedule.DateRecorded = selectdate;
            schedule.Title = title;
            schedule.Description = description;
            schedule.Hours = hours;
            schedule.Nutrient = nutrient;
            schedule.LunchTitle = lunchtitle;
            schedule.LunchDescription = lunchdescription;
            schedule.LunchHours = lunchhours;
            schedule.LunchNutrient = lunchnutrient;
            schedule.DinnerTitle = dinnertitle;
            schedule.DinnerDescription = dinnerdescription;
            schedule.DinnerHours = dinnerhours;
            schedule.DinnerNutrient = dinnernutrient;


            await firebaseHelper.AddRecord(schedule);
            await Navigation.PopModalAsync();

            await DisplayAlert("Information", "Meal plan has been saved.", "Ok");
                
        


        }

    }
}
