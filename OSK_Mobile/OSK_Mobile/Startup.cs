using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using OSK_Mobile.Entities;

namespace OSK_Mobile
{
    public static class Startup{
        private static IServiceProvider serviceProvider;
        public static void ConfigureServices() {
            var services = new ServiceCollection();

            //add services
            /*services.AddHttpClient<Test>(c => {
                c.BaseAddress = new Uri("http://10.0.2.2:44634/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });*/

            //add viewmodels
            /*services.AddTransient<BooksViewModel>();
            services.AddTransient<AddBookViewModel>();
            services.AddTransient<BookDetailsViewModel>();

            serviceProvider = services.BuildServiceProvider();*/
        }

        //public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
