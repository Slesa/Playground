#ifndef				POSLIB_TSHIFT_H
#define				POSLIB_TSHIFT_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Schichten.
		Alle verfügbaren Schichten werden in einer Instanz von TShiftList
		zur Verfügung gestellt.
		\brief POS-Klassen: Schichten.
	*/
	class			TShift
	: public TNValue
	{
	public:
		static const char	entryGroup[];
		static const char	entryStart[];
		static const char	entryEnd[];
		static const char	entryDays[];
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
		/*!	Erzeuge eine leere Instanz einer Schicht.
		*/
		TShift()
		: TNValue()
		{
		}
		/*!	Erzeuge eine Instanz einer Schicht als Kopie von shift.
			\param shift	zu kopierende Schicht.
		TShift(const TShift& shift)
		: TNValue(shift)
		{
		}
		*/
		~TShift()
		{
		}
		int			getGroup() const
		{
			return getValue(entryGroup, 0);
		}
		void		setGroup(int group)
		{
			if( !group )
				clrValue(entryGroup);
			else
				setValue(entryGroup, group);
		}
		/*!	\return die Startzeit der Schicht im Format [Stunde]*100+[Minute].
			\brief Startzeit abfragen.
			\sa setStart
		*/
		uint		getStart() const
		{
			return getValue(entryStart, 0);
		}
		/*!	Ändert die Startzeit der Schicht.
			\param start	Die neue Startzeit im Format [Stunde]*100+[Minute].
			\brief Startzeit der Schicht ändern.
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
		/*!	\return die Endzeit der Schicht im Format [Stunde]*100+[Minute].
			\brief Endzeit abfragen.
		*/
		uint		getEnd() const
		{
			return getValue(entryEnd, 0);
		}
		/*!	Ändert die Endzeit der Schicht.
			\param end		Die neue Endzeit im Format [Stunde]*100+[Minute].
			\brief Endzeit der Schicht ändern.
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
		/*!	\return die Zeit-Informationen als String, um sie zB in eine Kombobox einzufügen.
			\brief Zeit-Informationen als String abfragen.
			\sa startAsString, endAsString
		*/
		QString		asString()
		{
			QString tmp;
			tmp.sprintf("%02u:%02u-%02u:%02u", getStart()/100, getStart()%100, getEnd()/100, getEnd()%100);
			return tmp;
		}
		/*!	\return die Startzeit-Informationen als String, um sie zB in eine Kombobox einzufügen.
			\brief Startzeit-Informationen als String abfragen.
		*/
		QString		startAsString()
		{
			QString tmp;
			tmp.sprintf("%02d:%02d", getStart()/100, getStart()%100);
			return tmp;
		}
		/*!	\return die Endzeit-Informationen als String, um sie zB in eine Kombobox einzufügen.
			\brief Endzeit-Informationen als String abfragen.
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
	};

	class			TShifts
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TShifts(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TShifts()
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
		TShift*	operator [] (int index)
		{
			return (TShift*) TValueList::operator [](index);
		}
		int			getCurrent(int group, const QDateTime& dt=QDateTime());
	/*
	protected:
		QString		m_Path;
	*/
	};

	class			TShiftIt
	: public TValueListIt
	{
	public:
		TShiftIt(TShifts& list)
		: TValueListIt(list)
		{
		}
		TShift*		operator () ()
		{
			return (TShift*) TValueListIt::operator()();
		}
		TShift*		toFirst()
		{
			return (TShift*) TValueListIt::toFirst();
		}
		TShift*		current()
		{
			return (TShift*) TValueListIt::current();
		}
		TShift*		operator ++ ()
		{
			return (TShift*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif

