namespace FakeWin8.Generator.Core
{
    using System;
    using System.Linq;
    using System.Text;

    public class ClassGenerator
    {
        private readonly Type type;

        public ClassGenerator(Type type)
        {
            this.type = type;
        }

        public string Generate()
        {
            var builder = new StringBuilder();
            builder.Append(new FakeTypeSignatureGenerator(this.type).Generate());
            builder.Append(Environment.NewLine);
            builder.Append("{");
            builder.Append(Environment.NewLine);

            var methods = new TypeExplorer(this.type).GetFakeableMethods();

            builder.Append(
                string.Join(
                    Environment.NewLine + Environment.NewLine,
                    methods.Select(methodInfo => new FakeMethodPropertyGenerator(methodInfo).Generate()).ToArray()));

            builder.Append(
                string.Join(
                    Environment.NewLine + Environment.NewLine,
                    methods.Select(methodInfo => new FakeMethodImplementationGenerator(methodInfo).Generate()).ToArray()));

            builder.Append(Environment.NewLine);

            builder.Append("}");

            return builder.ToString();
        }
    }
}