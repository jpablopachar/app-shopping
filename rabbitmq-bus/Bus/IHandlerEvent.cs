using rabbitmq_bus.Events;

namespace rabbitmq_bus.Bus
{
    public interface IHandlerEvent<in TEvent> : IHandlerEvent where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IHandlerEvent { }
}