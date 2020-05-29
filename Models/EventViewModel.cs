using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Hutech.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace E_Hutech.Models
{
    public class EventViewModel
    {

        public EventViewModel()
        {
        }
        public EventViewModel(int id, string name, string desciption, string content, string image1,
            string Icon, string keyword, string seoTitle, int id_Cate, Category_Event category,
            double sL_Thamgia, DateTime date, string dksk, int diadiem, DateTime date_end, bool isfullday,
            string themecolor,string nameCate)
        {
            Id = id;
            Name = name;
            Desciption = desciption;
            Content = content;
            Image1 = image1;
            icon = Icon;
            Keyword = keyword;
            SeoTitle = seoTitle;
            Id_Cate = id_Cate;
            this.category = category;
            SL_Thamgia = sL_Thamgia;
            Date = date;
            DKSK = dksk;
            DiaDiem = diadiem;
            Date_End = date_end;
            ThemeColor = themecolor;
            IsFullDay = isfullday;
            CateName = nameCate;

        }
        public string CateName { get; set; }
        public int Id_Event { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string Content { get; set; }
        public string Image1 { get; set; }
        public string icon { get; set; }
        public int? DiaDiem { get; set; }
        public string DKSK { get; set; }
        public string Keyword { get; set; }
        public string SeoTitle { get; set; }
        [ForeignKey("category")]
        public int? Id_Cate { get; set; }
        public Category_Event category { get; set; }
        public double? SL_Thamgia { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Date_End { get; set; }
        public bool? IsFullDay { get; set; }
        public string ThemeColor { get; set; }
        public string UserAvatarBase64String { get; internal set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public List<Event> Events { get; set; }
        public string Pd { get; internal set; }
        public string FullAD { get; internal set; }
        public string NameAD { get; internal set; }

    }
}