using EasyIn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Interfaces
{
    public interface ICredentialRepository : IRepository<Credential>
    {
        Task<List<Credential>> GetByUserId(int userId);
    }
}
