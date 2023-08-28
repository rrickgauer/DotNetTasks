using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.WpfUi.Helpers.ModelForms;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ModelFormPropertyAttribute : Attribute
{
    public string PropertyName { get; private set; }

    public ModelFormPropertyAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }

}
