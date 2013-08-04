namespace FakeWin8
{
    using System.Collections.Generic;

    public interface IFakeFunc<T1, T2, T3, out TResult>
    {
        IEnumerable<Invocation<T1, T2, T3>> Invocations { get; }

        TResult Invoke(T1 param1, T2 param2, T3 param3);
    }
}
