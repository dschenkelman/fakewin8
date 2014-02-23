module FakeWin8.Generator.Core

open System
open System.Text.RegularExpressions
open System.Reflection
open FakeWin8.Generator.Utils
open FakeWin8.Generator.GenericArgument

let aliasFor t = 
    match t with
        | t when t = typeof<Boolean> -> "bool"
        | t when t = typeof<Byte>    ->"byte" 
        | t when t = typeof<SByte>   -> "sbyte" 
        | t when t = typeof<Char>    ->"char" 
        | t when t = typeof<Decimal> -> "decimal" 
        | t when t = typeof<Double>  -> "double" 
        | t when t = typeof<Single>  -> "float" 
        | t when t = typeof<Int32>   ->"int" 
        | t when t = typeof<UInt32>  -> "uint" 
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
                [
                    "<"
                    t.GetGenericArguments() |> Array.map friendlyString |> String.concat ", "
                    ">"] |> String.concat String.Empty)
    | false -> t |> aliasFor

let private typeConstraints(t:Type) =
    t.GetGenericArguments() 
    |> Array.map (fun a -> GenericArgument(a).ConstraintsString())
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

let generateFakeProperty (methodInfo:MethodInfo) =
    let returnType = methodInfo.ReturnType
    let returnsVoid = returnType = typeof<Void>
    let delegateType = if returnsVoid then "Action" else "Func"

    let returnArray = (if returnsVoid then [|String.Empty|] else [| friendlyString returnType |]) 

    let paramsString = 
        Array.append (methodInfo.GetParameters() 
                        |> Array.map (fun p -> friendlyString p.ParameterType)) returnArray
        |> concatNonEmpty ", "

    let delegateParams = 
        if String.IsNullOrEmpty paramsString then String.Empty else sprintf "<%s>" paramsString

    sprintf "public Fake%s%s %s%s { get; set; }" delegateType delegateParams methodInfo.Name delegateType