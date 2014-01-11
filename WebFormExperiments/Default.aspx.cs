using Mtelligent.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormExperiments
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblExperimentHeading.Text = ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["Title"];
            lblExperimentHeading.Font.Bold = true;
            lblExperimentHeading.Style.Add("color:", ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["TitleColor"]);
            lblExperimentMessage.Text = ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["Copy"];
            imgExperiment.ImageUrl = ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Variables["Image Source"];
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            ExperimentManager.Current.AddConversion("Honey vs Vinegar");

            if (ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").Name == "Honey")
            {
                lblConversionMessage.Text = "Thanks you so much you wonderful user.";
            }
            else
            {
                lblConversionMessage.Text = "We knew you would do as you were told. Carry on worm.";
            }

            lblConversionMessage.Visible = true;
        }
    }
}