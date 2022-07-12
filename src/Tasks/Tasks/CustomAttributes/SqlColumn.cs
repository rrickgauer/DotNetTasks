/**
 * 
 * This is a custom attribute used for data row column mapping
 * 
 */
namespace Tasks.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class SqlColumn : System.Attribute
    {
        public string ColumnName { get; }

        public SqlColumn(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
