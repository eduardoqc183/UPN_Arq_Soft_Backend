namespace Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {

        }
    }

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string msg) : base(msg)
        {

        }
    }
}
