using System;
using AutoMapper;

namespace Application.Common.Mappings
{
    public class DateTimeOffsetLongConverter : ITypeConverter<DateTimeOffset, long>
    {
        public long Convert(DateTimeOffset source, long destination, ResolutionContext context)
        {
            return source.ToUnixTimeSeconds();
        }
    }
}