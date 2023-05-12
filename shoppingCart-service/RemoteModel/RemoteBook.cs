namespace shoppingCart_service.RemoteModel
{
    public class RemoteBook
    {
        public Guid? LibraryMaterialId { get; set; }
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }
}