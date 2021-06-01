using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Extensions;
using EasyIn.Repositories.Interfaces;
using EasyIn.Repositories.RepositoryBase;
using Microsoft.EntityFrameworkCore;
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
    }
}
