using System.Data;
using Tasks.CustomAttributes;
using Tasks.Domain.Models;

namespace Tasks.Mappers
{
    public static class EventMapper
    {

        #region Old shit commented out

        //public static Event ToModel(DataRow dr)
        //{
        //	Event e = new();
        //	Type t = e.GetType();
        //	var props = t.GetProperties();

        //	foreach (var eventObjectProperty in props)
        //          {
        //		// assume that the sql column name is the name of the object's property
        //		string sqlColumnName = eventObjectProperty.Name;

        //		// see if the property has an SqlColumn attribute value
        //		var customAttributeSearchResult = from attr in eventObjectProperty.GetCustomAttributes(false) where attr.GetType() == typeof(SqlColumn) select attr;

        //		// if it does, use that value for the sql column name
        //		if (customAttributeSearchResult != null)
        //		{
        //			SqlColumn sqlColumnAttribute = (SqlColumn)customAttributeSearchResult.First();
        //			sqlColumnName = sqlColumnAttribute.ColumnName;
        //		}

        //		if (dr.IsNull(sqlColumnName))
        //              {
        //			eventObjectProperty.SetValue(e, null);
        //              }
        //              else
        //              {
        //			try
        //			{
        //				eventObjectProperty.SetValue(e, dr[sqlColumnName]);
        //			}
        //			catch (Exception ex)
        //			{ 
        //				var message = ex.Message;
        //			}
        //		}

        //          }

        //	return e;
        //}

        #endregion

        /// <summary>
        /// Transform the given datarow into an Event domain model
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static Event ToModel(DataRow dataRow)
        {
            Event e = new()
            {
                Id              = dataRow.Field<Guid?>("id"),
                UserId          = dataRow.Field<Guid?>("user_id"),
                Name            = dataRow.Field<string?>("name"),
                Description     = dataRow.Field<string?>("description"),
                PhoneNumber     = dataRow.Field<string?>("phone_number"),
                Location        = dataRow.Field<string?>("location"),
                StartsOn        = dataRow.Field<DateTime?>("starts_on"),
                EndsOn          = dataRow.Field<DateTime?>("ends_on"),
                StartsAt        = dataRow.Field<TimeSpan?>("starts_at"),
                EndsAt          = dataRow.Field<TimeSpan?>("ends_at"),
                //Frequency     = dto.Field<?string>("frequency"),
                Separation      = dataRow.Field<uint?>("separation"),
                CreatedOn       = dataRow.Field<DateTime?>("created_on"),
                RecurrenceDay   = dataRow.Field<int?>("recurrence_day"),
                RecurrenceWeek  = dataRow.Field<int?>("recurrence_week"),
                RecurrenceMonth = dataRow.Field<int?>("recurrence_month"),
            };
            return e;
        }

	}
}
