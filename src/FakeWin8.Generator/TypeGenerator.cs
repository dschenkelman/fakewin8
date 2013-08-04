namespace FakeWin8.Generator.Core
{
    using System;
    using System.Linq;
    using System.Text;

    public class TypeGenerator
    {
        private readonly Type type;

        public TypeGenerator(Type type)
        {
            this.type = type;
        }

        public string Generate()
        {
            if (!this.type.IsGenericType)
            {
                return AliasMappings.GetAliasForType(this.type);
            }

            var builder = new StringBuilder();
            builder.Append(this.type.Name.Substring(0, this.type.Name.IndexOf("`", StringComparison.Ordinal)));
            builder.Append("<");

            builder.Append(string.Join(", ",  this.type.GetGenericArguments().Select(t => new TypeGenerator(t).Generate()).ToArray()));

            builder.Append(">");

            return builder.ToString();
        }
    }
}
