namespace Registration.Services.Exceptions;

/// <summary>
/// UniqueException class.
/// </summary>
public class UniqueException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueException"/> class.
    /// </summary>
    public UniqueException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueException"/> class.
    /// </summary>
    /// <param name="message">Custom exception message.</param>
    public UniqueException(string message)
        : base(message)
    {
    }
}
