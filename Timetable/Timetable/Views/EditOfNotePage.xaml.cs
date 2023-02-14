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
            todoItem.Time = DateTime.Today + timePicker.Time;
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
    }
}