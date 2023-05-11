using book_service.Models;
using book_service.Persistence;
using FluentValidation;
using MediatR;

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

            public Handler(LibraryContext context)
            {
                _context = context;
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

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el libro");
            }
        }
    }
}