using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mtelligent.Dashboard.Web.ViewModels
{
    public class SegmentViewModel
    {
        public ExperimentSegment Segment { get; set; }

        public string ExperimentName { get; set; }
        public List<string> Variables { get; set; }
        public int ExperimentId { get; set; }
    }

}