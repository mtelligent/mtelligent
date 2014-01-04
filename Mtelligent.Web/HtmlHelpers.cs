using Mtelligent.Entities;
using System;
using System.Web.Mvc;

namespace Mtelligent.Web
{
	public static class HtmlHelpers
	{
        public static ExperimentSegment GetHypothesis(this HtmlHelper helper, string experimentName)
        {
            return ExperimentManager.Current.GetHypothesis(experimentName);
        }

        public static MvcHtmlString GetHypothesisVariable(this HtmlHelper helper, string experimentName, string variableName)
        {
            var exp = ExperimentManager.Current.GetHypothesis(experimentName);
            if (exp != null && exp.Variables.ContainsKey(variableName))
            {
                return new MvcHtmlString(exp.Variables[variableName]);
            }
            return new MvcHtmlString("Not found");
        }

	}
}

