using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Hutech.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Hutech.Models
{
    public class NewsViewModels
    {
        public NewsViewModels() { }

        public NewsViewModels(int id,string nametitle, string description, string content,string image,DateTime createddate, Cate_News cate, int idcatenews)
        {
            Id = id;
            NameTitle = nametitle;
            Description = description;
            Content = content;
             Image = image;
            DateTime CreatedDate = createddate;
            ID_CateNews = idcatenews;
            this.cate = cate;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string NameTitle { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ID_CateNews { get; set; }
        [ForeignKey("cate")]
        public Cate_News cate { get; set; }

    }
}