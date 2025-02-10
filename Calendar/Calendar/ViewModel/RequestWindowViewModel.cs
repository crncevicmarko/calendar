using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class RequestWindowViewModel: ViewModelBase
    {

        private Time selectedTime;
        private Appointment appointment = new Appointment { Date = DateTime.Today };
        private Absence absence = new Absence { StartOfTheEvent = DateTime.Today, EndOfTheEvent = DateTime.Today };

        public ObservableCollection<int> Hours { get; }
        public ObservableCollection<int> Minutes { get; }

        private Window window1;

        public ICommand SendAbsenceCommand { get; set; }
        public ICommand SendAppointmentCommand { get; set; }

        private IAbsenceService absenceService = new AbsenceService();
        private IAppointmentService appointmentService = new AppointmentService();

        public RequestWindowViewModel(Window window)
        {
            Hours = new ObservableCollection<int>(Enumerable.Range(0, 24));
            Minutes = new ObservableCollection<int>(Enumerable.Range(0, 60));

            SelectedTime = new Time { StartHours = 12, StartMinutes = 0, EndHours = 12, EndMinutes = 0 };
            SendAbsenceCommand = new RelayCommand(SendAbsenceCommandExecute, CanSendAbsenceCommandExecute);
            SendAppointmentCommand = new RelayCommand(SendAppointmentCommandExecute, CanSendAppointmentCommandExecute);
            window1 = window;
        }

        private bool CanSendAppointmentCommandExecute(object obj)
        {
            return true;
        }

        private void SendAppointmentCommandExecute(object obj)
        {
            // TODO validacija za ulogovanog korisnika
            if(SelectedTime != null) 
            {
                TimeSpan startOfTheAppointment = new TimeSpan(SelectedTime.StartHours, SelectedTime.StartMinutes, 0);
                TimeSpan endOfTheAppointment = new TimeSpan(SelectedTime.EndHours, SelectedTime.EndMinutes, 0);
                Appointment appointment1 = new Appointment
                {
                    Title = Appointment.Title,
                    Date = Appointment.Date,
                    StartOfTheAppointment = startOfTheAppointment,
                    EndOfTheAppointment = endOfTheAppointment,
                    User = Data.Instance.LoggedInUser
                };
                appointmentService.Add(appointment1);
                MessageBox.Show("Uspesno ste dodali sastanak");
                //odkomentarisati za logovanje
                //string userString = $"{DateTime.Now} {Data.Instance.LoggedInUser.FirstName} {Data.Instance.LoggedInUser.LastName} sent request for appointment";
                //Log(userString);
                Log.Information("{Timestamp} {UserName} sent request for appointment", DateTime.Now, $"{Data.Instance.LoggedInUser.FirstName} {Data.Instance.LoggedInUser.LastName}");
                window1.Close();
            }
        }

        private bool CanSendAbsenceCommandExecute(object obj)
        {
            return true;
        }

        private void SendAbsenceCommandExecute(object obj)
        {
            if (SelectedTime != null)
            {
                Absence absence = new Absence
                {
                    Event = Absence.Event,
                    StartOfTheEvent = Absence.StartOfTheEvent,
                    EndOfTheEvent = Absence.EndOfTheEvent,
                    IsApproved = false,
                    IsDeleted = false,
                    User = Data.Instance.LoggedInUser
                };
                absenceService.Add(absence);
                //odkomentarisati za logovanje
                //string userString = $"{DateTime.Now} {Data.Instance.LoggedInUser.FirstName} {Data.Instance.LoggedInUser.LastName} created absence";
                //Log(userString);
                Log.Information("{Timestamp} {UserName} created absence", DateTime.Now, $"{Data.Instance.LoggedInUser.FirstName} {Data.Instance.LoggedInUser.LastName}");
                window1.Close();
            }
        }
        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                if (appointment != value)
                {
                    appointment = value;
                    OnPropertyChanged(nameof(Appointment));
                }
            }
        }

        public Absence Absence
        {
            get { return absence; }
            set
            {
                if (absence != value)
                {
                    absence = value;
                    OnPropertyChanged(nameof(Absence));
                }
            }
        }
        public Time SelectedTime
        {
            get { return selectedTime; }
            set
            {
                if (selectedTime != value)
                {
                    selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }
    }
}
