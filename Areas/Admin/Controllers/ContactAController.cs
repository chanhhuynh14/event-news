using E_Hutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
namespace E_Hutech.Areas.Admin.Controllers
{
    public class ContactAController : Controller
    {

        private EVENTEntities1 db = new EVENTEntities1();

        public ActionResult Index()

        {
            IEnumerable<ContactViewModels> contacts = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("contact");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ContactViewModels>>();
                    readTask.Wait();

                    contacts = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    contacts = Enumerable.Empty<ContactViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(contacts);
        }
    }
}