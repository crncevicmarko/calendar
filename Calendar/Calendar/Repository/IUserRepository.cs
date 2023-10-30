using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Repository
{
    interface IUserRepository
    {
        ObservableCollection<User> GetAll();
        User GetOneById(int? id);
        User GetUserByUserNameAndPassword(string userName, string password);
        int Add(User user);
        void Update(int id, User user);
        void Delete(int id);

    }
}
