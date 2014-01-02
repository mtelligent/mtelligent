using Mtelligent.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mtelligent.Dashboard.Web.ViewModels
{
    public class CohortViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="System Name")]
        public string SystemName { get; set; }

        public List<CohortType> CohortTypes { get; set; }

        [Display(Name = "Cohort Type")]
        [Required]
        public string SelectedCohortType { get; set; }

        public int Id { get; set; }

        public List<CustomCohortPropertyInfo> Properties { get; set; }
    }

    public class CustomCohortPropertyInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
        public string Value { get; set; }
    }
}