namespace Services.Exceptions
{
    public class Exceptions
    {
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }

    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }
    }
}
