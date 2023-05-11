using book_service.Models;
using Microsoft.EntityFrameworkCore;

namespace book_service.Persistence
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<LibraryMaterial> LibraryMaterial { get; set; }
    }
}