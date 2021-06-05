using System;
using System.Collections.Generic;
using MediatR;

namespace Microscope.Application.Features.Analytic.Queries
{
    public class FilteredAnalyticQuery : IRequest<IEnumerable<AnalyticQueryResult>>
    {

    }

    public class AnalyticQueryResult
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Dimension { get; set; }
    }
}