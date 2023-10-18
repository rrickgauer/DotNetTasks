using System.Data;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class ChecklistTableViewMapper : ModelMapper<ChecklistView>
{
    public override ChecklistView ToModel(DataRow dataRow)
    {
        ChecklistView checklist = new()
        {
            Id                     = dataRow.Field<Guid?>(SqlColumn(nameof(ChecklistView.Id))),
            CommandLineReferenceId = dataRow.Field<uint?>(SqlColumn(nameof(ChecklistView.CommandLineReferenceId))),
            UserId                 = dataRow.Field<Guid?>(SqlColumn(nameof(ChecklistView.UserId))),
            Title                  = dataRow.Field<string>(SqlColumn(nameof(ChecklistView.Title))),
            ListType               = (ChecklistType)dataRow.Field<ushort>(SqlColumn(nameof(ChecklistView.ListType))),
            CreatedOn              = dataRow.Field<DateTime>(SqlColumn(nameof(ChecklistView.CreatedOn))),
            CountItems             = dataRow.Field<long>(SqlColumn(nameof(ChecklistView.CountItems))),
        };

        return checklist;
    }
}
