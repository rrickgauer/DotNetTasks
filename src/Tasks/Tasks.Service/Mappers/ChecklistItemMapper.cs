using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class ChecklistItemMapper : ModelMapper<ChecklistItem>
{
    public override ChecklistItem ToModel(DataRow dataRow)
    {
        ChecklistItem item = new()
        {
            Id          = dataRow.Field<Guid?>(SqlColumn(nameof(ChecklistItem.Id))),
            ChecklistId = dataRow.Field<Guid?>(SqlColumn(nameof(ChecklistItem.ChecklistId))),
            Content     = dataRow.Field<string?>(SqlColumn(nameof(ChecklistItem.Content))),
            CreatedOn   = dataRow.Field<DateTime>(SqlColumn(nameof(ChecklistItem.CreatedOn))),
            CompletedOn = dataRow.Field<DateTime?>(SqlColumn(nameof(ChecklistItem.CompletedOn))),
            Position    = dataRow.Field<uint>(SqlColumn(nameof(ChecklistItem.Position))),
        };

        return item;
    }
}
