namespace FakeWin8
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
