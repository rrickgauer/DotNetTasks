using System.Data;
using Tasks.Domain.Models;

namespace Tasks.Mappers;


public static class LabelMapper
{
    /// <summary>
    /// Map the data rows in the data table to a collection of Labels
    /// </summary>
    /// <param name="dataTable"></param>
    /// <returns></returns>
    public static IEnumerable<Label> ToModels(DataTable dataTable)
    {
        var labels =
            from row
            in dataTable.AsEnumerable()
            select ToModel(row);

        return labels;
    }


    /// <summary>
    /// Generate a new Label object from the given datarow
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public static Label ToModel(DataRow dataRow)
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
