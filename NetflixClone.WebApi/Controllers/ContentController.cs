using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Features.Content.Queries;

namespace NetflixClone.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ContentDto>>> SearchContent([FromQuery] string q)
        {
            var content = await _mediator.Send(new SearchContentQuery { SearchTerm = q });
            return Ok(content);
        }

        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<ContentDto>>> GetContentByGenre(int genreId)
        {
            var content = await _mediator.Send(new GetContentByGenreQuery { GenreId = genreId });
            return Ok(content);
        }
    }
}