namespace Fake.StrongTargets


module Target =
    module ByRef =
        open Fake.Core
        let create name f = 
            Target.create name f
            Target.get name

        let private getName target = target.Name
        let dependsOn deps target = 
            TargetOperators.(<==) target.Name (deps |> List.map getName)
            target

        let softDependsOn deps target = 
            deps
            |> List.map (getName >> (TargetOperators.(<=?) target.Name))
            |> ignore

            target

        let run (parallelJobs:int) (target:Target) (args: string list) : unit =
            Target.run parallelJobs target.Name args

        let runOrDefault target =
            Target.runOrDefault target.Name

        let runOrDefaultWithArguments target =
            Target.runOrDefaultWithArguments target.Name


module Operators = 

    let (<==) dependent dependencies =
        dependent |> Target.ByRef.dependsOn dependencies

    let (<=?) dependent dependency =
        dependent |> Target.ByRef.softDependsOn [dependency]
    

