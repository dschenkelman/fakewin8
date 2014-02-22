module FakeWin8.Generator.Tests.FakeTypeSignatureGeneration

open Xunit
open FsUnit.Xunit
open System
open FakeWin8.Generator.Core
open FakeWin8.Generator.Tests.Helpers

[<Fact>] 
let ``should generate fake type signature for interface.`` () =
    let expectedSignature = "public class FakeInterface : IInterface"
    typeof<IInterface> 
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for class.`` () =
    let expectedSignature = "public class FakeAbstractClass : AbstractClass"
    typeof<AbstractClass> 
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface.`` () =
    let expectedSignature = "public class FakeGenericInterface<T> : IGenericInterface<T>"
    typedefof<IGenericInterface<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic abstract class.`` () =
    let expectedSignature = "public class FakeAbstractGenericClass<T> : AbstractGenericClass<T>"
    typedefof<AbstractGenericClass<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface with default constructor type constraint.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithDefaultConstructorConstraint<T> : IGenericInterfaceWithDefaultConstructorConstraint<T> where T : new()"
    typedefof<IGenericInterfaceWithDefaultConstructorConstraint<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface with reference type constraint.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithReferenceConstraint<T> : IGenericInterfaceWithReferenceConstraint<T> where T : class"
    typedefof<IGenericInterfaceWithReferenceConstraint<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface with struct type constraint.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithStructConstraint<T> : IGenericInterfaceWithStructConstraint<T> where T : struct"
    typedefof<IGenericInterfaceWithStructConstraint<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface when constraint is other argument.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithOtherArgumentConstraint<T, U> : IGenericInterfaceWithOtherArgumentConstraint<T, U> where T : U"
    typedefof<IGenericInterfaceWithOtherArgumentConstraint<_, _>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface with interface type constraint.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithInterfaceConstraint<T> : IGenericInterfaceWithInterfaceConstraint<T> where T : IInterface"
    typedefof<IGenericInterfaceWithInterfaceConstraint<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature

[<Fact>] 
let ``should generate fake type signature for generic interface with class type constraint.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithClassConstraint<T> : IGenericInterfaceWithClassConstraint<T> where T : AbstractClass"
    typedefof<IGenericInterfaceWithClassConstraint<_>>
    |> generateFakeSignature 
    |> should equal expectedSignature


[<Fact>] 
let ``should generate fake type signature for generic interface with multiple constraints.`` () =
    let expectedSignature = "public class FakeGenericInterfaceWithMultipleConstraints<T, U, V, W> : IGenericInterfaceWithMultipleConstraints<T, U, V, W> where T : Implementation, W, new() where U : struct, V"
    typedefof<IGenericInterfaceWithMultipleConstraints<Implementation,int,int,Implementation>>
    |> generateFakeSignature 
    |> should equal expectedSignature