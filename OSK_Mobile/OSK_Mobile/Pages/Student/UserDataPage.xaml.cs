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

namespace OSK_Mobile.Pages.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDataPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private int _userID;

        public UserDataPage(int userID) {
            InitializeComponent();
            _userID = userID;

            GetUserData();
        }

        private async void GetUserData() {
            string UrlGetUserName = "http://10.0.2.2:32141/Student/GetStudentData";

            try {

                UserName userName = new UserName() {
                    userID = _userID,
                };

                string JsonData = JsonConvert.SerializeObject(userName);
                var resourse = await _clientOSK.PostAsync(UrlGetUserName, new StringContent(JsonData, Encoding.UTF8, "application/json"));
                var resultData = await resourse.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<UserData>(resultData);

                namesTxt.Text = responseData.FirstName + " " + (responseData.SecondName != null ? responseData.SecondName : "");
                surnameTxt.Text = responseData.Surname;
                peselTxt.Text = responseData.PESEL;
                userNameTxt.Text = responseData.UserName;

                townTxt.Text = responseData.Town;
                streetTxt.Text = responseData.Street;
                nrHomeTxt.Text = responseData.NrHome + " " + (responseData.NrLocal != null ? " / " + responseData.NrLocal : "");
                postCodeTxt.Text = responseData.PostCode;
                postTxt.Text = responseData.Post;
                phoneTxt.Text = responseData.PhoneNumber;
                emailTxt.Text = responseData.Email;

            }
            catch (Exception er) {
                DisplayAlert("Result", er.ToString(), "OK");
            }

        }

    }
}