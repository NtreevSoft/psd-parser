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

        public static Guid ToGuid(this IProperties props, string property, params string[] properties)
        {
            return new Guid(props.ToString(property, properties));
        }

        public static string ToString(this IProperties props, string property, params string[] properties)
        {
            return ToValue<string>(props, property, properties);
        }

        public static byte ToByte(this IProperties props, string property, params string[] properties)
        {
            return ToValue<byte>(props, property, properties);
        }

        public static int ToInt32(this IProperties props, string property, params string[] properties)
        {
            return ToValue<int>(props, property, properties);
        }

        public static float ToSingle(this IProperties props, string property, params string[] properties)
        {
            return ToValue<float>(props, property, properties);
        }

        public static double ToDouble(this IProperties props, string property, params string[] properties)
        {
            return ToValue<double>(props, property, properties);
        }

        public static bool ToBoolean(this IProperties props, string property, params string[] properties)
        {
            return ToValue<bool>(props, property, properties);
        }

        public static bool TryGetValue<T>(this IProperties props, ref T value, string property, params string[] properties)
        {
            string propertyName = GeneratePropertyName(property, properties);
            if (props.Contains(propertyName) == false)
                return false;
            value = props.ToValue<T>(propertyName);
            return true;
        }

        private static string GeneratePropertyName(string property, params string[] properties)
        {
            if (properties.Length == 0)
                return property;

            return property + "." + string.Join(".", properties);
        }
    }
}
