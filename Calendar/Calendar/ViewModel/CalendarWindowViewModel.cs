using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
using Calendar.ViewModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class CalendarWindowViewModel : ViewModelBase
    {
        int month, year;
        private ObservableCollection<UserControl> items;
        private string labelText;

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand WeekCommand { get; set; }
        public ICommand DayCommand { get; set; }

        private IAbsenceService absenceService = new AbsenceService();
        private IAppointmentService appointmentService = new AppointmentService();

        private UserControlDays _selectedDay;
        public UserControlDays SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    OnPropertyChanged(nameof(SelectedDay));
                    ((RelayCommand)DayCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private UserControlWeeksViewModel _weekView;
        public UserControlWeeksViewModel WeekView
        {
            get => _weekView;
            set
            {
                _weekView = value;
                OnPropertyChanged(nameof(WeekView));
            }
        }

        private bool _isWeekView;
        public bool IsWeekView
        {
            get => _isWeekView;
            set
            {
                _isWeekView = value;
                OnPropertyChanged(nameof(IsWeekView));
                UpdateLabelText();
            }
        }

        public ObservableCollection<UserControl> Items
        {
            get { return items; }
            set
            {
                if (value != items)
                {
                    items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public string LabelText
        {
            get { return labelText; }
            set
            {
                if (value != labelText)
                {
                    labelText = value;
                    OnPropertyChanged(nameof(LabelText));
                }
            }
        }

        
        public CalendarWindowViewModel()
        {
            Items = new ObservableCollection<UserControl>();

            // Default view = month
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            IsWeekView = false; // uverimo se da je mesec prikaz aktivan
            DisplayDays(); // popuni Items sa danima

            // Komande
            PreviousCommand = new RelayCommand(PreviousCommandExecute, CanPreviousCommandExecute);
            NextCommand = new RelayCommand(NextCommandExecute, CanNextCommandExecute);
            WeekCommand = new RelayCommand(WeekCommandExecute, CanWeekCommandExecute);
            DayCommand = new RelayCommand(DayCommandExecute, CanDayCommandExecute);
        }


        #region Commands

        private bool CanNextCommandExecute(object obj) => true;

        private void NextCommandExecute(object obj)
        {
            Items.Clear();
            month++;
            if (month == 13)
            {
                month = 1;
                year++;
            }

            if (!IsWeekView)
            DisplayDays();
            UpdateLabelText();
        }

        private bool CanPreviousCommandExecute(object obj) => true;

        private void PreviousCommandExecute(object obj)
        {
            Items.Clear();
            month--;
            if (month == 0)
            {
                month = 12;
                year--;
            }

            if (!IsWeekView)
            DisplayDays();
            UpdateLabelText();
        }

        private bool CanDayCommandExecute(object obj) => SelectedDay != null;

        private void DayCommandExecute(object obj)
        {
            if (SelectedDay == null) return;
            IsWeekView = false;

            string yearAndMonth = LabelText;
            var day = SelectedDay.lblnum.Content.ToString();
            var dayWindow = new DayWindow(yearAndMonth, day);
            dayWindow.Show();
        }

        private bool CanWeekCommandExecute(object obj) => true;

        private void WeekCommandExecute(object obj)
        {
            IsWeekView = !IsWeekView;

            if (IsWeekView)
            {
                var referenceDate = SelectedDay != null
                    ? new DateTime(year, month, int.Parse(SelectedDay.lblnum.Content.ToString()))
                    : DateTime.Today;

                WeekView = new UserControlWeeksViewModel(referenceDate);

                // Kreiraj UserControlWeek sa ViewModel-om
                CurrentWeekControl = new UserControlWeek(WeekView);
            }
        }


        private UserControlWeek _currentWeekControl;
        public UserControlWeek CurrentWeekControl
        {
            get => _currentWeekControl;
            set
            {
                _currentWeekControl = value;
                OnPropertyChanged(nameof(CurrentWeekControl));
            }
        }


        #endregion

        private void DisplayDays()
        {
            Items.Clear();
            LabelText = new DateTime(year, month, 1).ToString("MMMM yyyy");

            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayOfTheWeek = (int)startOfTheMonth.DayOfWeek;

            // Blank days for alignment
            for (int i = 0; i < dayOfTheWeek; i++)
            {
                Items.Add(new UserControlBlanck());
            }

            for (int i = 1; i <= days; i++)
            {
                DateTime date = new DateTime(year, month, i);
                UserControlDays dayControl = new UserControlDays();
                dayControl.days(i);

                try
                {
                    IEnumerable<DayItem> dayAbsences = new List<DayItem>();
                    IEnumerable<DayItem> dayAppointments = new List<DayItem>();

                    if (Data.Instance.LoggedInUser != null)
                    {
                        if (Data.Instance.LoggedInUser.IsAdmin)
                        {
                            dayAbsences = absenceService.GetAllForDate(date)
                                .Select(a => new DayItem
                                {
                                    Title = $"{a.User.FirstName}: {a.Event}",
                                    Start = a.StartOfTheEvent,
                                    End = a.EndOfTheEvent,
                                    AbsenceType = a.Event.ToString()
                                });
                        }
                        else
                        {
                            dayAbsences = absenceService.GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, date)
                                .Select(a => new DayItem
                                {
                                    Title = $"{a.User.FirstName}: {a.Event}",
                                    Start = a.StartOfTheEvent,
                                    End = a.EndOfTheEvent,
                                    AbsenceType = a.Event.ToString()
                                });

                            dayAppointments = appointmentService.GetAllForUserByDate(Data.Instance.LoggedInUser.Id, date)
                                .Select(a => new DayItem
                                {
                                    Title = a.Title,
                                    Start = date + a.StartOfTheAppointment,
                                    End = date + a.EndOfTheAppointment
                                });
                        }
                    }

                    dayControl.absences(dayAbsences);
                    dayControl.appointments(dayAppointments);
                }
                catch (Exception ex)
                {
                    Log.Warning("Failed to retrieve events for {Date}: {ExceptionMessage}", date, ex.Message);
                }

                Items.Add(dayControl);
            }
        }

        private void UpdateLabelText()
        {
            if (IsWeekView)
            {
                var referenceDate = SelectedDay != null
                ? new DateTime(year, month, int.Parse(SelectedDay.lblnum.Content.ToString()))
                : DateTime.Today;

                int diffToSunday = (int)referenceDate.DayOfWeek; // Sunday = 0
                DateTime sunday = referenceDate.AddDays(-diffToSunday); // Prvi dan nedelje
                DateTime saturday = sunday.AddDays(6);

                LabelText = $"{sunday:dd MMM} - {saturday:dd MMM yyyy}";
            }
            else
            {
                LabelText = new DateTime(year, month, 1).ToString("MMMM yyyy");
            }
        }
    }
}
