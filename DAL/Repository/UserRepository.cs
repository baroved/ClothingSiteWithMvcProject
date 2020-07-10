using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB;
using DAL.ContrectRepo;
namespace DAL.Repository
{
    public class UserRepository : IRepoUser
    {
        public bool LoginUser(string username, string password)
        {
            User user;
            using (var context = new DBContext())
            {
                user = context.UserList.Where(a => a.UserName == username && a.Password == password).FirstOrDefault();
                if (user != null)
                    return true;
                return false;
            }
        }
        public bool AddUser(User newUser)
        {
            using (var context = new DBContext())
            {
                if (newUser != null && !IsExist(newUser.UserName))
                {
                    context.UserList.Add(newUser);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool CheckAgeOfUser(User newUser)
        {
            if (newUser != null)
            {
                if ((DateTime.Now.Year - newUser.BirthDate.Year) >= 18)
                    return true;
            }
            return false;
        }
        public bool IsExist(string name)
        {
            using (var context = new DBContext())
            {
                User temp = context.UserList.Where(a => a.UserName == name).FirstOrDefault();
                if (temp == null)
                    return false;
                return true;
            }
        }

        public User GetUser(string name)
        {
            using (var context = new DBContext())
            {
                User temp = context.UserList.Where(a => a.UserName == name).FirstOrDefault();
                return temp;
            }
        }

        public bool EditUser(User userForChange)
        {
            using (var context = new DBContext())
            {
                User temp = context.UserList.Where(a => a.ID == userForChange.ID).FirstOrDefault();
                if (temp != null)
                {
                    temp.FirstName = userForChange.FirstName;
                    temp.LastName = userForChange.LastName;
                    temp.BirthDate = userForChange.BirthDate;
                    temp.Password = userForChange.Password;
                    temp.Email = userForChange.Email;
                    temp.ConfirmPassword = userForChange.ConfirmPassword;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
