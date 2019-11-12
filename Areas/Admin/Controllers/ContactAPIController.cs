using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using E_Hutech.Areas.Admin.Models;
using E_Hutech.Models;


namespace E_Hutech.Areas.Admin.Controllers
{
    public class ContactAPIController : ApiController
    {

        private EVENTEntities1 db = new EVENTEntities1();

        public IHttpActionResult GetAllContact()
        {
            IList<ContactViewModels> contacts = null;

            contacts = db.Contacts.Include("Contact")
                           .Select(s => new ContactViewModels()
                           {
                               Id = s.Id,
                               Fname = s.Fname,
                               Lname = s.Lname,
                               Email = s.Email,
                               Phonenumber = s.Phonenumber,
                               Message = s.Message
                           }).ToList();

            if (contacts.Count == 0)
            {
                return NotFound();
            }

            return Ok(contacts);
        }
        public IHttpActionResult PostNewContact(ContactViewModels s)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new EVENTEntities1())
            {
                ctx.Contacts.Add(new Contact()
                {
                    Id = s.Id,
                    Fname = s.Fname,
                    Lname = s.Lname,
                    Email = s.Email,
                    Phonenumber = s.Phonenumber,
                    Message = s.Message
                });
                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult DeleteContact(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new EVENTEntities1())
            {
                var contacts = ctx.Contacts
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                ctx.Entry(contacts).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}