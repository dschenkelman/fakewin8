namespace FakeWin8.Generator.Console
{
    using System;
    using System.Reflection;

    using FakeWin8.Generator.Core;

    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = typeof(Program).Assembly;

            var types = new AssemblyExplorer(assembly).GetFakeableTypes();

            foreach (var type in types)
            {
                System.Console.WriteLine(new FakeTypeSignatureGenerator(type).Generate());
                System.Console.WriteLine("{");

                var methods = new TypeExplorer(type).GetFakeableMethods();

                foreach (var methodInfo in methods)
                {
                    var property = new FakeMethodPropertyGenerator(methodInfo).Generate();
                    System.Console.WriteLine(property);
                    System.Console.WriteLine(Environment.NewLine);
                }

                foreach (var methodInfo in methods)
                {
                    var property = new FakeMethodImplementationGenerator(methodInfo).Generate();
                    System.Console.WriteLine(property);
                    System.Console.WriteLine(Environment.NewLine);
                }

                System.Console.WriteLine("}");
            }

            System.Console.ReadLine();
        }
    }
}
