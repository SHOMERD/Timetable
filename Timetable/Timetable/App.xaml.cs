using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Timetable.Models;
using Timetable.Data;
using Timetable.Views;

namespace Timetable
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new NotesPage((int)DateTime.Now.DayOfWeek));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

