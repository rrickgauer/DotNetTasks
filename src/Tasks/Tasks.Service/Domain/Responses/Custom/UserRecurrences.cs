using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Responses.Custom;

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
