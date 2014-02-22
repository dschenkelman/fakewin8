namespace FakeWin8.Generator.Tests.Helpers
{
    public interface IGenericInterfaceWithMultipleConstraints<T, U, V, W>
        where T : Implementation, W, new()
        where U : struct, V
    {
    }
}
