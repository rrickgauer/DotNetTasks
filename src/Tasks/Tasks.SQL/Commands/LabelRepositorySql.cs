using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.SQL.Commands
{
    public sealed class LabelRepositorySql
    {
        /// <summary>
        /// Select all the user labels
        /// Order by name
        /// </summary>
        public const string SelectAllByUserId = @"
            SELECT
                *
            FROM
                View_Labels l
            WHERE
                l.user_id = @user_id
            ORDER BY
                l.name ASC";

        public const string Modify = @"
            INSERT INTO
                Labels (id, user_id, name, color, created_on)
            VALUES
                (@id, @user_id, @name, @color, @created_on) AS new_values 
            ON DUPLICATE KEY UPDATE
                name = new_values.name,
                color = new_values.color";
    }
}
