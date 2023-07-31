using NutriGuide.Models;
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
    public partial class MealPlan : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MealPlan()
        {
            InitializeComponent();
            BindingContext = new ScheduleViewModel();

            //datePicker.DateSelected += OnDateSelected;
        }

        private void onDatePickerSelected(object sender, DateChangedEventArgs e)
        {
            // Update the date description label with the selected date
            var selectedDate = e.NewDate.ToString("dddd, d MMMM");
        }

        async void MealClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMealPage());

        }

        protected async override void OnAppearing()
        {
            var schedules = await firebaseHelper.GetAllSchedule();
            displayMeal.ItemsSource = schedules;
        }



        //private async void EditTap_Tapped(object sender, EventArgs e)
        //{
        //    //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

        //    string id = ((TappedEventArgs)e).Parameter.ToString();
        //    var schedule = await firebaseHelper.GetById(id);
        //    if (schedule == null)
        //    {
        //        await DisplayAlert("Warning", "Data not found.", "Ok");
        //    }
        //    schedule.Id = id;
        //    await Navigation.PushAsync(new MealEdit(schedule));


        //}

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddMealPage());
        }


        //private async void EditTap_Tapped(object sender, EventArgs e)
        //{
        //    var menuItem = sender as Button;
        //    var mealId = menuItem?.CommandParameter.ToString();
        //    if (!string.IsNullOrEmpty(mealId))
        //    {
        //        await Navigation.PushAsync(new MealEdit(mealId));
        //    }
        //}

        //private async void EditTap_Tapped(object sender, EventArgs e)
        //{
        //    var menuItem = sender as MenuItem;
        //    var schedule = menuItem.BindingContext as Schedule;
        //    await Navigation.PushAsync(new MealEdit(schedule));
        //}

        //private async void EditTap_Tapped(object sender, EventArgs e)
        //{
        //    //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

        //    string id = ((TappedEventArgs)e).Parameter.ToString();
        //    var schedule = await firebaseHelper.GetById(id);
        //    if (schedule == null)
        //    {
        //        await DisplayAlert("Warning", "Data not found.", "Ok");
        //    }
        //    schedule.Id = id;
        //    await Navigation.PushModalAsync(new MealEdit(schedule));




        //    }

        //private async void EditTap_Tapped(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to edit this meal?", "Yes", "No");
        //        if (answer)
        //        {
        //            var button = (Button)sender;
        //            string id = (string)button.CommandParameter;

        //            var schedule = await firebaseHelper.GetById(id);

        //            if (schedule == null)
        //            {
        //                await Navigation.PushModalAsync(new MealEdit(schedule));
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", ex.Message, "Ok");
        //    }
        //}

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var schedule = await firebaseHelper.GetById(id);
            if (schedule == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            schedule.Id = id;
            await Navigation.PushModalAsync(new MealEdit(schedule));

        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this meal?", "Yes", "No");
                if (answer)
                {
                    string id = ((TappedEventArgs)e).Parameter.ToString();
                    var schedule = await firebaseHelper.GetById(id);

                    if (schedule != null)
                    {
                        await firebaseHelper.DeleteMealPlan(id);
                        //await Navigation.PopAsync();
                        await DisplayAlert("Success", "Meal plan has been deleted", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        //private async void DeleteTapp_Tapped(object sender, EventArgs e)
        //{

        //    var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
        //    if (response)
        //    {
        //        string id = ((TappedEventArgs)e).Parameter.ToString();
        //        bool isDelete = await firebaseHelper.DeleteMealPlan(id);
        //        if (isDelete)
        //        {
        //            await DisplayAlert("Information", "Meal plan has been deleted.", "Ok");
        //            OnAppearing();
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Meal plan deleted failed.", "Ok");
        //        }
        //    }
        //}



                //private async void EditTap_Tapped(object sender, EventArgs e)
                //{
                //    try
                //    {
                //        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to edit this meal?", "Yes", "No");
                //        if (answer)
                //        {
                //            var menuItem = sender as MenuItem;
                //            var scheduleItem = menuItem.CommandParameter as Schedule;
                //            string id = scheduleItem.Id;

                //            var schedule = await firebaseHelper.GetById(id);
                //            if (schedule != null)
                //            {
                //                await Navigation.PushModalAsync(new MealEdit(schedule));
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        await DisplayAlert("Error", ex.Message, "Ok");
                //    }
                //}

        //        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this meal?", "Yes", "No");
        //        if (answer)
        //        {
        //            var menuItem = sender as MenuItem;
        //            var scheduleItem = menuItem.CommandParameter as Schedule;
        //            string id = scheduleItem.Id;

        //            var schedule = await firebaseHelper.GetById(id);
        //            if (schedule != null)
        //            {
        //                await firebaseHelper.DeleteMeal(id);
        //                await Navigation.PopAsync();
        //                await DisplayAlert("Success", "Meal has been deleted", "Ok");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", ex.Message, "Ok");
        //    }
        //}












    }
}