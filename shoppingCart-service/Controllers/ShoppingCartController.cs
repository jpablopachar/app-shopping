using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    }
}