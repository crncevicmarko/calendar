using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    public class LoginWindowViewModel: ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        LoginWindow loginWindow;
        MainWindow mainWindow;
        private IUserService userService = new UserService();
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }

        public LoginWindowViewModel(Window window, Window mainWindow)
        {
            LoginCommand = new RelayCommand(Login, CanUserLogin);
            loginWindow = (LoginWindow)window;
            this.mainWindow = (MainWindow)mainWindow;
        }

        private bool CanUserLogin(object obj)
        {
            return true;
        }

        private void Login(object obj)
        {
            User user;
            if (LoginUserName != null && LoginPassword != null)
            {
                //user = userService.GetOneByUserNameAndPassword(LoginUserName, HashPassword(LoginPassword));
                user = userService.GetOneByUserNameAndPassword(LoginUserName, LoginPassword);
                if (user != null)
                {
                    Data.Instance.LoggedInUser = user;
                    MessageBox.Show("Uspesno ste se ulogovali");
                    var azazaz = new MainWindow();
                    azazaz.Show();
                    loginWindow.Close();
                    mainWindow.Close();
                    Log.Information("User: ID: {Id}, Firstname: {FirstName}, Lastname: {LastName}", user.Id, user.FirstName, user.LastName);


                }
                else 
                {
                    Log.Warning("User tryed loggin with wrong credentials: Username: {UserName}, Password: {Password}", string.IsNullOrEmpty(LoginUserName) ? "EMPTY" : LoginUserName,
                    string.IsNullOrEmpty(LoginPassword) ? "EMPTY" : LoginPassword);
                    MessageBox.Show("Korisnik ne postoji");
                }
            }
            else 
            {
                Log.Warning("User tried logging in without passing credentials");
                MessageBox.Show("Polja ne smeju da budu prazna");  
            }
        }

        private string HashPassword(string password)
        {
            var hasher = new SHA256Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);
            return Convert.ToBase64String(hashed);
        }
    }
}
