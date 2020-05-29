using E_Hutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using E_Hutech.Areas.Admin.Models;

namespace E_Hutech.Areas.Admin.Controllers
{
    public class ShowAdminController : Controller
    {
        private EVENTEntities db = new EVENTEntities();
        public ActionResult Index()
        {
            IEnumerable<EventViewModel> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("event");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EventViewModel>>();
                    readTask.Wait();

                    events = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    events = Enumerable.Empty<EventViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(events);
        }

        public ActionResult Create()
        {
            return View();
        }                
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(EventViewModel events)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EventViewModel>("events", events);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(events);
        }
        public ActionResult Edit(int id)
        {
            EventViewModel eventt = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("event?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EventViewModel>();
                    readTask.Wait();

                    eventt = readTask.Result;
                }
            }

            return View(eventt);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("event/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Login login)
        {
            var item = db.Admins.Where(s => s.UserName == login.UserName && s.Password == login.Password).FirstOrDefault();
            if (item == null)
            {
                Session["ShowAdmin"] = item;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["ShowAdmin"] = null;
            Session["Fullname"] = null;
            return RedirectToAction("Login", "ShowAdmin");
        }
    }
    
}