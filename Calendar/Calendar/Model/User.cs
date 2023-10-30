using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    [Serializable]
    public class User: ICloneable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }  
        public bool IsDeleted { get; set; }

        public object Clone()
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                UserName = UserName,
                Password = Password,
                IsAdmin = IsAdmin,
                IsDeleted = IsDeleted
            };
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
