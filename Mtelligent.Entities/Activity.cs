using System;

namespace Mtelligent.Entities
{
	public class Activity
	{
		public Activity ()
		{
		}

		public Guid Id { get; set; }

		public Visitor Visitor { get; set; }

		public string Url { get; set; }
	}
}

