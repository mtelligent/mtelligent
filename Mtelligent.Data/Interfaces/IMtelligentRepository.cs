using System;
using Mtelligent.Entities;

namespace Mtelligent.Data
{
	public interface IMtelligentRepository
	{
        Visitor GetDetails(Visitor visitor);
        Visitor ReconcileUser(Visitor visitor);

        Visitor GetLandingPages(Visitor visitor);
        Visitor GetReferrers(Visitor visitor);

        Visitor GetAttributes(Visitor visitor);
        Visitor GetCohorts(Visitor visitor);
        Visitor GetSegments(Visitor visitor);
        Visitor GetConversions(Visitor visitor);

		Visitor SaveChanges(Visitor visitor);

        Experiment GetExperiment(string experimentName);
        Goal GetGoal(string goalName);
        
	}
}

