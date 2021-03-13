using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Extensions;
using EasyIn.Repositories.Interfaces;
using EasyIn.Repositories.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EasyIn.Repositories
{
    public class PlatformRepository : RepositoryBase<Platform>, IPlatformRepository
    {
        public PlatformRepository(MyContext context) : base(context) { }

        public Task<Platform> GetByName(string name)
        {
            return Queryable()
                .WithName(name)
                .FirstOrDefaultAsync();
        }
    }

}
