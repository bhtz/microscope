using MediatR;
using Microscope.Domain.Aggregates.Analytic.Events;

namespace Microscope.Application.Handlers.Analytic.Events
{
    public class CreatedAnalyticEventHandler : INotificationHandler<CreatedAnalyticEvent>
    {
        public CreatedAnalyticEventHandler()
        {
            
        }

        public Task Handle(CreatedAnalyticEvent notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification;
            
            return Task.CompletedTask;
        }
    }
}
