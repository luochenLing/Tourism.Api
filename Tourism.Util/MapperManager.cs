using AutoMapper;

namespace Tourism.Util
{
    public static class MapperManager
    {

        /// <summary>
        /// 简单映射
        /// </summary>
        /// <typeparam name="TDestination">目标</typeparam>
        /// <typeparam name="TSource">源/typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static TDestination SetMapper<TDestination, TSource>(TSource query) where TDestination : class where TSource : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            TDestination info = mapper.Map<TSource, TDestination>(query);
            return info;
        }


    }
}
