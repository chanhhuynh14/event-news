using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Hutech.Models
{
    public class ContactViewModels
    {
        public ContactViewModels()
        {

        }

        public ContactViewModels(int id, string fname, string lname,string phonenumber, string email, string message)
        {
            Id = id;
            Fname = fname;
            Email = email;
            Phonenumber = phonenumber;
            Message = message;
        }
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Message { get; set; }


    }
}