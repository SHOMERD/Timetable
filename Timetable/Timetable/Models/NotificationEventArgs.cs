using System;

namespace Timetable
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime dateTimeOfNotification { get; set; }

    }
}
