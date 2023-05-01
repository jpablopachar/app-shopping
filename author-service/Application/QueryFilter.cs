using author_service.Models;
using author_service.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace author_service.Application
{
    public class QueryFilter
    {
        public class UniqueAuthor : IRequest<AuthorBook>
        {
            public string? AuthorGuid { get; set; }
        }

        public class Handler : IRequestHandler<UniqueAuthor, AuthorBook>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<AuthorBook> Handle(UniqueAuthor request, CancellationToken cancellationToken)
            {
                var author = await _context.AuthorBooks.Where(x => x.AuthorBookGuid == request.AuthorGuid).FirstOrDefaultAsync();

                if (author == null)
                {
                    throw new Exception("No se ha encontrado el autor");
                }

                return author;
            }
        }
    }
}