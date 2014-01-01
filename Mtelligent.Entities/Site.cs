using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities
{
    public class Site : DBEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<SiteUrl> Urls { get; set; }
    }

    public class SiteUrl : DBEntity
    {
        public int Id { get; set; }

        [Required]
        public int SiteId { get; set; }

        [Required]
        public string Url { get; set; }
    }

}
