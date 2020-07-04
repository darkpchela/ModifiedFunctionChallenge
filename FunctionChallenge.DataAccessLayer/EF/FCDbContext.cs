using Microsoft.EntityFrameworkCore;
using FunctionChallenge.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionChallenge.DataAccessLayer.EF
{
    public class FCDbContext:DbContext
    {
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Chart> Charts { get; set; }
        public FCDbContext(DbContextOptions<FCDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
