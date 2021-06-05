using System;
using FluentValidation;
using MediatR;

namespace Microscope.Application.Features.Analytic.Commands
{
    public class AddAnalyticCommand : IRequest<Guid>
    {
        public string Key { get; set; }
        public string Dimension { get; set; }
    }

    public class AddAnalyticCommandValidator : AbstractValidator<AddAnalyticCommand>
    {
        public AddAnalyticCommandValidator()
        {
            RuleFor(v => v.Key).NotEmpty();
            RuleFor(v => v.Dimension).NotEmpty();
        }
    }
}