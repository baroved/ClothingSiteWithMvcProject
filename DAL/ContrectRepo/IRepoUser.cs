using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
namespace DAL.ContrectRepo
{
    interface IRepoUser
    {
        bool LoginUser(string username, string password);
        bool AddUser(User newUser);
        bool CheckAgeOfUser(User newUser);
        bool IsExist(string name);
        User GetUser(string name);
        bool EditUser(User userForChange);

    }
}
