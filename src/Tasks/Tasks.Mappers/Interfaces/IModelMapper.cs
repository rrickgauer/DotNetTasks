using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Mappers.Interfaces;

public interface IModelMapper<T>
{
    public T ToModel(DataRow dataRow);
    public IEnumerable<T> ToModels(DataTable table) => table.AsEnumerable().Select(row => ToModel(row));
}
