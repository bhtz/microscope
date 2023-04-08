using AutoMapper;
using MediatR;
using Microscope.Core.Queries.RemoteConfig;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;

namespace Microscope.Application.Handlers.RemoteConfig.Queries
{
    public class FilteredRemoteConfigQueryHandler : IRequestHandler<FilteredRemoteConfigQuery, IEnumerable<FilteredRemoteConfigQueryResult>>
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;

        public FilteredRemoteConfigQueryHandler(IRemoteConfigRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilteredRemoteConfigQueryResult>> Handle(FilteredRemoteConfigQuery request, CancellationToken cancellationToken)
        {
            var result =  await this._repository.GetAllAsync();
            
            return _mapper
                .ProjectTo<FilteredRemoteConfigQueryResult>(result.AsQueryable())
                .ToList();
        }
    }
}
