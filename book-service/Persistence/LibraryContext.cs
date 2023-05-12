using book_service.Models;
using Microsoft.EntityFrameworkCore;

namespace book_service.Persistence
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() { }

        public LibraryContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<LibraryMaterial> LibraryMaterial { get; set; }
    }
}