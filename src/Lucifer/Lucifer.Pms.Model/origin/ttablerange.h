#ifndef				POSLIB_TTABLERANGE_H
#define				POSLIB_TTABLERANGE_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\brief POS-Klassen: Tischbereiche.
	*/
	class			TTableRange
	: public TNValue
	{
	public:
		static const char	entryFrom[];
		static const char	entryTo[];
		static const char	entryReserve[];
	public:
		/*!	Erzeuge eine leere Instanz eines Kellnerteams.
		*/
		TTableRange()
		: TNValue()
		{
		}
		~TTableRange()
		{
		}
		bool		contains(long table);
		long		getTableFrom() const
		{
			return getValue(entryFrom, 0L);
		}
		void		setTableFrom(long from)
		{
			setValue(entryFrom, from);
		}
		long		getTableTo() const
		{
			return getValue(entryTo, 0L);
		}
		void		setTableTo(long to)
		{
			setValue(entryTo, to);
		}
		bool		doReservation() const
		{
			return getValue(entryReserve, FALSE);
		}
		void		setReservation(bool on)
		{
			if( !on )
				clrValue(entryReserve);
			else
				setValue(entryReserve, on);
		}
	};

	class			TTableRanges
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TTableRanges(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TTableRanges()
		{
		}
		int		findRange(long table);
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
		TTableRange*	operator [] (int index)
		{
			return (TTableRange*) TValueList::operator [](index);
		}
	};

	class			TTableRangeIt
	: public TValueListIt
	{
	public:
		TTableRangeIt(TTableRanges& list)
		: TValueListIt(list)
		{
		}
		TTableRange*	operator () ()
		{
			return (TTableRange*) TValueListIt::operator()();
		}
		TTableRange*	toFirst()
		{
			return (TTableRange*) TValueListIt::toFirst();
		}
		TTableRange*	current()
		{
			return (TTableRange*) TValueListIt::current();
		}
		TTableRange*	operator ++ ()
		{
			return (TTableRange*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


