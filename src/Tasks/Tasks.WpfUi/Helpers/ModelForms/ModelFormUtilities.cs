#pragma warning disable CS8604 // Possible null reference argument.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tasks.WpfUi.Helpers.ModelForms;

public static class ModelFormUtilities
{
    /// <summary>
    /// Copy over the model properties into the form
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TForm"></typeparam>
    /// <param name="model"></param>
    /// <param name="form"></param>
    public static void SetFormPropertyValues<TModel, TForm>(TModel model, TForm form) 
        where TForm : class, IModelForm<TModel> 
        where TModel : class
    {
        var formProperties = form.GetPropertiesToCopy();

        var modelProperties = model.GetType().GetProperties();

        foreach (var formProperty in formProperties)
        {
            var customAttribute = formProperty.GetCustomAttribute<ModelFormPropertyAttribute>();

            if (customAttribute == null) continue;

            var modelPropertyName = customAttribute.PropertyName;
            var modelPropertyValue = GetPropertyValue(modelProperties, model, modelPropertyName);

            formProperty.SetValue(form, modelPropertyValue);
        }
    }

    /// <summary>
    /// Copy over the form property values into the model
    /// </summary>
    /// <typeparam name="TForm"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="form"></param>
    /// <param name="model"></param>
    public static void SetModelPropertyValues<TForm, TModel>(TForm form, TModel model) 
        where TModel : class 
        where TForm : class, IModelForm<TModel>
    {

        var modelProperties = model.GetType().GetProperties();

        foreach (var formProperty in form.GetPropertiesToCopy())
        {
            var propertyName = formProperty.GetCustomAttribute<ModelFormPropertyAttribute>().PropertyName;
            var propertyValue = formProperty.GetValue(form);

            modelProperties.Where(p => p.Name == propertyName).FirstOrDefault().SetValue(model, propertyValue);
        }
    }


    private static object? GetPropertyValue(IEnumerable<PropertyInfo> properties, object model, string propertyName)
    {
        var property = properties.Where(p => p.Name == propertyName).FirstOrDefault();

        return property.GetValue(model);
    }

    public static List<PropertyInfo> GetModelFormProperties<T>() where T : class
    {
        var props = typeof(T).GetProperties().Where(p => p.GetCustomAttribute<ModelFormPropertyAttribute>() != null);

        return props.ToList();
    }
}
