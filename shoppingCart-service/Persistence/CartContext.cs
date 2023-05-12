using Microsoft.EntityFrameworkCore;
using shoppingCart_service.Models;

namespace shoppingCart_service.Persistence
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions options) : base(options)
        { }

        public DbSet<CartSession> CartSessions { get; set; }
        public DbSet<CartSessionDetail> CartSessionDetail { get; set; }
    }
}