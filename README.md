# Fake.Targets.Strong
Refer to [FAKE](https://fake.build/) targets by reference, not strings, to increase compiler safety.


## Example

```fsharp
open Fake.StrongTargets

let clean = 
    Target.ByRef.create "clean" <| fun _ ->
        //...

let build = 
    Target.ByRef.create "build" <| fun _ ->
        //...
    |> Target.ByRef.softDependsOn [clean]

let test = 
    Target.ByRef.create "test" <| fun _ ->
        //...
    |> Target.ByRef.dependsOn [build]

let default' = 
    Target.ByRef.create "default" <| fun _ -> 
        //...
    |> Target.ByRef.dependsOn [clean; build; test]


Target.ByRef.runOrDefault default'
```

Note that creating a target still results in a global target registration.


## Project Status

This repo is a proof of concept that I'm considering publishing as a nuget package. Feedback is helpful!
If it's popular enough I may contribute it to FAKE.