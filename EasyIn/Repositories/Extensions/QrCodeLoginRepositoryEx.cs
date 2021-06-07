using System;
using System.Linq;

namespace EasyIn.Repositories.Extensions
{
    public static class QrCodeLoginRepositoryEx
    {
        public static IQueryable<QrCodeLogin> WithPlatformId(this IQueryable<QrCodeLogin> query, int platformId)
        {
            return query.Where(q => q.Platform.Id == platformId);
        }

        public static IQueryable<QrCodeLogin> WithBrowserToken(this IQueryable<QrCodeLogin> query, string browserToken)
        {
            return query.Where(q => q.BrowserToken == browserToken);
        }

        public static IQueryable<QrCodeLogin> NotExpired(this IQueryable<QrCodeLogin> query)
        {
            return query.Where(q => q.ExpirationDate > DateTime.UtcNow);
        }

        public static IQueryable<QrCodeLogin> NotRemoved(this IQueryable<QrCodeLogin> query)
        {
            return query.Where(q => !q.Removed);
        }

        public static IQueryable<QrCodeLogin> WithUserId(this IQueryable<QrCodeLogin> query, int userId)
        {
            return query.Where(q => q.Credential.User.Id == userId);
        }
    }

}
