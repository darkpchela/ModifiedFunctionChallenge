using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionChallenge.DataAccessLayer.EF;
using FunctionChallenge.DataAccessLayer.Entities;
using FunctionChallenge.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FunctionChallenge.DataAccessLayer.Repositories
{
    public class UserDataRepository : Repository<UserData>, IUserDataRepository
    {
        public UserDataRepository(FCDbContext context) : base(context)
        {

        }
    }
}
