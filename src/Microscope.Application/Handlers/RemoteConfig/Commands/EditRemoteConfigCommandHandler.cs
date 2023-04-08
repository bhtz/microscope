using AutoMapper;
using MediatR;
using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Features.RemoteConfig.Commands;

namespace Microscope.Application.Handlers.RemoteConfig.Commands
{
    public class EditRemoteConfigCommandHandler : IRequestHandler<EditRemoteConfigCommand, Guid>
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditRemoteConfigCommandHandler(IRemoteConfigRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(EditRemoteConfigCommand command, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(command.Id);
            entity.Update(command.Key, command.Dimension);

            await this._repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

            return entity.Id;
        }
    }
}
