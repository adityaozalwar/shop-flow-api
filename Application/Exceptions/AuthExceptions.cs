namespace ShopFlowAPI.Exceptions;

public sealed class InvalidCredentialsException : Exception
{
}

public sealed class AuthValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public AuthValidationException(IEnumerable<string> errors)
        : base("Authentication validation failed.")
    {
        Errors = errors.ToArray();
    }
}