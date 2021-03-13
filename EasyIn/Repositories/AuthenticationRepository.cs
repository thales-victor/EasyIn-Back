﻿using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Extensions;
using EasyIn.Repositories.Interfaces;
using EasyIn.Repositories.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyIn.Repositories
{
    public class AuthenticationRepository : RepositoryBase<Authentication>, IAuthenticationRepository
    {
        public AuthenticationRepository(MyContext context) : base(context) { }

        public async Task<List<Authentication>> GetByUserId(int userId)
        {
            return await Queryable()
                .WithUserId(userId)
                .ToListAsync();
        }
    }
}
