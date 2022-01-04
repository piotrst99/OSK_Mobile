using Android.Widget;
using Newtonsoft.Json;
using OSK_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OSK_Mobile.Pages.Student
{
    //[Activity(NoHistory = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentMainPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private string _userName;
        private int _userID;

        public StudentMainPage(string userLogin) {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            _userName = userLogin;
            GetUserName();
        }

        private async void GetUserName() {
            string UrlGetUserName = "http://10.0.2.2:32141/Student/GetStudentName";

            try {

                //var tt = await _clientOSK.GetStringAsync(UrlGetUserName+"/"+_userName);
                //DisplayAlert("Result", UrlGetUserName + "/" + _userName, "OK");
                //var responseData = JsonConvert.DeserializeObject<UserData>(tt);
                //_test = new ObservableCollection<Test>(deserializedCount);

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

        /*protected override bool OnBackButtonPressed() {
            Toast.MakeText(Android.App.Application.Context, base.OnBackButtonPressed().ToString(), ToastLength.Short).Show();
            return base.OnBackButtonPressed();
        }*/

        /*private void ShowUserData_Clicked(object sender, EventArgs e) {
            Toast.MakeText(Android.App.Application.Context, "Dane kursanta", ToastLength.Short).Show();
        }*/

        private async void ShowUserData(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Dane kursanta", ToastLength.Short).Show();
            await Navigation.PushAsync(new UserDataPage(_userID), true);
        }

        private async void ShowUserCourses(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Kursy kursanta", ToastLength.Short).Show();
            await Navigation.PushAsync(new StudentCoursesPage(_userID), true);
        }

        private async void ShowDrives(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Jazdy", ToastLength.Short).Show();
            await Navigation.PushAsync(new DriveActivitiesPage(_userID), true);
        }

        private async void ShowPayments(object sender, EventArgs e) {
            await Navigation.PushAsync(new PaymentsPage(_userID), true);
        }

        private void ShowSettings(object sender, EventArgs e) {
            Toast.MakeText(Android.App.Application.Context, "Ustawienia", ToastLength.Short).Show();
        }
        
        private async void LogOut(object sender, EventArgs e) {
            //Toast.MakeText(Android.App.Application.Context, "Wylogowanie się", ToastLength.Short).Show();
            //await Navigation.PushAsync(new MainPage(), true);
            bool result = await DisplayAlert("WYLOGOWANIE", "Czy wylogować się z konta?", "TAK", "NIE");
            if (result) {
                try {
                    //Application.Current.MainPage = new MainPage();
                    //App.Current.MainPage = new MainPage();
                    await Navigation.PushAsync(new MainPage());
                    this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                }
                catch (Exception) {

                }
                 
            }
        }
        
        

        /*protected override bool OnBackButtonPressed() {
            //return base.OnBackButtonPressed();
            Device.BeginInvokeOnMainThread(async () => {
                //if (await DisplayAlert("", "Are you sure you want to exit from this page?", "Yes", "No")){
                if (base.OnBackButtonPressed()){
                    
                    //Thread.CurrentThread.Abort();
                    await Navigation.PopAsync();
                }
            });
            return true;
        }*/



    }
}