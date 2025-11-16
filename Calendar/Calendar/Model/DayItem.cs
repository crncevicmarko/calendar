using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class DayItem
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string AbsenceType { get; set; }

        public int LaneIndex { get; set; }
        public int LaneCount { get; set; }
    }

}
