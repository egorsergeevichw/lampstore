using System;
using LampStore.Domain.Utils;

namespace LampStore.Domain.Models
{
    public class EnumModel
    {
        public EnumModel(Enum en)
        {
            Description = EnumUtils.GetEnumDescription(en);
            Name = en.ToString();
            Value = (int)Enum.Parse(en.GetType(), en.ToString());
        }

        public string Description { get; set; }
        public string Name { get; set; } 
        public int Value { get; set; }
    }
}
