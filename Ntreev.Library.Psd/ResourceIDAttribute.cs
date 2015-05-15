using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    [AttributeUsage(AttributeTargets.Class)]
    class ResourceIDAttribute : Attribute
    {
        private readonly string layerResourceID;
        private string displayName;

        public ResourceIDAttribute(string layerResourceID)
        {
            this.layerResourceID = layerResourceID;
        }

        public string ID
        {
            get { return this.layerResourceID; }
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.displayName) == true)
                    return this.layerResourceID;
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }
    }
}
