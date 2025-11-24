using Calendar.Model;
using Calendar.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Calendar.ViewModel
{
    public class UserControlWeeksViewModel : ViewModelBase
    {
        private IAbsenceService absenceService = new AbsenceService();
        private IAppointmentService appointmentService = new AppointmentService();

        public ObservableCollection<DayItemViewModel> DaysOfWeek { get; set; }
        public string WeekRange { get; set; }

        public UserControlWeeksViewModel() : this(DateTime.Today) { }

        public UserControlWeeksViewModel(DateTime referenceDate)
        {
            DaysOfWeek = new ObservableCollection<DayItemViewModel>();
            GenerateWeek(referenceDate);
        }

        public void GenerateWeek(DateTime referenceDate)
        {
            // Prvi dan nedelje = Sunday
            int diffToSunday = (int)referenceDate.DayOfWeek; // Sunday = 0, Monday = 1 ...
            DateTime sunday = referenceDate.AddDays(-diffToSunday);

            DaysOfWeek.Clear();

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = sunday.AddDays(i);

                // Dohvati odsustva i termine za taj dan
                var absences = new List<DayItem>();
                var appointments = new List<DayItem>();

                if (Data.Instance.LoggedInUser != null)
                {
                    if (Data.Instance.LoggedInUser.IsAdmin)
                    {
                        absences = absenceService.GetAllForDate(currentDay)
                            .Select(a => new DayItem
                            {
                                Title = $"{a.User.FirstName}: {a.Event}",
                                Start = a.StartOfTheEvent,
                                End = a.EndOfTheEvent,
                                AbsenceType = a.Event.ToString()
                            }).ToList();
                    }
                    else
                    {
                        absences = absenceService.GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, currentDay)
                            .Select(a => new DayItem
                            {
                                Title = $"{a.User.FirstName}: {a.Event}",
                                Start = a.StartOfTheEvent,
                                End = a.EndOfTheEvent,
                                AbsenceType = a.Event.ToString()
                            }).ToList();

                        appointments = appointmentService.GetAllForUserByDate(Data.Instance.LoggedInUser.Id, currentDay)
                            .Select(a => new DayItem
                            {
                                Title = a.Title,
                                Start = currentDay + a.StartOfTheAppointment,
                                End = currentDay + a.EndOfTheAppointment
                            }).ToList();
                    }
                }

                // Dodaj u listu dana za week view
                DaysOfWeek.Add(new DayItemViewModel
                {
                    DayNumber = currentDay.Day,
                    Date = currentDay,
                    Absences = absences,
                    Appointments = appointments
                });
            }
        }



    }


    public class DayItemViewModel
    {
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<DayItem> Absences { get; set; }
        public IEnumerable<DayItem> Appointments { get; set; }
    }
}
