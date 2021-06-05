using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Features.Storage.Queries;
using Microscope.Domain.Services;

namespace Microscope.Application.QueryHandlers.Storage
{
    public class GetBlobsByContainerQueryHandler : IRequestHandler<GetBlobsByContainerQuery, IEnumerable<GetBlobsByContainerQueryResult>>
    {
        private readonly IStorageService _storageService;
        
        public GetBlobsByContainerQueryHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<IEnumerable<GetBlobsByContainerQueryResult>> Handle(GetBlobsByContainerQuery request, CancellationToken cancellationToken)
        {
            var results = await this._storageService.ListBlobsAsync(request.ContainerName);
            
            return results
                .Select(x => new GetBlobsByContainerQueryResult(x));
        }
    }
}
