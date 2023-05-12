using MediatR;
using Microsoft.AspNetCore.Mvc;
using shoppingCart_service.Application;
using static shoppingCart_service.Application.New;

namespace shoppingCart_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> New(Main data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCart(int id)
        {
            return await _mediator.Send(new Query.Main { SessionCartId = id });
        }
    }
}