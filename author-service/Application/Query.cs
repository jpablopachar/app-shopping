using author_service.Models;
using author_service.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace author_service.Application
{
    public class Query
    {
        public class AuthorList : IRequest<List<AuthorDto>> { }

        public class Handler : IRequestHandler<AuthorList, List<AuthorDto>>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;

            public Handler(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AuthorDto>> Handle(AuthorList request, CancellationToken cancellationToken)
            {
                var authorList = await _context.AuthorBooks.ToListAsync();
                var authorListDto = _mapper.Map<List<AuthorBook>, List<AuthorDto>>(authorList);

                return authorListDto;
            }
        }
    }
}