using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Models;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Threading;
using System.Globalization;

namespace E_Hutech.Controllers
{
    public class EventsController : Controller
    {
        private EVENTEntities db = new EVENTEntities();
        public new System.Web.HttpContextBase HttpContext { get; }
       
        // GET: Events
        public ActionResult Index()
        {
            IEnumerable<EventViewModel> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://sukienhutech.com/api");
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
                else 
                {
                    events = Enumerable.Empty<EventViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(events);
        }
        public ActionResult Change(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
            HttpCookie cookie = new HttpCookie("Events");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);
            return View("Index");
        }
        [HttpPost]
        public JsonResult AjaxMethod(int pageIndex)
        {
            EventViewModel model = new EventViewModel();
            EVENTEntities entities = new EVENTEntities();
            model.PageIndex = pageIndex;
            model.PageSize = 10;
            model.RecordCount = entities.Events.Count();
            int startIndex = (pageIndex - 1) * model.PageSize;
            model.Events = (from events in entities.Events
                            select events)
                            .OrderBy(events => events.Id)
                            .Skip(startIndex)
                            .Take(model.PageSize).ToList();
            return Json(model);
        }
        [ValidateInput(false)]
        public ActionResult ShowAllEvent()
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
                else 
                {
                    events = Enumerable.Empty<EventViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(events);
        }
        [ValidateInput(false)]
        public ActionResult Detail()
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
                else
                {
                    events = Enumerable.Empty<EventViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(events);
        }
        public ActionResult NewsPartial()
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
                else  
                {
                    news = Enumerable.Empty<NewsViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(news);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Login login)
        {
            if (ModelState.IsValid)
            {
                using (EVENTEntities db = new EVENTEntities())
                {
                    var obj = db.SinhViens.Where(a => a.UserName.Equals(login.UserName) && a.Password.Equals(login.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["ID"] = obj.ID.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(login);
        }
        List<EventViewModel> listEvent = new List<EventViewModel>();
        [HttpPost]
        public JsonResult LoadData(int page, int pageSize=1)
        {
            var model = listEvent.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = listEvent.Count;
            return Json(new
            {
                data = listEvent,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDepartment()
        {
            EVENTEntities db = new EVENTEntities();
            return Json(db.Category_Event.Select(x => new
            {
                DepartmentID = x.Id_CE,
                DepartmentName = x.Name_CE,
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAddress()
        {
            EVENTEntities db = new EVENTEntities();
            return Json(db.linkAddresses.Select(x => new
            {
                AddressID = x.Id_Address,
                AddressName = x.Name_Address,
                AddressFull = x.Full_Address,
                AddressPd = x.Pb_Address,
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Cate_E()
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
                else
                {
                    events = Enumerable.Empty<EventViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(events);
        }
   
        //    public ActionResult Logout()
        //    {
        //        Session["Events"] = null;
        //        Session["Fullname"] = null;
        //        return RedirectToAction("Login", "Events");
        //    }
    }
}


