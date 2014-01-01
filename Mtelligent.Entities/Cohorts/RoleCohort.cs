using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class RoleCohort : Cohort
    {
        [UserEditable]
        [Required]
        public string Role
        {
            get
            {
                if (Properties.ContainsKey("Role"))
                {
                    return Properties["Role"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("Role"))
                {
                    Properties["Role"] = value;
                }
                else
                {
                    Properties.Add("Role", value);
                }
            }
        }


        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.Roles.IndexOf(this.Role) > -1)
            {
                return true;
            }
            return false;
        }
    }
}
