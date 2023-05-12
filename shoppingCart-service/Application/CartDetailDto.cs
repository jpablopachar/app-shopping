namespace shoppingCart_service.Application
{
    public class CartDetailDto
    {
        public Guid? BookId { get; set; }
        public string? titleBook { get; set; }
        public string? authorBook { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}