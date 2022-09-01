using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Views
{
    public class UserRecurrences
    {
        public User User { get; set; }
        public List<Recurrence> Recurrences { get; set; } = new();

        public UserRecurrences(User user)
        {
            User = user;
        }

        public UserRecurrences(User user, List<Recurrence> recurrences)
        {
            User = user;
            Recurrences = recurrences;
        }   
    }
}
