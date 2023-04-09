using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace Microscope.Workflow.Workflows;

public class HeartbeatWorkflow : IWorkflow
{
    private readonly IClock _clock;
    public HeartbeatWorkflow(IClock clock) => _clock = clock;

    public void Build(IWorkflowBuilder builder) =>
        builder
            .Timer(Duration.FromMinutes(10))
            .WriteLine(context => $"Heartbeat at {_clock.GetCurrentInstant()}");
}