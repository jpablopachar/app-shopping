namespace shoppingCart_service.Application
{
    public class CartDto
    {
        public int? CartId { get; set; }
        public DateTime? DateCreatedSession { get; set; }
        public List<CartDetailDto>? ProductsList { get; set; }
    }
}