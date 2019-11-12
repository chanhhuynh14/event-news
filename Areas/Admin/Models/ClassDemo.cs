using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Hutech.Areas.Admin.Models
{
    public class ClassDemo
    {
        private string lastName;
        private string firstName;

        public string LastName { get => lastName; set => lastName = value; }

        public string FirstName { get => firstName; set => firstName = value; } 
        
    }
}