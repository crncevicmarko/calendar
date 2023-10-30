using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
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

        private IAbsenceService absenceSevice = new AbsenceService();
        private IAppointmentService appointmentService = new AppointmentService();


        public CalendarWindowViewModel()
        {
            Items = new ObservableCollection<UserControl>();
            DisplayDays();
            PreviousCommand = new RelayCommand(PreviousCommandExecute, CanPreviousCommandExecute);
            NextCommand = new RelayCommand(NextCommandExecute, CanNextCommandExecute);
        }

        private void DisplayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            LabelText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + " " + year;


            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayoftheweek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));
            //int dayoftheweek = (int)startOfTheMonth.DayOfWeek + 1;

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
                if (Data.Instance.LoggedInUser != null && Data.Instance.LoggedInUser.IsAdmin)
                {
                    userControlDays.absences(absenceSevice.GetAllForDate(date));
                }
                if (Data.Instance.LoggedInUser != null && !Data.Instance.LoggedInUser.IsAdmin)
                {
                    userControlDays.absences(absenceSevice.GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, date));
                    userControlDays.appointments(appointmentService.GetAllForUserByDate(Data.Instance.LoggedInUser.Id, date));
                }
                Items.Add(userControlDays);
            }
        }

        private bool CanNextCommandExecute(object obj)
        {
            return true;
        }

        private void NextCommandExecute(object obj)
        {
            Items.Clear();
            month++;

            if (month == 13)
            {
                month = 1;
                year++;
            }
            LabelText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + " " + year;


            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayoftheweek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));
            //int dayoftheweek = (int)startOfTheMonth.DayOfWeek + 1;

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
                if (Data.Instance.LoggedInUser != null && Data.Instance.LoggedInUser.IsAdmin)
                {
                    userControlDays.absences(absenceSevice.GetAllForDate(date));
                }
                if (Data.Instance.LoggedInUser != null && !Data.Instance.LoggedInUser.IsAdmin)
                {
                    userControlDays.absences(absenceSevice.GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, date));
                    userControlDays.appointments(appointmentService.GetAllForUserByDate(Data.Instance.LoggedInUser.Id, date));
                }
                Items.Add(userControlDays);
            }
        }

        private bool CanPreviousCommandExecute(object obj)
        {
            return true;
        }

        private void PreviousCommandExecute(object obj)
        {
            Items.Clear();
            month--;

            if (month == 0)
            {
                month = 12;
                year--;
            }
            LabelText = DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + " " + year;


            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);

            int dayoftheweek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d"));
            //int dayoftheweek = (int)startOfTheMonth.DayOfWeek + 1;

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
                if (Data.Instance.LoggedInUser != null && Data.Instance.LoggedInUser.IsAdmin)
                {
                    userControlDays.absences(absenceSevice.GetAllForDate(date));
                }
                if (Data.Instance.LoggedInUser != null && !Data.Instance.LoggedInUser.IsAdmin)
                {
                    userControlDays.absences(absenceSevice.GetAllByUserIdAndDate(Data.Instance.LoggedInUser.Id, date));
                    userControlDays.appointments(appointmentService.GetAllForUserByDate(Data.Instance.LoggedInUser.Id, date));
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

    }
}
