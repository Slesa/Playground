#ifndef				_POSLIB_TRESTABLE_
#define				_POSLIB_TRESTABLE_
#include			"basics/tvalue.h"

namespace			PosLib
{
	class			TResTable
	: public TValue
	{
	public:
		static const char	entryTable[];
		static const char	entryReservations[];
	public:
		TResTable()
		: TValue()
		{
		}
		int			getTable() const
		{
			return getValue(entryTable);
		}
		QStringList	getReservations() const
		{
			return QStringList::split(";", getString(entryReservations));
		}
	};
	
	class			TResTables
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TResTables(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TResTables()
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
		TResTable*	operator [] (int index)
		{
			return (TResTable*) TValueList::operator [](index);
		}
	};

	class			TResTableIt
	: public TValueListIt
	{
	public:
		TResTableIt(TResTables& list)
		: TValueListIt(list)
		{
		}
		TResTable*	operator () ()
		{
			return (TResTable*) TValueListIt::operator()();
		}
		TResTable*	toFirst()
		{
			return (TResTable*) TValueListIt::toFirst();
		}
		TResTable*	current()
		{
			return (TResTable*) TValueListIt::current();
		}
		TResTable*	operator ++ ()
		{
			return (TResTable*) TValueListIt:: operator ++();
		}
	};
}

#endif

