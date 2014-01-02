using System;
using Mtelligent.Entities;

namespace Mtelligent.Data
{
	public interface IVistorProvider
	{
        Visitor GetDetails(Visitor visitor);
        Visitor GetLandingPages(Visitor visitor);
        Visitor GetReferrers(Visitor visitor);

        Visitor GetCohorts(Visitor visitor);
        Visitor GetSegments(Visitor visitor);
        Visitor GetConversions(Visitor visitor);

		Visitor SaveChanges(Visitor visitor);
	}
}

