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
    public partial class EmployeeActivitePage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private int _userID;

        public EmployeeActivitePage(int userID) {
            InitializeComponent();
            GetData(beginDate.Date.ToString("yyyy-MM-dd"), endDate.Date.ToString("yyyy-MM-dd"));
            _userID = userID;
        }

        public void GetDataClick(object sender, EventArgs e) {
            GetData(beginDate.Date.ToString("yyyy-MM-dd"), endDate.Date.ToString("yyyy-MM-dd"));
        }

        public async void GetData(string beginDate, string endDate) {
            string Url_EmployeeActivites = "http://10.0.2.2:32141/Employee/GetEmoloyeeActivites";

            var data = new EmployeeActiviteData() {
                UserID = _userID,
                BeginDate = beginDate,
                EndDate = endDate
            };

            string JsonData = JsonConvert.SerializeObject(data);
            var resourse = await _clientOSK.PostAsync(Url_EmployeeActivites, new StringContent(JsonData, Encoding.UTF8, "application/json"));
            var resultData = await resourse.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<EmployeeActiviteData>(resultData);

            countTotal.Text = (responseData.CountOfPlaned + responseData.CountOfDone + responseData.CountOfCancel).ToString();
            countPlanned.Text = responseData.CountOfPlaned.ToString();
            countDone.Text = responseData.CountOfDone.ToString();
            countCancel.Text = responseData.CountOfCancel.ToString();

        }

    }
}