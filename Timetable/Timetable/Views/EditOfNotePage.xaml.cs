using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Timetable.Models;
using Timetable.Data;
using System.Globalization;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Timetable.Views
{
    
    public partial class EditOfNotePage : ContentPage
    {
        public EditOfNotePage()
        {
            InitializeComponent();
           
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            Note todoItem = (Note)BindingContext;

            todoItem.StartTime = todoItem.StartTime;
            todoItem.EndTime = todoItem.EndTime;
            todoItem.StringStartTime = todoItem.StartTime.ToShortTimeString();
            todoItem.StringEndTime = todoItem.EndTime.ToShortTimeString();

            if (itsSpecialNote.IsToggled)
            {
                todoItem.DayOfTheWeek = (int)todoItem.DateOfNote.DayOfWeek;
            }

            NotesDB database = await NotesDB.Instance;
            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (Note)BindingContext;
            NotesDB database = await NotesDB.Instance;
            await database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        protected override void OnBindingContextChanged()
        {

            Note todoItem = (Note)BindingContext;
            //!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (todoItem.StartTime != null)
            {
                TimeSpan timeSpan = new TimeSpan(todoItem.EndTime.Ticks);
                EndTimePicker.Time = timeSpan;
                timeSpan = new TimeSpan(todoItem.StartTime.Ticks);
                StartTimePicker.Time = timeSpan;
            }
        }

        private void ChangedTypeOfNote(object sender, ToggledEventArgs e)
        {
                dayPicker.IsVisible = !itsSpecialNote.IsToggled;
                datePicker.IsVisible = itsSpecialNote.IsToggled;
        }
    }
}