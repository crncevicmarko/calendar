using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Repository
{
    interface IAppointmentRepository
    {
        ObservableCollection<Appointment> GetAllForUserByDate(int id, DateTime date);
        int Add(Appointment appointment);
    }
}
