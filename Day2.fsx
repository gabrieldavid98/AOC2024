open System
open System.IO

type Report = { Levels: (int * int) array }

module Report =
    let private increasing report = Array.forall ((<||) (<)) report.Levels
    let private decreasing report = Array.forall ((<||) (>)) report.Levels

    let private within lower upper report =
        report.Levels
        |> Array.forall (fun (a, b) ->
            let diff = abs (a - b)
            lower <= diff && diff <= upper)

    let create levels = { Levels = Array.pairwise levels }

    let isSafe report =
        (increasing report || decreasing report) && within 1 3 report

Path.Join [| __SOURCE_DIRECTORY__; "day2_input.txt" |]
|> File.ReadAllLines
|> Array.map _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
|> Array.map (Array.map (int) >> Report.create >> Report.isSafe)
|> Array.countBy id
