using EasyIn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Interfaces
{
    public interface ILoginRepository : IRepository<User>
    {
        Task<User> Get(string username, string password);
    }
}
