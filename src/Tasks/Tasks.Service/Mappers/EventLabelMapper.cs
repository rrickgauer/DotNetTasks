using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class EventLabelMapper : ModelMapper<EventLabel>
{

    public override EventLabel ToModel(DataRow dataRow)
    {
        EventLabel eventLabel = new()
        {
            EventId = dataRow.Field<Guid?>("event_id"),
            LabelId = dataRow.Field<Guid?>("label_id"),
            CreatedOn = dataRow.Field<DateTime?>("created_on"),
        };

        return eventLabel;
    }
}
