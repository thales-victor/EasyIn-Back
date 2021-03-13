using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Interfaces;
using EasyIn.Repositories.RepositoryBase;
using EasyIn.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyIn.Repositories
{
    public class LoginRepository : RepositoryBase<User>, ILoginRepository
    {
        public LoginRepository(MyContext context) : base(context) { }

        public async Task<User> Get(string username, string password)
        {
            return await Queryable()
                .WithUsernameOrEmail(username)
                .WithPassword(password)
                .FirstOrDefaultAsync();
        }
    }

}
