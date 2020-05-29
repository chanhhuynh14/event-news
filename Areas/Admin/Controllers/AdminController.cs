using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using E_Hutech.Areas.Admin.Models;
using E_Hutech.Models;
using System.Data.Entity;
namespace E_Hutech.Areas.Admin.Controllers
{
    public class AdminController : ApiController
    {
        private EVENTEntities db = new EVENTEntities();
        public IHttpActionResult GetAllEvent(string keyword="")
        {
            keyword = keyword == null ? "" : keyword;   
            IList<EventViewModel> events = null;
            events = db.Events.Include("Event").Where(s => (s.Name.Contains(keyword) || s.Desciption.Contains(keyword)))
                           .Select(s => new EventViewModel()
                           {
                               Id = s.Id,
                               Name = s.Name,
                               Desciption = s.Desciption,
                               Content = s.Content,
                               Image1 = s.Image1,
                               icon = s.icon,
                               DKSK = s.DKSK,
                               DiaDiem = s.DiaDiem,
                               Keyword = s.Keyword,
                               SeoTitle = s.SeoTitle,
                               Id_Cate = s.Id_Cate,
                               SL_Thamgia = s.SL_Thamgia,
                               Date = s.Date,
                               Date_End = s.Date_End,
                           }).ToList<EventViewModel>();
            if (events.Count == 0)
            {
                return NotFound();
            }
            return Ok(events);
        }
        public IHttpActionResult GettNewEvent(int id,int page, int pageSize = 1)
        {
            
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            IList<EventViewModel> events = null;
            var model = events.Skip((page - 1) * pageSize).Take(pageSize);
            int totalRow = events.Count;
            events = db.Events.Include("Event").Where(s => s.Id == id)
                           .Select(s => new EventViewModel()
                           {
                               Id = s.Id,
                               Name = s.Name,
                               Desciption = s.Desciption,
                               Content = s.Content,
                               Image1 = s.Image1,
                               icon = s.icon,
                               DKSK = s.DKSK,
                               DiaDiem = s.DiaDiem,
                               Keyword = s.Keyword,
                               SeoTitle = s.SeoTitle,
                               Id_Cate = s.Id_Cate,
                               SL_Thamgia = s.SL_Thamgia,
                               Date = s.Date,

                           }).ToList<EventViewModel>();

            if (events.Count == 0)
            {
                return NotFound();
            }
            return Json(new
            {
                data = model,
                total= totalRow,
                status= true
            
            });
        }
        [HttpPost]
        public IHttpActionResult PostNewEvent(EventViewModel s)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new EVENTEntities())
            {
                ctx.Events.Add(new Event()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Desciption = s.Desciption,
                    Content = s.Content,
                    Image1 = s.Image1,
                    icon = s.icon,
                    DKSK = s.DKSK,
                    DiaDiem = s.DiaDiem,
                    Keyword = s.Keyword,
                    SeoTitle = s.SeoTitle,
                    Id_Cate = s.Id_Cate,
                    SL_Thamgia = s.SL_Thamgia,
                    Date = s.Date,
                });

                ctx.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult DeleteEvent(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new EVENTEntities())
            {
                var events = ctx.Events
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                ctx.Entry(events).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
        [HttpPost]
        public IHttpActionResult UpdateEvent(EventViewModel s)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid data");

                using (var ctx = new EVENTEntities())
                {
                    var chanh = ctx.Events.Where(a => a.Id == s.Id).FirstOrDefault<Event>();

                    if (chanh != null)
                    {

                        chanh.Id = s.Id;
                        chanh.Name = s.Name;
                        chanh.Desciption = s.Desciption;
                        chanh.Content = s.Content;
                        chanh.Image1 = s.Image1;
                        chanh.icon = s.icon;
                        chanh.DKSK = s.DKSK;
                        chanh.DiaDiem = s.DiaDiem;
                        chanh.Keyword = s.Keyword;
                        chanh.SeoTitle = s.SeoTitle;
                        chanh.Id_Cate = s.Id_Cate;
                        chanh.SL_Thamgia = s.SL_Thamgia;
                        chanh.Date = s.Date;

                        ctx.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return Ok();
            }


        }
        public IHttpActionResult GetDetail(int id)
        {
            EventViewModel events = null;

            events = db.Events.Include("Event").Where(s=>s.Id ==id)
                           .Select(s => new EventViewModel()
                           {
                               Id = s.Id,
                               Name = s.Name,
                               Desciption = s.Desciption,
                               Content = s.Content,
                               Image1 = s.Image1,
                               icon = s.icon,
                               DKSK = s.DKSK,
                               DiaDiem = s.DiaDiem,
                               Keyword = s.Keyword,
                               SeoTitle = s.SeoTitle,
                               Id_Cate = s.Id_Cate,
                               SL_Thamgia = s.SL_Thamgia,
                               Date = s.Date,
                               NameAD= s.linkAddress.Name_Address,
                               FullAD= s.linkAddress.Full_Address,
                               Pd= s.linkAddress.Pb_Address
                           }).SingleOrDefault();

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }
        public EventViewModel Get(string image1)
        {
            string imagePath;
            EventViewModel userProfile = new EventViewModel() { Image1 = image1 };
            try
            {
                imagePath = HttpContext.Current.Server.MapPath("~/Avatar/") + image1 + ".jpg";
                if (File.Exists(imagePath))
                {
                    using (Image img = Image.FromFile(imagePath))
                    {
                        if (img != null)
                        {
                            userProfile.UserAvatarBase64String = ImageToBase64(img, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception)
            {
                userProfile.UserAvatarBase64String = null;
            }
            return userProfile;
        }
        private string ImageToBase64(Image image, ImageFormat format)
        {
            string base64String;
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                ms.Position = 0;
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);
            }
            return base64String;
        } 
        public bool Post([FromBody]EventViewModel userProfile)
        {
            bool result = false;
            try
            {
                //For demo purpose I only use jpg file and save file name by userId
                if (!string.IsNullOrEmpty(userProfile.UserAvatarBase64String))
                {
                    using (Image image = Base64ToImage(userProfile.UserAvatarBase64String))
                    {
                        string strFileName = "~/Avatar/" + userProfile.Image1 + ".jpg";
                        image.Save(HttpContext.Current.Server.MapPath(strFileName), ImageFormat.Jpeg);
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        private Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Bitmap tempBmp;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                using (Image image = Image.FromStream(ms, true))
                {
                    //Create another object image for dispose old image handler
                    tempBmp = new Bitmap(image.Width, image.Height);
                    Graphics g = Graphics.FromImage(tempBmp);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            return tempBmp;
        }
        public IHttpActionResult GetCate(int id)
        {
            IList<EventViewModel> events = null;

            events = db.Events.Include("Event").Where(s => s.Id_Cate == id)
                           .Select(s => new EventViewModel()
                           {
                               Id = s.Id,
                               Name = s.Name,
                               Desciption = s.Desciption,
                               Content = s.Content,
                               Image1 = s.Image1,
                               icon = s.icon,
                               DKSK = s.DKSK,
                               DiaDiem = s.DiaDiem,
                               Keyword = s.Keyword,
                               SeoTitle = s.SeoTitle,
                               Id_Cate = s.Id_Cate,
                               SL_Thamgia = s.SL_Thamgia,
                               Date = s.Date
                           }).ToList();

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }
        [HttpGet]
        //public HttpResponseMessage GetPaged(int pageNo = 1, int pageSize = 50)
        //{
        //    // Determine the number of records to skip
        //    int skip = (pageNo - 1) * pageSize;

        //    // Get total number of records
        //    int total = db.Events.Count();

        //    // Select the customers based on paging parameters
        //    var customers = db.Events
        //        .OrderBy(c => c.Id)
        //        .Skip(skip)
        //        .Take(pageSize)
        //        .ToList();

        //    // Determine page count
        //    int pageCount = total > 0
        //        ? (int)Math.Ceiling(total / (double)pageSize)
        //        : 0;

        //    // Create the response
        //    var response = Request.CreateResponse(HttpStatusCode.OK, customers);

        //    // Set headers for paging
        //    response.Headers.Add("X-Paging-PageNo", pageNo.ToString());
        //    response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
        //    response.Headers.Add("X-Paging-PageCount", pageCount.ToString());
        //    response.Headers.Add("X-Paging-TotalRecordCount", total.ToString());

        //    // Return the response
        //    return response;
        //}
        public IHttpActionResult GetPaged(int pageNo = 1)
        {
            int pageSize = 3;
            // Determine the number of records to skip
            int skip = (pageNo - 1) * pageSize;

            // Get total number of records
            int total = db.Events.Count();
            IList<EventViewModel> customers = null;

            // Select the customers based on paging parameters
             customers = db.Events.Include("Event")
                .OrderBy(s => s.Id)
                .Skip(skip)
                .Take(pageSize)
                .Select(s => new EventViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Desciption = s.Desciption,
                    Content = s.Content,
                    Image1 = s.Image1,
                    icon = s.icon,
                    DKSK = s.DKSK,
                    DiaDiem = s.DiaDiem,
                    Keyword = s.Keyword,
                    SeoTitle = s.SeoTitle,
                    Id_Cate = s.Id_Cate,
                    SL_Thamgia = s.SL_Thamgia,
                    Date = s.Date,
                    Date_End = s.Date_End,
                    ThemeColor = s.ThemeColor,
                    IsFullDay = s.IsFullDay,
                }).ToList<EventViewModel>();

            // Return the list of customers
            return Ok(customers);
        }
        public IHttpActionResult GetLastEvent(int pageNo = 1)
        {
            int pageSize = 3;
            // Determine the number of records to skip
            int skip = (pageNo - 1) * pageSize;

            // Get total number of records
            int total = db.Events.Count();
            IList<EventViewModel> customers = null;

            // Select the customers based on paging parameters
            customers = db.Events.Include("Event")
               .OrderByDescending(s => s.Id)
               .Skip(skip)
               .Take(pageSize)
               .Select(s => new EventViewModel()
               {
                   Id = s.Id,
                   Name = s.Name,
                   Desciption = s.Desciption,
                   Content = s.Content,
                   Image1 = s.Image1,
                   icon = s.icon,
                   DKSK = s.DKSK,
                   DiaDiem = s.DiaDiem,
                   Keyword = s.Keyword,
                   SeoTitle = s.SeoTitle,
                   Id_Cate = s.Id_Cate,
                   SL_Thamgia = s.SL_Thamgia,
                   Date = s.Date,
                   Date_End = s.Date_End,
                   ThemeColor = s.ThemeColor,
                   IsFullDay = s.IsFullDay,
               }).ToList<EventViewModel>();

            // Return the list of customers
            return Ok(customers);
        }
    }
}
