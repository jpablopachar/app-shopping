namespace shoppingCart_service.Models
{
    public class CartSession
    {
        public int CartSessionId { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<CartSessionDetail>? CartSessionDetails { get; set; }
    }
}