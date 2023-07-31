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
    public partial class addMeal : ContentPage
    {
        MediaFile file;
        MediaFile file2;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public addMeal()
        {
            InitializeComponent();
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
                resultImage.Source = ImageSource.FromStream(() =>
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
                resultImage2.Source = ImageSource.FromStream(() =>
                {
                    return file2.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }


        async void OnAddMealClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string description = DescriptionEntry.Text;
            string category = CategoryPicker.SelectedItem?.ToString();
            string benefit = BenefitEntry.Text;
            string disadvantage = DisadvantageEntry.Text;
            string mealname = MealNameEntry.Text;
            string ingredients = IngredientsEntry.Text;
            string instruction = InstructionEntry.Text;


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(category))
            {
                await DisplayAlert("Error", "Please fill all the fields", "OK");
                return;
            }


            Meal meal = new Meal();
            meal.Name = name;
            meal.Description = description;
            meal.Category = category;
            meal.Benefit = benefit;
            meal.Disadvantage = disadvantage;
            meal.MealName = mealname;
            meal.Ingredient = ingredients;
            meal.Instruction = instruction;



            //if (file != null)
            //{
            //    string imageUrl = await firebaseHelper.UploadImage(file.GetStream(), $"{Guid.NewGuid()}.jpg");
            //    meal.Image = imageUrl;
            //}

            Stream imageStream = null;
            if (file != null)
            {
                imageStream = file.GetStream();
            }

            Stream imageStream2 = null;
            if (file2 != null)
            {
                imageStream2 = file2.GetStream();
            }

            //var imageStreams = new List<Stream>();
            //imageStreams.Add(imageStream); // Add the first image stream
            //imageStreams.Add(imageStream2); // Add the second image stream
            //await firebaseHelper.AddMeal(meal, imageStreams); // Call the AddMeal method




            await firebaseHelper.AddMeal(meal, imageStream, imageStream2);
            

            await DisplayAlert("Information", "Meal has been saved.", "Ok");




        }




    }
}