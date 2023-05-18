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

            todoItem.StartTime = DateTime.MinValue + StartTimePicker.Time;
            todoItem.EndTime = DateTime.MinValue + EndTimePicker.Time;
            todoItem.DateOfNote = DateOfTasc.Date;


            todoItem.Text = NoteText.Text;

            todoItem.IsDelayed = itsSpecialNote.IsToggled;
            todoItem.WithТotice = WithТotice.IsToggled;


            if (itsSpecialNote.IsToggled)
            {
                todoItem.DayOfTheWeek = (int)todoItem.DateOfNote.DayOfWeek;
                todoItem.TimeString = todoItem.StartTime.ToShortTimeString() + " " + todoItem.DateOfNote.ToShortDateString();
            }
            else
            {
                todoItem.DayOfTheWeek = (DayPicker.SelectedIndex + 1) % 7;
                todoItem.TimeString = todoItem.StartTime.ToShortTimeString() + " - " + todoItem.EndTime.ToShortTimeString();
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
            if (todoItem.TimeString != null)
            {
                TimeSpan timeSpan = new TimeSpan(todoItem.EndTime.Ticks);
                EndTimePicker.Time = timeSpan;
                timeSpan = new TimeSpan(todoItem.StartTime.Ticks);
                StartTimePicker.Time = timeSpan;

                itsSpecialNote.IsToggled = todoItem.IsDelayed;
                WithТotice.IsToggled = todoItem.WithТotice;

                DateOfTasc.Date = todoItem.DateOfNote;

                NoteText.Text = todoItem.Text;

                DayPicker.SelectedIndex = (todoItem.DayOfTheWeek -1) % 7;

            }
        }


        private void ChangedTypeOfNote(object sender, ToggledEventArgs e)
        {
                dayPicker.IsVisible = !itsSpecialNote.IsToggled;
                datePicker.IsVisible = itsSpecialNote.IsToggled;
        }
    }
}