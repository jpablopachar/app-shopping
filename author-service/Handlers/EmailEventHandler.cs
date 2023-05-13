using email.SendGrid.Interfaces;
using email.SendGrid.Models;
using rabbitmq_bus.Bus;
using rabbitmq_bus.Queues;

namespace author_service.Handlers
{
    public class EmailEventHandler : IHandlerEvent<EmailEventQueue>
    {
        private readonly ILogger<EmailEventHandler> _logger;
        private readonly ISendGridSend _sendGridSend;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public EmailEventHandler(ILogger<EmailEventHandler> logger, ISendGridSend sendGridSend, IConfiguration configuration)
        {
            _logger = logger;
            _sendGridSend = sendGridSend;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation(@event.Title);

            var data = new SendGridData();

            data.Content = @event.Content;
            data.EmailTo = @event.Addressee;
            data.NameTo = @event.Addressee;
            data.Title = @event.Title;
            data.SendGridApiKey = _configuration["SendGrid:ApiKey"];

            var res = await _sendGridSend.SendEmail(data);

            if (res.result)
            {
                await Task.CompletedTask;

                return;
            }
        }
    }
}