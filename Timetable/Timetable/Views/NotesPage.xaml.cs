using System;
using Timetable.Data;
using Timetable.Models;
using Xamarin.Forms;

namespace Timetable.Views
{
    public partial class NotesPage : ContentPage
    {
        public int DayOfTheWeek;
        public bool CanShouAll;

        INotificationManager notificationManager;

        public NotesPage(int l = 0, bool ShouAll = false)
        {
            this.CanShouAll = ShouAll;
            DayOfTheWeek = l;
            
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            switch (DayOfTheWeek)
            {
                case 1:
                    Day.Text = "Понедельник";
                    break;
                case 2:
                    Day.Text = "Вторник";
                    break;
                case 3:
                    Day.Text = "Среда";
                    break;
                case 4:
                    Day.Text = "Четверг";
                    break;
                case 5:
                    Day.Text = "Пятница";
                    break;
                case 6:
                    Day.Text = "Суббота";
                    break;
                case 0:
                    Day.Text = "Воскресенье";
                    break;
            }
            NotesDB database = await NotesDB.Instance;
            if (CanShouAll)
            {
                listView.ItemsSource = await database.GetItemsAsync();
            }
            else
            {
                listView.ItemsSource = await database.SortingPendingNotes(DayOfTheWeek);
            }
            
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditOfNotePage
            {
                BindingContext = new Note()
            });
        }

        async void ShouAll(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotesPage(0, true));
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditOfNotePage
                {
                    BindingContext = e.SelectedItem as Note
                });
            }
        }

        void LastDay(object sender, EventArgs e)
        {
            ChangeDay(-1);
        }

        void NextDay(object sender, EventArgs e)
        {
            ChangeDay(+1);
        }

        public void ChangeDay(int S)
        {
            DayOfTheWeek += S;
            if (DayOfTheWeek < 0)
            {
                for (; DayOfTheWeek < 0; DayOfTheWeek += 7) ;
            }
            DayOfTheWeek = (DayOfTheWeek + 7) % 7;
            OnAppearing();
        }

        private void Day_Clicked(object sender, EventArgs e)
        {
            Day_Cli();
        }


        private void Day_Cli()
        {
           

            DependencyService.Get<INotificationManager>().Initialize();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
            
             notificationManager.SendNotification("title", "message");
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
    }
}