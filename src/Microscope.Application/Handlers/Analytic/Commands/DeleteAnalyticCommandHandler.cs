using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Application.Features.Analytic.Commands;
using Microscope.Domain.Aggregates.AnalyticAggregate;

namespace Microscope.Application.Commands.AnalyticHandlers
{
    public class DeleteAnalyticCommandHandler : IRequestHandler<DeleteAnalyticCommand, Guid>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;
        
        public DeleteAnalyticCommandHandler(IAnalyticRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(DeleteAnalyticCommand request, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(request.Id);
            await this._repository.DeleteAsync(entity);
            return entity.Id;
        }
    }
}


