namespace FakeWin8.Generator.Console
{
    using System;

    public interface IInterface
    {
        void Method1();

        string Method2(int param);

        int Method3(Tuple<int, int> t, int i, string s);
    }
}
