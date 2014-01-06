using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities
{
    public abstract class DBEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
