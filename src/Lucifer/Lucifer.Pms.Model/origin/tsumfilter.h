#ifndef				POSLIB_TSUMFILTER_H
#define				POSLIB_TSUMFILTER_H
#include			"poslib/ttable.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Auswertungsfilter.
		Alle verfügbaren Auswertungsfilter-Informationen werden in einer Instanz von
		TSumFilterList zur Verfügung gestellt.
		Es gibt zwei Arten von Auswertungsfiltern:
		- Tischfilter greifen bei der Auswertung dann, wenn der Tisch bekannt ist, jedoch
		  vor dem untersuchen der Tischeinträge.
		- Eintragsfilter werden pro Eintrag einmal analysiert.
		Der Filter selbst besteht aus mehreren verschachtelten Stringlisten.
		1. Die äußerste Schicht umfaßt die Veroderung der einzelnen Ausdrücke
		2. Die nächste Schicht bildet die Verundung der einzelnen Ausdrücke
		3. Es folgt der auszuwertende Ausdruck. Dieser besteht aus den Elementen
			- Zu testendes Attribut
			- Vergleichsoperand
			- Zu vergleichender Wert
		\brief POS-Klassen: Auswertungsfilter.
	*/
	class			TSumFilter
	: public TNValue
	{
	public:
		static const char	entryDescription[];					// Beschreibung des Filters
		static const char	entryTables[];						// Tischfilter
		static const char	entryEntries[];						// Eintragsfilter

	public:
		/*!	Erzeuge eine leere Instanz eines Auswertungsfilters.
		*/
		TSumFilter()
		: TNValue()
		{
		}
		bool		checkTable(TTable* table);
		bool		checkEntry(TTable* table, TTableEntry* entry);
		static QStringList	getOrs(const QString& entry)
		{
			return QStringList::split('|', entry, TRUE);
		}
		static QStringList	getAnds(const QString& entry)
		{
			return QStringList::split('&', entry, TRUE);
		}
		QStringList	getItems(const QString& entry)
		{
			return QStringList::split(':', entry);
		}
		/*!	\return die Beschreibung dieses Auswertungsfilters.
			\brief Beschreibung des Filters abfragen.
			\sa setDescription
		*/
		QString		getDescription() const
		{
			return TNValue::getString(entryDescription);
		}
		/*!	Ändert die Beschreibung dieses Auswertungsfilters.
			\param descr	Die neue Beschreibung
			\brief Beschreibung des Filters ändern.
			\sa getDescription
		*/
		void		setDescription(const QString& descr)
		{
			setValue(entryDescription, descr);
		}
		/*!	\return Liefert den Filter für den Tisch bzw den Tischheader. Dieser Filter
			beeinflußt, ob der Tisch komplett verworfen wird oder ob die Einträge des
			Tisches untersucht werden.
			\brief Tisch-/Tischheader-Filter ermitteln.
		*/
		QString		getTables() const
		{
			return TNValue::getString(entryTables);
		}
		/*!	Ändert den Filter für den Tisch bzw den Tischheader. Dieser Filter
			beeinflußt, ob der Tisch komplett verworfen wird oder ob die Einträge des
			Tisches untersucht werden.
			\param str		Der neue Filter
			\brief Tisch-/Tischheader-Filter ändern.
		*/
		void		setTables(const QString& str)
		{
			setValue(entryTables, str);
		}
		/*!	\return Liefert den Filter für einen Tischeintrag. Dieser Filter wird pro
			Tischeintrag ausgewertet, sofern der Tischfilter den Tisch nicht vorher
			verworfen hat.
			\brief Tischeintrags-Filter ermitteln.
		*/
		QString		getEntries() const
		{
			return TNValue::getString(entryEntries);
		}
		/*!	Ändert den Filter für einen Tischeintrag.
			\param str		Der neue Filter
			\brief Tischeintrags-Filter ändern.
		*/
		void		setEntries(const QString& str)
		{
			setValue(entryEntries, str);
		}
	protected:
		bool		checkValue(char comp, const QString& val1, const QString& val2);
		QString		getValue(TValue* val, const QString& entry);
		bool		checkFilter(const char* what, TTable* table, TTableEntry* entry);
	};

	class			TSumFilters
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TSumFilters(bool autodel=TRUE)
		: TValueList(autodel)
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
		virtual TValue*	createValue()
		{
			return new TSumFilter();
		}
		TSumFilter*	operator [] (int index)
		{
			return (TSumFilter*) TValueList::operator [](index);
		}
	};

	class			TSumFilterIt
	: public TValueListIt
	{
	public:
		TSumFilterIt(TSumFilters& list)
		: TValueListIt(list)
		{
		}
		TSumFilter*	operator () ()
		{
			return (TSumFilter*) TValueListIt::operator()();
		}
		TSumFilter*	toFirst()
		{
			return (TSumFilter*) TValueListIt::toFirst();
		}
		TSumFilter*	current()
		{
			return (TSumFilter*) TValueListIt::current();
		}
		TSumFilter*	operator ++ ()
		{
			return (TSumFilter*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


