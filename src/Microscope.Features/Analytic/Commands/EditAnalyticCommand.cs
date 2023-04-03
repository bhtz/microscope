using System;
using FluentValidation;
using MediatR;

namespace Microscope.Features.Analytic.Commands
{
    public class EditAnalyticCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Dimension { get; set; }
    }

    public class EditAnalyticCommandValidator : AbstractValidator<EditAnalyticCommand>
    {
        public EditAnalyticCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.Key).NotEmpty();
            RuleFor(v => v.Dimension).NotEmpty();
        }
    }
}