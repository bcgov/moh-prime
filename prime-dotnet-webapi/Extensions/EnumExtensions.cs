using System;
using System.Linq;

namespace Prime.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();

            return enumType
                .GetField(Enum.GetName(enumType, value))
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        // To avoid "The annotation for nullable reference types should only be used in code within a '#nullable' annotations context." warning
#nullable enable
        public static bool IsValid(this Enum? value)
#nullable disable
        {
            if (value != null)
            {
                try
                {
                    var enumType = value.GetType();
                    enumType.GetField(Enum.GetName(enumType, value));
                    return true;
                }
                catch (ArgumentNullException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
