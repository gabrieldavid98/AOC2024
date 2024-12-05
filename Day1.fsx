open System
open System.IO

let computeFrequencies (left, right) =
    left, Array.countBy id right |> Map.ofArray

let computeSimilarityScore (numbers, frequencies) =
    numbers
    |> Array.map (fun n -> Map.tryFind n frequencies |> Option.map ((*) n) |> Option.defaultValue 0)
    |> Array.sum

Path.Join [| __SOURCE_DIRECTORY__; "input.txt" |]
|> File.ReadAllLines
|> Array.map _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
|> Array.map (fun a -> (int a.[0]), (int a.[1]))
|> Array.unzip
|> computeFrequencies
|> computeSimilarityScore
|> printfn "Result: %A" // 21070419
