#ifndef				POSLIB_TPRICELEVEL_H
#define				POSLIB_TPRICELEVEL_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Preisebenen.
		Alle verf�gbaren Preisebenen werden in einer Instanz von TPricelevelList
		zur Verf�gung gestellt.
		\brief POS-Klassen: Preisebenen.
	*/
	class			TPricelevel
	: public TNValue
	{
	public:
		static const char	entryStart[];
		static const char	entryEnd[];
		static const char	entryDays[];
		static const char	entryNoDiscounts[];
	public:
		enum		Days
		{
			sunday		= 0
		,	monday		= 1
		,	tuesday		= 2
		,	wednesday	= 3
		,	thursday	= 4
		,	friday		= 5
		,	saturday	= 6
		};
	public:
		/*!	Erzeuge eine leere Instanz einer Preisebene.
		*/
		TPricelevel()
		: TNValue()
		{
		}
		/*!	Erzeuge eine Instanz einer Preisebene als Kopie von level.
			\param level	zu kopierende Preisebene.
		TPricelevel(const TPricelevel& level)
		: TNValue(level)
		{
		}
		*/
		~TPricelevel()
		{
		}
		/*!	\return die Startzeit der Preisebene im Format [Stunde]*100+[Minute].
			\brief Startzeit abfragen.
			\sa setStart
		*/
		uint		getStart() const
		{
			return getValue(entryStart, 0);
		}
		/*!	�ndert die Startzeit der Preisebene.
			\param start	Die neue Startzeit im Format [Stunde]*100+[Minute].
			\brief Startzeit der Preisebene �ndern.
			\sa getStart
		*/
		void		setStart(uint start)
		{
			if( !start )
				clrValue(entryStart);
			else
				setValue(entryStart, start);
		}
		void		setStart(const QTime& start)
		{
			setValue(entryStart, start.hour()*100+start.minute());
		}
		QTime		startTime() const
		{
//			int secs = startTime()<endTime() ? 0 : 59;
			return QTime(getStart()/100, getStart()%100, 0);
		}
		/*!	\return die Endzeit der Preisebene im Format [Stunde]*100+[Minute].
			\brief Endzeit abfragen.
			\sa setEnd
		*/
		uint		getEnd() const
		{
			return getValue(entryEnd, 0);
		}
		/*!	�ndert die Endzeit der Preisebene.
			\param end		Die neue Endzeit im Format [Stunde]*100+[Minute].
			\brief Endzeit der Preisebene �ndern.
			\sa getEnd
		*/
		void		setEnd(uint end)
		{
			if( !end )
				clrValue(entryEnd);
			else
				setValue(entryEnd, end);
		}
		void		setEnd(const QTime& end)
		{
			setValue(entryEnd, end.hour()*100+end.minute());
		}
		QTime		endTime() const
		{
//			int secs = startTime()<endTime() ? 0 : 59;
			return QTime(getEnd()/100, getEnd()%100, 59);
		}
		/*!	\return die Zeit-Informationen als String, um sie zB in eine Kombobox einzuf�gen.
			\brief Zeit-Informationen als String abfragen.
			\sa startAsString, endAsString
		*/
		QString		asString()
		{
			QString tmp;
			tmp.sprintf("%02u:%02u-%02u:%02u", getStart()/100, getStart()%100, getEnd()/100, getEnd()%100);
			return tmp;
		}
		/*!	\return die Startzeit-Informationen als String, um sie zB in eine Kombobox einzuf�gen.
			\brief Startzeit-Informationen als String abfragen.
			\sa asString, endAsString
		*/
		QString		startAsString()
		{
			QString tmp;
			tmp.sprintf("%02d:%02d", getStart()/100, getStart()%100);
			return tmp;
		}
		/*!	\return die Endzeit-Informationen als String, um sie zB in eine Kombobox einzuf�gen.
			\brief Endzeit-Informationen als String abfragen.
			\sa asString, startAsString
		*/
		QString		endAsString()
		{
			QString tmp;
			tmp.sprintf("%02d:%02d", getEnd()/100, getEnd()%100);
			return tmp;
		}
		bool		onDay(int day) const
		{
			int x = getValue(entryDays, 32767);
			if( x&(1<<(day%7)) )
				return TRUE;
			return FALSE;
		}
		void		setDay(int day, bool flag)
		{
			int x = getValue(entryDays, 32767);
			if( flag )
				x |= 1<<day;
			else
				x &= ~(1<<day);
			setValue(entryDays, x);
		}
		void		setDays(int days)
		{
			if( days==32767 )
				clrValue(entryDays);
			else
				setValue(entryDays, days);
		}
		bool		isNoDiscounts() const
		{
			return getValue(entryNoDiscounts, FALSE);
		}
		void		setNoDiscounts(bool on)
		{
			if( !on )
				clrValue(entryNoDiscounts);
			else
				setValue(entryNoDiscounts, on);
		}
	};

	class			TPricelevels
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TPricelevels(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TPricelevels()
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
	/*
		virtual void	load(const char* path="")
		{
			load(getFileName(), TDir::checkPath(TDir::checkPath(path)+getDefPath()));
		}
		virtual void	save(const char* path="")
		{
			save(getFileName(), TDir::checkPath(TDir::checkPath(path)+getDefPath()));
		}
		virtual void	load(const QString& file, const char* path)
		{
			m_Path = path;
			TValueList::load(file, path);
		}
		virtual void	save(const QString& file, const char* path)
		{
			m_Path = path;
			TValueList::save(file, path);
		}
		virtual bool	remove(TValue* item);
	*/
		TPricelevel*	operator [] (int index)
		{
			return (TPricelevel*) TValueList::operator [](index);
		}
		int			getCurrent(const QDateTime& dt=QDateTime());
	/*
	protected:
		QString		m_Path;
	*/
	};

	class			TPricelevelIt
	: public TValueListIt
	{
	public:
		TPricelevelIt(TPricelevels& list)
		: TValueListIt(list)
		{
		}
		TPricelevel*	operator () ()
		{
			return (TPricelevel*) TValueListIt::operator()();
		}
		TPricelevel*	toFirst()
		{
			return (TPricelevel*) TValueListIt::toFirst();
		}
		TPricelevel*	current()
		{
			return (TPricelevel*) TValueListIt::current();
		}
		TPricelevel*	operator ++ ()
		{
			return (TPricelevel*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif

