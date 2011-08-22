revert([], []).
revert([Head|Tail], Reversed) :-
	revert(Tail, List),
	append(List, [Head], Reversed).

:- use_module(library(plunit)).

:- begin_tests(revert).

test(revert_empty) :- revert([], []).
test(revert_two) :- revert([a, b], [b, a]).
test(revert_three) :- revert([a, b, c], [c, b, a]).

:- end_tests(revert).

