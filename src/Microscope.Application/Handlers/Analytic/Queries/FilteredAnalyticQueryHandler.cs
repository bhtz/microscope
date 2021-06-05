using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microscope.Application.Features.Analytic.Queries;
using Microscope.Domain.Aggregates.AnalyticAggregate;

namespace Microscope.Application.QueryHandlers.Analytics
{
    public class FilteredAnalyticQueryHandler : IRequestHandler<FilteredAnalyticQuery, IEnumerable<AnalyticQueryResult>>
    {
        private readonly IAnalyticRepository _repository;
        private readonly IMapper _mapper;

        public FilteredAnalyticQueryHandler(IAnalyticRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnalyticQueryResult>> Handle(FilteredAnalyticQuery request, CancellationToken cancellationToken)
        {
            var result = await this._repository.GetAllAsync();
            
            return _mapper
                .ProjectTo<AnalyticQueryResult>(result.AsQueryable())
                .ToList();
        }
    }
}
