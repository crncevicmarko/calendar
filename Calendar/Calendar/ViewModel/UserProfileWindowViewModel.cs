using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class UserProfileWindowViewModel: ViewModelBase
    {
        public ICommand ChangeCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private Visibility userProfileVisibility = Visibility.Visible;
        private Visibility registrationVisibility = Visibility.Collapsed;
        Window window;
        private IUserService userService;
        private string firstName;
        private string lastName;
        private string email;
        private string userName;
        private string password;
        private User user1;
        private bool isAddCommand1;


        public UserProfileWindowViewModel(Window window, bool mode, User user, bool isAddCommand)
        {
            if(user != null)
            {
                user1 = user;
            }
            isAddCommand1 = isAddCommand;
            this.window = window;
            userService = new UserService();
            ChangeCommand = new RelayCommand(Change, CanChange);
            RegisterCommand = new RelayCommand(Register, CanRegister);
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            SetVisibility(mode);
            LoadUserData(mode);
            Log.Information("UserProfileWindowViewModel initialized.");
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                    Log.Information("First name set to {FirstName}.", firstName);
                }
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                    Log.Information("Last name set to {LastName}.", lastName);
                }
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                    Log.Information("Email set to {Email}.", email);
                }
            }
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged(nameof(UserName));
                    Log.Information("User name set to {UserName}.", userName);
                }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(password));
                    Log.Information("Password set.");
                }
            }
        }

        private void LoadUserData(bool mode)
        {
            if (mode)
            {
                User user = Data.Instance.LoggedInUser;
                if(user1 != null)
                {
                    user = user1;
                }
                if (user != null)
                {
                    FirstName = user.FirstName;
                    LastName = user.LastName;
                    Email = user.Email;
                    UserName = user.UserName;
                    Password = "";
                    Log.Information("User data loaded: {FirstName} {LastName}, {Email}, {UserName}.", FirstName, LastName, Email, UserName);
                }
            }
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void Cancel(object obj)
        {
            Log.Information("Cancel command triggered.");
            this.window.Close();
            if (user1 != null)
            {
                var usersWindow = new UsersWindow();
                usersWindow.ShowDialog();
                Log.Information("Opened UsersWindow after cancel.");
            }
        }

        private bool CanRegister(object obj)
        {
            return true;
        }

        private void Register(object obj)
        {
            User user = new User()
            {
                FirstName = FirstName?.Trim(),
                LastName = LastName?.Trim(),
                Email = Email?.Trim(),
                UserName = UserName?.Trim(),
                Password = HashPassword(Password?.Trim())
                //Password = Password
            };
            if (HasEmptyFields(user))
            {
                Log.Warning("Registration failed: fields are empty.");
                MessageBox.Show("Sva polja moraju biti popunjena!");
            }
            else
            {
                userService.Add(user);
                Log.Information("User registered: {UserName}.", user.UserName);
                this.window.Close();
                if (isAddCommand1)
                {
                    var usersWindow = new UsersWindow();
                    usersWindow.ShowDialog();
                    Log.Information("Opened UsersWindow after registration.");
                }
            }
        }

        private string HashPassword(string password)
        {
            var hasher = new SHA256Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);
            return Convert.ToBase64String(hashed);
        }

        public bool HasEmptyFields(object obj)
        {
            return obj.GetType()
                      .GetProperties(bindingAttr: BindingFlags.Public | BindingFlags.Instance)
                      .Where(p => p.PropertyType == typeof(string))
                      .Select(p => (string)p.GetValue(obj))
                      .Any(value => string.IsNullOrEmpty(value));
        }

        public void SetVisibility(bool bolean)
        {
            userProfileVisibility = bolean ? Visibility.Visible : Visibility.Collapsed;
            registrationVisibility = bolean ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility UserProfileVisibility
        {
            get { return userProfileVisibility; }
            set
            {
                userProfileVisibility = value;
                OnPropertyChanged(nameof(UserProfileVisibility));
            }
        }


        public Visibility RegistrationVisibility
        {
            get { return registrationVisibility; }
            set
            {
                registrationVisibility = value;
                OnPropertyChanged(nameof(RegistrationVisibility));
            }
        }


        private bool CanChange(object obj)
        {
            return true;
        }

        private void Change(object obj)
        {
            string newEmail = Email?.Trim();
            if (!string.IsNullOrEmpty(newEmail) && !IsValidEmail(newEmail))
            {
                MessageBox.Show("Unesite ispravnu email adresu.");
                return;
            }
            User user = new User()
            {
                Id = Data.Instance.LoggedInUser.Id,
                FirstName = FirstName?.Trim(),
                LastName = LastName?.Trim(),
                Email = newEmail,
                UserName = UserName?.Trim(),
                Password = string.IsNullOrWhiteSpace(Password) ? Data.Instance.LoggedInUser.Password : HashPassword(Password) // Keep old password if unchanged
            };
            if(user1 != null)
            {
                userService.Update(user1.Id, user);
                Log.Information("Updated user with ID {UserId}.", user1.Id);
            }
            else 
            {
                userService.Update(user.Id, user);
                Log.Information("Updated user with ID {UserId}.", user.Id);
            }
            Data.Instance.LoggedInUser = user;
            MessageBox.Show("Uspesno ste izmenili podatke");
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
