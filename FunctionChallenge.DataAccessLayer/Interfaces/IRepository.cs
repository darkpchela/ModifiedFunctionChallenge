using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionChallenge.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetAsync(int id);
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(int id);
        void Update(TEntity entity);

    }
}
