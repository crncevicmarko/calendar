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
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }
        public void Add(User user)
        {
            userRepository.Add(user);
        }

        public ObservableCollection<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetOneByUserNameAndPassword(string userName, string password)
        {
            return userRepository.GetUserByUserNameAndPassword(userName, password);
        }

        public void Update(int id, User user)
        {
            userRepository.Update(id, user);
        }
        public void Delete(int id)
        {
            userRepository.Delete(id);
        }
        public User GetOneById(int? id)
        {
            return userRepository.GetOneById(id);
        }

    }
}
