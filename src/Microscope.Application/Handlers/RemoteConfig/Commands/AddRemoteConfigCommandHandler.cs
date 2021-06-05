using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Application.Features.RemoteConfig.Commands;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;

namespace Microscope.Application.Commands.RemoteConfig
{
    public class AddRemoteConfigCommandHandler : IRequestHandler<AddRemoteConfigCommand, Guid>
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;

        public AddRemoteConfigCommandHandler(IRemoteConfigRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddRemoteConfigCommand command, CancellationToken cancellationToken)
        {
            var entity = Microscope.Domain.Entities.RemoteConfig.NewRemoteConfig(Guid.NewGuid(), command.Key, command.Dimension);

            await this._repository.AddAsync(entity);
            await this._repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
