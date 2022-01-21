using Android.Content;
using Android.Widget;
using Newtonsoft.Json;
using OSK_Mobile.Entities;
using OSK_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OSK_Mobile.Pages;
using System.Security.Cryptography;
using System.IO;

namespace OSK_Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userData.txt");

        public MainPage() {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            if (!File.Exists(_fileName)) {
                File.WriteAllText(_fileName, "");
            }
            else {
                //string text = File.ReadAllText(_fileName);
                if (File.ReadAllText(_fileName) != "") {
                    string userLogin = File.ReadLines(_fileName).First();
                    string role = File.ReadLines(_fileName).ElementAtOrDefault(1);
                    AutoLogin(userLogin, role);
                }
            }

        }
         
        private async void LoginToAccount(object sender, EventArgs e) {
            string Url_CheckLoginData = "http://10.0.2.2:32141/Home/CheckLoginData";

            string loginStr, passwordStr;
            int value = 0;

            if (StudentRadio.IsChecked) {
                value += Int32.Parse(StudentRadio.Value.ToString());
            }
            if (EmployeeRadio.IsChecked) {
                value += Int32.Parse(EmployeeRadio.Value.ToString());
            }

            loginStr = LoginTxt.Text;
            passwordStr = PasswordTxt.Text;

            try {

                var sha256 = new SHA256Managed();
                byte[] password = Encoding.ASCII.GetBytes(passwordStr);
                byte[] hashValue = sha256.ComputeHash(password);

                string pwdStr = "";
                foreach (byte b in hashValue)
                    pwdStr += b.ToString("x2");

                LoginData loginData = new LoginData() {
                    login = loginStr,
                    password = pwdStr,
                    val = value
                };

                string JsonData = JsonConvert.SerializeObject(loginData);
                var resourse = await _clientOSK.PostAsync(Url_CheckLoginData, new StringContent(JsonData, Encoding.UTF8, "application/json"));
                var resultData = await resourse.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<LoginResult>(resultData);

                if(!responseData.result){
                    Toast.MakeText(Android.App.Application.Context, "Nieprawidłowe dane logowania", ToastLength.Short).Show();
                }
                else {
                    if(value == 1) {
                        Application.Current.Properties["isLogin"] = 1;

                        var studentMainPage = new Pages.Student.StudentMainPage(loginStr);
                        NavigationPage.SetHasNavigationBar(studentMainPage, false);
                        NavigationPage mypage = new NavigationPage(studentMainPage);

                        File.WriteAllText(_fileName, loginStr+"\n"+"student");
                        await Navigation.PushAsync(new Pages.Student.StudentMainPage(loginStr), true);
                        
                        LoginTxt.Text = "";
                        PasswordTxt.Text = "";
                    }
                    else {
                        LoginTxt.Text = "";
                        PasswordTxt.Text = "";
                        File.WriteAllText(_fileName, loginStr+"\n"+"employee");
                        await Navigation.PushAsync(new Pages.Employee.EmployeeMainPage(loginStr), true);
                    }

                    

                }

            }
            catch (Exception er) {
                //result.Text = er.ToString();
                await DisplayAlert("Result", er.ToString(), "OK");
            }

        }

        public async void AutoLogin(string userName, string role) {
            if(role == "student") {
                var studentMainPage = new Pages.Student.StudentMainPage(userName);
                NavigationPage.SetHasNavigationBar(studentMainPage, false);
                NavigationPage mypage = new NavigationPage(studentMainPage);
                await Navigation.PushAsync(new Pages.Student.StudentMainPage(userName), true);
            }
            else {
                await Navigation.PushAsync(new Pages.Employee.EmployeeMainPage(userName), true);
            }
        }

    }
}




// LINKI

// https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/xaml/xaml-basics/get-started-with-xaml

// https://www.c-sharpcorner.com/article/xamarin-forms-create-a-login-page-mvvm/

// https://www.youtube.com/watch?v=xRUnOnDkYLI

// https://www.youtube.com/watch?v=XH4FYKIf8Ts

// https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/user-interface/layouts/grid

// Activity Page
//https://www.youtube.com/watch?v=ZNz2gRPvAuw

//
// https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/app-fundamentals/application-class#Properties_Dictionary
