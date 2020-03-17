using E_Hutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace E_Hutech.Areas.Admin.Controllers
{
    public class BannerAController : Controller
    {
        private EVENTEntities db = new EVENTEntities();

        public ActionResult Index()
        {
            IEnumerable<BannerViewModels> banner = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                var responseTask = client.GetAsync("banner");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BannerViewModels>>();
                    readTask.Wait();

                    banner = readTask.Result;
                }
                else
                {
                    banner = Enumerable.Empty<BannerViewModels>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(banner);
        }
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(BannerViewModels banner)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                var postTask = client.PostAsJsonAsync<BannerViewModels>("banner", banner);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(banner);
        }
        public ActionResult Edit(int id)
        {
            BannerViewModels banner = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");
                //HTTP GET
                var responseTask = client.GetAsync("banner?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BannerViewModels>();
                    readTask.Wait();

                    banner = readTask.Result;
                }
            }

            return View(banner);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60976/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("banner/" + id.ToString());
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