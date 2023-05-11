using AutoMapper;
using book_service.Models;
using book_service.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace book_service.Application
{
    public class QueryFilter
    {
        public class UniqueBook : IRequest<LibraryMaterialDto>
        {
            public Guid LibraryMaterialId { get; set; }
        }

        public class Handler : IRequestHandler<UniqueBook, LibraryMaterialDto>
        {
            private readonly LibraryContext _context;
            private readonly IMapper _mapper;

            public Handler(LibraryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LibraryMaterialDto> Handle(UniqueBook request, CancellationToken cancellationToken)
            {
                var book = await _context.LibraryMaterial.Where(x => x.LibraryMaterialId == request.LibraryMaterialId).FirstOrDefaultAsync();

                if (book == null)
                {
                    throw new Exception("No se encontró el libro");
                }

                var bookDto = _mapper.Map<LibraryMaterial, LibraryMaterialDto>(book);

                return bookDto;
            }
        }
    }
}