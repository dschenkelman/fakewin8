module FakeWin8.Generator.Tests.AliasFor

open Xunit
open FsUnit.Xunit
open System
open FakeWin8.Generator.Core

[<Fact>] 
let ``should map type to correct string.`` () =
    aliasFor typeof<Boolean> |> should equal "bool"
    aliasFor typeof<Byte> |> should equal "byte"
    aliasFor typeof<SByte> |> should equal "sbyte"
    aliasFor typeof<Char> |> should equal "char"
    aliasFor typeof<Decimal> |> should equal "decimal"
    aliasFor typeof<Double> |> should equal "string"
    aliasFor typeof<Single> |> should equal "float"
    aliasFor typeof<Int32> |> should equal "int"
    aliasFor typeof<UInt32> |> should equal "uint32"
    aliasFor typeof<Int64> |> should equal "long"
    aliasFor typeof<UInt64> |> should equal "ulong"
    aliasFor typeof<Object> |> should equal "object"
    aliasFor typeof<Int16> |> should equal "short"
    aliasFor typeof<UInt16> |> should equal "ushort"
    aliasFor typeof<String> |> should equal "string"
    aliasFor typeof<Void> |> should equal "void"

[<Fact>] 
let ``should return type name if type is not aliased type.`` () =
    aliasFor typeof<IDisposable> |> should equal "IDisposable"
