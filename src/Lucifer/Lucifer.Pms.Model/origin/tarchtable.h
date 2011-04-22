#ifndef				POSLIB_TARCHTABLE_H
#define				POSLIB_TARCHTABLE_H
#include			"poslib/ttable.h"
#include			"poslib/ttablecreate.h"
#include			"poslib/twaiter.h"
#include			"qmap.h"

namespace PosLib
{
	class			TArchTable
	: public TTable
	{
	public:
		TArchTable(TWaiter* waiter);
		void		load(QDataStream& st);
		void		save(QDataStream& st, TTable* table);
	};

	class			TArchTables
	{
		static const char	pathName[];
	public:
		TArchTables(TWaiter* waiter, const QDate& date, const char* path);
		~TArchTables();
		long		getSize();
		void		createFile();
		TTable*		nextTable(long& pos);
		QString		getFileName();
	protected:
		bool		openFile();
		void		checkUpdates();
	protected:
		TWaiter*	m_Waiter;
		QString		m_Path;
		QDate		m_Date;
		QByteArray	m_Buffer;
		TArchTable*	m_Table;
		QDataStream*	m_Stream;
		int			m_TableCount;
	};

}

using namespace PosLib;

#endif
