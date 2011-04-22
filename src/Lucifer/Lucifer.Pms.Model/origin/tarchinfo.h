#ifndef				POSLIB_TARCHINFO_H
#define				POSLIB_TARCHINFO_H
#include			"poslib/ttable.h"
#include			"poslib/ttablecreate.h"
#include			"poslib/twaiter.h"
#include			"qmap.h"

namespace PosLib
{
	class			TArchInfo
	: public TValue
	{
		static const char	entryFile[];
		static const char	entryArchDate[];
	public:
		TArchInfo(const QString& line);
		TArchInfo(TTable* table);
		QString		toString();
		static QString	normalize(const QString& fn);
	public:
		QString		getArchDate() const
		{
			return getString(entryArchDate);
		}
		QString		getFile() const
		{
			return getString(entryFile);
		}
		int			getTerminal() const
		{
			return getValue(TTableAction::entryTerminal, 0);
		}
		QString		getTermName() const
		{
			return getString(TTableAction::entryTermName);
		}
		int			getCostCenter() const
		{
			return getValue(TTableAction::entryCenter, 0);
		}
		QString		getCenterName() const
		{
			return getString(TTableAction::entryCenterName);
		}
		int			getWaiter() const
		{
			return getValue(TTableAction::entryWaiter, 0);
		}
		QString		getWaiterName() const
		{
			return getString(TTableAction::entryWaiterName);
		}
		long		getTable() const
		{
			return getValue(TTable::entryTable, 0L);
		}
		int			getParty() const
		{
			return getValue(TTable::entryParty, 0);
		}
		int			getArchive() const
		{
			return getValue(TTable::entryArchive, 0);
		}
		int			getBillNum() const
		{
			return getValue(TTable::entryBillNum, 0);
		}
		long		getMaxAmount() const
		{
			return getValue(TTable::entryMaxAmount, 0L);
		}
	};

	class			TArchInfos
	{
		static const char	pathName[];
		static const char	fileName[];
		enum		Opener {
			Normal
		,	Brute
		,	Append
		};
	public:
		typedef QMap<QString,TArchInfo*>	TArchMap;
		typedef QDict<TArchInfo>	TArchList;
	public:
		TArchInfos(const char* path, bool use=TRUE);
		~TArchInfos();
		void		insert(TTable* table);
		void		remove(const QString& fn);
		TArchMap&	getMap()
		{
			return m_Map;
		}
		void		load(int offs=0);
		void		save();
		void		refresh();
	protected:
		TArchInfo*	doinsert(TTable* table);
		void		openFile(Opener how=Normal);
		QString		getMapKey(TArchInfo* info);
		QString		getFileName();
	protected:
		QString		m_Path;
		TArchMap	m_Map;
		TArchList	m_List;
		TFile		m_File;
	};

}

using namespace PosLib;

#endif
