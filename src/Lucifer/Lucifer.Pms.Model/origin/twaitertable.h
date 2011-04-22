#ifndef				POSLIB_TWAITERTABLE_H
#define				POSLIB_TWAITERTABLE_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	class			TWaiterTableInfo
	{
	public:
		TWaiterTableInfo(const TWaiterTableInfo& info);
		TWaiterTableInfo(const QStringList& list);
		TWaiterTableInfo(const QString& name, const QDateTime& dt, long am);
		TWaiterTableInfo& operator = (const TWaiterTableInfo& info);
		QStringList	toList();
		QString		toString();
		static TWaiterTableInfo fromString(const QString& list);
	public:
		QString		getName() const
		{
			return m_Name;
		}
		QDateTime	getCreated() const
		{
			return m_Created;
		}
		long		getAmount() const
		{
			return m_Amount;
		}
		void		setAmount(long am)
		{
			m_Amount = am;
		}
	protected:
		QString		m_Name;
		QDateTime	m_Created;
		long		m_Amount;
	};

	/*!	\ingroup PosLib
	*/
	class			TWaiterTable
	: public TValue
	{
	public:
		TWaiterTable()
		: TValue()
		{
		}
	};

	class			TWaiterTables
	: public TValueList
	{
		Q_OBJECT
	public:
		static const char	fileName[];					//!< 
		static const char	listName[];					//!< Name der Liste (articles)
		static const char	elementName[];				//!< Name eines Elements der Liste (article)
	public:
		TWaiterTables(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TWaiterTables()
		{
		}
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString getFileName() const
		{
			return fileName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		TWaiterTable*	operator [] (int index)
		{
			return (TWaiterTable*) TValueList::operator [](index);
		}
	};

	class			TWaiterTableIt
	: public TValueListIt
	{
	public:
		TWaiterTableIt(TWaiterTables& list)
		: TValueListIt(list)
		{
		}
		TWaiterTable*	operator () ()
		{
			return (TWaiterTable*) TValueListIt::operator()();
		}
		TWaiterTable*	toFirst()
		{
			return (TWaiterTable*) TValueListIt::toFirst();
		}
		TWaiterTable*	current()
		{
			return (TWaiterTable*) TValueListIt::current();
		}
		TWaiterTable*	operator ++ ()
		{
			return (TWaiterTable*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


