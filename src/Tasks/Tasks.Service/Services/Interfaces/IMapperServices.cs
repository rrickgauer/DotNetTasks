using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Services.Interfaces;

public interface IMapperServices
{
    public T ToModel<T>(DataRow dataRow);
    public IEnumerable<T> ToModels<T>(DataTable dataTable);
    public ModelMapper<T> GetMapper<T>();
}
