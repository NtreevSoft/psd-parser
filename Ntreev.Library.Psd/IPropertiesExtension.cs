using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public static class IPropertiesExtension
    {
        public static bool Contains(this IProperties props, string property, params string[] properties)
        {
            return props.Contains(GeneratePropertyName(property, properties));
        }

        public static T ToValue<T>(this IProperties props, string property, params string[] properties)
        {
            return (T)props[GeneratePropertyName(property, properties)];
        }

        public static string ToString(this IProperties props, string property, params string[] properties)
        {
            return props[GeneratePropertyName(property, properties)] as string;
        }

        public static int ToInt32(this IProperties props, string property, params string[] properties)
        {
            return (int)props[GeneratePropertyName(property, properties)];
        }

        public static float ToSingle(this IProperties props, string property, params string[] properties)
        {
            return (float)props[GeneratePropertyName(property, properties)];
        }

        public static double ToDouble(this IProperties props, string property, params string[] properties)
        {
            return (double)props[GeneratePropertyName(property, properties)];
        }

        public static bool ToBoolean(this IProperties props, string property, params string[] properties)
        {
            return (bool)props[GeneratePropertyName(property, properties)];
        }

        private static string GeneratePropertyName(string property, params string[] properties)
        {
            if (properties.Length == 0)
                return property;

            return property + "." + string.Join(".", properties);
        }
    }
}
