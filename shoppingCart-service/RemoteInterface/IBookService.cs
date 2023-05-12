using shoppingCart_service.RemoteModel;

namespace shoppingCart_service.RemoteInterface
{
    public interface IBookService
    {
        Task<(bool result, RemoteBook? book, string? error)> GetBook(Guid bookId);
    }
}