using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtelligent.Entities
{
    [AttributeUsage(System.AttributeTargets.Property)]
    public class UserEditableAttribute : Attribute
    {
        public UserEditableAttribute() { }
    }
}
