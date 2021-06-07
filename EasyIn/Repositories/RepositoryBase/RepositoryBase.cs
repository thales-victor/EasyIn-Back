using EasyIn.Domain;
using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.RepositoryBase
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MyContext Context;
        protected readonly DbSet<TEntity> Entities;

        public RepositoryBase(MyContext context)
        {
            Context = context;
            Entities = context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var newEntity = Entities.Add(entity);
            await SaveChanges();
            return newEntity.Entity;
        }

        public async Task DeleteById(int id)
        {
            Entities.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            Entities.Update(entity);
            await SaveChanges();
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await Entities
                .AsNoTracking()
                .Where(e => !e.Removed)
                .ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Entities
                .FirstOrDefaultAsync(e => e.Id.Equals(id) && !e.Removed);
        }

        public IQueryable<TEntity> Queryable(bool ignoreRemoved = false)
        {
            var queryable = Entities.AsQueryable();

            if (!ignoreRemoved)
                queryable = queryable.Where(e => !e.Removed);

            return queryable;
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
