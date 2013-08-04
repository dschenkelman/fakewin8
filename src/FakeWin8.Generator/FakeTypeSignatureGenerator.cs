namespace FakeWin8.Generator.Core
{
    using System;

    public class FakeTypeSignatureGenerator
    {
        private readonly Type type;

        public FakeTypeSignatureGenerator(Type type)
        {
            this.type = type;
        }

        public string Generate()
        {
            var typeGenerator = new TypeGenerator(this.type);
            string trimmedName = typeGenerator.Generate();

            trimmedName = GetFakeTypeName(this.type, trimmedName);

            return string.Format("public class Fake{0} : {1}", trimmedName, typeGenerator.Generate());
        }

        public static string GetFakeTypeName(Type type, string representation)
        {
            if (type.IsInterface && type.Name.StartsWith("I"))
            {
                return representation.Substring(1);
            }

            return representation;
        }
    }
}