using System.Web.Mvc;
using Mtelligent.Entities;

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

		public static bool IsVisitorInCohort(this HtmlHelper helper, string cohortSystemName)
		{
			return ExperimentManager.Current.IsVisitorInCohort(cohortSystemName);
		}

		public static MvcHtmlString RenderConversionScripts(this HtmlHelper helper)
		{
			return new MvcHtmlString(ExperimentManager.Current.RenderConversionScripts());
		}

	}
}

