namespace FakeWin8.Generator.Console
{
    using System;
    using System.IO;
    using System.Reflection;

    using FakeWin8.Generator.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage FakeWin8.Generator.Console.exe <dllPath> <outputDir>");
                return;
            }

            var dllPath = args[0];
            var outputDirectory = args[1];

            if (!File.Exists(dllPath))
            {
                Console.WriteLine("Assembly could not be found.");
                return;
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
                Console.WriteLine("Created output directory...");
                return;
            }

            Assembly assembly = Assembly.LoadFrom(dllPath);

            var types = new AssemblyExplorer(assembly).GetFakeableTypes();

            foreach (var type in types)
            {
                var fileContent = new ClassGenerator(type).Generate();

                var fileName = string.Format("Fake{0}.cs", FakeTypeSignatureGenerator.GetFakeTypeName(type, type.Name));

                var outFile = Path.Combine(outputDirectory, fileName);
                File.WriteAllText(outFile, fileContent);

                Console.WriteLine("Wrote file {0}", outFile);
            }
        }
    }
}
