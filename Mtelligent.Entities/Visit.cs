using System;

namespace Mtelligent.Entities
{
	public class Visit
	{
		public Visit ()
		{
			Id = Guid.NewGuid ();
			VisitDate = DateTime.Now;
		}

		public Guid Id { get; set; }

		public Visitor Visitor { get; set; }

		public string Referrer { get; set; }

		public string RequestedUrl { get; set; }

		public DateTime VisitDate { get; set; }

	}
}

