using MediatR;

namespace Microscope.BuildingBlocks.SharedKernel;

public abstract class DomainEvent : INotification
{
    public DateTime CreatedAt { get; set; }
}