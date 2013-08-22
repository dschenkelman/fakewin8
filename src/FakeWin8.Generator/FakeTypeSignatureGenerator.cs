namespace FakeWin8.Generator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class FakeTypeSignatureGenerator
    {
        private const string ParameterConstraintFormat = "where {0} : {1}";

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

            var constraints = this.GetConstraints();

            var signature = string.Format("public class Fake{0} : {1}", trimmedName, typeGenerator.Generate());

            if (constraints.Count != 0)
            {
                signature += " " + string.Join(",", constraints.ToArray());
            }

            return signature;
        }

        private List<string> GetConstraints()
        {
            var constraints = new List<string>();

            foreach (var argument in this.type.GetGenericArguments())
            {
                var genericArgumentName = argument.Name;

                string nonTypedConstraint = GetNonTypedConstraint(argument, genericArgumentName);

                if (nonTypedConstraint != null)
                {
                    constraints.Add(nonTypedConstraint);
                }

                constraints.AddRange(argument
                    .GetGenericParameterConstraints().Where(t => t.Name != "ValueType")
                    .Select(constraintType => string.Format(
                        ParameterConstraintFormat, 
                        genericArgumentName, 
                        new TypeGenerator(constraintType).Generate())));
            }

            return constraints;
        }

        private static string GetNonTypedConstraint(Type argument, string genericArgumentName)
        {
            if ((argument.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
            {
                return string.Format(ParameterConstraintFormat, genericArgumentName, "class");
            }
            
            if ((argument.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
            {
                return string.Format(ParameterConstraintFormat, genericArgumentName, "struct");
            }
            
            if ((argument.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0)
            {
                return string.Format(ParameterConstraintFormat, genericArgumentName, "new()");
            }

            return null;
        }
    }
}