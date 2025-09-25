namespace deneme_2.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string? message) : base(message)
        {
        }
    }
}
