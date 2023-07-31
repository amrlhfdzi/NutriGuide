using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NutriGuide.ViewModel;
using NutriGuide.Views;
using NutriGuide.Models;

namespace NutriGuide
{
    
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();

            // Set the BindingContext to this page
            BindingContext = new MainPageViewModel();
        }

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedBodyPart = e.CurrentSelection.FirstOrDefault() as MainPageModel;
            if (selectedBodyPart != null)
            {
                var meals = new List<Meal>();
                switch (selectedBodyPart.Id)
                {
                    case 1:
                        meals = await firebaseHelper.GetMealsByCategory("Eyes");
                        var eyesPageDetailViewModel = new EyesPageDetailViewModel { FoodList = meals };
                        var eyesPageDetail = new EyesPageDetail { BindingContext = eyesPageDetailViewModel };
                        await Navigation.PushAsync(eyesPageDetail);
                        break;
                    case 2:
                        meals = await firebaseHelper.GetMealsByCategory("Brain");
                        var brainPageDetailViewModel = new BrainPageDetailViewModel { FoodList = meals };
                        var brainPageDetail = new BrainPageDetail { BindingContext = brainPageDetailViewModel };
                        await Navigation.PushAsync(brainPageDetail);
                        break;
                    case 3:
                        meals = await firebaseHelper.GetMealsByCategory("Skin");
                        var skinPageDetailViewModel = new SkinPageDetailViewModel { FoodList = meals };
                        var skinPageDetail = new SkinPageDetail { BindingContext = skinPageDetailViewModel };
                        await Navigation.PushAsync(skinPageDetail);
                        break;
                    case 4:
                        meals = await firebaseHelper.GetMealsByCategory("Bones");
                        var bonesPageDetailViewModel = new BonesPageDetailViewModel { FoodList = meals };
                        var bonesPageDetail = new BonesPageDetail { BindingContext = bonesPageDetailViewModel };
                        await Navigation.PushAsync(bonesPageDetail);
                        break;
                }
            }
        }



    }
}