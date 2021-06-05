using System;
using MediatR;

namespace Microscope.Application.Features.Analytic.Queries
{
    public class GetAnalyticByIdQuery : IRequest<GetAnalyticByIdQueryResult>
    {
        public Guid Id { get; set; }
        
        public GetAnalyticByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetAnalyticByIdQueryResult
    {
        public Guid Id { get; set; }
        public Guid Key { get; set; }
        public Guid Dimension { get; set; }
    }
}
