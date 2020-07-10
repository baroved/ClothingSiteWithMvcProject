using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DB
{
   public class DBContext:DbContext
    {
        public DBContext() : base("name=AsosStore")
        {
            Database.SetInitializer(new Init());
        }

        public DbSet<Product> ProductList { get; set; }
        public DbSet<User> UserList { get; set; }
    }
}
