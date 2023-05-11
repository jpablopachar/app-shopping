using book_service.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static book_service.Application.New;

namespace book_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibraryMaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> New(Main data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryMaterialDto>>> GetBooks()
        {
            return await _mediator.Send(new Query.Main());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryMaterialDto>> GetBook(Guid id)
        {
            return await _mediator.Send(new QueryFilter.UniqueBook { LibraryMaterialId = id });
        }
    }
}