namespace book_service.Models
{
    public class LibraryMaterial
    {
        public Guid? LibraryMaterialId { get; set; }
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }
}