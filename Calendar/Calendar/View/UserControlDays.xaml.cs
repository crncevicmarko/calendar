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

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(UserControlDays), new PropertyMetadata(false));


        //public void absences(ObservableCollection<Absence> absences)
        //{
        //    foreach(var absence in absences)
        //    {
        //        TextBlock textBlock = new TextBlock();
        //        textBlock.Text = absence.User.FirstName.ToUpper() + ": " + absence.Event.ToString();
        //        stackPanel.Children.Add(textBlock);
        //    }
        //}

        //public void appointments(ObservableCollection<Appointment> appointments)
        //{
        //    foreach (var appointment in appointments)
        //    {
        //        TextBlock textBlock = new TextBlock();
        //        string time = appointment.StartOfTheAppointment.Hours.ToString("D2") + ":" + appointment.StartOfTheAppointment.Minutes.ToString("D2");
        //        textBlock.Text = appointment.Title.ToUpper() + " - " + time;
        //        stackPanel.Children.Add(textBlock);
        //    }
        //}

        public void absences(IEnumerable<DayItem> absences)
        {
            stackPanel.Children.Clear();

            foreach (var a in absences)
            {
                Border border = new Border
                {
                    CornerRadius = new CornerRadius(3),
                    Padding = new Thickness(2),
                    Margin = new Thickness(1),
                    Background = GetBrushForAbsence(a.AbsenceType)
                };

                TextBlock text = new TextBlock
                {
                    Text = a.Title,
                    FontSize = 9,
                    TextWrapping = TextWrapping.Wrap
                };

                border.Child = text;
                stackPanel.Children.Add(border);
            }
        }

        public void appointments(IEnumerable<DayItem> appointments)
        {
            foreach (var a in appointments)
            {
                Border border = new Border
                {
                    CornerRadius = new CornerRadius(3),
                    Padding = new Thickness(2),
                    Margin = new Thickness(1),
                    Background = Brushes.LightBlue
                };

                TextBlock text = new TextBlock
                {
                    Text = a.Title,
                    FontSize = 9,
                    TextWrapping = TextWrapping.Wrap
                };

                border.Child = text;
                stackPanel.Children.Add(border);
            }
        }


        private Brush GetBrushForAbsence(string type)
        {
            switch (type)
            {
                case "BOLOVANJE": return Brushes.LightPink;
                case "GODISNJI_ODMOR": return Brushes.LightGreen;
                case "VERSKI_PRAZNIK": return Brushes.LightYellow;
                case "SLOBODAN_DAN": return Brushes.LightBlue;
                case "OSTALO": return Brushes.LightGray;
                default: return Brushes.LightGray;
            }
        }


        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as CalendarWindow;
            var vm = parentWindow.DataContext as CalendarWindowViewModel;

            foreach (var child in FindVisualChildren<UserControlDays>(parentWindow))
            {
                child.IsSelected = false;
            }

            this.IsSelected = true;

            vm.SelectedDay = this;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!Data.Instance.LoggedInUser.IsAdmin)
            {
                var formWindow = new FormWindow();
                formWindow.ShowDialog();
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T t) yield return t;

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}
