using System;
using System.Collections.Generic;
using System.ComponentModel;
using LampStore.Domain.Enums;
using LampStore.Domain.Models;

namespace LampStore.Domain.Utils
{
    public class EnumUtils
    {
        public static List<EnumModel> GetEnumModel(Type type)
        {
            var result = new List<EnumModel>();
            var values = Enum.GetValues(typeof(ProductTypeEnum));

            foreach (var value in values)
            {
                var model = new EnumModel((ProductTypeEnum)value);

                result.Add(model);
            }

            return result;
        } 

        public static string GetEnumDescription(Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static IEnumerable<string> GetEnumDescriptions(Type type)
        {
            var descs = new List<string>();
            var names = Enum.GetNames(type);

            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

                foreach (DescriptionAttribute fd in fds)
                {
                    descs.Add(fd.Description);
                }
            }

            return descs;
        }
    }
}
