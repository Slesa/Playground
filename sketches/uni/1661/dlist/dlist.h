#pragma once

template <class T>
class DPosition
{
	T	_value;
	DPosition*	_pred;
	DPosition*	_succ;	
public:
	DPosition() 
	{
		//_value = null;
		_succ = NULL;
		_pred = NULL;
	}
		
	T Value() { return _value; }
	DPosition* Pred() { return _pred; }
	DPosition* Succ() { return _succ; }
};

template <class T>
class DList : public DPosition<T>
{
public:
	DPosition<T>* Front() { return this; }
};

