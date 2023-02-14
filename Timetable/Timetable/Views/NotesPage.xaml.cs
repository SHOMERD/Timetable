using System;
using Timetable.Data;
using Timetable.Models;
using Xamarin.Forms;

namespace Timetable.Views
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            NotesDB database = await NotesDB.Instance;
            listView.ItemsSource = await database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditOfNotePage
            {
                BindingContext = new Note()
            });
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
    }
}