using EasyIn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Interfaces
{
    public interface IAuthenticationRepository : IRepository<Authentication>
    {
        Task<List<Authentication>> GetByUserId(int userId);
    }
}
