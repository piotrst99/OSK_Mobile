using Newtonsoft.Json;
using OSK_API.Models;
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
    public partial class StudentCoursesPage : ContentPage
    {
        private readonly HttpClient _clientOSK = new HttpClient();
        private int _userID;
        private List<StudentCourseData> studentCourseDatas;

        public StudentCoursesPage(int userID) {
            InitializeComponent();
            _userID = userID;
            GetUserCourses();
        }

        private async void GetUserCourses() {
            string UrlGetUserName = "http://10.0.2.2:32141/Student/GetStudentCourses";

            UserName userName = new UserName() {
                userID = _userID,
            };

            try {
                string JsonData = JsonConvert.SerializeObject(userName);
                var resourse = await _clientOSK.PostAsync(UrlGetUserName, new StringContent(JsonData, Encoding.UTF8, "application/json"));
                var resultData = await resourse.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<StudentCourseData>>(resultData);

                studentCourseDatas = responseData;

                listOfStudentCourses.ItemsSource = responseData;
                listOfStudentCourses.ItemTapped += Click;
                //listOfStudentCourses.ItemSelected += Click;

            }
            catch (Exception er) {
                DisplayAlert("Result", er.ToString(), "OK");
            }


        }

        private async void Click(object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            int index = (listOfStudentCourses.ItemsSource as List<StudentCourseData>).IndexOf(e.Item as StudentCourseData);
            
            if(e.Item == null) {
                return;
            }
            else if(sender is ListView lv) {
                lv.SelectedItem = null;
            }

            await Navigation.PushAsync(new StudentCourseDetailPage(studentCourseDatas[index]), true); 
        }

    }
}