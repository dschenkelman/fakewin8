namespace FakeWin8
{
    using System.Collections.Generic;

    public interface IFakeAction<T1, T2, T3>
    {
        IEnumerable<Invocation<T1, T2, T3>> Invocations { get; }

        void Invoke(T1 param1, T2 param2, T3 param3);
    }
}
