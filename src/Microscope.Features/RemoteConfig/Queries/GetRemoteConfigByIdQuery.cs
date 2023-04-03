using System;
using MediatR;

namespace Microscope.Features.RemoteConfig.Queries
{
    public class GetRemoteConfigByIdQuery : IRequest<GetRemoteConfigByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRemoteConfigByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetRemoteConfigByIdQueryResult 
    {
        public Guid Id { get; set; }
        public Guid Key { get; set; }
        public Guid Dimension { get; set; }
    }
}
