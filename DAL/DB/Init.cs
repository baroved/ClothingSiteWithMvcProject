using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DB
{
    public class Init : DropCreateDatabaseIfModelChanges<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            User first = new User()
            {
                LastName = "Oved",
                FirstName = "Bar",
                Email = "Baroved6@gmail.com",
                BirthDate = new DateTime(1996, 09, 23),
                Password = "123456",
                ConfirmPassword = "123456",
                UserName = "baroved"
            };
            User second = new User()
            {
                LastName = "harush",
                FirstName = "Dean",
                Email = "DeanHarush@gmail.com",
                BirthDate = new DateTime(1996, 12, 26),
                Password = "123456",
                ConfirmPassword = "123456",
                UserName = "deanharush"
            };
            context.UserList.Add(first);
            context.UserList.Add(second);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
