namespace FakeWin8.Generator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class AssemblyExplorer
    {
        private readonly Assembly assembly;

        public AssemblyExplorer(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public IEnumerable<Type> GetFakeableTypes()
        {
            return this.assembly.GetTypes().Where(t => t.IsAbstract || t.IsInterface);
        }
    }
}
