using CarDealerBeta.Interfaces;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealerBeta.Repositories
{
   
    public class UserRepository: IUserRepository
    {
        CarEntities db;
        public UserRepository()
        {
            db = new CarEntities();
        }
        public List<User> GetUsersByRole(string role)
        {
            return db.Users.Where(s => s.Role == role).ToList();
        }
        public User GetUserByUserNameAndPassword(string email, string password)
        {
            return db.Users.Where(s => s.Email==email && s.Password==password).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }
    }
}