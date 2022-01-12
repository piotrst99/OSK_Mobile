using OSK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OSK_Mobile.Pages.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentCourseDetailPage : ContentPage
    {
        StudentCourseData studentCourseData;// = new StudentCourseData();
        public StudentCourseDetailPage(StudentCourseData data) {
            InitializeComponent();
            studentCourseData = data;
            SetData();
            
        }

        private void SetData() {
            //dataTest.Text = studentCourseData.CourseName + " " + studentCourseData.StartDate + " " + studentCourseData.StudentCourseStatus;
            courseNameTxt.Text = studentCourseData.CourseName;
            category.Text = studentCourseData.Category;
            startCourseDate.Text = studentCourseData.StartDate;
            endCourseDate.Text = studentCourseData.EndDate;
            extraHours.Text = studentCourseData.ExtraHours.ToString();
            sumOfPayments.Text = studentCourseData.SumOfPayments.ToString()+" zł";
            studentCourseStatus.Text = studentCourseData.StudentCourseStatus;
            countOfCompletedPractical.Text = studentCourseData.CountOfCompletedPractical.ToString() + "/" + studentCourseData.CountOfPractical.ToString();
        }

    }
}