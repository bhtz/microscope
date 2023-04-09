using AutoMapper;
using Microscope.Core.Queries.RemoteConfig;
using Microscope.Domain.Aggregates.RemoteConfig;
using Microscope.Features.RemoteConfig.Queries;

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
