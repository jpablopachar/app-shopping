using rabbitmq_bus.Bus;
using rabbitmq_bus.Queues;

namespace author_service.Handlers
{
    public class EmailEventHandler : IHandlerEvent<EmailEventQueue>
    {

        public EmailEventHandler() { }

        public async Task Handle(EmailEventQueue @event)
        {
            await Task.CompletedTask;
        }
    }
}