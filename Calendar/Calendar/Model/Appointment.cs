using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Appointment : ICloneable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartOfTheAppointment { get; set; }
        public TimeSpan EndOfTheAppointment { get; set; }
        public bool IsDeleted { get; set; }
        private User user;

        public int? UserId { get; set; }
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                UserId = user?.Id;
            }
        }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
