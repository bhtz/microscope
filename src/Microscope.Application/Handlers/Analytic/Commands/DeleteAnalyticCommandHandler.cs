using AutoMapper;
using MediatR;
using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.Analytic;
using Microscope.Features.Analytic.Commands;

namespace Microscope.Application.Handlers.Analytic.Commands
{
    public class DeleteAnalyticCommandHandler : IRequestHandler<DeleteAnalyticCommand, Guid>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteAnalyticCommandHandler(IAnalyticRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteAnalyticCommand request, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(request.Id);
            
            await this._repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}


