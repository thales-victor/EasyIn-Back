using EasyIn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Interfaces
{
    public interface IQrCodeLoginRepository : IRepository<QrCodeLogin>
    {
        Task<QrCodeLogin> Get(int platformId, string browserToken);
    }
}
