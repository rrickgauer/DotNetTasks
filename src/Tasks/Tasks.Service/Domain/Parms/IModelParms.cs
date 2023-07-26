using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.Parms;

public interface IModelParms<T>
{
    public void CopyFieldsToModel(T model);
}
