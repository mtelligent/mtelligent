using System;
using System.ComponentModel.DataAnnotations;

namespace Mtelligent.Entities
{
	public class Goal : DBEntity
	{
		public Goal ()
		{
		}

		public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
		public string Name { get; set; }

        [Required]
        [Display(Name = "System Name")]
        public string SystemName { get; set; }

        [Display(Name = "Google Analytics Code")]
        public string GACode { get; set; }

        [Display(Name = "Custom JavaScript.")]
        public string CustomJS { get; set; }
        
	}
}

