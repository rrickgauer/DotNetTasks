using System.Data;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class ChecklistItemViewMapper : ModelMapper<ChecklistItemView>
{
    public override ChecklistItemView ToModel(DataRow dataRow)
    {
        ChecklistItemView item = new()
        {
            Id = dataRow.Field<Guid?>(SqlColumn(nameof(ChecklistItemView.Id))),
            ChecklistId = dataRow.Field<Guid?>(SqlColumn(nameof(ChecklistItemView.ChecklistId))),
            Content = dataRow.Field<string?>(SqlColumn(nameof(ChecklistItemView.Content))),
            CreatedOn = dataRow.Field<DateTime>(SqlColumn(nameof(ChecklistItemView.CreatedOn))),
            CompletedOn = dataRow.Field<DateTime?>(SqlColumn(nameof(ChecklistItemView.CompletedOn))),
            Position = dataRow.Field<uint>(SqlColumn(nameof(ChecklistItemView.Position))),
            CommandLineReference = dataRow.Field<uint?>(SqlColumn(nameof(ChecklistItemView.CommandLineReference))),
            ChecklistCommandLineReference = dataRow.Field<uint?>(SqlColumn(nameof(ChecklistItemView.ChecklistCommandLineReference))),
        };

        return item;
    }
}
