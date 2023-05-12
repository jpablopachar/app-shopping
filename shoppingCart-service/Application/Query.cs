using MediatR;
using Microsoft.EntityFrameworkCore;
using shoppingCart_service.Persistence;
using shoppingCart_service.RemoteInterface;

namespace shoppingCart_service.Application
{
    public class Query
    {
        public class Main : IRequest<CartDto>
        {
            public int SessionCartId { get; set; }
        }

        public class Handler : IRequestHandler<Main, CartDto>
        {
            private readonly CartContext _context;
            private readonly IBookService _bookService;

            public Handler(CartContext context, IBookService bookService)
            {
                _context = context;
                _bookService = bookService;
            }

            public async Task<CartDto> Handle(Main request, CancellationToken cancellationToken)
            {
                var sessionCart = await _context.CartSessions.FirstOrDefaultAsync(x => x.CartSessionId == request.SessionCartId);
                var sessionCartDetail = await _context.CartSessionDetail.Where(x => x.CartSessionId == request.SessionCartId).ToListAsync();

                var cartDtoList = new List<CartDetailDto>();

                foreach (var book in sessionCartDetail)
                {
                    var res = await _bookService.GetBook(new Guid(book.selectedProduct));

                    if (res.result)
                    {
                        var bookObj = res.book;

                        var cartDetail = new CartDetailDto
                        {
                            titleBook = bookObj?.Title,
                            PublicationDate = bookObj?.PublicationDate,
                            BookId = bookObj?.LibraryMaterialId
                        };

                        cartDtoList.Add(cartDetail);
                    }
                }

                var sessionCartDto = new CartDto
                {
                    CartId = sessionCart?.CartSessionId,
                    DateCreatedSession = sessionCart?.DateCreated,
                    ProductsList = cartDtoList
                };

                return sessionCartDto;
            }
        }
    }
}