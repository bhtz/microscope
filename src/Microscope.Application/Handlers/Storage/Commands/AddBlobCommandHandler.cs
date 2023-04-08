using MediatR;
using Microscope.ExternalSystems.Services;
using Microscope.Features.Storage.Commands;

namespace Microscope.Application.Handlers.Storage.Commands
{
    public class AddBlobCommandHandler : IRequestHandler<AddBlobCommand, bool>
    {
        private readonly IStorageService _storageService;
        
        public AddBlobCommandHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<bool> Handle(AddBlobCommand request, CancellationToken cancellationToken)
        {
            await this._storageService.SaveBlobAsync(request.ContainerName, request.BlobName, request.Data);
            return true;
        }
    }
}
