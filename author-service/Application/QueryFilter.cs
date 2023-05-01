using author_service.Models;
using author_service.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace author_service.Application
{
    public class QueryFilter
    {
        public class UniqueAuthor : IRequest<AuthorDto>
        {
            public string? AuthorGuid { get; set; }
        }

        public class Handler : IRequestHandler<UniqueAuthor, AuthorDto>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AuthorDto> Handle(UniqueAuthor request, CancellationToken cancellationToken)
            {
                var author = await _context.AuthorBooks.Where(x => x.AuthorBookGuid == request.AuthorGuid).FirstOrDefaultAsync();

                if (author == null)
                {
                    throw new Exception("No se ha encontrado el autor");
                }

                var authorDto = _mapper.Map<AuthorBook, AuthorDto>(author);

                return authorDto;
            }
        }
    }
}