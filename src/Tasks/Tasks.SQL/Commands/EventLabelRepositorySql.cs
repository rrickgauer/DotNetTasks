using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.SQL.Commands;

public class EventLabelRepositorySql
{
    /// <summary>
    /// @event_id
    /// @label_id
    /// @created_on
    /// @user_id
    /// </summary>
    public const string Insert = @"
        REPLACE INTO Event_Labels (event_id, label_id, created_on)
        SELECT
            @event_id AS event_id,
            @label_id AS label_id,
            @created_on
        WHERE
            EXISTS (
                SELECT
                    1
                FROM
                    Events e
                WHERE
                    e.id = @event_id
                    AND e.user_id = @user_id
            )
            AND EXISTS (
                SELECT
                    1
                FROM
                    Labels l
                WHERE
                    l.id = @label_id
                    AND l.user_id = @user_id
            )";


    /// <summary>
    /// Fetch all the assigned labels to a User's event
    /// 
    /// Parms:
    /// @event_id
    /// @user_id
    /// </summary>
    public const string SelectAllByIdAndUserId = @"
        SELECT
            l.*
        FROM
            Event_Labels el
            LEFT JOIN Labels l ON l.id = el.label_id
        WHERE
            el.event_id = @event_id
            AND EXISTS (
                SELECT
                    1
                FROM
                    Events e
                WHERE
                    e.id = el.event_id
                    AND e.user_id = @user_id
            )";
}
