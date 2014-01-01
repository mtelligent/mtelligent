using System;

namespace Mtelligent.Entities
{
	public class Conversion
	{
		public Conversion ()
		{
		}

		public Guid Id { get; set; }

		public DateTime ConversionDate { get; set; }

		public Visitor Visitor { get; set; }

		public Goal Goal { get; set; }
	}
}

