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
        public EventViewModel(int id, string name, string desciption, string content, string image1, string Icon, string keyword, string seoTitle, int id_Cate, Category_Event category, double sL_Thamgia, DateTime date,string dksk,string diadiem)
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
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string Content { get; set; }
        public string Image1 { get; set; }
        public string icon { get; set; }
        public string DiaDiem { get; set; }
        public string DKSK { get; set; }
        public string Keyword { get; set; }
        public string SeoTitle { get; set; }
        [ForeignKey("category")]
        public int? Id_Cate { get; set; }
        public Category_Event category { get; set; }
        public double? SL_Thamgia { get; set; }
        public DateTime? Date { get; set; }
        public string UserAvatarBase64String { get; internal set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public List<Event> Events { get; set; }

    }
}