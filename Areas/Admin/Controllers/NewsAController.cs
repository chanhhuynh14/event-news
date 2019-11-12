using E_Hutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace E_Hutech.Areas.Admin.Controllers
{
    public class NewsAController : Controller
    {
            private EVENTEntities1 db = new EVENTEntities1();

            public ActionResult Index()
            {
                IEnumerable<NewsViewModels> news = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60976/api/");
                    var responseTask = client.GetAsync("new");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<NewsViewModels>>();
                        readTask.Wait();

                    news = readTask.Result;
                    }
                    else 
                    {
                    news = Enumerable.Empty<NewsViewModels>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(news);
            }
        public ActionResult Create()
            {
                return View();
            }
        [ValidateInput(false)]
        [HttpPost]
            public ActionResult Create(NewsViewModels news)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60976/api/");
                    var postTask = client.PostAsJsonAsync<NewsViewModels>("news", news);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(news);
            }
        public ActionResult Edit(int id)
        {
            NewsViewModels news = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("news?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<NewsViewModels>();
                    readTask.Wait();

                    news = readTask.Result;
                }
            }

            return View(news);
        }
        public ActionResult Delete(int id)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60976/api/");

                    //HTTP DELETE
                    var deleteTask = client.DeleteAsync("news/" + id.ToString());
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