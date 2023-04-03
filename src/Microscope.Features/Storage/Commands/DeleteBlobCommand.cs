using System;
using FluentValidation;
using MediatR;

namespace Microscope.Features.Storage.Commands
{
    public class DeleteBlobCommand : IRequest<bool>
    {
        public string BlobName { get; set; }
        public string ContainerName { get; set; }
    }

    public class DeleteBlobCommandValidator : AbstractValidator<DeleteBlobCommand>
    {
        public DeleteBlobCommandValidator()
        {
            RuleFor(x => x.BlobName).NotEmpty();
            RuleFor(x => x.ContainerName).NotEmpty();
        }
    }
}
