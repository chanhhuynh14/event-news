using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Models;


namespace E_Hutech.Controllers
{
    public class NewsController : Controller
    {
     
        public ActionResult Index()
        {
            IEnumerable<NewsViewModels> news = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("new");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<NewsViewModels>>();
                    readTask.Wait();

                    news = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    news = Enumerable.Empty<NewsViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(news);
        }
        public ActionResult Detail()
        {
            IEnumerable<NewsViewModels> news = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("new");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<NewsViewModels>>();
                    readTask.Wait();

                    news = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    news = Enumerable.Empty<NewsViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(news);
        }
    }

   
}