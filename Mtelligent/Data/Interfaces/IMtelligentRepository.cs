using System;
using Mtelligent.Entities;

namespace Mtelligent.Data
{
	public interface IMtelligentRepository
	{
        Visitor GetDetails(Visitor visitor);
        Visitor GetVisitor(string userName);

        Visitor ReconcileUser(Visitor visitor);

        Visitor GetLandingPages(Visitor visitor);
        Visitor GetReferrers(Visitor visitor);

        Visitor GetAttributes(Visitor visitor);
        Visitor GetCohorts(Visitor visitor);
        Visitor GetSegments(Visitor visitor);
        Visitor GetConversions(Visitor visitor);

		Visitor SaveChanges(Visitor visitor, bool saveRequest);

        Experiment GetExperiment(string experimentName);
        Goal GetGoal(string goalName);

        Visitor AddVisitor(Visitor visitor);

        void RemoveVisitorAttribute(Visitor visitor, string name);

        Cohort GetCohort(string systemName);
        
	}
}

