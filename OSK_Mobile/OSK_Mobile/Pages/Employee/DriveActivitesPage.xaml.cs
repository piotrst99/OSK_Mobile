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
    public partial class DriveActivitesPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private int _userID;
        private List<PracticalData> practicalDatas;

        public DriveActivitesPage(int userID) {
            InitializeComponent();
            _userID = userID;
            dateSelect.DateSelected += GetDriveActivitiesFromCallendar;
            GetDriveActivities(DateTime.Now.ToString("yyyy-MM-dd"), 2);
            listOfDrivesActivities.ItemTapped += GetPracticalDetail;
            //GetDriveActivities(null);
        }

        private async void GetDriveActivities(string date, int val) {
            string UrlGetPractical = "http://10.0.2.2:32141/Activities/GetDriveActivities";

            CallendarData userName = new CallendarData() {
                UserID = _userID,
                Date = date,
                TypeData = val
            };

            try {
                string JsonData = JsonConvert.SerializeObject(userName);
                var resourse = await _clientOSK.PostAsync(UrlGetPractical, new StringContent(JsonData, Encoding.UTF8, "application/json"));
                var resultData = await resourse.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<PracticalData>>(resultData);

                practicalDatas = responseData;

                listOfDrivesActivities.ItemsSource = responseData;
                

            }
            catch (Exception er) {
                await DisplayAlert("Result", er.ToString(), "OK");
            }

        }

        private async void GetPracticalDetail(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            int index = (listOfDrivesActivities.ItemsSource as List<PracticalData>).IndexOf(e.Item as PracticalData);

            if (e.Item == null) {
                return;
            }
            else if (sender is ListView lv) {
                lv.SelectedItem = null;
            }

            //await Navigation.PushModalAsync(new DrivePracticalDetailPage(practicalDatas[index]), true);
            await Navigation.PushAsync(new DrivePracticalDetailPage(practicalDatas[index]), true);

        }

        private void GetDriveActivitiesFromCallendar(object sender, DateChangedEventArgs e) {
            GetDriveActivities(dateSelect.Date.ToString("yyyy-MM-dd"), 2);
        }

        private void GetDrivesActivitesForEmployee(object sender, EventArgs e) {
            GetDriveActivities(dateSelect.Date.ToString("yyyy-MM-dd"), 2);
        }

        private void GetDrivesActivitesForAllEmployees(object sender, EventArgs e) {
            GetDriveActivities(dateSelect.Date.ToString("yyyy-MM-dd"), 1);
        }
    }
}