using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Features.Genres.Queries;

namespace NetflixClone.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var genres = await _mediator.Send(new GetAllGenresQuery());
            return Ok(genres);
        }
    }
}