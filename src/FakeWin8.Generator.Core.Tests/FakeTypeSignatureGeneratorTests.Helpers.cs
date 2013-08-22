namespace FakeWin8.Generator.Core.Tests
{
    public partial class FakeTypeSignatureGeneratorTests
    {
        private interface IInterface
        {
        }

        private interface IGenericInterface<T>
        {
        }

        private interface IGenericInterfaceWithDefaultConstructorConstraint<T> where T : new()
        {
        }

        private interface IGenericInterfaceWithReferenceConstraint<T> where T : class
        {
        }

        private interface IGenericInterfaceWithStructConstraint<T> where T : struct
        {
        }

        private interface IGenericInterfaceWithOtherArgumentConstraint<T, U> where T : U
        {
        }

        private interface IGenericInterfaceWithInterfaceConstraint<T> where T : IInterface
        {
        }

        private interface IGenericInterfaceWithClassConstraint<T> where T : AbstractClass
        {
        }

        private interface IGenericInterfaceWithMultipleConstraints<T, U, V, W> 
            where T : class, IInterface, W, new()
            where U : struct, V
        {
        }

        private abstract class AbstractClass
        {
        }

        private abstract class AbstractGenericClass<T>
        {
        }
    }
}
