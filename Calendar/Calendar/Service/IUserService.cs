using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Service
{
    interface IUserService
    {
        ObservableCollection<User> GetAll();
        User GetOneById(int? id);
        User GetOneByUserNameAndPassword(string userName, string password);
        void Add(User user);
        void Update(int id, User user);
        void Delete(int id);
    }
}
