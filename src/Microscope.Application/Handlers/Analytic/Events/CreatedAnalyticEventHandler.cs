using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Common;
using Microscope.Domain.Events;

namespace Microscope.DomainEventHandlers
{
    public class CreatedAnalyticEventHandler : INotificationHandler<DomainEventNotification<CreatedAnalyticEvent>>
    {
        public CreatedAnalyticEventHandler()
        {
            
        }

        public Task Handle(DomainEventNotification<CreatedAnalyticEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            return Task.CompletedTask;
        }
    }
}
