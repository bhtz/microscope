using AutoMapper;
using Microscope.Application.Core.Queries.RemoteConfig;
using Microscope.Application.Features.RemoteConfig.Queries;
using Microscope.Domain.Entities;

namespace Microscope.Application.Common.Mappings
{
    public class RemoteConfigProfile : Profile
    {
        public RemoteConfigProfile()
        {
            CreateMap<RemoteConfig, FilteredRemoteConfigQueryResult>().ReverseMap();
            CreateMap<RemoteConfig, GetRemoteConfigByIdQueryResult>().ReverseMap();
        }
    }
}
