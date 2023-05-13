using rabbitmq_bus.Commands;
using rabbitmq_bus.Events;

namespace rabbitmq_bus.Bus
{
    public interface IBusEvent
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>() where T : Event where TH : IHandlerEvent<T>;
    }
}