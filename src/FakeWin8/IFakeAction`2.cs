namespace FakeWin8
{
    using System.Collections.Generic;

    public interface IFakeAction<T1, T2>
    {
        IEnumerable<Invocation<T1, T2>> Invocations { get; }

        void Invoke(T1 param1, T2 param2);
    }
}
