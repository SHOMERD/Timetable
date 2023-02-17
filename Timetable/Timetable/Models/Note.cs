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
        public DateTime Time { get; set; }  
        public int DayOfTheWeek { get; set; }
        public string StrinrTime { get; set; }
    }
}
