using author_service.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static author_service.Application.New;

namespace author_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Main data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthors()
        {
            return await _mediator.Send(new Query.AuthorList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorBook(string id)
        {
            return await _mediator.Send(new QueryFilter.UniqueAuthor { AuthorGuid = id });
        }
    }
}