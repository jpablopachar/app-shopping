namespace api_gateway.RemoteModel
{
    public class RemoteBookModel
    {
        public Guid LibraryMaterialId { get; set; }
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid AuthorBook { get; set; }
        public RemoteAuthorModel? AuthorData { get; set; }
    }
}