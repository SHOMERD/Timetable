using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Timetable.Models;
using Timetable.Views;
using Timetable.Views.BufferPages;

namespace Timetable
{
    public partial class App : Application
    {


        public App()
        {
            
            //MainPage = new NavigationPage(new NotesPage((int)DateTime.Now.DayOfWeek));
            NotesPage notesPage = new NotesPage((int)DateTime.Now.DayOfWeek);
            CarouselPage CP = new CarouselPage();
            CP.Children.Add(new Minus1());
            CP.Children.Add(notesPage);
            CP.Children.Add(new Plas1());
            //CP.Children[1].Parent = ;
            CP.CurrentPage = CP.Children[1];

            MainPage = new NavigationPage(CP);



           


        }



        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

