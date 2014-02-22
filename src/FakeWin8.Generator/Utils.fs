module FakeWin8.Generator.Utils

open System

let concatNonEmpty (separator:string) (sequence:seq<string>) =
    sequence
    |> Seq.filter (fun s -> not(String.IsNullOrEmpty s))
    |> String.concat separator