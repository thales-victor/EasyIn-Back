using System.Linq;

namespace EasyIn.Repositories.Extensions
{
    public static class AuthenticationRepositoryEx
    {
        public static IQueryable<Authentication> WithUserId (this IQueryable<Authentication> query, int id)
        {
            return query.Where(q => q.User.Id == id);
        }
    }

}
