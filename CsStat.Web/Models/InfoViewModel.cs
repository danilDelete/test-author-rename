using System;
using System.Collections.Generic;
using System.Web;

namespace CsStat.Web.Models
{
    public class InfoViewModel
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public DateTime PublishDate { get; set; }
        public List<string> Tags { get; set; }
        public string Id { get; set; }
    }
}