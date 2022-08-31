using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Views
{
    public class RecurrencesForUser
    {
        public User User { get; set; }
        public List<Recurrence> Recurrences { get; set; } = new();

        public RecurrencesForUser(User user)
        {
            User = user;
        }

        public RecurrencesForUser(User user, List<Recurrence> recurrences)
        {
            User = user;
            Recurrences = recurrences;
        }   
    }
}
