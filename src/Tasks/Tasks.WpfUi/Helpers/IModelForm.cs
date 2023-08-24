using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.WpfUi.Helpers;

public interface IModelForm<T> where T : class
{
    public T BuildModel();
    public void SetPropertyValues(T model);
}
