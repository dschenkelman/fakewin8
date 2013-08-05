namespace FakeWin8.Exceptions
{
    using System;

    public class InvalidInvocationException : Exception
    {
        public InvalidInvocationException()
        {
        }

        public InvalidInvocationException(string message) : base(message)
        {
        }
    }
}
