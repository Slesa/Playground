let printCity(cityInfo) =
    printfn "Population of %s is %d" (fst cityInfo) (snd cityInfo)

let withPopulation1 newPopulation tuple =
    let (originalCity, originalPopulation) = tuple 
    (originalCity, newPopulation)

let withPopulation2 newPopulation tuple =
    let (originalCity, _) = tuple 
    (originalCity, newPopulation)

let withPopulation3 newPopulation (originalCity, _) = (originalCity, newPopulation)

let withPopulation4 newPopulation tuple =
    match tuple with
    | (originalCity, _) -> (originalCity, newPopulation)

let setPopulation tuple newPopulation =
    match tuple with
    | ("New York", _) -> ("New York", newPopulation+100)
    | (cityName, _) -> (cityName, newPopulation)

let prague = ("Prague", 1188126)
printCity(setPopulation prague 10)
let seattle = ("Seattle", 594210)
let ny = ("New York", 123)
printCity(setPopulation ny 10)

printCity(withPopulation1 (snd(prague)+13195) prague)
printCity(withPopulation2 (snd(prague)+13195) prague)
printCity(withPopulation3 (snd(prague)+13195) prague)
printCity(withPopulation4 (snd(prague)+13195) prague)
printCity(seattle)
