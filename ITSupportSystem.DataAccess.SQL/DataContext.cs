using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{
    public class DataContext : DbContext
    {

        public DataContext()
             : base("DefaultConnetion")
        {

        }
            public DbSet<Users> User { get; set; }
            public DbSet<Role> Role { get; set; }
            public DbSet<UserRole> UserRole { get; set; }
            public DbSet<CommonLookUp> CommonLookUp { get; set; }
    }
}
