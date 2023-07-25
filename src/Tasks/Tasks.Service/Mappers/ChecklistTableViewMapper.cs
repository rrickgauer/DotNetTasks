using System.Data;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class ChecklistTableViewMapper : ModelMapper<ChecklistTableView>
{
    public override ChecklistTableView ToModel(DataRow dataRow)
    {
        ChecklistTableView checklist = new()
        {
            Id = dataRow.Field<Guid?>(SqlColumn(nameof(Checklist.Id))),
            UserId = dataRow.Field<Guid?>(SqlColumn(nameof(Checklist.UserId))),
            Title = dataRow.Field<string>(SqlColumn(nameof(Checklist.Title))),
            ListType = (ChecklistType)dataRow.Field<ushort>(SqlColumn(nameof(Checklist.ListType))),
            CreatedOn = dataRow.Field<DateTime>(SqlColumn(nameof(Checklist.CreatedOn))),
            CountItems = dataRow.Field<long>(SqlColumn(nameof(ChecklistTableView.CountItems))),
        };

        return checklist;
    }
}
