using AutoMapper;
using MediatR;
using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.AnalyticAggregate;
using Microscope.Features.Analytic.Commands;

namespace Microscope.Application.Handlers.Analytic.Commands
{
    public class AddAnalyticCommandHandler : IRequestHandler<AddAnalyticCommand, Guid>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddAnalyticCommandHandler(IAnalyticRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddAnalyticCommand command, CancellationToken cancellationToken)
        {
            var entity = Domain.Aggregates.Analytic.Analytic.NewAnalytic(Guid.NewGuid(), command.Key, command.Dimension);
            
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

            return entity.Id;
        }
    }
}
