using System;
using AutoMapper;

namespace Application.Common.Mappings
{
    public class IntDateTimeOffsetConverter : ITypeConverter<int, DateTimeOffset>
    {
        public DateTimeOffset Convert(int source, DateTimeOffset destination, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(source);
        }
    }
}