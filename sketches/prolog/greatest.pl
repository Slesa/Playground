%greatest(-1, []).
greatest(Entry, [Head|Tail]) :- greatest(Value, Tail), Head<Value -> Entry is Value ; Entry is Head.

:- use_module(library(plunit)).
:- begin_tests(greatest).

test(greatest_empty, fail) :- greatest(-1, []).
test(greatest_two_highest_first) :- greatest(8, [8, 1]).
test(greatest_two_highest_last) :- greatest(8, [1, 8]).
test(greatest_lots) :- greatest(13, [1, 2, 13, 8, 7, 8]).

:- end_tests(greatest).

