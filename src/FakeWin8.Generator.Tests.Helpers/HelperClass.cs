using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeWin8.Generator.Tests.Helpers
{
    public abstract class HelperClass
    {
        public abstract void Method1();

        public abstract string Method2(int p1);

        public abstract double Method3(int p1, int p2);

        public abstract int Method4(int p1, int p2, int p3);

        // to be excluded, 4 parameters
        public abstract void Method5(int p1, int p2, int p3, int p4);

        // to be excluded, 5 parameters
        public abstract void Method6(int p1, int p2, int p3, int p4, int p5);

        // to be excluded, protected
        protected abstract void Method7();

        // to be excluded, abstract
        public void Method8()
        {
        }
    }
}
