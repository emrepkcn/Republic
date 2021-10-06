using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Common.Function
{
    public static class EnumFunction
    {
        /// <summary>
        /// seçil
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null) return null;

            var memberinfo = value.GetType().GetMember(value.ToString());
            var attributes = memberinfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }
        //private static T GetAttribute<T,O>(this O value) where T : Attribute
        //{
        //    if (value == null) return null;

        //    var memberinfo = value.GetType().GetMember(value.ToString());
        //    var attributes = memberinfo[0].GetCustomAttributes(typeof(T), false);
        //    return (T)attributes[0];
        //}
        public static string ToName(this Enum value)
        {
            if (value == null) return null;
            var attrib = value.GetAttribute<DescriptionAttribute>();

            return attrib == null ? value.ToString() : attrib.Description;
        }
        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static ICollection GetEnumDescriptionList<T>() where T : Enum
        {

            return typeof(T)
                .GetMembers()
                .SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true)
                .Cast<DescriptionAttribute>())
                .Select(x => x.Description).ToList();
        }
        public static ICollection GetEnumDescriptionList<T>(Func<T, bool> filter) where T : Enum
        {
            var list = Enum.GetValues(typeof(T))
                   .Cast<T>()
                   .Where(filter)
                   .ToList()
                   .Select(x => x.GetType().GetField($"{x}"))
                   .SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true)
                   .Cast<DescriptionAttribute>())
                   .Select(x => x.Description).ToList();

            return list;
        }

        public static T GetEnum<T>(this string description)
        {
            var enumnames = Enum.GetNames(typeof(T));
            foreach (var item in enumnames.Select(x => Enum.Parse(typeof(T), x)).Where(x => description == ToName((Enum)x)))

                return (T)item;

            return default(T);
        }
    }
}
