using FunctionChallenge.DataAccessLayer.EF;
using FunctionChallenge.DataAccessLayer.Entities;
using FunctionChallenge.DataAccessLayer.Interfaces;
using FunctionChallenge.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionChallenge.DataAccessLayer.Repositories
{
    public class FCEFUnitOfWork : IUnitOfWork
    {
        private readonly FCDbContext dbContext;//????
        private IUserDataRepository _userDatas;
        private IPointRepository _points;
        private IChartRepository _charts;
        public FCEFUnitOfWork(FCDbContext dbContext)//???
        {
            this.dbContext = dbContext;
        }
        public IRepository<UserData> UserDatas
        {
            get
            {
                return _userDatas ??= new UserDataRepository(dbContext);
            }
        } 

        public IRepository<Point> Points
        {
            get
            {
                return _points ??= new PointRepository(dbContext);
            }
        }
        public IRepository<Chart> Charts
        {
            get
            {
                return _charts ??= new ChartRepository(dbContext);
            }
        }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
