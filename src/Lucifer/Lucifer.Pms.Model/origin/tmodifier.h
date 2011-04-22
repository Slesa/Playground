#ifndef				_POSLIB_TMODIFIER_

#define				_POSLIB_TMODIFIER_
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	class			TModifier
	: public TNValue
	{
		static const char	entryPrice[];
	public:
		/*!	Erzeuge eine leere Instanz eines Modifiers.
		*/
		TModifier()
		: TNValue()
		{
		}
		int			getPrice() const
		{
			return getValue(entryPrice, 0);
		}
		void		setPrice(int price)
		{
			setValue(entryPrice, price);
		}
	};

	class			TModifiers
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TModifiers(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString	getFileName() const
		{
			return fileName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		TModifier*	operator [] (int index)
		{
			return (TModifier*) TValueList::operator [](index);
		}
	};

	class			TModifierIt
	: public TValueListIt
	{
	public:
		TModifierIt(TModifiers& list)
		: TValueListIt(list)
		{
		}
		TModifier*	operator () ()
		{
			return (TModifier*) TValueListIt::operator()();
		}
		TModifier*	toFirst()
		{
			return (TModifier*) TValueListIt::toFirst();
		}
		TModifier*	current()
		{
			return (TModifier*) TValueListIt::current();
		}
		TModifier*	operator ++ ()
		{
			return (TModifier*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif

