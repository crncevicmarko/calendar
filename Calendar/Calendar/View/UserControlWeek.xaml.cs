using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.ViewModel;
using System.Windows.Controls;

namespace Calendar.View
{
    public partial class UserControlWeek : UserControl
    {
        public UserControlWeek(UserControlWeeksViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public UserControlWeek()
        {
            InitializeComponent();
        }
    }
}