using AutoMapper;
using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces.Repositories;

namespace NetflixClone.Application.Features.Movies.Queries
{
    public class GetTrendingMoviesQueryHandler : IRequestHandler<GetTrendingMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetTrendingMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetTrendingMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetTrendingAsync(request.Count);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }
    }
}

