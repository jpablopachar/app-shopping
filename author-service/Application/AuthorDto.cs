namespace author_service.Application
{
    public class AuthorDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? AuthorBookGuid { get; set; }
    }
}