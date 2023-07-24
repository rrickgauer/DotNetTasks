namespace Tasks.Service.Domain.Enums;

public enum ValidateUserResult : short
{
    Valid = 0,
    InvalidPasswordLength = 1,
    InvalidEmailLength = 2,
    EmailIsTaken = 3,
}
