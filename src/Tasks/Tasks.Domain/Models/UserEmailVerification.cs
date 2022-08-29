using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.CustomAttributes;

namespace Tasks.Domain.Models
{
    public class UserEmailVerification
    {
        [SqlColumn("id")]
        public Guid? Id { get; set; }

        [SqlColumn("user_id")]
        public Guid? UserId { get; set; }

        [SqlColumn("email")]
        public string? Email { get; set; }

        [SqlColumn("confirmed_on")]
        public DateTime? ConfirmedOn { get; set; }

        [SqlColumn("created_on")]
        public DateTime? CreatedOn { get; set; }

        public bool IsConfirmed => ConfirmedOn != null;
    }
}
