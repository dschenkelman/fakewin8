namespace FakeWin8.Conditions
{
    using System;

    public static class Any<T>
    {
        public static Func<T, bool> IsOK()
        {
            return o => true;
        }
    }
}