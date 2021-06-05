using AutoMapper;
using Microscope.Application.Features.Analytic.Queries;
using Microscope.Domain.Entities;

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
