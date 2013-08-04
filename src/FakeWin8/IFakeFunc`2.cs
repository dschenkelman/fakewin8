namespace FakeWin8
{
    using System.Collections.Generic;

    public interface IFakeFunc<T1, T2, out TResult>
    {
        IEnumerable<Invocation<T1, T2>> Invocations { get; }

        TResult Invoke(T1 param1, T2 param2);
    }
}
