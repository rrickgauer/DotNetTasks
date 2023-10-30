using System.Reflection;

namespace Tasks.Service.Domain.Other;

public class CustomAttributeProperty<TAttribute> where TAttribute : Attribute
{
    public PropertyInfo PropertyInfo { get; set; }
    public TAttribute Attribute { get; set; }

    public CustomAttributeProperty(PropertyInfo propertyInfo)
    {
        PropertyInfo = propertyInfo;
        Attribute = GetAttr(propertyInfo);
    }

    public CustomAttributeProperty(PropertyInfo propertyInfo, TAttribute attribute)
    {
        PropertyInfo = propertyInfo;
        Attribute = attribute;
    }

    private static TAttribute GetAttr(PropertyInfo propertyInfo)
    {
        var attr = propertyInfo.GetCustomAttribute<TAttribute>(true);

        if (attr is null)
        {
            throw new NotSupportedException($"{propertyInfo.Name} does not have the custom attribute {nameof(TAttribute)}");
        }

        return attr;
    }

    public T? GetValue<T>(object? classData) where T : class?
    {
        return PropertyInfo.GetValue(classData) as T;
    }

    public object? GetValueRaw(object? classData)
    {
        return GetValue<object?>(classData);
    }

}
