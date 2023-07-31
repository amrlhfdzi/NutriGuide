using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

using Firebase.Auth;
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
    public partial class RegistrationPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public string WebAPIkey = "AIzaSyBTl0uu5k1J57tPUBgpxBuCoMYzhv4E5Io";
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void signupbutton_Clicked(object sender, EventArgs e)
        {
            //string name = EntryName.Text;
            string email = EntryUserEmail.Text;
            string password = EntryUserPassword.Text;
            //string phone = EntryUserPhoneNumber.Text;

            //if (string.IsNullOrEmpty(name))
            //{
            //    await DisplayAlert("Warning", "Type name", "Ok");
            //    return;
            //}
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Type email", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Warning", "Type password", "Ok");
                return;
            }
            //if (string.IsNullOrEmpty(phone))
            //{
            //    await DisplayAlert("Warning", "Type phone number", "Ok");
            //    return;
            //}

            bool isSave = await firebaseHelper.Register(email, password);
            if(isSave)
            {
                await DisplayAlert("Register user", "Registration completed", "Ok");
            }
            else
            {
                await DisplayAlert("Register user", "Registration failed", "Ok");
            }

        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());

        }

    }
}