using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Hutech.Models
{
    public class BannerViewModels
    {
        public BannerViewModels()
        {

        }
        public BannerViewModels(int id_banner,string img)
        {
            Id_banner = id_banner;
            Img = img;
        }

        public int Id_banner { get; set; }
        public string Img { get; set; }

    }
}