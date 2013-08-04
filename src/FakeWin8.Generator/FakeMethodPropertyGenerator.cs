namespace FakeWin8.Generator.Core
{
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class FakeMethodPropertyGenerator
    {
        private readonly MethodInfo methodInfo;

        public FakeMethodPropertyGenerator(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        public string Generate()
        {
            var builder = new StringBuilder();

            builder.Append("public Fake");

            var returnType = this.methodInfo.ReturnType;

            var returnsVoid = returnType == typeof(void);

            var methodKind = returnsVoid ? "Action" : "Func";

            builder.Append(methodKind);

            var parameters = this.methodInfo.GetParameters();

            if (parameters.Length != 0 || !returnsVoid)
            {
                builder.Append("<");
            }

            builder.Append(string.Join(",", parameters.Select(p => new TypeGenerator(p.ParameterType).Generate()).ToArray()));

            if (!returnsVoid)
            {
                if (parameters.Length != 0)
                {
                    builder.Append(",");
                }

                builder.Append(new TypeGenerator(returnType).Generate());
            }

            if (parameters.Length != 0 || !returnsVoid)
            {
                builder.Append(">");
            }

            builder.Append(" ");
            builder.Append(this.methodInfo.Name);
            builder.Append(methodKind);

            builder.Append(" { get; set; }");

            return builder.ToString();
        }
    }
}