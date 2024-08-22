using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MossadApi.Models;
namespace MossadApi.DAL
{
    public class DBContext : DbContext
    {

        public DbSet<Agents> Agents { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<Target> Targets { get; set; }
   

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
            if (Database.EnsureCreated()  )
            {
                Seed();
            }
        }


        private void Seed()
        {
           
        }
    }
}
