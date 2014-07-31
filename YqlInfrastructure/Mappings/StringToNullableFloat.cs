using System.Globalization;
using System;
using AutoMapper;

namespace YQLDataProvider.Mappings
{
    internal class StringToNullableFloat : ITypeConverter<string,float?>
    {
        private readonly NumberFormatInfo _format;

        public StringToNullableFloat()
        {
            _format = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo ?? new NumberFormatInfo();
            _format.NumberDecimalSeparator = ".";
        }

        public float? Convert(ResolutionContext context)
        {
            return string.IsNullOrEmpty((string) context.SourceValue)
                       ? null
                       : new float?(float.Parse((string)context.SourceValue,_format));
        }
    }
}
