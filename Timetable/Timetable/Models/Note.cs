using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timetable.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }


        public DateTime StartTime { get; set;}
        public DateTime EndTime{ get ; set; }
        public int DayOfTheWeek { get; set; }
        public string StringStartTime { get; set; }
        public string StringEndTime { get; set; }

        public bool WithТotice { get; set; }

        public bool IsDelayed { get; set; }
        public DateTime DateOfNote { get; set; }


    }
}
