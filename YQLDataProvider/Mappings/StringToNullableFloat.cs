using System.Globalization;
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

        public float? Convert(string source, float? destination, ResolutionContext context)
        {
            return string.IsNullOrEmpty((string)source)
                       ? null
                       : new float?(float.Parse((string)source, _format));
        }
    }
}
