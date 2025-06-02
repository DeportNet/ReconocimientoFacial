namespace DeportNetReconocimiento.Api.GlobalExceptionHandler.Exceptions
{
    public class HikvisionException : Exception
    {
        public HikvisionException(string message) : base(message)
        {
        }

        public HikvisionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
