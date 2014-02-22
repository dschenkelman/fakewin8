module FakeWin8.Generator.Tests.GetFakeableMethods

open Xunit
open FsUnit.Xunit
open System
open FakeWin8.Generator.Core
open FakeWin8.Generator.Tests.Helpers

[<Fact>] 
let ``should only return public abstract methods with less than four parameters.`` () =
    let expectedMethods = 4
    
    let typeToUse = typeof<HelperClass>
    let methods = getFakeableMethods typeToUse

    Array.length methods |> should equal expectedMethods

    [|1..expectedMethods|]
    |> Array.map (fun i -> sprintf "Method%i" <| i)
    |> Array.iter (fun n -> (methods |> Array.exists (fun m -> m.Name = n) |> should equal true))