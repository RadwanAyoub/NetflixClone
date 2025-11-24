using AutoMapper;
using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces.Repositories;

namespace NetflixClone.Application.Features.Genres.Queries
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetAllGenresQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }
    }
}