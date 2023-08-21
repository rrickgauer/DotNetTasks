using System.Data;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class ChecklistLabelViewMapper : ModelMapper<ChecklistLabelView>
{
    public override ChecklistLabelView ToModel(DataRow dataRow)
    {
        ChecklistLabelView result = new()
        {
            ChecklistId        = dataRow.Field<Guid?>("checklist_id"),
            ChecklistUserId    = dataRow.Field<Guid?>("checklist_user_id"),
            ChecklistTitle     = dataRow.Field<string?>("checklist_title"),
            ChecklistTypeId    = (ChecklistType)dataRow.Field<ushort>("checklist_type_id"),
            ChecklistCreatedOn = dataRow.Field<DateTime?>("checklist_created_on"),
            LabelId            = dataRow.Field<Guid?>("label_id"),
            LabelUserId        = dataRow.Field<Guid?>("label_user_id"),
            LabelName          = dataRow.Field<string?>("label_name"),
            LabelColor         = dataRow.Field<string?>("label_color"),
            LabelCreatedOn     = dataRow.Field<DateTime?>("label_created_on"),
            CreatedOn          = dataRow.Field<DateTime?>("created_on"),
        };


        return result;
    }
}
