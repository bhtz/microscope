using MediatR;
using Microscope.ExternalSystems.Services;
using Microscope.Features.Storage.Commands;

namespace Microscope.Application.Handlers.Storage.Commands
{
    public class DeleteBlobCommandHandler : IRequestHandler<DeleteBlobCommand, bool>
    {
        private readonly IStorageService _storageService;

        public DeleteBlobCommandHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<bool> Handle(DeleteBlobCommand request, CancellationToken cancellationToken)
        {
            await this._storageService.DeleteBlobAsync(request.ContainerName, request.BlobName);
            return true;
        }
    }
}
