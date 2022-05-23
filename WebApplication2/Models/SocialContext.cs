using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class SocialContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Photos> Photos { get; set; }       
        public DbSet<Merjvacnery> Merjvacneries { get; set; }
        public SocialContext(DbContextOptions x) : base(x)
        {

        }
    }
}
