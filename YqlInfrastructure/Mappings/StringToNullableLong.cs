using System.Globalization;
using AutoMapper;

namespace YQLDataProvider.Mappings
{
    internal class StringToNullableLong : ITypeConverter<string, long?>
    {
        private readonly NumberFormatInfo _format;

        public StringToNullableLong()
        {
            _format = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo ?? new NumberFormatInfo();
            _format.NumberDecimalSeparator = ".";
        }

        public long? Convert(ResolutionContext context)
        {
            return string.IsNullOrEmpty((string)context.SourceValue)
                     ? null
                     : new long?(long.Parse((string)context.SourceValue, _format));
        }
    }
}
