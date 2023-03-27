# Fake.Targets.Strong
Improve compiler safety by refering to [FAKE](https://fake.build/) targets as values, not strings.

## Example

### Basic Project-based

See [build.fs](./build.fs)

A short sample is
```fsharp
open Fake.StrongTargets

let clean = 
    Target.ByRef.create "clean" <| fun _ ->
        //...

let build = 
    Target.ByRef.create "build" <| fun _ ->
        //...
    |> Target.ByRef.dependsOn [clean]

Targets.ByRef.runOrDefault build
```

Note that creating a target still results in a global target registration.

### Shorter Module Path

If you don't like writing `Target.ByRef.methodName` or just `ByRef.methodName` then you can alias the module

```fsharp
module TRef = Target.ByRef

let clean = TRef.create "clean" ...
```

### Fsx/script usage

Usage in a script-based build is the same, just reference the (coming soon) nuget package.

```fsharp
#r "paket:
nuget Fake.StrongTargets //"

open Fake.StrongTargets

...
```

### Separated Build Dependencies

I showed Target dependencies specified alongside target declaration, but they can as easily be separated
```fsharp
// open the module just for brevity sake
open open Fake.StrongTargets.Target.ByRef 

// declare deps as a single tree
default' 
|> dependsOn [
    clean
    test |> dependsOn [
        build |> dependsOn [clean]
    ]
] 
|> ignore
```

This approach can also be used for different dependencies based on different conditional statements.

## Project Status

This repo is a proof of concept that I'm considering publishing as a nuget package. Feedback is helpful!
If it's popular enough I may contribute it to FAKE.

The library is just [one short file](./src/Library.fs) if you want to use it before I publish to nuget.