namespace FakeWin8
{
    using System.Collections.Generic;

    public interface IFakeAction<T1>
    {
        IEnumerable<Invocation<T1>> Invocations { get; }

        void Invoke(T1 param1);
    }
}
