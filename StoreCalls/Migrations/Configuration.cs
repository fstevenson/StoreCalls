namespace StoreCalls.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StoreCalls.Models;
    using StoreCalls.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<CallContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CallContext context)
        {
            var employee = new List<Employee>
            {
                new Employee { Name = "Fernando",   LastName = "Stevenson", }
            };
            employee.ForEach(s => context.Employees.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

        }
    }
}