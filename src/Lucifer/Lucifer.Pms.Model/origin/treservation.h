#ifndef				_POSLIB_TRESERVATION_
#define				_POSLIB_TRESERVATION_
#include			"basics/tvalue.h"

namespace			PosLib
{
	class			TReservation
	: public TValue
	{
	public:
		static const char	entryResNo[];
		static const char	entryResType[];
		static const char	entryClient[];
		static const char	entryResDate[];
		static const char	entryResTime[];
		static const char	entryResState[];
		static const char	entryCount[];
		static const char	entryChildren[];
		static const char	entryTables[];
	public:
		TReservation()
		: TValue()
		{
		}
		int			getResNo() const
		{
			return getValue(entryResNo);
		}
		int			getResType() const
		{
			return getValue(entryResType);
		}
		int			getClient() const
		{
			return getValue(entryClient);
		}
		QDate		getResDate() const
		{
			return QDate::fromString(getString(entryResDate));
		}
		QTime		getResTime() const
		{
			return QTime::fromString(getString(entryResTime));
		}
		int			getResState() const
		{
			return getValue(entryResState);
		}
		int			getCount() const
		{
			return getValue(entryCount);
		}
		int			getChildren() const
		{
			return getValue(entryChildren);
		}
		QStringList	getTables() const
		{
			return QStringList::split(";", getString(entryTables));
		}
	};
	
	class			TReservations
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TReservations(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TReservations()
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
		TReservation*	operator [] (int index)
		{
			return (TReservation*) TValueList::operator [](index);
		}
	};

	class			TReservationIt
	: public TValueListIt
	{
	public:
		TReservationIt(TReservations& list)
		: TValueListIt(list)
		{
		}
		TReservation*	operator () ()
		{
			return (TReservation*) TValueListIt::operator()();
		}
		TReservation*	toFirst()
		{
			return (TReservation*) TValueListIt::toFirst();
		}
		TReservation*	current()
		{
			return (TReservation*) TValueListIt::current();
		}
		TReservation*	operator ++ ()
		{
			return (TReservation*) TValueListIt:: operator ++();
		}
	};
}

#endif

