using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Hutech.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace E_Hutech.Models
{
    public class AboutViewModels
    {
        public AboutViewModels()
        {
           
        }

        public AboutViewModels(int id, string tongquan,string sumenh,string tamnhin)
        {
            Id = id;
            TongQuan = tongquan;
            SuMenh = sumenh;
            TamNhin = tamnhin;

        }
        public int Id { get; set; }
        public string TongQuan { get; set; }
        public string SuMenh { get; set; }
        public string TamNhin { get; set; }

    }
}