using MediatR;
using Microscope.ExternalSystems.Services;
using Microscope.Features.Storage.Queries;

namespace Microscope.Application.Handlers.Storage.Queries
{
    public class GetBlobDataQueryHandler : IRequestHandler<GetBlobDataQuery, BlobDataQueryResult>
    {
        private readonly IStorageService _storageService;

        public GetBlobDataQueryHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<BlobDataQueryResult> Handle(GetBlobDataQuery request, CancellationToken cancellationToken)
        {
            var result = await this._storageService.GetBlobAsync(request.ContainerName, request.BlobName);

            return new BlobDataQueryResult(result);
        }
    }
}
