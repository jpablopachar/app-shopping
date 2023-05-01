using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using author_service.Models;
using Microsoft.EntityFrameworkCore;

namespace author_service.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
    }
}