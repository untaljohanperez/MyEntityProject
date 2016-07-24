using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyEntity.Models
{
    public class MyEntityContext : DbContext
    {
        public MyEntityContext()
            : base(@"Data Source=(local); Initial Catalog=MyEntityDB; Integrated Security=True; MultipleActiveResultSets=True")
        {
            //Database.SetInitializer<MyEntityContext>(new CreateDatabaseIfNotExists<SchoolDBContext>());
            Database.SetInitializer<MyEntityContext>(new DropCreateDatabaseIfModelChanges<MyEntityContext>());
            //Database.SetInitializer<MyEntityContext>(new DropCreateDatabaseAlways<MyEntityContext>());
            //Database.SetInitializer<MyEntityContext>(new SchoolDBInitializer());
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Detail> Details { get; set; }

        public DbSet<Seller> Sellers { get; set; }
    
    }
}