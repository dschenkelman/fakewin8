namespace FakeWin8.Generator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AssemblyExplorer
    {
        private readonly IEnumerable<Type> assemblyTypes;

        public AssemblyExplorer(IEnumerable<Type> assemblyTypes)
        {
            this.assemblyTypes = assemblyTypes;
        }

        public IEnumerable<Type> GetFakeableTypes()
        {
            return this.assemblyTypes.Where(t => t.IsAbstract || t.IsInterface);
        }
    }
}
