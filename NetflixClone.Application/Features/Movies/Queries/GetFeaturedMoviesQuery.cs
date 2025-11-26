using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Features.Movies.Queries
{
    public record GetFeaturedMoviesQuery() : IRequest<IEnumerable<MovieDto>>;
}

