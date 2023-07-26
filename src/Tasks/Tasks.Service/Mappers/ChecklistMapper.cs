using System.Data;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;


public class ChecklistMapper : ModelMapper<Checklist>
{
    public override Checklist ToModel(DataRow dataRow)
    {
        Checklist checklist = new()
        {
            Id = dataRow.Field<Guid?>(SqlColumn(nameof(Checklist.Id))),
            UserId = dataRow.Field<Guid?>(SqlColumn(nameof(Checklist.UserId))),
            Title = dataRow.Field<string>(SqlColumn(nameof(Checklist.Title))),
            ListType = (ChecklistType)dataRow.Field<ushort>(SqlColumn(nameof(Checklist.ListType))),
            CreatedOn = dataRow.Field<DateTime>(SqlColumn(nameof(Checklist.CreatedOn))),
        };

        return checklist;
    }
}
