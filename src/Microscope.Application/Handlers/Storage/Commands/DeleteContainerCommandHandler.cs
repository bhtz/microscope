using MediatR;
using Microscope.ExternalSystems.Services;
using Microscope.Features.Storage.Commands;

namespace Microscope.Application.Handlers.Storage.Commands
{
    public class DeleteContainerCommandHandler : IRequestHandler<DeleteContainerCommand, bool>
    {
        private readonly IStorageService _storageService;

        public DeleteContainerCommandHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<bool> Handle(DeleteContainerCommand request, CancellationToken cancellationToken)
        {
            await this._storageService.DeleteContainerAsync(request.ContainerName);
            return true;
        }
    }
}
