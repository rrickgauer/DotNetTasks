﻿using System.Reflection;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Other;
using Tasks.Service.Domain.Parms;

namespace Tasks.Service.Utilities;

public static class AttributeUtilities
{

    public static string GetSqlColumn<T>(string propertyName)
    {
        var attr = GetPropertyAttribute<SqlColumnAttribute, T>(propertyName);

        return attr.ColumnName;
    }


    public static IEnumerable<TAttribute> GetPropertyAttributes<TAttribute, TClass>(string propertyName) where TAttribute : Attribute
    {
        return GetPropertyAttributes<TAttribute>(propertyName, typeof(TClass));
    }

    public static IEnumerable<TAttribute> GetPropertyAttributes<TAttribute>(string propertyName, Type classType) where TAttribute : Attribute
    {
        Type t = classType;

        var prop = t.GetProperty(propertyName);

        if (prop is null)
        {
            throw new NotSupportedException($"{propertyName} is not a valid property for type {t}");
        }

        var attrs = prop.GetCustomAttributes<TAttribute>().ToList();

        if (attrs.Count < 1)
        {
            throw new NotSupportedException($"{propertyName} does not have ${nameof(TAttribute)} assignment");
        }

        return attrs;
    }


    public static TAttribute GetPropertyAttribute<TAttribute, TClass>(string propertyName) where TAttribute : Attribute
    {
        return GetPropertyAttribute<TAttribute>(propertyName, typeof(TClass));
    }

    public static TAttribute GetPropertyAttribute<TAttribute>(string propertyName, Type classType) where TAttribute : Attribute
    {
        var prop = classType.GetProperty(propertyName);

        if (prop is null)
        {
            throw new NotSupportedException($"{propertyName} is not a valid property for type {classType}");
        }

        var attr = prop.GetCustomAttribute<TAttribute>();

        if (attr is null)
        {
            throw new NotSupportedException($"{propertyName} does not have ${nameof(TAttribute)} assignment");
        }

        return attr;
    }


    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute, TClass>() where TAttribute : Attribute
    {
        return GetPropertiesWithAttribute<TAttribute>(typeof(TClass));
    }

    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(Type classType) where TAttribute : Attribute
    {
        var properties = classType.GetProperties().Where(p => p.GetCustomAttributes<TAttribute>().ToList().Count > 0);

        return properties;
    }

    public static IEnumerable<CustomAttributeProperty<TAttribute>> GetPropertiesWithAttributeDict<TAttribute>(Type classType) where TAttribute: Attribute
    {
        List<CustomAttributeProperty<TAttribute>> result = new();

        var properties = GetPropertiesWithAttribute<TAttribute>(classType);

        foreach (var prop in properties)
        {
            result.Add(new(prop));   
        }

        return result;
    }






    /// <summary>
    /// Copy over the property values with matching names from the TSource into the TTarget
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyOverProperties<TSource, TTarget>(TSource source, TTarget target)
    {
        var sourceProperties = typeof(TSource).GetProperties();
        var targetProperties = typeof(TTarget).GetProperties();

        foreach (var prop in sourceProperties)
        {
            var sourceValue = prop.GetValue(source);

            var targetProperty = targetProperties.Where(p => p.Name == prop.Name).FirstOrDefault();
            targetProperty?.SetValue(target, sourceValue);
        }
    }

}
