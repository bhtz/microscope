using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Domain.Entities;
using Microscope.Features.RemoteConfig.Queries;

namespace Microscope.QueryHandlers.RemoteConfigs
{
    public class GetRemoteConfigByIdQueryHandler : IRequestHandler<GetRemoteConfigByIdQuery, GetRemoteConfigByIdQueryResult>
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;

        public GetRemoteConfigByIdQueryHandler(IRemoteConfigRepository repository, IMapper mapper)
        {
            _repository = repository;    
            _mapper = mapper;
        }

        public async Task<GetRemoteConfigByIdQueryResult> Handle(GetRemoteConfigByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(request.Id);
            
            return _mapper.Map<RemoteConfig, GetRemoteConfigByIdQueryResult>(entity);
        }
    }
}
