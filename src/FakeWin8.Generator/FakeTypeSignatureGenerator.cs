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

            if (this.type.IsInterface && this.type.Name.StartsWith("I"))
            {
                trimmedName = trimmedName.Substring(1);
            }

            return string.Format("public class Fake{0} : {1}", trimmedName, typeGenerator.Generate());
        }
    }
}