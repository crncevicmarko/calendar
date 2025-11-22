using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Serilog;
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
    public class RequestsWindowViewModel : ViewModelBase
    {
        private IAbsenceService absenceService = new AbsenceService();

        public ObservableCollection<Absence> PendingRequests { get; set; }
        public ObservableCollection<Absence> DeclinedRequests { get; set; }

        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }

        private Absence selectedRequest;

        // Visibility properties
        public Visibility AdminGridVisibility => Data.Instance.LoggedInUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        public Visibility UserGridsVisibility => !Data.Instance.LoggedInUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        public Visibility AdminButtonsVisibility => Data.Instance.LoggedInUser.IsAdmin ? Visibility.Visible : Visibility.Collapsed;

        public RequestsWindowViewModel(Window window)
        {
            LoadRequests();

            ApproveCommand = new RelayCommand(ApproveExecute, obj => SelectedRequest != null);
            DeclineCommand = new RelayCommand(DeclineExecute, obj => SelectedRequest != null);
        }

        private void LoadRequests()
        {
            if (Data.Instance.LoggedInUser.IsAdmin)
            {
                PendingRequests = new ObservableCollection<Absence>(
                    absenceService.GetAll(true).Where(r => !r.IsApproved)
                );
            }
            else
            {
                PendingRequests = new ObservableCollection<Absence>(
                    absenceService.GetAll(false).Where(r => !r.IsDeleted && !r.IsApproved)
                );

                DeclinedRequests = new ObservableCollection<Absence>(
                    absenceService.GetAll(false).Where(r => r.IsDeleted)
                );
            }
        }

        public Absence SelectedRequest
        {
            get => selectedRequest;
            set
            {
                selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
                // Refresh buttons state
                ((RelayCommand)ApproveCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeclineCommand).RaiseCanExecuteChanged();
            }
        }

        private void ApproveExecute(object obj)
        {
            if (SelectedRequest == null) return;

            SelectedRequest.IsApproved = true;
            absenceService.Update(SelectedRequest.Id, SelectedRequest);
            MessageBox.Show("Successfully approved");
            PendingRequests.Remove(SelectedRequest);
        }

        private void DeclineExecute(object obj)
        {
            if (SelectedRequest == null) return;

            SelectedRequest.IsApproved = false;
            SelectedRequest.IsDeleted = true;
            absenceService.Update(SelectedRequest.Id, SelectedRequest);
            MessageBox.Show("Successfully approved");
            PendingRequests.Remove(SelectedRequest);
        }
    }


}
