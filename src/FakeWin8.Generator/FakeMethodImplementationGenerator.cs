namespace FakeWin8.Generator.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class FakeMethodImplementationGenerator
    {
        private readonly MethodInfo methodInfo;

        public FakeMethodImplementationGenerator(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        public string Generate()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("public ");

            builder.Append(new TypeGenerator(this.methodInfo.ReturnType).Generate());

            builder.Append(" ");

            builder.Append(this.methodInfo.Name);

            builder.Append("(");

            var parameters = this.methodInfo.GetParameters();

            builder.Append(
                string.Join(
                    ", ",
                    parameters.Select(p => string.Format("{0} {1}", new TypeGenerator(p.ParameterType).Generate(), p.Name)).ToArray()));

            builder.Append(")");
            builder.Append(Environment.NewLine);
            builder.Append("{");
            builder.Append(Environment.NewLine);

            var returnsVoid = this.methodInfo.ReturnType == typeof(void);

            var methodKind = returnsVoid ? "Action" : "Func";

            if (!returnsVoid)
            {
                builder.Append("return ");
            }

            builder.Append("this.");
            builder.Append(this.methodInfo.Name);
            builder.Append(methodKind);
            builder.Append(".Invoke(");

            builder.Append(string.Join(",", parameters.Select(p => p.Name).ToArray()));

            builder.Append(");");
            builder.Append(Environment.NewLine);
            builder.Append("}");

            return builder.ToString();
        }
    }
}
