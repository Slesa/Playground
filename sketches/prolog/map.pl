different(red, green). different(green, red).
different(blue, green). different(green, blue).
different(blue, red). different(red, blue).

coloring(Saarland, Pfalz, Baden, Hessen, Westfalen, Bayern) :-
  different(Pfalz, Saarland),
  different(Pfalz, Baden),
  different(Pfalz, Hessen),
  different(Pfalz, Westfalen),
  different(Hessen, Baden),
  different(Hessen, Westfalen),
  different(Hessen, Bayern).
