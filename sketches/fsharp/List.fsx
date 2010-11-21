let rec sumList list =
    match list with
    | [] -> 0
    | head::tail -> head + sumList(tail)

let rec prodList list =
    match list with
    | [] -> 1
    | head::tail -> head * prodList(tail)

let rec aggregateList (op:_->_->_) init list =
    match list with
    | [] -> init
    | head::tail -> 
        let result = aggregateList op init tail
        op result head

let numbers = [1..5]
//printfn "SumList.....: %4d" (sumList numbers)
//printfn "ProdList....: %4d" (prodList numbers)

let add a b = a+b
let mul a b = a*b
let sub a b = a-b

printfn "Aggregatesum: %4d" (aggregateList add 0 numbers)
printfn "Aggregatemul: %4d" (aggregateList mul 1 numbers)
printfn "Aggregatesub: %4d" (aggregateList sub 0 numbers)
printfn "Aggregate + : %4d" (aggregateList (+) 0 numbers)
printfn "Aggregate * : %4d" (aggregateList (*) 1 numbers)
printfn "Aggregate - : %4d" (aggregateList (-) 0 numbers)
printfn "Aggregatemax: %4d" (aggregateList max -1 numbers)
