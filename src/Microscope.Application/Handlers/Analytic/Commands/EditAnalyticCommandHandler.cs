using AutoMapper;
using MediatR;
using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.Analytic;
using Microscope.Features.Analytic.Commands;

namespace Microscope.Application.Handlers.Analytic.Commands
{
    public class EditAnalyticCommandHandler : IRequestHandler<EditAnalyticCommand, Guid>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditAnalyticCommandHandler(IAnalyticRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(EditAnalyticCommand command, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(command.Id);
            entity.Update(command.Key, command.Dimension);
            
            await this._repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

            return entity.Id;
        }
    }
}
