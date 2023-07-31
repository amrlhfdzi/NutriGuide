using NutriGuide.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutriGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealEdit : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Schedule schedule;

        public MealEdit(Schedule schedule)
        {
            InitializeComponent();

            this.schedule = schedule;
            selectDate.Date = DateTime.ParseExact(schedule.DateRecorded, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            Title.Text = schedule.Title;
            Description.Text = schedule.Description;
            Hours.Text = schedule.Hours;
            Nutrient.Text = schedule.Nutrient;
            LunchTitle.Text = schedule.LunchTitle;
            LunchDescription.Text = schedule.LunchDescription;
            LunchHours.Text = schedule.LunchHours;
            LunchNutrient.Text = schedule.LunchNutrient;
            DinnerTitle.Text = schedule.DinnerTitle;
            DinnerDescription.Text = schedule.DinnerDescription;
            DinnerHours.Text = schedule.DinnerHours;
            DinnerNutrient.Text = schedule.DinnerNutrient;
            TxtId.Text = schedule.Id;

            //var scheduleData = firebaseHelper.GetById(schedule.Id).Result;
            //var dateValue = scheduleData.DateRecorded.ToString();
            //// Replace "date" with your actual date field name in Firebase

            //// Assign the date value to the DatePicker control
            //selectDate.Date = DateTime.ParseExact(dateValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        //public MealEdit(string id)
        //{
        //    this.id = id;
        //}

        private void onDatePickerSelected(object sender, DateChangedEventArgs e)
        {
            // Update the date description label with the selected date
            var selectedDate = e.NewDate.ToString("dddd, d MMMM");
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
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
            schedule.Id = TxtId.Text;
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

            bool isUpdated = await firebaseHelper.UpdateMeal(schedule);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Update failed.", "Cancel");
            }
            return;


        }
    }
}