using FluentValidation;
using MediatR;

namespace Microscope.Features.Storage.Commands
{
    public class AddContainerCommand : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class AddContainerCommandValidator : AbstractValidator<AddContainerCommand>
    {
        public AddContainerCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .Matches(@"^\S*$");
        }
    }
}