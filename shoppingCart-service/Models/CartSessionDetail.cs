namespace shoppingCart_service.Models
{
    public class CartSessionDetail
    {
        public int CartSessionDetailId { get; set; }
        public DateTime DateCreated { get; set; }
        public string? selectedProduct { get; set; }
        public int CartSessionId { get; set; }
        public CartSession? CartSession { get; set; }
    }
}