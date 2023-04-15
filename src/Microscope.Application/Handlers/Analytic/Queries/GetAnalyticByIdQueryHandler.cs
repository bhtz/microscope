using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Domain.Aggregates.Analytic;
using Microscope.Features.Analytic.Queries;

namespace Microscope.QueryHandlers.Analytics
{
    public class GetAnalyticByIdQueryHandler : IRequestHandler<GetAnalyticByIdQuery, GetAnalyticByIdQueryResult>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;

        public GetAnalyticByIdQueryHandler(IAnalyticRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAnalyticByIdQueryResult> Handle(GetAnalyticByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(request.Id);

            return _mapper.Map<Analytic, GetAnalyticByIdQueryResult>(entity);
        }
    }
}
