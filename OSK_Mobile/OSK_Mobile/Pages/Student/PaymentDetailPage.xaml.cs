using OSK_Mobile.Models;
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
    public partial class PaymentDetailPage : ContentPage
    {
        private StudentPayment studentPayment;
        
        public PaymentDetailPage(StudentPayment s) {
            InitializeComponent();
            studentPayment = s;
            SetData();
        }

        private void SetData() {
            paymentData.Text = studentPayment.PaymentDate;
            timePayment.Text = studentPayment.PaymentTime;
            cost.Text = studentPayment.Cost.ToString() + " zł";
            employee.Text = studentPayment.Employee;
            typePayment.Text = studentPayment.TypePayment;
        }

    }
}