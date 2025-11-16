using Calendar.Model;
using Calendar.Service;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Calendar.ViewModel
{
    public class DayWindowViewModel : ViewModelBase
    {
        public ObservableCollection<string> Hours { get; set; }
        public ObservableCollection<DayItem> TimedEvents { get; set; }
        public ObservableCollection<DayItem> AllDayEvents { get; set; }

        private AppointmentService appointmentService = new AppointmentService();
        private AbsenceService absenceService = new AbsenceService();

        public DayWindowViewModel(string yearAndMonth, string day)
        {
            Hours = new ObservableCollection<string>(
                Enumerable.Range(0, 24).Select(h => $"{h:D2}:00"));

            TimedEvents = new ObservableCollection<DayItem>();
            AllDayEvents = new ObservableCollection<DayItem>();

            LoadEvents(yearAndMonth, day);
        }

        private void LoadEvents(string yearAndMonth, string day)
        {
            string dateString = $"{day} {yearAndMonth}";
            DateTime date;

            DateTime.TryParseExact(
                dateString,
                "d MMMM yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            try
            {
                //------------------------------------------------------
                // LOAD APPOINTMENTS 
                //------------------------------------------------------
                var appointments = appointmentService
                    .GetAllForUserByDate(Data.Instance.LoggedInUser.Id, date);

                foreach (var a in appointments)
                {
                    TimedEvents.Add(new DayItem
                    {
                        Title = a.Title,
                        Start = date + a.StartOfTheAppointment,
                        End = date + a.EndOfTheAppointment
                    });
                }

                //------------------------------------------------------
                // LOAD ABSENCES → ALL-DAY SECTION
                //------------------------------------------------------
                var absences = absenceService
                    .GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, date);
                var dayStart = date.Date;
                var dayEnd = dayStart.AddDays(1);

                foreach (var a in absences)
                {
                    var start = a.StartOfTheEvent;
                    var end = a.EndOfTheEvent;

                    // Ako odsustvo nema veze sa ovim danom, preskoči
                    if (start.Date > date || end.Date < date)
                        continue;

                    // Skini početak i kraj na granicu dana
                    if (start < dayStart) start = dayStart;
                    end = dayEnd; // poslednji trenutak dana

                    AllDayEvents.Add(new DayItem
                    {
                        Title = $"{a.User.FirstName}: {a.Event}",
                        Start = start,
                        End = end,
                        AbsenceType = a.Event.ToString()
                    });
                }




                //------------------------------------------------------
                // COMPUTE LANE INDEX FOR OVERLAPPING EVENTS
                //------------------------------------------------------
                CalculateLanes();
            }
            catch (Exception ex)
            {
                Log.Warning("Failed to load events for {Date}: {Message}", date, ex.Message);
            }
        }

        /// <summary>
        /// Assigns LaneIndex and LaneCount to events that overlap.
        /// </summary>
        private void CalculateLanes()
        {
            var events = TimedEvents.OrderBy(e => e.Start).ToList();
            var active = new List<DayItem>();

            foreach (var ev in events)
            {
                // remove finished events
                active.RemoveAll(a => a.End <= ev.Start);

                // assign smallest available lane index
                int lane = 0;
                while (active.Any(a => a.LaneIndex == lane))
                    lane++;

                ev.LaneIndex = lane;
                active.Add(ev);

                // total number of lanes for all overlapping events
                int maxLane = active.Max(a => a.LaneIndex) + 1;

                foreach (var a in active)
                    a.LaneCount = maxLane;
            }
        }
    }
}
