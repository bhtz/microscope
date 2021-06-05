using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Features.Storage.Commands;
using Microscope.Domain.Services;

namespace Microscope.Application.CommandHandlers.Storage
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
