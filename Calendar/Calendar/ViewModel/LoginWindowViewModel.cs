using Calendar.Commands;
using Calendar.Model;
using Calendar.Service;
using Calendar.View;
using System;
using System.Collections.Generic;
using System.Linq;
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
                user = userService.GetOneByUserNameAndPassword(LoginUserName, LoginPassword);
                if (user != null)
                {
                    Data.Instance.LoggedInUser = user;
                    MessageBox.Show("Uspesno ste se ulogovali");
                    var azazaz = new MainWindow();
                    azazaz.Show();
                    loginWindow.Close();
                    mainWindow.Close();

                }
                else { MessageBox.Show("Korisnik ne postoji"); }
            }
            else { MessageBox.Show("Polja ne smeju da budu prazna");  }
        }
    }
}
