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
    public class BannerAPIController : ApiController
    {
        private EVENTEntities1 db = new EVENTEntities1();

        public IHttpActionResult GetAllBanner()
        {
            IList<BannerViewModels> banners = null;

            banners = db.Banners.Include("Banner")
                           .Select(s => new BannerViewModels()
                           {
                               Id_banner = s.Id_banner,
                               Img = s.Img
                           }).ToList<BannerViewModels>();

            if (banners.Count == 0)
            {
                return NotFound();
            }

            return Ok(banners);
        }
        public IHttpActionResult GettNewBanner(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            IList<BannerViewModels> banners = null;

            banners = db.Banners.Include("Banner").Where(s => s.Id_banner == id)
                           .Select(s => new BannerViewModels()
                           {
                               Id_banner = s.Id_banner,
                               Img = s.Img

                           }).ToList<BannerViewModels>();

            if (banners.Count == 0)
            {
                return NotFound();
            }
            return Ok(banners);
        }
        [HttpPost]
        public IHttpActionResult PostNewBanner(BannerViewModels s)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new EVENTEntities1())
            {
                ctx.Banners.Add(new Banner()
                {
                    Id_banner = s.Id_banner,
                    Img = s.Img
                  
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult DeleteBanner(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new EVENTEntities1())
            {
                var banners = ctx.Banners
                    .Where(s => s.Id_banner == id)
                    .FirstOrDefault();

                ctx.Entry(banners).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
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
