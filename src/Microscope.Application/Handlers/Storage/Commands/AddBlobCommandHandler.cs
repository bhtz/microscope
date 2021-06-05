using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Features.Storage.Commands;
using Microscope.Domain.Services;

namespace Microscope.Application.CommandHandlers.Storage
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
