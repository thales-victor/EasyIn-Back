using EasyIn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<IList<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task DeleteById(int entity);
        Task<int> SaveChanges();
        IQueryable<TEntity> Queryable();
    }
}
