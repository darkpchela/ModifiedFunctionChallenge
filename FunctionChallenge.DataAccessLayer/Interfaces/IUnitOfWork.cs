using FunctionChallenge.DataAccessLayer.Entities;
using FunctionChallenge.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionChallenge.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<UserData> UserDatas { get; }
        IRepository<Point> Points { get; }
        IRepository<Chart> Charts { get; }
        Task SaveAsync();
    }
}
