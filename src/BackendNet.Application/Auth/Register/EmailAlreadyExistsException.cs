namespace BackendNet.Application.Auth.Register;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException()
        : base("Email already exists")
    {
    }
}