using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Common;
using Microscope.Domain.Events;

namespace Microscope.Application.DomainEventHandlers
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
