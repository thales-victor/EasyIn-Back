using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Extensions;
using EasyIn.Repositories.Interfaces;
using EasyIn.Repositories.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories
{
    public class QrCodeLoginRepository : RepositoryBase<QrCodeLogin>, IQrCodeLoginRepository
    {
        public QrCodeLoginRepository(MyContext context) : base(context) { }

        public async Task<QrCodeLogin> Get(int platformId, string browserToken)
        {
            var query = Queryable()
                        .WithPlatformId(platformId)
                        .WithBrowserToken(browserToken)
                        .NotExpired()
                        .NotRemoved();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<QrCodeLogin>> GetAllByUser(int userId)
        {
            var query = Queryable(true)
                        .WithUserId(userId)
                        .OrderByDescending(q => q.CreatedAt);

            return await query.ToListAsync();
        }
    }
}
