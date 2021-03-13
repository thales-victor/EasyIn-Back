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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyContext context) : base(context) { }

        public async Task<bool> AlreadyExists(string email)
        {
            var user = await Queryable()
                .WithEmail(email)
                .FirstOrDefaultAsync();

            return user != null;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await Queryable()
                .WithUsernameOrEmail(username)
                .FirstOrDefaultAsync();
        }
    }

}
