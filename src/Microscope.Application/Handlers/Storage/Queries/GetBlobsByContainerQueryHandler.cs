using MediatR;
using Microscope.ExternalSystems.Services;
using Microscope.Features.Storage.Queries;

namespace Microscope.Application.Handlers.Storage.Queries
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
