using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Features.RemoteConfig.Commands;

namespace Microscope.Commands.RemoteConfig
{
    public class DeleteRemoteConfigCommandHandler : IRequestHandler<DeleteRemoteConfigCommand, Guid> 
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;

        public DeleteRemoteConfigCommandHandler(IRemoteConfigRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;    
        }

        public async Task<Guid> Handle(DeleteRemoteConfigCommand request, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(request.Id);
            await this._repository.DeleteAsync(entity);
            return request.Id;
        }
    }
}
