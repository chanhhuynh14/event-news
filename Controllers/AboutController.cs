using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Models;
namespace E_Hutech.Controllers
{
    public class AboutController : Controller
    {

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
    }
}