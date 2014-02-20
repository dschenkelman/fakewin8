module FakeWin8.Generator

open System

let aliasFor t = 
    match t with
        | t when t = typeof<Boolean> -> "bool"
        | t when t = typeof<Byte>    ->"byte" 
        | t when t = typeof<SByte>   -> "sbyte" 
        | t when t = typeof<Char>    ->"char" 
        | t when t = typeof<Decimal> -> "decimal" 
        | t when t = typeof<Double>  -> "string" 
        | t when t = typeof<Single>  -> "float" 
        | t when t = typeof<Int32>   ->"int" 
        | t when t = typeof<UInt32>  -> "uint32" 
        | t when t = typeof<Int64>   ->"long" 
        | t when t = typeof<UInt64>  -> "ulong" 
        | t when t = typeof<Object>  -> "object" 
        | t when t = typeof<Int16>   -> "short" 
        | t when t = typeof<UInt16>  -> "ushort" 
        | t when t = typeof<String>  -> "string" 
        | t when t = typeof<Void>    -> "void" 
        | _ -> invalidArg "t" <| (sprintf "Type %s does not have an alias" <| t.ToString())

let getFakeableMethods (t:Type) =
    t.GetMethods()
    |> Array.filter (fun m -> (Array.length <| m.GetParameters()) <= 3 && m.IsAbstract && m.IsPublic)

let getFakeableTypes (types:Type[]) =
    types
    |> Array.filter (fun t -> t.IsAbstract || t.IsInterface)