using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Models;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using E_Hutech.Models.ViewModels;
using System.Data.Entity.Validation;


namespace E_Hutech.Controllers
{
    public class EventsController : Controller
    {
        private EVENTEntities db = new EVENTEntities();
        public new HttpContextBase HttpContext { get; }
       
        // GET: Events
        public ActionResult Index(string email, string username)
        {
            ViewBag.Email = email;
            ViewBag.UserName = username;
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
            using (var client = new HttpClient())
            {
                if (ModelState.IsValid)
                {
                    client.BaseAddress = new Uri("http://sukienhutech.com/api");
                    using (EVENTEntities db = new EVENTEntities())
                    {
                        var obj = db.SinhViens.Where(a => a.UserName.Equals(login.UserName) && a.Password.Equals(login.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            
                            Session["ID"] = obj.ID.ToString();
                            Session["UserName"] = obj.UserName.ToString();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.error = "Login failed";
                        }

                    }
                }
                return View(login);
            }

        }

        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(Models.Login login)
        {
            if (ModelState.IsValid)
            {
                using (EVENTEntities db = new EVENTEntities())
                {
                    var obj = db.SinhViens.Where(a => a.UserName.Equals(login.UserName) && a.Password.Equals(login.Password)).FirstOrDefault();
                   
                    if (obj != null)
                    {
                        //Session["Email"] = obj.Email.ToString();
                        //Session["UserName"] = obj.UserName.ToString();
                        
                        return RedirectToAction("Index",new { email = obj.Email, username = obj.UserName });

                    }
                    else
                    {

                        ViewBag.Msg = "Username does not exist !";
                        return RedirectToAction("LoginUser");

                    }
                }
            }
            return View(login);
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Events");
        }
        List<EventViewModel> listEvent = new List<EventViewModel>();
        [HttpPost]
        public JsonResult LoadData(int page, int pageSize = 1)
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
        //14/05/2020 Phần bi HU
        //[Authorize]
        //[AllowAnonymous]
        //public ActionResult Create_BL(int Id_Event, String Message)
        //{
        //    using (var client = new HttpClient())
        //    {

        //            client.BaseAddress = new Uri("http://sukienhutech.com/");
        //                var User_Idd = User.Identity.GetUserId();
        //                BinhLuanBUS.Them(Id_Event, User_Idd, Message);
        //                return RedirectToAction("Detail", "Events", new { Id = Id_Event });

        //    }


        //}
        //public ActionResult Index_BL(int Id_Event)
        //{
        //    ViewBag.Id_Event = Id_Event;
        //    return View(BinhLuanBUS.DanhSach(Id_Event));
        //}
        //    public ActionResult Logout()
        //    {
        //        Session["Events"] = null;
        //        Session["Fullname"] = null;
        //        return RedirectToAction("Login", "Events");
        //    }

        //Hôm nay 17/05/2020 làm phần bình luận mới
        //lấy bình luận
        [HttpGet]
        public ActionResult GetUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUsers(string username, int? id)
        {
            SinhVien user = db.SinhViens.Where(u => u.UserName.ToLower() == username.ToLower())
                                 .FirstOrDefault();

            if (user != null)
            {
                Session["UserID"] = user.ID;
                return RedirectToAction("Detail", new { id = 107 });
            }

            ViewBag.Msg = "Username does not exist !";
            return View();
        }
        //lấy Event
        [HttpGet]
        [ChildActionOnly]
        //[Authorize]
        public PartialViewResult GetPosts(int? id)
        {
            IQueryable<PostsVM> Posts = db.Events.Where(s => s.Id == id)
                                                 .Select(p => new PostsVM
                                                 {
                                                     PostID = p.Id,
                                                     Message = p.Name,
                                                 }).AsQueryable();
            ViewBag.PostID = id;
            return PartialView(Posts);
        }
        //lấy bình luận
        public PartialViewResult GetComments(int postId)
        {
            IQueryable<CommentsVM> comments = db.Comments.Where(c => c.Event.Id == postId)
                                                       .Select(c => new CommentsVM
                                                       {
                                                           ComID = c.ComID,
                                                           CommentMsg = c.CommentMsg,
                                                           //CommentedDate = c.CommentedDate.
                                                           Users = new UserVM
                                                           {
                                                               UserID = c.SinhVien.ID,
                                                               Username = c.SinhVien.UserName
                                                           }
                                                       }).AsQueryable();

            return PartialView("~/Views/Shared/_MyComments.cshtml", comments);
        }

        //[HttpPost]
        public JsonResult AddComment(string comment, int? postId)
        {
            //bool result = false;
            Comment commentEntity = null;
            //int userId = (int)Session["UserID"];

            var user = db.SinhViens.FirstOrDefault(u => u.ID == 1);
            var post = db.Events.FirstOrDefault(p => p.Id == postId);

            if (comment != null)
            {

                commentEntity = new Comment
                {
                    CommentMsg = comment
                };
                //CommentedDate = comment.CommentedDate,
            };


            if (user != null && post != null)
            {
                string sql = "INSERT INTO Comments (CommentMsg, CommentedDate, PostID, UserID) VALUES ('"+comment+"','2020-05-17 23:24:17.393','"+postId+"','"+1+"');";
                db.Database.ExecuteSqlCommand(sql);
                //post.Comments.Add(commentEntity);
                //user.Comments.Add(commentEntity);

                //db.SaveChanges();
                //result = true;
            }
            return Json(postId, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetSubComments(int ComID)
        {
            IQueryable<SubCommentsVM> subComments = db.SubComments.Where(sc => sc.Comment.ComID == ComID)
                                                              .Select(sc => new SubCommentsVM
                                                              {
                                                                  SubComID = sc.SubComID,
                                                                  CommentMsg = sc.CommentMsg,
                                                                  User = new UserVM
                                                                  {
                                                                      UserID = sc.SinhVien.ID,
                                                                      Username = sc.SinhVien.UserName,
                                                                  }
                                                              }).AsQueryable();

            return PartialView("~/Views/Shared/_MySubComments.cshtml", subComments);
        }

        //[HttpPost]
        public ActionResult AddSubComment(SubCommentsVM subComment, int ComID)
        {
            SubComment subCommentEntity = null;
            int userId = (int)Session["UserID"];

            var user = db.SinhViens.FirstOrDefault(u => u.ID == userId);
            var comment = db.Comments.FirstOrDefault(p => p.ComID == ComID);

            if (subComment != null)
            {

                subCommentEntity = new SubComment
                {
                    CommentMsg = subComment.CommentMsg,
                    //CommentedDate = subComment.CommentedDate,
                };


                if (user != null && comment != null)
                {
                    string sql = "INSERT INTO SubComments (CommentMsg, CommentedDate, ComID, UserID) VALUES ('"+comment+"','2020-05-17 23:24:17.393','"+ComID+"','"+userId+"');";
                    db.Database.ExecuteSqlCommand(sql);
                    //comment.SubComments.Add(subCommentEntity);
                    //user.SubComments.Add(subCommentEntity);

                    //db.SaveChanges();
                    ////result = true;
                }
            }

            return RedirectToAction("GetSubComments", "Events", new { ComID = ComID });

        }

    }


}



