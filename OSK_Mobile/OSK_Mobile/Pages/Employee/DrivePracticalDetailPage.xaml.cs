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
    public partial class DrivePracticalDetailPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private PracticalData practicalData;

        public DrivePracticalDetailPage(PracticalData p) {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            practicalData = p;
            SetData();
        }

        private void SetData() {
            data.Text = practicalData.Data;
            startTime.Text = practicalData.StartTime;
            endTime.Text = practicalData.EndTime;
            student.Text = practicalData.Student;
            employee.Text = practicalData.Employee;
            category.Text = practicalData.Category;
            vehicle.Text = practicalData.Vehicle;
            status.Text = practicalData.Status;
            studentPhone.Text = practicalData.StudentPhone;
            
            //cancelBtn.IsEnabled = !practicalData.IsCancel;
        }

        private async void CallToStudent(object sender, EventArgs e) {
            await DisplayAlert("POŁĄCZENIE", "Rozpoczęcie rozmowy z numerem: " + practicalData.StudentPhone, "OK");
        }


        /*private async void CancelPracticalRequest(object sender, EventArgs e) {

            bool result = await DisplayAlert("UWAGA", "Czy napewno chcesz odwołać jazdę?", "TAK", "NIE");

            if (result) {
                string UrlCancelPractical = "http://10.0.2.2:32141/Activities/CancelPracticalRequest";
                //cancelBtn.IsEnabled = false;
                practicalData.IsCancel = true;

                PracticalData p = new PracticalData() {
                    ID = practicalData.ID,
                    IsCancel = true
                };

                string JsonData = JsonConvert.SerializeObject(p);
                await _clientOSK.PostAsync(UrlCancelPractical, new StringContent(JsonData, Encoding.UTF8, "application/json"));
            }

        }*/

    }
}