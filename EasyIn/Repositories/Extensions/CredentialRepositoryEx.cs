using System.Linq;

namespace EasyIn.Repositories.Extensions
{
    public static class CredentialRepositoryEx
    {
        public static IQueryable<Credential> WithUserId (this IQueryable<Credential> query, int id)
        {
            return query.Where(q => q.User.Id == id);
        }
    }

}
