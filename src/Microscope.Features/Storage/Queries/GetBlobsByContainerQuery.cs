using System.Collections.Generic;
using MediatR;

namespace Microscope.Features.Storage.Queries
{
    public class GetBlobsByContainerQuery : IRequest<IEnumerable<GetBlobsByContainerQueryResult>>
    {
        public string ContainerName { get; set; }
    }

    public class GetBlobsByContainerQueryResult
    {
        public string Name { get; set; }

        public GetBlobsByContainerQueryResult(string Name)
        {
            this.Name = Name;
        }
    }
}
