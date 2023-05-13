using book_service.Models;
using book_service.Persistence;
using FluentValidation;
using MediatR;
using rabbitmq_bus.Bus;
using rabbitmq_bus.Queues;

namespace book_service.Application
{
    public class New
    {
        public class Main : IRequest
        {
            public string? Title { get; set; }
            public DateTime PublicationDate { get; set; }
            public Guid AuthorBook { get; set; }
        }

        public class Validator : AbstractValidator<Main>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
                RuleFor(x => x.AuthorBook).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Main>
        {
            public readonly LibraryContext _context;
            private readonly IBusEvent _busEvent;

            public Handler(LibraryContext context, IBusEvent busEvent)
            {
                _context = context;
                _busEvent = busEvent;
            }

            public async Task<Unit> Handle(Main request, CancellationToken cancellationToken)
            {
                var book = new LibraryMaterial
                {
                    Title = request.Title,
                    PublicationDate = request.PublicationDate,
                    AuthorBook = request.AuthorBook
                };

                await _context.LibraryMaterial.AddAsync(book);

                var value = await _context.SaveChangesAsync();

                _busEvent.Publish(new EmailEventQueue("jpablopachar1993@gmail.com", request.Title, "Este es un contenido de ejemplo"));

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el libro");
            }
        }
    }
}