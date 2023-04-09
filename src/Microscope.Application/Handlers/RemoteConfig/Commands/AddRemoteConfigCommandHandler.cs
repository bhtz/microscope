using AutoMapper;
using MediatR;
using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Features.RemoteConfig.Commands;

namespace Microscope.Application.Handlers.RemoteConfig.Commands
{
    public class AddRemoteConfigCommandHandler : IRequestHandler<AddRemoteConfigCommand, Guid>
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddRemoteConfigCommandHandler(IRemoteConfigRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddRemoteConfigCommand command, CancellationToken cancellationToken)
        {
            var entity = Domain.Aggregates.RemoteConfig.RemoteConfig.NewRemoteConfig(Guid.NewGuid(), command.Key, command.Dimension);

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

            return entity.Id;
        }
    }
}
