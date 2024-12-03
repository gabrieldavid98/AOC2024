open System
open System.IO

Path.Join [| __SOURCE_DIRECTORY__; "input.txt" |]
|> File.ReadAllLines
|> Array.map _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
|> Array.map (fun a -> (int a.[0]), (int a.[1]))
|> Array.unzip
|> (fun (aa, bb) -> Array.zip (Array.sort aa) (Array.sort bb))
|> Array.map (fun (a, b) -> Math.Abs(b - a))
|> Array.sum
|> printfn "Result: %A" // 1223326
