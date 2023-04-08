namespace Microscope.BuildingBlocks.SharedKernel;

public class AuditableEntity<TId> : Entity
{
    public DateTime CreatedAt { get; set; }
    public TId CreatedBy { get; set; }
    public string CreatorMail { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TId UpdatedBy { get; set; }
}
