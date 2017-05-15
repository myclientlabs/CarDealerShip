using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interface
{
   public interface IUserRepo
    {
        int createUser(User user);
        int removeUser(int id);
        bool updateUser(User user);
        User getUser(int id);
        IEnumerable<User> getAllUsers();
    }
}
