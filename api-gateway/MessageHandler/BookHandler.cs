using System.Diagnostics;
using System.Text.Json;
using api_gateway.RemoteInterface;
using api_gateway.RemoteModel;

namespace api_gateway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {
        private readonly ILogger<BookHandler> _logger;
        private readonly IRemoteAuthor _remoteAuthor;

        public BookHandler(ILogger<BookHandler> logger, IRemoteAuthor remoteAuthor)
        {
            _logger = logger;
            _remoteAuthor = remoteAuthor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var time = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<RemoteBookModel>(content, options);
                var authorResponse = await _remoteAuthor.GetAuthor(result?.AuthorBook ?? Guid.Empty);

                if (authorResponse.result) {
                    var authorObj = authorResponse.author;
                }
            }

            _logger.LogInformation("Inicia el  request");
            _logger.LogInformation($"El request se hizo en {time.ElapsedMilliseconds} ms");

            return response;
        }
    }
}