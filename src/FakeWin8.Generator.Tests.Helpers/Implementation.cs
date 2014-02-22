namespace FakeWin8.Generator.Tests.Helpers
{
    public class Implementation : IInterface
    {
        public Implementation()
        {
            var t = typeof(IGenericInterfaceWithMultipleConstraints<Implementation, int, int, IInterface>);
        }
    }
}
