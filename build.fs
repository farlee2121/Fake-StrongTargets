module Build

open Fake.Core
open Fake.StrongTargets

module TRef = Target.ByRef

[<EntryPoint>]
let main argv =
    argv
    |> Array.toList
    |> Context.FakeExecutionContext.Create false "build.fsx"
    |> Context.RuntimeContext.Fake
    |> Context.setExecutionContext

    let hello = TRef.create "Hello" (fun _ ->
        printfn "hello from FAKE!"
    )   

    Target.ByRef.runOrDefault hello
    0