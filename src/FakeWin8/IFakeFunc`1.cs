namespace FakeWin8
{
    using System.Collections.Generic;

    public interface IFakeFunc<T1, out TResult>
    {
        IEnumerable<Invocation<T1>> Invocations { get; }

        TResult Invoke(T1 param1);
    }
}
