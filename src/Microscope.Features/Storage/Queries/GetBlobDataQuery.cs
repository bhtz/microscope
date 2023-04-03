using System.IO;
using MediatR;

namespace Microscope.Features.Storage.Queries
{
    public class GetBlobDataQuery : IRequest<BlobDataQueryResult>
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
    }

    public class BlobDataQueryResult 
    {
        public Stream Data { get; set; }

        public BlobDataQueryResult(Stream data)
        {
            this.Data = data;
        }
    }
}
