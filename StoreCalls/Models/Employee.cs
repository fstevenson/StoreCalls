using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreCalls.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public ICollection<Call> Calls { get; set; }
    }
}