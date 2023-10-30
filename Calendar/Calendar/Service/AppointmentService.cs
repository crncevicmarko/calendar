using Calendar.Model;
using Calendar.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Service
{
    public class AppointmentService: IAppointmentService
    {
        private IAppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new AppointmentRepository();
        }

        public ObservableCollection<Appointment> GetAllForUserByDate(int id, DateTime date)
        {
            return appointmentRepository.GetAllForUserByDate(id, date);
        }
        public int Add(Appointment appointment)
        {
            return appointmentRepository.Add(appointment);
        }
    }
}
