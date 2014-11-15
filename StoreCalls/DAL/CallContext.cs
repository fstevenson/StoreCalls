using StoreCalls.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace StoreCalls.DAL
{
    public class CallContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }

        static CallContext()
        {
            Database.SetInitializer<CallContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}