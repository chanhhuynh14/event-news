using E_Hutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Hutech.Areas.Admin.Models
{
    public class ThongKeEvents
    {
        public int id { set; get; }
        public string name { set; get; }
        public int? id_Cate { get; set; }
        public Category_Event Category { get; set; }


    }
}