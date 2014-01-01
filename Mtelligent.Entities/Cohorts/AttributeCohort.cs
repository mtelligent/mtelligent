using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities.Cohorts
{
    public class AttributeCohort : Cohort
    {
        [UserEditable]
        [Required]
        [Display(Name = "Attribute Name")]
        public string AttributeName 
        {
            get
            {
                if (Properties.ContainsKey("AttributeName"))
                {
                    return Properties["AttributeName"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("AttributeName"))
                {
                    Properties["AttributeName"] = value;
                }
                else
                {
                    Properties.Add("AttributeName", value);
                }
            }
        }

        [UserEditable]
        [Required]
        [Display(Name = "Attribute Value")]
        public string AttributeValue
        {
            get
            {
                if (Properties.ContainsKey("AttributeValue"))
                {
                    return Properties["AttributeValue"];
                }
                return string.Empty;
            }
            set
            {
                if (Properties.ContainsKey("AttributeValue"))
                {
                    Properties["AttributeValue"] = value;
                }
                else
                {
                    Properties.Add("AttributeValue", value);
                }
            }
        }

        public override bool IsInCohort(Visitor visitor)
        {
            if (visitor.Attributes.ContainsKey(this.AttributeName))
            {
                if (!string.IsNullOrEmpty(this.AttributeValue))
                {
                    if (visitor.Attributes[this.AttributeName] != this.AttributeValue)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
