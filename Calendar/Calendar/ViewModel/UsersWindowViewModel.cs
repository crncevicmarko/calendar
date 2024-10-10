using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
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
    public class UsersWindowViewModel: ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; }
        private ObservableCollection<User> allUsers;
        private string searchText;

        private IUserService userService = new UserService();
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private User selectedUser;
        private Window window1;
        public UsersWindowViewModel(Window window)
        {
            window1 = window;
            //Users = new ObservableCollection<User>(userService.GetAll().Where(p => !p.IsDeleted && !p.IsAdmin));
            allUsers = new ObservableCollection<User>(userService.GetAll().Where(p => !p.IsDeleted && !p.IsAdmin));
            Users = new ObservableCollection<User>(allUsers);
            AddCommand = new RelayCommand(AddCommandExecute, CanAddCommandExecute);
            UpdateCommand = new RelayCommand(UpdateCommandExecute, CanUpdateCommandExecute);
            DeleteCommand = new RelayCommand(DeleteCommandExecute, CanDeleteCommandExecute);
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    SearchCommandExecute();
                }
            }
        }
        private void SearchCommandExecute()
        {
            Users = new ObservableCollection<User>(
                allUsers.Where(p => p.FirstName.Contains(SearchText) ||
                p.LastName.Contains(SearchText) ||
                p.Email.Contains(SearchText) ||
                p.UserName.Contains(SearchText))
            );
            OnPropertyChanged(nameof(Users));
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        private bool CanDeleteCommandExecute(object obj)
        {
            return true;
        }

        private void DeleteCommandExecute(object obj)
        {
            if(SelectedUser != null)
            {
                User selectedUser = SelectedUser;
                userService.Delete(selectedUser.Id);
                MessageBox.Show("Uspesno ste obrisali korisnika");
                Users.Remove(SelectedUser);
            }
            else
            {
                MessageBox.Show("Morate da selektujete korisnika");
            }
        }

        private bool CanUpdateCommandExecute(object obj)
        {
            return true;
        }

        private void UpdateCommandExecute(object obj)
        {
            if(SelectedUser != null)
            {
                var userProfile = new UserProfileWindow(true, SelectedUser, false);
                this.window1.Close();
                userProfile.ShowDialog();
            }
            else { MessageBox.Show("Morate da selektujete korisnika"); }
        }

        private bool CanAddCommandExecute(object obj)
        {
            return true;
        }

        private void AddCommandExecute(object obj)
        {
            var userProfile = new UserProfileWindow(false, null, true);
            this.window1.Close();
            userProfile.ShowDialog();
        }
    }
}
