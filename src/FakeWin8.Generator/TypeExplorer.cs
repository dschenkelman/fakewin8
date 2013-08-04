namespace FakeWin8.Generator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypeExplorer
    {
        private readonly Type type;

        public TypeExplorer(Type type)
        {
            this.type = type;
        }

        public IEnumerable<MethodInfo> GetFakeableMethods()
        {
            return this.type.GetMethods()
                .Where(m => m.GetParameters().Length <= 3 
                    && m.IsAbstract 
                    && m.IsPublic);
        }
    }
}
