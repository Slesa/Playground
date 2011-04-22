#ifndef				POSLIB_TTABLEENTRY_H
#define				POSLIB_TTABLEENTRY_H
#include			"poslib/tshift.h"
#include			"poslib/tshiftgroup.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	class	TTableEntries;
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r Tischeintr�ge. Im Unterschied
		zu Aktionen (TTableAction) ben�tigt ein Tischeintrag keinen Kellner.
		Benutzerdefinierte Eintr�ge, die zB durch externen Modulen eingetragen werden k�nnen,
		k�nnen dadurch von TTableEntry abgeleitet werden.
		\brief POS-Klassen: Tischeintr�ge.
	*/
	class			TTableEntry
	: public TValue
	{
	public:
		static const char	entryType[];
		static const char	entryParent[];
		static const char	entryTime[];
		static const char	entryDate[];
		static const char	entryFather[];
		static const char	entryTmp[];
		static const char	entryShift[];
		static const char	entryShiftName[];
		static const char	entryShiftGroup[];
		static const char	entryShiftGrName[];
	public:
		/*	Die verschiedenen Typen eines Eintrags.
			\brief Eintrags-Typen
		*/
		enum		Types
		{
			Unknown	= 0				//!< Unbekannte Aktion
		,	Create	= 1				//!< Tisch erzeugt, nur einmal pro Tisch
		,	Change	= 2				//!< Tisch gewechselt
		,	Order	= 3				//!< Bestell-Aktion
		,	Void	= 4				//!< Storno-Aktion
		,	Pay		= 5				//!< Bezahl-Aktion
		,	Split	= 6				//!< Splitten-Aktion
		,	Restore	= 7				//!< Retten-Aktion
		,	Archived= 8				//!< Tisch archiviert
		,	Control	= 9				//!< Bonsteuerung
		,	Cashing	= 10			//!< Ein- und Auszahlungen
		,	Modifier= 11			//!< Modifizieren von Orders
		,	CombiArt= 12			//!< Kombi-Artikel von Orders
		,	Balance = 13			//!< Kellnerselbstabrechnung
		,	User	= 100			//!< Benutzerdefinierte Aktion
		};
	public:
		TTableEntry();
		/*!	Erzeugt eine neue Instanz eines Tischeintrags mit dem Typ type.
			\param type		Der Typ des Eintrags laut Types
			\brief ctor
		*/
		TTableEntry(int type);
		~TTableEntry();
		void		load(QDataStream& st, TTableEntries& entries);
		void		store(QDataStream& st);

		/*!	\return den Typ dieses Tischeintrags laut Types.
			\brief Eintragstyp ermitteln.
		*/
		int			getType() const
		{
			return getValue(entryType, Unknown);
		}
		/*!	�ndern den Eintragstyp auf type.
			\param type		Neuer Typ des Eintrags
			\brief Eintragstyp �ndern.
		*/
		void		setType(int type)
		{
			if( !type )
				clrValue(entryType);
			else
				setValue(entryType, type);
		}
		/*!	\return den Index des zugeh�rigen Eltern-Eintrags, oder 0 falls der Eintrag keinen Eltern-Eintrag hat.
			\brief Eltern-Eintrag ermitteln.
		*/
		int			getParent() const
		{
			return getValue(entryParent, 0);
		}
		/*!	�ndert den Index des zugeh�rigen Eltern-Eintrags. Ist parent 0, hat der Eintrag keinen Eltern-Eintrag.
			\param parent	Index des Eltern-Eintrags
			\brief Eltern-Eintrag �ndern.
		*/
		void		setParent(int parent)
		{
			if( parent )
				setValue(entryParent, parent);
			else
				clrValue(entryParent);
		}
		/*!	\return das Datum, zu dem dieser Tischeintrag in den Tisch eingetragen wurde.
			\brief Eintragsdatum ermitteln.
		*/
		QDate		getDate() const
		{
			return TValue::getDate(entryDate);
		}
		/*!	�ndert das Datum des Tischeintrags auf date.
			\param date		Neues Datum des Tischeintrags.
			\brief Eintragsdatum �ndern
		*/
		void		setDate(const QDate& date)
		{
			setValue(entryDate, date);
		}
		/*!	\return die Uhrzeit, zu der dieser Tischeintrag in den Tisch eingetragen wurde.
			\brief Eintragszeit ermitteln.
		*/
		QTime		getTime() const
		{
			return TValue::getTime(entryTime);
		}
		/*!	�ndert die Uhrzeit des Tischeintrags auf time.
			\param time		Neue Uhrzeit des Tischeintrags.
			\brief Eintragszeit �ndern
		*/
		void		setTime(const QTime& time)
		{
			setValue(entryTime, time);
		}
		void		setTemporary()
		{
			setValue(entryTmp, 1);
		}
		bool		hasChilds();
		TTableEntries*	getChilds(bool autocreate=FALSE);
		void		setChilds(TTableEntries* childs);
		void		setShift(TShift* shift, TShiftGroup* group);
	protected:
		TTableEntries*	m_Childs;
	private:
		friend class TTableEntries;
		TTableEntry& operator = (const TTableEntry& entry);
	};

	class			TTableEntries
	: public TValueList
	{
		Q_OBJECT
	public:
		static const char		elementName[];
		static const char		listName[];
	public:
		TTableEntries(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TTableEntries()
		{
		}
		/*!	\return Liefert den Namen der Liste. Der Name wird u.a. von TWorkspace und den XML-Funktionen benutzt.
			\note Die Funktion mu�berschrieben werden, falls die Liste exportiert
			werden soll .
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		/*!	\return Liefert den Namen eines Elements in der Liste. Der Name wird von den XML-Funktionen benutzt.
			\note Die Funktion mu�berschrieben werden, falls die Liste exportiert
			werden soll .
			\brief Elementnamen ermitteln.
		*/
		virtual QString	getElementName()
		{
			return elementName;
		}
		virtual TValue*	createValue();
		TTableEntry*	operator [] (int index)
		{
			return (TTableEntry*) TValueList::operator [](index);
		}
		TTableEntries*	getEntries(int type, bool controls=FALSE);
		TTableEntries&	operator += (TTableEntries& entries);
		virtual void	importXmlItem(TValue* item, const QDomElement& node);
		virtual void	exportXmlItem(TValue* item, QDomDocument& doc, QDomElement& node);
	private:
		virtual int		load(const QString& /*file*/, const char* /*path*/) { return errNone; }
		virtual void	save(const QString& /*file*/, const char* /*path*/) {}
	};

	class			TTableEntryIt
	: public TValueListIt
	{
	public:
		TTableEntryIt(TTableEntries& list)
		: TValueListIt(list)
		{
		}
		TTableEntry*	operator () ()
		{
			return (TTableEntry*) TValueListIt::operator()();
		}
		TTableEntry*	toFirst()
		{
			return (TTableEntry*) TValueListIt::toFirst();
		}
		TTableEntry*	current()
		{
			return (TTableEntry*) TValueListIt::current();
		}
		TTableEntry*	operator ++ ()
		{
			return (TTableEntry*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif
