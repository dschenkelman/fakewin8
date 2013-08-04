namespace FakeWin8.Generator.Console
{
    using System;

    public interface IInterface
    {
        void Test1();

        string Test2(int param);

        int Test3(string s);

        AbstractClass Test4(Tuple<int, int> t, int i, string s);
    }
}
