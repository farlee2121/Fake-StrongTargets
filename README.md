
# Fake.Targets.Strong
[![Nuget](https://img.shields.io/nuget/v/Fake.StrongTargets)](https://www.nuget.org/packages/Fake.StrongTargets/)

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

Usage in a script-based build is the same, just reference the nuget package.

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

### Operators

There are also operators defined in `Fake.StrongTargets.Operators` equivalent to the basic fake operators
- left depends`dependent <== [dependencies]`
- right depends `dependency ==> dependent`
- left soft depends `dependent <=? dependency`
- right soft depends `dependency ?=> dependent`

So you could still define a build tree similar to what is normally shown in the Fake docs.
```fsharp
open Fake.StrongTargets.Operators

clean ==> build ==> test ==> default'
```

Note that you can't use both the string-based operators and these operators at the same time because the definitions conflict

## Project Status

This repo is a proof of concept, but it works and is low risk since it just aliases a few commands in Fake.Core.Targets.

If it's popular enough I may contribute it to FAKE and/or pursue an API that separates target declaration from global registration.
Feedback is appreciated!