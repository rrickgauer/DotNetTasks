using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;


public class LabelMapper : ModelMapper<Label>
{

    /// <summary>
    /// Generate a new Label object from the given datarow
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public override Label ToModel(DataRow dataRow)
    {
        Label label = new()
        {
            Id        = dataRow.Field<Guid?>("id"),
            UserId    = dataRow.Field<Guid?>("user_id"),
            Name      = dataRow.Field<string?>("name"),
            CreatedOn = dataRow.Field<DateTime?>("created_on"),
            Color     = dataRow.Field<string?>("color"),
        };

        return label;
    }
}
