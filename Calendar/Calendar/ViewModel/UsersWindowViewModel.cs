using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
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
    public class UsersWindowViewModel: ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; }
        private ObservableCollection<User> allUsers;
        private string searchText;

        private IUserService userService = new UserService();
        public ICommand BlockCommand { get; set; }
        private User selectedUser;
        private Window window1;
        public UsersWindowViewModel(Window window)
        {
            window1 = window;
            //Users = new ObservableCollection<User>(userService.GetAll().Where(p => !p.IsDeleted && !p.IsAdmin));
            allUsers = new ObservableCollection<User>(userService.GetAll().Where(p => !p.IsDeleted && !p.IsAdmin));
            Users = new ObservableCollection<User>(allUsers);
            BlockCommand = new RelayCommand(BlockCommandExecute, CanBlockCommandExecute);

            //Log.Information("UsersWindowViewModel initialized.");
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
            //Log.Information("Executing search with text: {SearchText}", SearchText);
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

        private bool CanBlockCommandExecute(object obj)
        {
            return true;
        }

        private void BlockCommandExecute(object obj)
        {
            if(SelectedUser != null)
            {
                //Log.Information("Deleting user with ID: {UserId}", SelectedUser.Id);
                User selectedUser = SelectedUser;
                userService.Delete(selectedUser.Id);
                MessageBox.Show("Uspesno ste obrisali korisnika");
                Users.Remove(SelectedUser);
            }
            else
            {
                //Log.Warning("Delete attempted without a selected user.");
                MessageBox.Show("Morate da selektujete korisnika");
            }
        }
    }
}
