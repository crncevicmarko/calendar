using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class CalendarWindowViewModel: ViewModelBase
    {
        int month, year;
        private ObservableCollection<UserControl> items;
        private string labelText;
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand WeekCommand { get; set; }
        public ICommand DayCommand { get; set; }

        private IAbsenceService absenceSevice = new AbsenceService();
        private IAppointmentService appointmentService = new AppointmentService();


        public CalendarWindowViewModel()
        {
            Items = new ObservableCollection<UserControl>();
            DisplayDays();
            PreviousCommand = new RelayCommand(PreviousCommandExecute, CanPreviousCommandExecute);
            NextCommand = new RelayCommand(NextCommandExecute, CanNextCommandExecute);
            WeekCommand = new RelayCommand(WeekCommandExecute, CanWeekCommandExecute);
            DayCommand = new RelayCommand(DayCommandExecute, CanDayCommandExecute);
        }

        private void DisplayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            LabelText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + " " + year;
            Log.Information("Displaying days for {MonthName} {Year}", LabelText);


            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayoftheweek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));
            //int dayoftheweek = (int)startOfTheMonth.DayOfWeek + 1;

            DisplayEvents(dayoftheweek, days);
        }

        private bool CanNextCommandExecute(object obj)
        {
            return true;
        }

        private void NextCommandExecute(object obj)
        {
            Log.Information("Navigating to next month.");
            Items.Clear();
            month++;

            if (month == 13)
            {
                month = 1;
                year++;
            }
            LabelText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + " " + year;
            Log.Information("Displaying days for {MonthName} {Year}", LabelText);

            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayoftheweek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));
            //int dayoftheweek = (int)startOfTheMonth.DayOfWeek + 1;

            DisplayEvents(dayoftheweek, days);
        }

        private bool CanPreviousCommandExecute(object obj)
        {
            return true;
        }

        private void PreviousCommandExecute(object obj)
        {
            Log.Information("Navigating to previous month.");
            Items.Clear();
            month--;

            if (month == 0)
            {
                month = 12;
                year--;
                Log.Information("Year changed to {Year}.", year);
            }
            LabelText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + " " + year;
            Log.Information("Displaying days for {MonthName} {Year}", LabelText);

            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayoftheweek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));
            //int dayoftheweek = (int)startOfTheMonth.DayOfWeek + 1;

            DisplayEvents(dayoftheweek, days);
        }

        private bool CanDayCommandExecute(object obj)
        {
            return SelectedDay != null;
        }

        private void DayCommandExecute(object obj)
        {
            if (SelectedDay == null) return;
            string yearAndMonth = LabelText;

            var day = SelectedDay.lblnum;

            var dayWindow = new DayWindow();
            dayWindow.Show();
        }

        private bool CanWeekCommandExecute(object obj)
        {
            return true;
        }

        private void WeekCommandExecute(object obj)
        {
            Console.WriteLine("Usli u week command");
        }

        private void DisplayEvents(int dayoftheweek, int days)
        {
            Log.Information("Displaying events for {DayCount} days in month {Month} {Year}", days, month, year);
            for (int i = 0; i < dayoftheweek; i++)
            {
                UserControlBlanck userControlBlanck = new UserControlBlanck();
                Items.Add(userControlBlanck);
            }

            for (int i = 1; i < days + 1; i++)
            {
                DateTime date = new DateTime(year, month, i);
                UserControlDays userControlDays = new UserControlDays();
                userControlDays.days(i);
                try
                {
                    if (Data.Instance.LoggedInUser != null && Data.Instance.LoggedInUser.IsAdmin)
                    {
                        userControlDays.absences(absenceSevice.GetAllForDate(date));
                    }
                    if (Data.Instance.LoggedInUser != null && !Data.Instance.LoggedInUser.IsAdmin)
                    {
                        userControlDays.absences(absenceSevice.GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, date));
                        userControlDays.appointments(appointmentService.GetAllForUserByDate(Data.Instance.LoggedInUser.Id, date));
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning("Failed to retrieve events for {Date}: {ExceptionMessage}", date, ex.Message);
                }
                Items.Add(userControlDays);
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


    }
}
