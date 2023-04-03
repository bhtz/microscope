using System;
using FluentValidation;
using MediatR;

namespace Microscope.Features.Analytic.Commands
{
    public class DeleteAnalyticCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAnalyticCommandValidator : AbstractValidator<DeleteAnalyticCommand>
    {
        public DeleteAnalyticCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}