using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MossadApi.Models;
namespace MossadApi.DAL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Agents> Agents { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<Target> Targets { get; set; }


    }
}
