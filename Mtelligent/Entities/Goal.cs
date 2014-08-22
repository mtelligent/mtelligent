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

        [Range(typeof(double), "0", "79228162514264337593543950335", ErrorMessage = "The Value must be numeric.")]
        public double Value { get; set; }

        [Required]
        [Display(Name = "System Name")]
        public string SystemName { get; set; }

        [Display(Name = "Google Analytics Category")]
        public string GACode { get; set; }

        [Display(Name = "Custom JavaScript.")]
        public string CustomJS { get; set; }
        
	}
}

