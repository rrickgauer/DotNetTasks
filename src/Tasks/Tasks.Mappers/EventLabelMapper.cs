using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Mappers
{
    public static class EventLabelMapper
    {

        public static IEnumerable<EventLabel> ToModels(DataTable dataTable)
        {
            var eventLabels = dataTable.AsEnumerable().Select(e => ToModel(e));

            return eventLabels;
        }

        public static EventLabel ToModel(DataRow dataRow)
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
}
