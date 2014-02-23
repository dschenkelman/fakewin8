namespace FakeWin8.Generator.GenericArgument

open System
open System.Reflection

type internal GenericArgument(t : Type) = 
    let hasFlag = t.GenericParameterAttributes.HasFlag
    let useNew = hasFlag GenericParameterAttributes.DefaultConstructorConstraint
    let useClass = hasFlag GenericParameterAttributes.ReferenceTypeConstraint
    let useStruct = hasFlag GenericParameterAttributes.NotNullableValueTypeConstraint
    let genericConstraintsNames = t.GetGenericParameterConstraints() |> Array.filter (fun param -> param.Name <> "ValueType")

    member this.constraintsAsString () = 
        // the order is important. in regex form (class|struct)?(types)*(new())?
        let constraints = [
            (useStruct, "struct")
            (useClass, "class")
            (genericConstraintsNames.Length <> 0, genericConstraintsNames |> Array.map (fun c -> c.Name) |> String.concat ", ")
            (useNew && not useStruct, "new()")]
                            |> List.filter (fun i -> match i with
                                                        | (a, b) -> a)
                            |> List.map (fun i -> match i with
                                                        | (true, b) -> b
                                                        | (false, _) -> String.Empty)
                            |> String.concat ", "

        if not(String.IsNullOrEmpty constraints)
            then (sprintf "where %s : %s" t.Name constraints) 
            else String.Empty