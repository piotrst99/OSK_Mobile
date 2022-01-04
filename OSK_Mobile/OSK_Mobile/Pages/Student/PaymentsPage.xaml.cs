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
    public partial class PaymentsPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private int _userID;
        private List<StudentPayment> studentPayments;

        public PaymentsPage(int userID) {
            InitializeComponent();
            _userID = userID;
            GetPayments();
            listOfPayments.ItemTapped += GetPaymentDetail;
        }

        private async void GetPayments() {
            string UrlGetPractical = "http://10.0.2.2:32141/Student/GetStudentPayments";

            UserName userData = new UserName() {
                userID = _userID
            };

            //listOfDrivesActivities.ItemTemplate = null;

            try {
                string JsonData = JsonConvert.SerializeObject(userData);
                var resourse = await _clientOSK.PostAsync(UrlGetPractical, new StringContent(JsonData, Encoding.UTF8, "application/json"));
                var resultData = await resourse.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<StudentPayment>>(resultData);

                studentPayments = responseData;

                listOfPayments.ItemsSource = responseData;


            }
            catch (Exception er) {
                await DisplayAlert("Result", er.ToString(), "OK");
            }
        }

        private async void GetPaymentDetail(object sender, ItemTappedEventArgs e) {
            int index = (listOfPayments.ItemsSource as List<StudentPayment>).IndexOf(e.Item as StudentPayment);

            if (e.Item == null) {
                return;
            }
            else if (sender is ListView lv) {
                lv.SelectedItem = null;
            }

            await Navigation.PushAsync(new PaymentDetailPage(studentPayments[index]), true);

        }

    }
}