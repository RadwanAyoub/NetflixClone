using AutoMapper;
using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces.Repositories;

namespace NetflixClone.Application.Features.Movies.Queries
{
    public class GetFeaturedMoviesQueryHandler : IRequestHandler<GetFeaturedMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetFeaturedMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetFeaturedMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetFeaturedAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }
    }
}

