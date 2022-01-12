using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OSK_Mobile
{
    public partial class App : Application
    {
        public static bool isLogin { get; set; }
        public string userName { get; set; }
        public int role { get; set; }

        public App() {
            InitializeComponent();

            Startup.ConfigureServices();

            if (!isLogin) {
                MainPage = new NavigationPage(new MainPage());
                /*var mPage = new MainPage();
                NavigationPage.SetHasNavigationBar(mPage, false);
                NavigationPage mypage = new NavigationPage(mPage);
                MainPage = mypage;*/
            }
            else {
                //
            }

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage());
            //NavigationPage.SetHasNavigationBar(MainPage, false);

            

            /*if (Application.Current.Properties.ContainsKey("isLogin")) {
                //bool id = Application.Current.Properties["isLogin"] as int;
                //DisplayAlert("Result", "Is Logged", "OK");
            }*/

        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}


// https://www.youtube.com/watch?v=-Nj_TRPlx-8

