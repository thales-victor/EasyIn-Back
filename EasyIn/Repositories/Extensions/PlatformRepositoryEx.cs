using System.Linq;

namespace EasyIn.Repositories.Extensions
{
    public static class PlatformRepositoryEx
    {
        public static IQueryable<Platform> WithName (this IQueryable<Platform> query, string name)
        {
            return query.Where(q => q.Name == name);
        }
    }

}
