module FakeWin8.Generator.Tests.GetFakeableTypes

open Xunit
open FsUnit.Xunit
open System
open FakeWin8.Generator.Core
open FakeWin8.Generator.Tests.Helpers

[<Fact>] 
let ``should return abstract and interface types.`` () =
    let expectedMethods = 4
    
    let types = [|typeof<ConcreteClass>; typeof<IInterface>; typeof<AbstractClass>|]

    let fakeableTypes = getFakeableTypes types

    Array.length fakeableTypes |> should equal 2

    fakeableTypes |> Array.exists (fun t -> t = typeof<IInterface>) |> should equal true

    fakeableTypes |> Array.exists (fun t -> t = typeof<AbstractClass>) |> should equal true