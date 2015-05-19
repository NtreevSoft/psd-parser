using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    [AttributeUsage(AttributeTargets.Class)]
    class ResourceIDAttribute : Attribute
    {
        private readonly string resourceID;
        private string displayName;

        public ResourceIDAttribute(string resourceID)
        {
            this.resourceID = resourceID;
        }

        public string ID
        {
            get { return this.resourceID; }
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.displayName) == true)
                    return this.resourceID;
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }
    }
}
