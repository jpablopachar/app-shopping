using AutoMapper;
using book_service.Models;
using book_service.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace book_service.Application
{
    public class Query
    {
        public class Main : IRequest<List<LibraryMaterialDto>> { }

        public class Handler : IRequestHandler<Main, List<LibraryMaterialDto>>
        {
            private readonly LibraryContext _context;
            private readonly IMapper _mapper;

            public Handler(LibraryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<LibraryMaterialDto>> Handle(Main request, CancellationToken cancellationToken)
            {
                var books = await _context.LibraryMaterial.ToListAsync();
                var booksDto = _mapper.Map<List<LibraryMaterial>, List<LibraryMaterialDto>>(books);

                return booksDto;
            }
        }
    }
}