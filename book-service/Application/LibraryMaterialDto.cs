namespace book_service.Application
{
    public class LibraryMaterialDto
    {
        public Guid LibraryMaterialId { get; set; }
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid AuthorBook { get; set; }
    }
}