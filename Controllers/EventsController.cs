using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Models;
using System.Web.Script.Serialization;
using System.Net.Mail;

namespace E_Hutech.Controllers
{
    public class EventsController : Controller
    {
        private EVENTEntities1 db = new EVENTEntities1();
        public new System.Web.HttpContextBase HttpContext { get; }

        // GET: Events
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
                else 
                {
                    events = Enumerable.Empty<EventViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(events);
        }
        [HttpPost]
        public JsonResult AjaxMethod(int pageIndex)
        {
            EventViewModel model = new EventViewModel();
            EVENTEntities1 entities = new EVENTEntities1();
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
                using (EVENTEntities1 db = new EVENTEntities1())
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
            EVENTEntities1 db = new EVENTEntities1();
            return Json(db.Category_Event.Select(x => new
            {
                DepartmentID = x.Id_CE,
                DepartmentName = x.Name_CE,
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


        //public ActionResult DanhMuc()
        //{
        //    var danhmuc = from dm in db.Category_Event select dm;
        //    return PartialView(danhmuc);
        //}
        //public ActionResult SPtheoDanhMuc(int id)
        //{
        //    var sp = from s in db.Events where s.Category_Event.Id_CE == id select s;
        //    return PartialView(sp);
        //}

        //public ActionResult UserDashBoard()
        //{
        //    if (Session["ID"] != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
        //    public ActionResult Login(Models.Login login)
        //    {
        //        var item = db.Admins.Where(s => s.UserName == login.UserName && s.Password == login.Password).FirstOrDefault();
        //        if (item != null)
        //        {
        //            Session["Events"] = item;
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Login");

        //        }
        //    }
        //    public ActionResult Logout()
        //    {
        //        Session["Events"] = null;
        //        Session["Fullname"] = null;
        //        return RedirectToAction("Login", "Events");
        //    }
    }
}


