/**
* 
* This is a custom attribute used for data row column mapping
* 
*/
namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SqlColumn : Attribute
{
    public string ColumnName { get; }

    public SqlColumn(string columnName)
    {
        ColumnName = columnName;
    }
}
