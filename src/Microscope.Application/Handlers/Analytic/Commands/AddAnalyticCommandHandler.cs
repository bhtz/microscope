using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Domain.Aggregates.AnalyticAggregate;
using Microscope.Domain.Entities;
using Microscope.Features.Analytic.Commands;

namespace Microscope.Commands.AnalyticHandlers
{
    public class AddAnalyticCommandHandler : IRequestHandler<AddAnalyticCommand, Guid>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;

        public AddAnalyticCommandHandler(IAnalyticRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddAnalyticCommand command, CancellationToken cancellationToken)
        {
            var entity = Analytic.NewAnalytic(Guid.NewGuid(), command.Key, command.Dimension);
            await this._repository.AddAsync(entity);
            await this._repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
