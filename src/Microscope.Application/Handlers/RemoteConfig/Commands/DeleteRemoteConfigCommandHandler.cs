using AutoMapper;
using MediatR;
using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Features.RemoteConfig.Commands;

namespace Microscope.Application.Handlers.RemoteConfig.Commands
{
    public class DeleteRemoteConfigCommandHandler : IRequestHandler<DeleteRemoteConfigCommand, Guid> 
    {
        private readonly IRemoteConfigRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRemoteConfigCommandHandler(IRemoteConfigRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteRemoteConfigCommand request, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(request.Id);
            
            await this._repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
            
            return request.Id;
        }
    }
}
