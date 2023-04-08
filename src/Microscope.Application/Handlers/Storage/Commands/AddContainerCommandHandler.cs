using MediatR;
using Microscope.ExternalSystems.Services;
using Microscope.Features.Storage.Commands;

namespace Microscope.Application.Handlers.Storage.Commands
{
    public class AddContainerCommandHandler : IRequestHandler<AddContainerCommand, string>
    {
        private readonly IStorageService _storageService;

        public AddContainerCommandHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string> Handle(AddContainerCommand command, CancellationToken cancellationToken)
        {
            await this._storageService.CreateContainerAsync(command.Name);
            return command.Name;
        }
    }
}
