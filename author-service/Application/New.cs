using author_service.Models;
using author_service.Persistence;
using MediatR;

namespace author_service.Application
{
    public class New
    {
        public class Main : IRequest {
            public string? Name { get; set; }
            public string? LastName { get; set; }
            public DateTime BirthDate { get; set; }
        }

        public class Handler : IRequestHandler<Main>
        {
            public readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Main request, CancellationToken cancellationToken)
            {
                var authorBook = new AuthorBook
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    AuthorBookGuid = Convert.ToString(Guid.NewGuid())
                };

                await _context.AuthorBooks.AddAsync(authorBook);

                var value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo crear el autor");
            }
        }
    }
}