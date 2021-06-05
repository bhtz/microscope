using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Features.Storage.Commands;
using Microscope.Domain.Services;

namespace Microscope.Application.Commands.Storage
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
