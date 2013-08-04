namespace FakeWin8.Generator.Console
{
    using System;

    public abstract class AbstractClass
    {
        public abstract void Test1();

        public abstract string Test2(int param);

        public abstract int Test3(string s);
        
        public abstract AbstractClass Test4(Tuple<int, int> t, int i, string s);
    }
}
