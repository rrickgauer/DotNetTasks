using System.Data;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Services.Interfaces;

public interface IMapperServices
{
    public T ToModel<T>(DataRow dataRow);
    public IEnumerable<T> ToModels<T>(DataTable dataTable);
    public ModelMapper<T> GetMapper<T>();
}
