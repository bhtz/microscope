using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Domain.Aggregates.AnalyticAggregate;
using Microscope.Features.Analytic.Commands;

namespace Microscope.Commands.AnalyticHandlers
{
    public class EditAnalyticCommandHandler : IRequestHandler<EditAnalyticCommand, Guid>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;

        public EditAnalyticCommandHandler(IAnalyticRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EditAnalyticCommand command, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(command.Id);
            entity.Update(command.Key, command.Dimension);
            
            await this._repository.UpdateAsync(entity);
            await this._repository.UnitOfWork.SaveChangesAsync();

            return entity.Id;
        }
    }
}
