using System.Linq;

namespace EasyIn.Repositories.Extensions
{
    public static class LoginRepositoryEx 
    {
        public static IQueryable<User> WithUsernameOrEmail (this IQueryable<User> query, string username)
        {
            return query.Where(q => q.Username == username || q.Email == username);
        }
        public static IQueryable<User> WithEmail (this IQueryable<User> query, string email)
        {
            return query.Where(q => q.Email == email);
        }
        public static IQueryable<User> WithPassword (this IQueryable<User> query, string password)
        {
            return query.Where(q => q.Password == password);
        }
    }

}
