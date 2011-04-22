#ifndef				POSLIB_TSUBVENTION_H
#define				POSLIB_TSUBVENTION_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tlockfile.h"
#include			"basics/tinifile.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Subventionen.
		Alle verfügbaren Sparten werden in einer Instanz von TSubventions
		zur Verfügung gestellt.
		\brief POS-Klassen: Subventionen.
	*/
	class			TSubvention
	: public TNValue
	{
	public:
		static const char	entryPricelevel[];
		static const char	entryDiscounts[];
		static const char	entryMinAmount[];
		static const char	entryMaxAmount[];
		static const char	entryDepartment[];
		static const char	entryPlu[];
		static const char	pathLock[];
		static const char	pathSub[];
	public:
		/*!	Erzeuge eine leere Instanz einer Subvention.
		*/
		TSubvention()
		: TNValue()
		, m_Ini(NULL)
		{
		}
		/*!	Erzeuge eine Instanz einer Sparte als Kopie von dep.
			\param dep		zu kopierende Sparte.
		TSubvention(const TSubvention& dep)
		: TNValue(dep)
		{
		}
		*/
		~TSubvention()
		{
			if( m_Ini )
				delete m_Ini;
			m_Lock.closeFile();
		}
		long		getToday(long guest, int offs, const char* type, const QString& path);
		void		setToday(long guest, long amount, const char* type);
		void		freeToday();
	public:
		int			getPricelevel() const
		{
			return getValue(entryPricelevel, 0);
		}
		void		setPricelevel(int lev)
		{
			if( !lev )
				clrValue(entryPricelevel);
			else
				setValue(entryPricelevel, lev);
		}
		long		getMinAmount() const
		{
			return getValue(entryMinAmount, 0L);
		}
		void		setMinAmount(long am)
		{
			if( !am )
				clrValue(entryMinAmount);
			else
				setValue(entryMinAmount, am);
		}
		long		getMaxAmount() const
		{
			return getValue(entryMaxAmount, 0L);
		}
		void		setMaxAmount(long am)
		{
			if( !am )
				clrValue(entryMaxAmount);
			else
				setValue(entryMaxAmount, am);
		}
		int			getDepartment() const
		{
			return getValue(entryDepartment, 0);
		}
		void		setDepartment(int dep)
		{
			if( !dep )
				clrValue(entryDepartment);
			else
				setValue(entryDepartment, dep);
		}
		int			getPlu() const
		{
			return getValue(entryPlu, 0);
		}
		void		setPlu(int plu)
		{
			if( !plu )
				clrValue(entryPlu);
			else
				setValue(entryPlu, plu);
		}
		QStringList	getDiscounts() const
		{
			return QStringList::split(";", getValue(entryDiscounts));
		}
		QString		strDiscounts() const
		{
			return getString(entryDiscounts);
		}
		void		setDiscounts(const QString& discs)
		{
			if( discs.isEmpty() )
				clrValue(entryDiscounts);
			else
				setValue(entryDiscounts, discs);
		}
	protected:
		TInifile*	m_Ini;
		TLockfile	m_Lock;
	};

	class			TSubventions
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TSubventions(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TSubventions()
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
		TSubvention*	operator [] (int index)
		{
			return (TSubvention*) TValueList::operator [](index);
		}
		virtual TValue*	createValue()
		{
			return new TSubvention();
		}
	};

	class			TSubventionIt
	: public TValueListIt
	{
	public:
		TSubventionIt(TSubventions& list)
		: TValueListIt(list)
		{
		}
		TSubvention*	operator () ()
		{
			return (TSubvention*) TValueListIt::operator()();
		}
		TSubvention*	toFirst()
		{
			return (TSubvention*) TValueListIt::toFirst();
		}
		TSubvention*	current()
		{
			return (TSubvention*) TValueListIt::current();
		}
		TSubvention*	operator ++ ()
		{
			return (TSubvention*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


