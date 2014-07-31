using System;
using System.Globalization;
using AutoMapper;

namespace YQLDataProvider.Mappings
{
    internal class StringToNullableDateTime : ITypeConverter<string, DateTime?>
    {
        private readonly DateTimeFormatInfo _format;
        
        public StringToNullableDateTime()
        {
            _format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
        }

        public DateTime? Convert(ResolutionContext context)
        {
            return string.IsNullOrEmpty((string) context.SourceValue)
                       ? null
                       : new DateTime?((DateTime.Parse((string) context.SourceValue, _format)));
        }
    }
}
