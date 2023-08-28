using System.Collections.Generic;
using System.Reflection;

namespace Tasks.WpfUi.Helpers.ModelForms;

public interface IModelForm<T> where T : class
{
    public T BuildModel();
    public void SetPropertyValues(T model);
    public List<PropertyInfo> GetPropertiesToCopy();
}
