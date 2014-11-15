using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreCalls.Models
{
    public class Person
    {
        public long PersonId { get; set; }

        public int PhoneNumber { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PostCode { get; set; }

        public virtual ICollection<Call> Calls { get; set; }

        public Person(int phoneNumber, string name, string lastName, string address1, string address2, string postcode)
        {
            this.PhoneNumber = phoneNumber;
            this.Name = name;
            this.LastName = lastName;
            this.Address1 = address1;
            this.Address2 = address2;
            this.PostCode = postcode;
        }
        public Person() { }

    }
}