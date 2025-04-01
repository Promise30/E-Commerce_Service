using System.ComponentModel;
using System.Reflection;

namespace ECommerceService.API.Helpers
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttirbute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttirbute == null ? enumValue.ToString() : descriptionAttirbute.Description;
        }
    }
}
