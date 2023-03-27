module Build

open Fake.Core
open Fake.StrongTargets
open Fake.DotNet

open Fake.StrongTargets.Operators
module TRef = Target.ByRef

[<EntryPoint>]
let main argv =
    argv
    |> Array.toList
    |> Context.FakeExecutionContext.Create false "build.fsx"
    |> Context.RuntimeContext.Fake
    |> Context.setExecutionContext

    let clean = 
        Target.ByRef.create "clean" <| fun _ ->
            printfn "Cleaning..."
            printfn "All Clean"

    let build = 
        Target.ByRef.create "build" <| fun _ ->
            DotNet.build id "Fake.StrongTargets.sln"
        //|> Target.ByRef.softDependsOn [clean]

    let test = 
        Target.ByRef.create "test" <| fun _ ->
            printfn "Pretend this has tests..."
            printfn "Passed: All of None"
        //|> Target.ByRef.dependsOn [build]

    let default' = 
        TRef.create "default" <| fun _ -> 
            ()
        //|> Target.ByRef.dependsOn [clean; build; test]

    let hello = TRef.create "Hello" (fun _ ->
        printfn "hello from FAKE!"
    )   


    clean ==> build ==> test ==> default' |> ignore

    Target.ByRef.runOrDefault default'
    0