using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Areas.Admin.Models;
using E_Hutech.Models;


namespace E_Hutech.Areas.Admin.Controllers
{
    public class TestLayoutController : Controller
    {
        private EVENTEntities db = new EVENTEntities();

        // GET: Admin/TestLayout
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using (EVENTEntities dc = new EVENTEntities())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult ShowEvent()
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
            if (item != null)
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
        public ActionResult GetDepartment()
        {
            EVENTEntities db = new EVENTEntities();
            return Json(db.Category_Event.Select(x => new
            {
                DepartmentID = x.Id_CE,
                DepartmentName = x.Name_CE
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
        ///Thống kê Events
        [HttpGet]
        public JsonResult JsonThongKeSanPham()
        {
            var products = db.Events.ToList();
            var result = new List<ThongKeEvents>();

            foreach (var item in products)
            {
                result.Add(new ThongKeEvents { id = item.Id, name = item.Name, id_Cate = item.Id_Cate });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult JsonThongKeDanhMuc()
        {
            var categories = db.Category_Event.ToList();
            var result = new List<ThongKeTheoDanhMuc>();
            foreach (var item in categories)
            {
                int SoLuong = db.Events.Where(s => s.Id_Cate == item.Id_CE).Count();
                result.Add(new ThongKeTheoDanhMuc { id = item.Id_CE, name = item.Name_CE, amount = SoLuong });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    
    }
}