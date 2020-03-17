using E_Hutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace E_Hutech.Areas.Admin.Controllers
{
    public class AboutAdminController : Controller
    {

        private EVENTEntities db = new EVENTEntities();

        public ActionResult Index()

        {
            IEnumerable<AboutViewModels> abouts = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("about");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AboutViewModels>>();
                    readTask.Wait();

                    abouts = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    abouts = Enumerable.Empty<AboutViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(abouts);
        }
        public ActionResult Index1()

        {
            IEnumerable<AboutViewModels> abouts = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("about");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AboutViewModels>>();
                    readTask.Wait();

                    abouts = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    abouts = Enumerable.Empty<AboutViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(abouts);
        }
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(AboutViewModels abouts)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<AboutViewModels>("abouts", abouts);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(abouts);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("about/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}