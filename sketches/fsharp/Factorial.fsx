open System

let rec factorial n = 
    if n<=1 then
        1
    else
        n * factorial(n-1)

let doFactorial n =
    let starts = DateTime.Now
    let x = factorial(n)
    let ends = DateTime.Now
    let diff = ends - starts
    printfn "Factorial of %d: %d (%d)" n x diff.Milliseconds

for i in [0..30] do
    doFactorial i

