using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoreCalls.Models
{
    public class Call
    {
        public long CallId { get; set; }

        [ForeignKey ("Category")]
        public long CategoryId { get; set; }

        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }

        public string Program { get; set; }

        public string Comments { get; set; }

        public bool Positive { get; set; }

        public DateTime TimeCall { get; set; }

        public virtual Person Person { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Category Category { get; set; }

        public Call(long CategoryId, long EmployeeId, string program, string comments, bool positive, Person person)
        {
            this.CategoryId = CategoryId;
            this.EmployeeId = EmployeeId;
            this.Program = program;
            this.Comments = comments;
            this.Positive = positive;
            this.Person = person;
            this.TimeCall = DateTime.Now;
        }
        public Call() { }

        public Call(string program, string comments, bool positive)
        {
            this.Program = program;
            this.Comments = comments;
            this.Positive = positive;
        }

    }
}