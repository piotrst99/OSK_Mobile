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

namespace OSK_Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        
        public MainPage() {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
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

                LoginData loginData = new LoginData() {
                    login = loginStr,
                    password = passwordStr,
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

                        await Navigation.PushAsync(new Pages.Student.StudentMainPage(loginStr), true);
                        
                        LoginTxt.Text = "";
                        PasswordTxt.Text = "";
                    }
                    else {
                        LoginTxt.Text = "";
                        PasswordTxt.Text = "";
                        await Navigation.PushAsync(new Pages.Employee.EmployeeMainPage(loginStr), true);
                    }

                    

                }

            }
            catch (Exception er) {
                //result.Text = er.ToString();
                await DisplayAlert("Result", er.ToString(), "OK");
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







/*string param = "?login=" + login + "&password=" + password + "&val=" + value.ToString();
                string r = Url_CheckLoginData + param;

                var resourse = await _clientOSK.GetStringAsync(r);
                var deserializedCount = JsonConvert.DeserializeObject<LoginResult>(resourse);
                //_loginResult = new ObservableCollection<LoginResult>(deserializedCount);
               
                DisplayAlert("Result", deserializedCount.result.ToString(), "OK");*/




/*protected override async void OnAppearing() {

            try {
                var countTest = await _clientOSK.GetStringAsync(Url_Osk);
                var deserializedCount = JsonConvert.DeserializeObject<List<Test>>(countTest);
                _test = new ObservableCollection<Test>(deserializedCount);
                countOfStudents.Text = "Liczba uzytkowników: "+_test.Count.ToString();
                //countOfStudents.Text = 0.ToString();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

            base.OnAppearing();
        }*/


/*private async void OnAdd(object sender, EventArgs e) {
    try {
        var post = new Post { Title = $"Title: Timestamp {DateTime.UtcNow.Ticks}" }; //Creating a new instane of Post with a Title Property and its value in a Timestamp format
        var serializedPost = JsonConvert.SerializeObject(post); //Serializes or convert the created Post into a JSON String
        var response = await _client.PostAsync(Url, new StringContent(serializedPost, Encoding.UTF8, "application/json")); //Send a POST request to the specified Uri as an asynchronous operation and with correct character encoding (utf9) and contenct type (application/json).
        var content = await response.Content.ReadAsStringAsync();
        var deserializedPost = JsonConvert.DeserializeObject<Post>(content); // deserializing 
        _posts.Insert(0, deserializedPost); //Updating the UI by inserting an element into the first index of the collection 
    }
    catch (Exception exception) {
        Console.WriteLine(exception);
        throw;
    }

}*/


/*private async void OnUpdate(object sender, EventArgs e) {
    var post = _posts[0]; //Assigning the first Post object of the Post Collection to a new instance of Post
    post.Title += " [updated]"; //Appending an [updated] string to the current value of the Title property
    var serializedPost = JsonConvert.SerializeObject(post); //Serializes or convert the created Post into a JSON String
    await _client.PutAsync(Url + "/" + post.Id, new StringContent(serializedPost, Encoding.UTF8, "application/json")); //Send a PUT request to the specified Uri as an asynchronous operation.
}*/

/*private async void OnDelete(object sender, EventArgs e) {
    var post = _posts[0]; //Assigning the first Post object of the Post Collection to a new instance of Post
    var response = await _client.DeleteAsync(Url + "/" + post.Id); //Send a DELETE request to the specified Uri as an asynchronous 

    if (response.IsSuccessStatusCode == false) return;

    _posts.Remove(post); //Removes the first occurrence of a specific object from the Post collection. This will be visible on the UI instantly
}*/