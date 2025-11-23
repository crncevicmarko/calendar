using Calendar.Model;
using Calendar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            isUserLogedIn();
        }

        public void isUserLogedIn()
        {
            if (Data.Instance.LoggedInUser.FirstName != null) // ako je korisnik ulogovan
            {
                btnLogin.Visibility = Visibility.Collapsed;
                btnRegister.Visibility = Visibility.Collapsed;
                btnRequests.Visibility = Visibility.Visible;
                btnUsers.Visibility = Visibility.Collapsed;
                btnCalendar.Visibility = Visibility.Visible;
                btnLogOut.Visibility = Visibility.Visible;
                if (Data.Instance.LoggedInUser.IsAdmin)
                {
                    btnRequests.Visibility = Visibility.Visible;
                    btnUsers.Visibility = Visibility.Visible;
                }
            }
            else
            {
                btnLogin.Visibility = Visibility.Visible;
                btnRegister.Visibility = Visibility.Visible;
                btnUsers.Visibility = Visibility.Collapsed;
                btnProfile.Visibility = Visibility.Collapsed;
                btnCalendar.Visibility = Visibility.Collapsed;
                btnRequests.Visibility = Visibility.Collapsed;
                btnLogOut.Visibility = Visibility.Collapsed;

            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(this);
            loginWindow.ShowDialog();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new UserProfileWindow(false, null, false);
            loginWindow.ShowDialog();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new UserProfileWindow(true, null, false);
            profileWindow.ShowDialog();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new UsersWindow();
            usersWindow.ShowDialog();
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            var calendarWindow = new CalendarWindow();
            calendarWindow.ShowDialog();
        }

        private void btnRequests_Click(object sender, RoutedEventArgs e)
        {
            var requestsWindow = new RequestsWindow();
            requestsWindow.ShowDialog();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.LoggedInUser = new User();
            isUserLogedIn();
        }
    }
}