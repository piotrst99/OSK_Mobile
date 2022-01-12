using Android.Widget;
using Newtonsoft.Json;
using OSK_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OSK_Mobile.Pages.Employee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeMainPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private string _userName;
        private int _userID;

        public EmployeeMainPage(string userLogin) {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            _userName = userLogin;
            GetUserName();

        }

        private async void GetUserName() {
            string UrlGetUserName = "http://10.0.2.2:32141/Employee/GetEmployeeName";

            try {

                LoginData loginData = new LoginData() {
                    login = _userName,
                };

                string JsonData = JsonConvert.SerializeObject(loginData);
                var resourse = await _clientOSK.PostAsync(UrlGetUserName, new StringContent(JsonData, Encoding.UTF8, "application/json"));
                var resultData = await resourse.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<UserName>(resultData);

                _userID = responseData.userID;
                userName.Text = responseData.FirstName + " " + responseData.Surname;

            }
            catch (Exception er) {
                DisplayAlert("Result", er.ToString(), "OK");
            }


        }

        private async void ShowUserData(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Dane kursanta", ToastLength.Short).Show();
            await Navigation.PushAsync(new EmployeeDataPage(_userID), true);
        }

        private async void ShowEmployeeActivite(object sender, EventArgs e) {
            await Navigation.PushAsync(new EmployeeActivitePage(), true);
        }

        private async void ShowDrives(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Jazdy", ToastLength.Short).Show();
            await Navigation.PushAsync(new DriveActivitesPage(_userID), true);

        }

        private async void ShowSettings(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Ustawienia", ToastLength.Short).Show();
            await Navigation.PushAsync(new Settings.SettingsPage(_userID), true);
        }

        private async void LogOut(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Wylogowanie się", ToastLength.Short).Show();
            //await Navigation.PushAsync(new MainPage(), true);
            bool result = await DisplayAlert("WYLOGOWANIE", "Czy wylogować się z konta?", "TAK", "NIE");
            if (result) {
                try {
                    //Application.Current.MainPage = new MainPage();
                    await Navigation.PushAsync(new MainPage());
                    this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                }
                catch (Exception) {

                }

            }
        }





        /*protected override bool OnBackButtonPressed() {
            Toast.MakeText(Android.App.Application.Context, base.OnBackButtonPressed().ToString(), ToastLength.Short).Show();
            return base.OnBackButtonPressed();
        }*/

        

    }
}