using System.Text.Json;
using shoppingCart_service.RemoteInterface;
using shoppingCart_service.RemoteModel;

namespace shoppingCart_service.RemoteService
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BookService> _logger;

        public BookService(IHttpClientFactory httpClientFactory, ILogger<BookService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool result, RemoteBook? book, string? error)> GetBook(Guid bookId)
        {
            try {
                var client = _httpClientFactory.CreateClient("Books");
                var response = await client.GetAsync($"api/libraryMaterial/{bookId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var res = JsonSerializer.Deserialize<RemoteBook>(content, options);

                    return (true, res, null);
                }

                return (false, null, response.ReasonPhrase);
            } catch (Exception ex) {
                _logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}