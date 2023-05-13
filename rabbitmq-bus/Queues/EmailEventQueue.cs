using rabbitmq_bus.Events;

namespace rabbitmq_bus.Queues
{
    public class EmailEventQueue : Event
    {
        public string Addressee { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }

        public EmailEventQueue(string addressee, string? title, string content)
        {
            Addressee = addressee;
            Title = title;
            Content = content;
        }
    }
}