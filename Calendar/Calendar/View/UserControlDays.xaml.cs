using Calendar.Model;
using Calendar.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Calendar.View
{
    /// <summary>
    /// Interaction logic for UserControlDays.xaml
    /// </summary>
    public partial class UserControlDays : UserControl
    {
        public UserControlDays()
        {
            InitializeComponent();
        }
        public void days(int number)
        {
            lblnum.Content = number.ToString();
        }

        public void absences(ObservableCollection<Absence> absences)
        {
            foreach(var absence in absences)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = absence.User.FirstName.ToUpper() + ": " + absence.Event.ToString();
                stackPanel.Children.Add(textBlock);
            }
        }

        public void appointments(ObservableCollection<Appointment> appointments)
        {
            foreach (var appointment in appointments)
            {
                TextBlock textBlock = new TextBlock();
                string time = appointment.StartOfTheAppointment.Hours.ToString("D2") + ":" + appointment.StartOfTheAppointment.Minutes.ToString("D2");
                textBlock.Text = appointment.Title.ToUpper() + " - " + time;
                stackPanel.Children.Add(textBlock);
            }
        }

        private void lblnum_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Data.Instance.LoggedInUser.IsAdmin)
            {
                var formWindow = new FormWindow();
                formWindow.ShowDialog();
            }
        }

    }
}
