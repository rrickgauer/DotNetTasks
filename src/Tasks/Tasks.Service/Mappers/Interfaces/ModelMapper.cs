using System.Data;
using Tasks.Service.Utilities;

namespace Tasks.Service.Mappers.Interfaces;

public abstract class ModelMapper<T>
{
    public abstract T ToModel(DataRow dataRow);

    public IEnumerable<T> ToModels(DataTable dataTable)
    {
        List<T> models = new();

        foreach(DataRow dataRow in dataTable.AsEnumerable())
        {
            var model = ToModel(dataRow);
            models.Add(model);
        }

        return models;
    }

    protected string SqlColumn(string propertyName) 
    {
        return AttributeUtilities.GetSqlColumn<T>(propertyName);
    }
}


