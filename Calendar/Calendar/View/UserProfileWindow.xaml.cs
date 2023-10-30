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
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        public UserProfileWindow(bool model, User user, bool isAddCommand)
        {
            InitializeComponent();
            DataContext = new UserProfileWindowViewModel(this, model, user, isAddCommand);
        }
    }
}
