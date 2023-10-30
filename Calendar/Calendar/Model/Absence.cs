using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    [Serializable]
    public class Absence
    {
        public int Id { get; set; }
        public ETypeOfEvent Event { get; set; }
        public DateTime StartOfTheEvent { get; set; }
        public DateTime EndOfTheEvent { get; set; }
        private User user;
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }

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

    }
}
