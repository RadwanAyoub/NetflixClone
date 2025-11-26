using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Features.Movies.Queries;

namespace NetflixClone.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _mediator.Send(new GetTrendingMoviesQuery());
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _mediator.Send(new GetMovieByIdQuery(id));
            
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetTrendingMovies([FromQuery] int count = 10)
        {
            var movies = await _mediator.Send(new GetTrendingMoviesQuery(count));
            return Ok(movies);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetFeaturedMovies()
        {
            var movies = await _mediator.Send(new GetTrendingMoviesQuery());
            return Ok(movies);
        }
    }
}