using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Timetable.Models;
using Timetable.Data;
using Timetable.Views;
using Timetable.Views.BufferPages;

namespace Timetable
{
    public partial class App : Application
    {
        INotificationManager notificationManager;

        public App()
        {
            DependencyService.Get<INotificationManager>().Initialize();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
            
             notificationManager.SendNotification("title", "message");

            //MainPage = new NavigationPage(new NotesPage((int)DateTime.Now.DayOfWeek));
            NotesPage d = new NotesPage((int)DateTime.Now.DayOfWeek);
            CarouselPage CP = new CarouselPage();
            CP.Children.Add(new Minus1());
            CP.Children.Add(d);
            CP.Children.Add(new Plas1());
            //CP.Children[1].Parent = ;
            CP.CurrentPage = CP.Children[1];

            MainPage = new NavigationPage(CP);



           


        }


        void OnSendClick(object sender, EventArgs e)
        {
            string title = $"Local Notification #";
            string message = $"You have now received  notifications!";
            notificationManager.SendNotification(title, message);
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            string title = $"Local Notification #";
            string message = $"You have now received  notifications!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
               
            });
        }
        protected override void OnStart()
        {
            string title = $"Local Notification #";
            string message = $"You have now received  notifications!";
            notificationManager.SendNotification(title, message);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

