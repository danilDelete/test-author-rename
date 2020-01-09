using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MongoRepository;

namespace CsStat.Domain.Entities
{
    public class UsefulInfo : Entity
    {
        public string Caption { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime PublishDate { get; set; }
        public string Tags { get; set; }
    }
}