using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreCalls.Models
{
    public class Registration
    {
        public int PhoneNumber { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [DataType(DataType.Text)]
        public string Program { get; set; }

        public string Categories { get; set; }

        [DataType(DataType.Text)]
        public string Comments { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "How you evalute the comment?")]
        public bool Positive { get; set; }

        public IEnumerable<SelectListItem> ProgramList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Morning", Value = "Morning"},
                    new SelectListItem { Text = "Afternoon", Value = "Afternoon"}
                };
            }
        }
    }
}