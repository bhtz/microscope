using AutoMapper;
using Microscope.Domain.Aggregates.Analytic;
using Microscope.Features.Analytic.Queries;

namespace Microscope.Application.Common.Mappings
{
    public class AnalyticProfile : Profile
    {
        public AnalyticProfile()
        {
            CreateMap<Analytic, AnalyticQueryResult>().ReverseMap();
            CreateMap<Analytic, GetAnalyticByIdQueryResult>().ReverseMap();
        }
    }
}
