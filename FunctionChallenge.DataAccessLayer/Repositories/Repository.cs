using System;
using System.Collections.Generic;
using System.Text;
using FunctionChallenge.DataAccessLayer.Interfaces;
using FunctionChallenge.DataAccessLayer.Entities;
using FunctionChallenge.DataAccessLayer.EF;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FunctionChallenge.DataAccessLayer.Repositories
{
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected readonly FCDbContext dbContext;
        protected readonly DbSet<TEntity> currentSet;
        public Repository(FCDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.currentSet = dbContext.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await currentSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await currentSet.FindAsync(id);
            currentSet.Remove(entity);
        }


        public IQueryable<TEntity> GetAll()
        {
            return currentSet.AsNoTracking();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await currentSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            currentSet.Update(entity);
        }

        #region Disposable
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposed = true;
            }
        }

        ~Repository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Disposable
    }
}
