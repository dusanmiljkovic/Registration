namespace Registration.Api.Exceptions;
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RacesNotFoundException"/> class.
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RacesNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Custom exception message.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
