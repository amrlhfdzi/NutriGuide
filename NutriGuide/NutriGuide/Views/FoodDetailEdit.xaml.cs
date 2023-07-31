using Firebase.Storage;
using NutriGuide.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutriGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetailEdit : ContentPage
    {
        MediaFile file;
        MediaFile file2;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Meal meal;
        public FoodDetailEdit(Meal meal)
        {
            InitializeComponent();
            this.meal = meal;
            TxtCategory.Text = meal.Category;
            TxtName.Text = meal.Name;
            TxtDescription.Text = meal.Description;
            TxtBenefit.Text = meal.Benefit;
            TxtDisadvantage.Text = meal.Disadvantage;
            TxtMealName.Text = meal.MealName;
            TxtIngredient.Text = meal.Ingredient;
            TxtInstruction.Text = meal.Instruction;
            TxtId.Text = meal.Id;
            TxtImage.Source = ImageSource.FromUri(new Uri(meal.Image));
            TxtImage2.Source = ImageSource.FromUri(new Uri(meal.Image2));



        }

        private async void ImageTap_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                TxtImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async void ImageTap_Tapped2(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file2 = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file2 == null)
                {
                    return;
                }
                TxtImage2.Source = ImageSource.FromStream(() =>
                {
                    return file2.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }

        //private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        //{
        //    string name = TxtName.Text;
        //    string description = TxtDescription.Text;



        //    Meal meal = new Meal();
        //    meal.Name = name;
        //    meal.Description = description;






        //    Stream imageStream = null;
        //    if (file != null)
        //    {
        //        imageStream = file.GetStream();
        //    }



        //    await firebaseHelper.UpdateMeal(meal, imageStream);

        //    await DisplayAlert("Information", "Meal has been update.", "Ok");

        //}

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string category = TxtCategory.Text;
            string name = TxtName.Text;
            string description = TxtDescription.Text;
            string mealname = TxtMealName.Text;
            string benefit = TxtBenefit.Text;
            string disadvantage = TxtDisadvantage.Text;
            string ingredient = TxtIngredient.Text;
            string instruction = TxtInstruction.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Warning", "Please enter your name", "Cancel");
                return;
            }
            if (string.IsNullOrEmpty(description))
            {
                await DisplayAlert("Warning", "Please enter your description", "Cancel");
                return;
            }

            Meal meal = new Meal();
            meal.Id = TxtId.Text;
            meal.Category = category;
            meal.Name = name;
            meal.Description = description;
            meal.MealName = mealname;
            meal.Benefit = benefit;
            meal.Disadvantage = disadvantage;
            meal.Ingredient = ingredient;
            meal.Instruction = instruction;

            Stream imageStream2 = null;
            if (file2 != null)
            {
                imageStream2 = file2.GetStream();
                meal.Image = this.meal.Image;
                bool isUpdated = await firebaseHelper.Updates(meal, imageStream2);
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
            else
            {
                meal.Image2 = this.meal.Image2;
                meal.Image = this.meal.Image;
                bool isUpdated = await firebaseHelper.Update(meal);
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

            // At this point, an image was selected, so we can update the meal and its image



        }





    }
}