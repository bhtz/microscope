using System;
using FluentValidation;
using MediatR;

namespace Microscope.Features.RemoteConfig.Commands
{
    public class EditRemoteConfigCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Dimension { get; set; }
    }

    public class EditRemoteConfigCommandValidator : AbstractValidator<EditRemoteConfigCommand>
    {
        public EditRemoteConfigCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.Key).NotEmpty();
            RuleFor(v => v.Dimension).NotEmpty();
        }
    }
}
