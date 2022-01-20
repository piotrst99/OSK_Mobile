using OSK_Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OSK_Mobile.Pages.Employee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcceptDrivePage : ContentPage
    {
        private PracticalData practicalData;
        public AcceptDrivePage(PracticalData p) {
            InitializeComponent();
            practicalData = p;
        }
    }
}