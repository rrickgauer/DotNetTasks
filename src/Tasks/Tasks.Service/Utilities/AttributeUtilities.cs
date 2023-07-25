using System.Reflection;
using Tasks.Service.CustomAttributes;

namespace Tasks.Service.Utilities;

public static class AttributeUtilities
{

    public static string GetSqlColumn<T>(string propertyName)
    {
        var attr = GetPropertyAttribute<SqlColumnAttribute, T>(propertyName);

        return attr.ColumnName;
    }


    public static TAttribute GetPropertyAttribute<TAttribute, TClass>(string propertyName) where TAttribute : Attribute
    {
        Type t = typeof(TClass);

        var prop = t.GetProperty(propertyName);

        if (prop is null)
        {
            throw new NotSupportedException($"{propertyName} is not a valid property for type {t}");
        }

        var attr = prop.GetCustomAttribute<TAttribute>();

        if (attr is null)
        {
            throw new NotSupportedException($"{propertyName} does not have ${nameof(TAttribute)} assignment");
        }

        return attr;
    }






}
