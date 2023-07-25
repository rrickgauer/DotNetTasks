/**
* 
* This is a custom attribute used for data row column mapping
* 
*/
namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SqlColumnAttribute : Attribute
{
    public string ColumnName { get; }

    public SqlColumnAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}
