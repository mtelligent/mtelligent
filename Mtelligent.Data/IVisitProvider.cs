using System;
using Mtelligent.Entities;

namespace Mtelligent.Data
{
	public interface IVisitProvider
	{
		Visit RecordVisit(Visit visit);
		Visitor GetCohorts(Visitor visitor);
		Visitor SaveChanges(Visitor visitor);
	}
}

