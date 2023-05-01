using author_service.Models;
using author_service.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace author_service.Application
{
    public class Query
    {
        public class AuthorList : IRequest<List<AuthorBook>> { }

        public class Handler : IRequestHandler<AuthorList, List<AuthorBook>>
        {
            public readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<AuthorBook>> Handle(AuthorList request, CancellationToken cancellationToken)
            {
                var authorList = await _context.AuthorBooks.ToListAsync();

                return authorList;
            }
        }
    }
}