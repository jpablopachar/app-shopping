using MediatR;
using shoppingCart_service.Models;
using shoppingCart_service.Persistence;

namespace shoppingCart_service.Application
{
    public class New
    {
        public class Main : IRequest
        {
            public DateTime DateCreatedSession { get; set; }
            public List<string>? ProductList { get; set; }
        }

        public class Handler : IRequestHandler<Main>
        {
            public readonly CartContext _context;

            public Handler(CartContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Main request, CancellationToken cancellationToken)
            {
                var cartSession = new CartSession
                {
                    DateCreated = request.DateCreatedSession
                };

                await _context.CartSessions.AddAsync(cartSession);

                var value = await _context.SaveChangesAsync();

                if (value == 0)
                {
                    throw new Exception("No se pudo crear el carrito de compras");
                }

                int id = cartSession.CartSessionId;

                foreach (var item in request.ProductList)
                {
                    var cartSessionDetail = new CartSessionDetail
                    {
                        DateCreated = DateTime.Now,
                        CartSessionId = id,
                        selectedProduct = item
                    };

                    await _context.CartSessionDetail.AddAsync(cartSessionDetail);
                }

                value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}