using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Configuration
{
    [ConfigurationCollection(typeof(CohortType), AddItemName = "cohort", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class CohortTypes : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CohortType();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((CohortType)element).Name;
        }

        public CohortType this[string name]
        {
            get { return (CohortType)BaseGet(name); }
        }

        public List<CohortType> ToList()
        {
            var cohortTypes = new List<CohortType>();
            foreach (var cohortType in this)
            {
                cohortTypes.Add(cohortType as CohortType);
            }
            return cohortTypes;
        }
    }

    public class CohortType : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("TypeName", IsRequired = true)]
        public string TypeName
        {
            get
            {
                return (string)this["TypeName"];
            }
            set
            {
                this["TypeName"] = value;
            }
        }

        [ConfigurationProperty("AllowNew", IsRequired = false)]
        public bool AllowNew
        {
            get
            {
                return (bool)this["AllowNew"];
            }
            set
            {
                this["AllowNew"] = value;
            }
        }
    }
}
