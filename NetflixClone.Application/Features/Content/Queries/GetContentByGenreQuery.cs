using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces.Repositories;

namespace NetflixClone.Application.Features.Content.Queries
{
    public class GetContentByGenreQuery : IRequest<IEnumerable<ContentDto>>
    {
        public int GenreId { get; set; }
    }

    public class GetContentByGenreQueryHandler : IRequestHandler<GetContentByGenreQuery, IEnumerable<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetContentByGenreQueryHandler(
            IContentRepository contentRepository,
            AutoMapper.IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContentDto>> Handle(GetContentByGenreQuery request, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.GetByGenreIdAsync(request.GenreId);
            return _mapper.Map<IEnumerable<ContentDto>>(content);
        }
    }
}