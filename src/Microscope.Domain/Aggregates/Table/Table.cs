using Microscope.BuildingBlocks.SharedKernel;
using Microscope.Domain.Aggregates.Table.Entities;

namespace Microscope.Domain.Aggregates.Table;

public class Table : Entity, IAggregateRoot
{
    #region Properties

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Schema { get; private set; } = "public";
    public string Comment { get; private set; } = string.Empty;
    
    private readonly List<Field> _fields;
    public IReadOnlyCollection<Field> Fields => _fields;
    
    private readonly List<Relationship> _relationships;
    public IReadOnlyCollection<Relationship> Relationships => _relationships;
    
    private readonly List<Permission> _permissions;
    public IReadOnlyCollection<Permission> Permissions => _permissions;

    #endregion

    #region CTOR

    protected Table()
    {
        
    }

    protected Table(Guid id, string name, string schema, string comment)
    {
        Id = id;
        Name = name;
        Schema = schema;
        Comment = comment;

        _fields = new List<Field>();
        _relationships = new List<Relationship>();
        _permissions = new List<Permission>();
    }

    public static Table NewTable(Guid id, string name, string schema, string comment)
    {
        return new Table(id, name, schema, comment);
    }

    #endregion
}