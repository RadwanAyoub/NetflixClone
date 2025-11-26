using MediatR;
using NetflixClone.Application.DTOs;
using NetflixClone.Application.Interfaces.Repositories;

namespace NetflixClone.Application.Features.Content.Queries
{
    public class SearchContentQuery : IRequest<IEnumerable<ContentDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
    }

    public class SearchContentQueryHandler : IRequestHandler<SearchContentQuery, IEnumerable<ContentDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly AutoMapper.IMapper _mapper;

        public SearchContentQueryHandler(
            IContentRepository contentRepository,
            AutoMapper.IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContentDto>> Handle(SearchContentQuery request, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.SearchAsync(request.SearchTerm);
            return _mapper.Map<IEnumerable<ContentDto>>(content);
        }
    }
}