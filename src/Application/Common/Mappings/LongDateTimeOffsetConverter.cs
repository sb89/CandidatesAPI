using System;
using AutoMapper;

namespace Application.Common.Mappings
{
    public class LongDateTimeOffsetConverter : ITypeConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long source, DateTimeOffset destination, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(source);
        }
    }
}