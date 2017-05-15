using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerBeta.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        List<User> GetUsersByRole(string role);
        User GetUserByUserNameAndPassword(string email, string password);
    }
}
