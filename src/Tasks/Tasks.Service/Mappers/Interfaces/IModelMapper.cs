using System.Data;

namespace Tasks.Service.Mappers.Interfaces;

public interface IModelMapper<T>
{
    public T ToModel(DataRow dataRow);
    public IEnumerable<T> ToModels(DataTable table) => table.AsEnumerable().Select(row => ToModel(row));
}
