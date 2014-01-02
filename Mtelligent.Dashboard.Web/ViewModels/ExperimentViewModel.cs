using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mtelpligent.Dashboard.Web.ViewModels
{
    public class ExperimentViewModel
    {
        public Experiment Experiment { get; set; }
        public List<Cohort> Cohorts { get; set; }
    }
}