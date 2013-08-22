namespace FakeWin8.Generator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class FakeTypeSignatureGenerator
    {
        private readonly Type type;

        public FakeTypeSignatureGenerator(Type type)
        {
            this.type = type;
        }

        public static string GetFakeTypeName(Type type, string representation)
        {
            if (type.IsInterface && type.Name.StartsWith("I"))
            {
                return representation.Substring(1);
            }

            return representation;
        }

        public string Generate()
        {
            var typeGenerator = new TypeGenerator(this.type);
            string trimmedName = typeGenerator.Generate();

            trimmedName = GetFakeTypeName(this.type, trimmedName);

            var signature = string.Format("public class Fake{0} : {1}", trimmedName, typeGenerator.Generate());

            var contraints = this.GetTypeConstraints();

            if (!string.IsNullOrEmpty(contraints))
            {
                signature += " " + contraints;
            }

            return signature;
        }

        private string GetTypeConstraints()
        {
            var typeConstraints = new List<string>();

            foreach (var argument in this.type.GetGenericArguments())
            {
                var builder = new StringBuilder();
                var parameterConstraints = new List<string>();

                bool useClass = (argument.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0;
                bool useStruct = (argument.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0;
                bool useNew = (argument.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0;

                // the order is important. in regex form (class|struct)?(types)*(new())?
                if (useClass)
                {
                    parameterConstraints.Add("class");
                }

                if (useStruct)
                {
                    parameterConstraints.Add("struct");
                }

                parameterConstraints.AddRange(
                    argument.GetGenericParameterConstraints()
                            .Where(t => t.Name != "ValueType")
                            .Select(constraintType => new TypeGenerator(constraintType).Generate()));

                if (useNew && !useStruct)
                {
                    parameterConstraints.Add("new()");
                }

                if (parameterConstraints.Count == 0)
                {
                    continue;
                }

                builder.Append("where ");

                builder.Append(argument.Name);

                builder.Append(" : ");

                builder.Append(string.Join(", ", parameterConstraints.ToArray()));

                typeConstraints.Add(builder.ToString());
            }

            return string.Join(" ", typeConstraints.ToArray());
        }
    }
}