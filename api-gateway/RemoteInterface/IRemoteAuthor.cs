using api_gateway.RemoteModel;

namespace api_gateway.RemoteInterface
{
    public interface IRemoteAuthor
    {
        Task<(bool result, RemoteAuthorModel? author, string? errorMessage)> GetAuthor(Guid AuthorId);
    }
}