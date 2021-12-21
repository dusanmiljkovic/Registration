namespace Registration.Services.Exceptions;

/// <summary>
/// NotFoundException class.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="message">Custom exception message.</param>
    public NotFoundException(string message)
        : base(message)
    {
    }
}
