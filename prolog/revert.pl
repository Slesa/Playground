revert([], []).
revert([Head|Tail], List) :- revert(Tail, What), append(What, Head, List).
