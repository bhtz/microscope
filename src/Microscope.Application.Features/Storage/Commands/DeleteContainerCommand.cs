using System;
using FluentValidation;
using MediatR;

namespace Microscope.Application.Features.Storage.Commands
{
    public class DeleteContainerCommand : IRequest<bool>
    {
        public string ContainerName { get; set; }
    }

    public class DeleteContainerCommandValidator : AbstractValidator<DeleteContainerCommand>
    {
        public DeleteContainerCommandValidator()
        {
            RuleFor(x => x.ContainerName).NotEmpty();
        }
    }
}
