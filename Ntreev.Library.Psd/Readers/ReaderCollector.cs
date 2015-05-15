using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    static class ReaderCollector
    {
        public static IDictionary<string, Type> Collect(Type type)
        {
            Assembly assembly = type.Assembly;

            var query = from item in assembly.GetTypes()
                        where type.IsAssignableFrom(item) &&
                              (item.Attributes & TypeAttributes.Abstract) != TypeAttributes.Abstract
                        select item;

            Dictionary<string, Type> readers = new Dictionary<string, Type>(query.Count());

            foreach (var item in query)
            {
                var attrs = item.GetCustomAttributes(typeof(ResourceIDAttribute), true);

                if (attrs.Length == 0)
                    continue;

                ResourceIDAttribute attr = attrs.First() as ResourceIDAttribute;
                readers.Add(attr.ID, item);
            }

            return readers;
        }

        public static string GetDisplayName(this Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(ResourceIDAttribute), true);

            ResourceIDAttribute attr = attrs.First() as ResourceIDAttribute;
            return attr.DisplayName;
        }
    }
}
