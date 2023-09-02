using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Data;
using Timetable.Models;
using Xamarin.Forms;

namespace Timetable.Droid
{
    public class NotificationNoteHandler
    {
        INotificationManager notificationManager;


        List<Note> Notes;

        public NotificationNoteHandler(INotificationManager notificationManager)
        {
            this.notificationManager = notificationManager;

            TakeDatabase();
            Transformer();
        }


        async void TakeDatabase()
        {
            
        }

        async void Transformer()
        {
            NotesDB database = await NotesDB.Instance;
            Notes = await database.GetItemsAsync();
            for (int i = 0; i <= Notes.Count;  i++)
            {
                if (Notes[i].IsDelayed && (Notes[i].DateOfNote > DateTime.Now && Notes[i].DateOfNote < DateTime.Now.AddDays(5)))
                {
                    notificationManager.SendNotification(Notes[i].Text, $"{Notes[i].Text} у вас отложка на аывпы", Notes[i].DateOfNote);
                    notificationManager.SendNotification(Notes[i].Text, $"{Notes[i].Text} у вас отложка на аывпы", Notes[i].DateOfNote.AddDays(-4));
                    notificationManager.SendNotification(Notes[i].Text, $"{Notes[i].Text} у вас отложка на аывпы", Notes[i].DateOfNote.AddTicks(Notes[i].StartTime.TimeOfDay.Ticks));
                }
                else
                {
                    if (Notes[i].StartTime.AddHours(-1).TimeOfDay > DateTime.Now.TimeOfDay)
                    {
                        
                    }
                }


                notificationManager.SendNotification(Notes[i].Text, $"{Notes[i].Text} начнется через час просле этого уведомления!!", DateTime.Now.Date.Add(Notes[i].StartTime.TimeOfDay));
            }
        }

    }
}