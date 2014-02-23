module FakeWin8.Generator.Tests.FakeMethodPropertyGeneration

open Xunit
open FsUnit.Xunit
open System
open FakeWin8.Generator.Core
open FakeWin8.Generator.Tests.Helpers

[<Fact>] 
let ``should generate property for method returning void and receiving no parameteres.`` () =
    let methodName = "NoParametersReturnsVoid"
    let expectedProperty = sprintf "public FakeAction %sAction { get; set; }" methodName
    typeof<IHelper>.GetMethod(methodName)
    |> generateFakeProperty 
    |> should equal expectedProperty

[<Fact>] 
let ``should generate property for method returning void and receiving one simple parameter.`` () =
    let methodName = "OneSimpleParameterReturnsVoid"
    let expectedProperty = sprintf "public FakeAction<int> %sAction { get; set; }" methodName
    typeof<IHelper>.GetMethod(methodName)
    |> generateFakeProperty 
    |> should equal expectedProperty

[<Fact>] 
let ``should generate property for method returning void and receiving three generic parameter.`` () =
    let methodName = "ThreeGenericParametersReturnsVoid"
    let expectedProperty = sprintf "public FakeAction<IEnumerable<int>, Task<double>, Tuple<char, string>> %sAction { get; set; }" methodName
    
    typeof<IHelper>.GetMethod(methodName) 
    |> generateFakeProperty 
    |> should equal expectedProperty

[<Fact>]
let ``should generate property for method returning int and receiving one simple parameter.`` () =
    let methodName = "OneSimpleParameterReturnsString"
    let expectedProperty = sprintf "public FakeFunc<int, string> %sFunc { get; set; }" methodName
    
    typeof<IHelper>.GetMethod(methodName) 
    |> generateFakeProperty 
    |> should equal expectedProperty

[<Fact>]
let ``should generate property for method returning int and receiving three generic parameters.`` () =
    let methodName = "ThreeGenericParametersReturnsGeneric"
    let expectedProperty = sprintf "public FakeFunc<IEnumerable<int>, Task<double>, Tuple<char, string>, Tuple<Task<string>, double>> %sFunc { get; set; }" methodName
    
    typeof<IHelper>.GetMethod(methodName) 
    |> generateFakeProperty 
    |> should equal expectedProperty