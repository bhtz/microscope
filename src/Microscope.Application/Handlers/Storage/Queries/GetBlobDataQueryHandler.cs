using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Features.Storage.Queries;
using Microscope.Domain.Services;

namespace Microscope.Application.QueryHandlers.Storage
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
