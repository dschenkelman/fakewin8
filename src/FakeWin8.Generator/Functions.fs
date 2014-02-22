module FakeWin8.Generator.Core

open System
open System.Text.RegularExpressions
open System.Reflection
open FakeWin8.Generator.Utils

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
        | _ -> t.Name

let getFakeableMethods (t:Type) =
    t.GetMethods() |> Array.filter (fun m -> m.GetParameters().Length <= 3 && m.IsAbstract && m.IsPublic)

let getFakeableTypes (types:Type[]) =
    types |> Array.filter (fun t -> t.IsAbstract || t.IsInterface)
        
let rec private friendlyString(t:Type) =   
    match t.IsGenericType with
    | true -> Regex.Replace(t.Name, 
                "`[0-9]+", 
                ["<";t.GetGenericArguments() |> Array.map friendlyString |> String.concat ", ";">" ] 
                |> String.concat String.Empty)
    | false -> t |> aliasFor

let rec private parameterConstraints(p: Type) =
    let hasFlag = p.GenericParameterAttributes.HasFlag
    let useNew = hasFlag GenericParameterAttributes.DefaultConstructorConstraint
    let useClass = hasFlag GenericParameterAttributes.ReferenceTypeConstraint
    let useStruct = hasFlag GenericParameterAttributes.NotNullableValueTypeConstraint
    let genericConstraints = p.GetGenericParameterConstraints() |> Array.filter (fun param -> param.Name <> "ValueType")

    // the order is important. in regex form (class|struct)?(types)*(new())?
    let constraints = [
        (useStruct, "struct")
        (useClass, "class")
        (genericConstraints.Length <> 0, genericConstraints |> Array.map (fun c -> c.Name) |> String.concat ", ")
        (useNew && not useStruct, "new()")]
                        |> List.filter (fun i -> match i with
                                                    | (a, b) -> a)
                        |> List.map (fun i -> match i with
                                                    | (true, b) -> b
                                                    | (false, _) -> String.Empty)
                        |> String.concat ", "
    if not(String.IsNullOrEmpty constraints)
        then (sprintf "where %s : %s" p.Name constraints) 
        else String.Empty

let rec private typeConstraints(t:Type) =
    t.GetGenericArguments() 
    |> Array.map parameterConstraints
    |> concatNonEmpty " " 

let private trimName (friendly:String) (t:Type) =
    match t.IsInterface && t.Name.StartsWith("I") && t.Name.Length > 1 with 
        | true -> friendly.Substring(1)
        | false -> friendly

let generateFakeSignature (t:Type) =
    let friendly = friendlyString t
    let fakeName = trimName friendly t
    
    [
        sprintf "public class Fake%s : %s" fakeName friendly
        typeConstraints t
    ]
    |> concatNonEmpty " "