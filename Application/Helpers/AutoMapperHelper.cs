using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace FoodApplication.Application
{
    public static class AutoMapperHelper
    {
        public static IMapper Mapper { get; set; }
        public static T Map<T>(this object source)
        {
            return Mapper.Map<T>(source);
        }
        public static IQueryable<T> Project<T>(this IQueryable<T> source)
        {
            return source.ProjectTo<T>(Mapper.ConfigurationProvider);
        }
    }
}
