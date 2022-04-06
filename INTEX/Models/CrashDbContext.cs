using System;
using Microsoft.EntityFrameworkCore;

namespace INTEX.Models
{
    public class CrashDbContext : DbContext
    {
        public CrashDbContext(DbContextOptions<CrashDbContext> options) : base(options)
        {

        }

        public DbSet<Crash> Utah_Crashes { get; set; }
        public DbSet<Severity> Severity { get; set; }
    }
}
