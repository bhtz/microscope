using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Features.RemoteConfig.Commands;

namespace Microscope.Commands.RemoteConfig
{
    public class EditRemoteConfigCommandHandler : IRequestHandler<EditRemoteConfigCommand, Guid>
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;

        public EditRemoteConfigCommandHandler(IRemoteConfigRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EditRemoteConfigCommand command, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(command.Id);
            entity.Update(command.Key, command.Dimension);

            await this._repository.UpdateAsync(entity);
            await this._repository.UnitOfWork.SaveChangesAsync();

            return entity.Id;
        }
    }
}
