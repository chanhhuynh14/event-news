 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Hutech.Models;

namespace E_Hutech.Areas.Admin.Controllers
{
    public class AboutAPIController : ApiController
    {
        private EVENTEntities db = new EVENTEntities();

        public IHttpActionResult GetAllAbout()
        {
            IList<AboutViewModels> abouts = null;

            abouts = db.Abouts.Include("About")
                           .Select(s => new AboutViewModels()
                           {
                               Id = s.Id,
                               TongQuan = s.TongQuan,
                               SuMenh = s.SuMenh,
                               TamNhin = s.TamNhin
                           }).ToList<AboutViewModels>();

            if (abouts.Count == 0)
            {
                return NotFound();
            }

            return Ok(abouts);
        }
        public IHttpActionResult GettNewAbout(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            IList<AboutViewModels> abouts = null;

            abouts = db.Abouts.Include("About").Where(s => s.Id == id)
                           .Select(s => new AboutViewModels()
                           {
                               Id = s.Id,
                               TongQuan = s.TongQuan,
                               SuMenh = s.SuMenh,
                               TamNhin = s.TamNhin

                           }).ToList<AboutViewModels>();

            if (abouts.Count == 0)
            {
                return NotFound();
            }
            return Ok(abouts);
        }
        [HttpPost]
        public IHttpActionResult PostNewAbout(AboutViewModels s)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new EVENTEntities())
            {
                ctx.Abouts.Add(new About()
                {
                    Id = s.Id,
                    TongQuan = s.TongQuan,
                    SuMenh = s.SuMenh,
                    TamNhin = s.TamNhin
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult DeleteAbout(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new EVENTEntities())
            {
                var abouts = ctx.Abouts
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                ctx.Entry(abouts).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
