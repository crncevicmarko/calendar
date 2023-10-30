using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class RequestsWindowViewModel: ViewModelBase
    {
        public ObservableCollection<Absence> Requests { get; set; }

        private IAbsenceService absenceService = new AbsenceService();
        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        private Absence selectedAbsence;
        private Visibility approveButtonVisibility = Visibility.Visible;
        private Visibility declineButtonVisibility = Visibility.Visible;

        public RequestsWindowViewModel(Window window)
        {
            RefreshPage();
            SetVisibility();
            ApproveCommand = new RelayCommand(ApproveCommandExecute, CanApproveCommandExecute);
            DeclineCommand = new RelayCommand(DeclineCommandExecute, CanDeclineCommandExecute);
        }
        public void SetVisibility()
        {
            approveButtonVisibility = Data.Instance.LoggedInUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            declineButtonVisibility = Data.Instance.LoggedInUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility ApproveButtonVisibility
        {
            get { return approveButtonVisibility; }
            set
            {
                approveButtonVisibility = value;
                OnPropertyChanged(nameof(ApproveButtonVisibility));
            }
        }

        public Visibility DeclineButtonVisibility
        {
            get { return declineButtonVisibility; }
            set
            {
                declineButtonVisibility = value;
                OnPropertyChanged(nameof(DeclineButtonVisibility));
            }
        }

        public void RefreshPage()
        {
            if(Data.Instance.LoggedInUser != null)
            {
                if (Data.Instance.LoggedInUser.IsAdmin)
                {
                    Requests = new ObservableCollection<Absence>(absenceService.GetAll(true));
                }
                else
                {
                    Requests = new ObservableCollection<Absence>(absenceService.GetAll(false).Where(p => p.UserId == Data.Instance.LoggedInUser.Id));
                }
            }
        }

        public Absence SelectedRequest
        {
            get { return selectedAbsence; }
            set
            {
                if (selectedAbsence != value)
                {
                    selectedAbsence = value;
                    OnPropertyChanged(nameof(SelectedRequest));
                }
            }
        }

        private bool CanDeclineCommandExecute(object obj)
        {
            return true;
        }

        private void DeclineCommandExecute(object obj)
        {
            if (SelectedRequest != null)
            {
                Absence absence = SelectedRequest;
                absence.IsApproved = false;
                absence.IsDeleted = true;
                absenceService.Update(absence.Id, absence);
                //odkomentarisati za logovanje
                //string userString = $"{DateTime.Now} {Data.Instance.LoggedInUser.FirstName} {Data.Instance.LoggedInUser.LastName} declined absence";
                //Log(userString);
                MessageBox.Show("Uspesno ste izmenili zahtev");
                Requests.Remove(SelectedRequest);
            }
            else { MessageBox.Show("Morate da selektujete zahtev"); }
        }

        private bool CanApproveCommandExecute(object obj)
        {
            return true; 
        }

        private void ApproveCommandExecute(object obj)
        {
            if(SelectedRequest != null)
            {
                Absence absence = SelectedRequest;
                absence.IsApproved = true;
                absenceService.Update(absence.Id, absence);
                MessageBox.Show("Uspesno ste izmenili zahtev");
                //odkomentarisati za logovanje
                //string userString = $"{DateTime.Now} {Data.Instance.LoggedInUser.FirstName} {Data.Instance.LoggedInUser.LastName} approved absence";
                //Log(userString);
                Requests.Remove(SelectedRequest);
            }
            else { MessageBox.Show("Morate da selektujete zahtev"); }
        }

    }
}
