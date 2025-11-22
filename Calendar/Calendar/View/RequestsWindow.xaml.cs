using Calendar.Model;
using Calendar.ViewModel;
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
using System.Windows.Shapes;

namespace Calendar.View
{
    /// <summary>
    /// Interaction logic for RequestsWindow.xaml
    /// </summary>
    public partial class RequestsWindow : Window
    {
        public RequestsWindow()
        {
            InitializeComponent();
            DataContext = new RequestsWindowViewModel(this);
        }

        private void myDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (Data.Instance.LoggedInUser.IsAdmin)
            {
                if (e.PropertyName == "IsApproved" || e.PropertyName == "IsDeleted" || e.PropertyName == "UserId")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (e.PropertyName == "UserId")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                else if(e.PropertyName == "IsDeleted"){
                    e.Column.Header = "IsDeclined";
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
