namespace Microscope.Domain.Aggregates.Table.Entities;

public class Field
{
    public string Name { get; private set; }
    public ColumnDataType Type { get; private set; }
    public string DefaultValue { get; private set; }
    public bool IsNullable { get; private set; }
    public bool IsUnique { get; private set; }
}
