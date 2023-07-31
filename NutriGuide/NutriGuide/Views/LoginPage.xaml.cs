using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutriGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string WebAPIkey = "AIzaSyBTl0uu5k1J57tPUBgpxBuCoMYzhv4E5Io";
        public LoginPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());

        }

        async void loginbutton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (UserEmail.Text == "admin@gmail.com" && UserPassword.Text == "admin123")
            {
                await Navigation.PushAsync(new AdminTabbed());
            }
            else
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                try
                {
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserEmail.Text, UserPassword.Text);
                    var content = await auth.GetFreshAuthAsync();
                    var serializedcontnet = JsonConvert.SerializeObject(content);
                    Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                    await Navigation.PushAsync(new Tabbed());
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
                }
            }
        }

    }
}