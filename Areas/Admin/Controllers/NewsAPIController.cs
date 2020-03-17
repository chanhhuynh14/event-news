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
using E_Hutech.Models;


namespace E_Hutech.Areas.Admin.Controllers
{
    public class NewsAPIController : ApiController
    {
        private EVENTEntities db = new EVENTEntities();

        public IHttpActionResult GetAllNews ()
        {
            IList<NewsViewModels> news = null;

            news = db.News.Include("News")
                           .Select(s => new NewsViewModels()
                           {
                               Id = s.Id,
                               NameTitle = s.NameTitle,
                               Description = s.Description,
                               Content = s.Content,
                               Image = s.Image,
                               CreatedDate = s.CreatedDate,
                               ID_CateNews = s.ID_CateNews
                           }).ToList<NewsViewModels>();

            if (news.Count == 0)
            {
                return NotFound();
            }

            return Ok(news);
        }
        public IHttpActionResult GettNewNews(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            IList<NewsViewModels> news = null;

            news = db.News.Include("News").Where(s => s.Id == id)
                           .Select(s => new NewsViewModels()
                           {
                               Id = s.Id,
                               NameTitle = s.NameTitle,
                               Description = s.Description,
                               Content = s.Content,
                               Image = s.Image,
                               CreatedDate = s.CreatedDate,
                               ID_CateNews = s.ID_CateNews

                           }).ToList<NewsViewModels>();

            if (news.Count == 0)
            {
                return NotFound();
            }
            return Ok(news);
        }

        [HttpPost]
        public IHttpActionResult PostNewNews(NewsViewModels s)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new EVENTEntities())
            {
                ctx.News.Add(new News()
                {
                    Id = s.Id,
                    NameTitle = s.NameTitle,
                    Description = s.Description,
                    Content = s.Content,
                    Image = s.Image,
                    CreatedDate = s.CreatedDate,
                    ID_CateNews = s.ID_CateNews

                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult DeleteNews(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new EVENTEntities())
            {
                var news = ctx.News
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                ctx.Entry(news).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult GetDetail(int id)
        {
            NewsViewModels news = null;

            news = db.News.Include("News").Where(s => s.Id == id)
                           .Select(s => new NewsViewModels()
                           {
                               Id = s.Id,
                               NameTitle = s.NameTitle,
                               Description = s.Description,
                               Content = s.Content,
                               Image = s.Image,
                               CreatedDate = s.CreatedDate,
                               ID_CateNews = s.ID_CateNews
                           }).SingleOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
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

        /// <summary>
        /// Save image to Folder's Avatar
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>@Created by tungnt.net - 6/2015</returns>
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
    }
}
