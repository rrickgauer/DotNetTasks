namespace Tasks.Service.Domain.Constants;

public class InvalidSignUpRequestErrorMessages
{
    public const string InvalidPasswordLengthMessage = "Password value length must fall between 7-250.";
    public const string InvalidEmailLengthMessage = "Email value length must not exceed 100.";
    public const string EmailIsTakenMessage = "The email provided has already been taken.";
                
}
