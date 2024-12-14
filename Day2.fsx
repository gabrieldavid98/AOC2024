open System
open System.IO

type Report =
    { Levels: int array
      Pairs: (int * int) array }

module Report =
    let private increasing report = Array.forall ((<||) (<)) report.Pairs
    let private decreasing report = Array.forall ((<||) (>)) report.Pairs

    let private within lower upper report =
        report.Pairs
        |> Array.forall (fun (a, b) ->
            let diff = abs (a - b)
            lower <= diff && diff <= upper)

    let create levels =
        { Levels = levels
          Pairs = Array.pairwise levels }

    let isSafe report =
        (increasing report || decreasing report) && within 1 3 report

    let isDampenedSafe report =
        [| 0 .. (Array.length report.Levels) - 1 |]
        |> Array.map (fun index -> Array.removeAt index report.Levels)
        |> Array.exists (create >> isSafe)


Path.Join [| __SOURCE_DIRECTORY__; "day2_input.txt" |]
|> File.ReadAllLines
|> Array.map _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
|> Array.map (Array.map (int) >> Report.create >> Report.isDampenedSafe)
|> Array.countBy id
