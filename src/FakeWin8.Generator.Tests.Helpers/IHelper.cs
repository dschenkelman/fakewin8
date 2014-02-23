using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeWin8.Generator.Tests.Helpers
{
    public interface IHelper
    {
        void NoParametersReturnsVoid();

        void OneSimpleParameterReturnsVoid(int p1);

        void ThreeGenericParametersReturnsVoid(IEnumerable<int> p1, Task<double> p2, Tuple<char, string> p3);

        int NoParametersReturnsInt();

        string OneSimpleParameterReturnsString(int p1);

        Tuple<Task<string>, double> ThreeGenericParametersReturnsGeneric(IEnumerable<int> p1, Task<double> p2, Tuple<char, string> p3);
    }
}
