using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    static class ReaderCollector
    {
        private static readonly Dictionary<string, Type> readers;

        static ReaderCollector()
        {
            Assembly assembly = typeof(ResourceReaderBase).Assembly;

            var query = from item in assembly.GetTypes()
                        where typeof(ResourceReaderBase).IsAssignableFrom(item) &&
                              (item.Attributes & TypeAttributes.Abstract) != TypeAttributes.Abstract
                        select item;

            readers = new Dictionary<string, Type>(query.Count());

            foreach (var item in query)
            {
                var attrs = item.GetCustomAttributes(typeof(ResourceIDAttribute), true);

                if (attrs.Length == 0)
                    continue;

                ResourceIDAttribute attr = attrs.First() as ResourceIDAttribute;
                readers.Add(attr.ID, item);
            }
        }

        public static ResourceReaderBase CreateReader(string resourceID, PsdReader reader, long length)
        {
            Type readerType = typeof(EmptyResourceReader);
            if (readers.ContainsKey(resourceID) == true)
            {
                readerType = readers[resourceID];
            }
            return TypeDescriptor.CreateInstance(null, readerType, new Type[] { typeof(PsdReader), typeof(long), }, new object[] { reader, length, }) as ResourceReaderBase;
        }

        public static string GetDisplayName(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(ResourceIDAttribute), true);

            ResourceIDAttribute attr = attrs.First() as ResourceIDAttribute;
            return attr.DisplayName;
        }

        public static string GetDisplayName(string resourceID)
        {
            if (readers.ContainsKey(resourceID) == true)
            {
                return GetDisplayName(readers[resourceID]);
            }

            return resourceID;
        }
    }
}
