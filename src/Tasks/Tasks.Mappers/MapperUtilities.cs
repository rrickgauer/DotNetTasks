using System.Reflection;

namespace Tasks.Mappers;

public static class MapperUtilities
{

    public static DateOnly? ToDateOnly(DateTime? dateTime)
    {
        if (dateTime.HasValue)
        {
            return DateOnly.FromDateTime(dateTime.Value);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Get a list of PropertyInfo's of the given source Type that are assigned the specified attribute
    /// </summary>
    /// <param name="source"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static List<PropertyInfo> GetPropertiesWithAttribute(Type source, Type attribute)
    {
        List<PropertyInfo> properties = (source).GetProperties().Where(x => x.GetCustomAttributes(attribute, true).Any()).ToList();

        return properties ?? new();
    }

    /// <summary>
    /// Get the specified CustomAttribute from the given PropertyInfo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prop"></param>
    /// <returns></returns>
    public static T? GetAttributeFromProperty<T>(PropertyInfo prop)
    {
        var propertyCustomAttributes = prop.GetCustomAttributes(false);
        var customAttr = propertyCustomAttributes.Where(x => x.GetType().Name == typeof(T).Name).FirstOrDefault();

        if (customAttr is null)
        {
            return default;
        }

        return (T)customAttr;
    }
}
