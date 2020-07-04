using FunctionChallenge.DataAccessLayer.Interfaces;
using FunctionChallenge.DataAccessLayer.Entities;
using FunctionChallenge.DataAccessLayer.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FunctionChallenge.DataAccessLayer.Repositories
{
    public class PointRepository : Repository<Point>, IPointRepository
    {
        public PointRepository(FCDbContext dbContext):base(dbContext)
        {

        }
    }
}
