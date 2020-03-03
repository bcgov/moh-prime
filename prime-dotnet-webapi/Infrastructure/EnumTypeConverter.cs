using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace Prime.Infrastructure
{
    public class EnumTypeConverter<T> : TypeConverter where T : struct
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string strVal = value as String;
            if (string.IsNullOrEmpty(strVal))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>($"\"{value}\"");
            }
            catch
            {
                return null;
            }
        }
    }
}
