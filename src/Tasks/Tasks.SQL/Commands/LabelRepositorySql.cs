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
    }
}
