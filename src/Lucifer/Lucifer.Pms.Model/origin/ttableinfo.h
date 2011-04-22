#ifndef				POSLIB_TTABLEINFO_H
#define				POSLIB_TTABLEINFO_H
#include			"poslib/ttable.h"
#include			"poslib/twaiter.h"

namespace PosLib
{
	/*!	Diese Klasse umfaßt die Datei-Informationen, die mittels TTableInfos zu einer Liste
		zusammengefaßt werden. Somit sind auch nur die anhand der Dateiinformationen zugänglichen
		Werte abrufbar.
		Der zur Information gehörige Tisch kann mittels TTableIt ermittelt werden, oder man kann
		über loadTable den Tisch manuell laden. In dem Fall muß nach dem Zugriff auf den entsprechenden
		Tisch freeTable aufgerufen werden.
		\brief Hilfsklasse, um Informationen zu einem Tisch/Archiv/Statist zu speichern.
	*/
	class			TTableInfo
	: public TValue
	{
		static const char	entryPath[];
		static const char	entryDate[];
	public:
		/*!	Erzeugt einen neuen Eintrag einer Tischinformation. Es werden Tisch- und Parteinummer
			sowie der Pfad laut Parametern initialisiert.
			\param table	Die Tischnummer des Tisches
			\param party	Die Parteinummer des Tisches
			\param path		Pfad zur Laufumgebung.
			\brief ctor
		*/
		TTableInfo(const QDateTime& dt, long table, int party, const QString& path)
		: m_Table(NULL)
		, m_AccessDT(dt)
		{
			setValue(TTable::entryTable, table);
			setValue(TTable::entryParty, party);
			setValue(entryPath, path);
		}
		/*!	Erzeugt einen neuen Eintrag einer Archivinformation. Es werden die Archivnummer sowie
			der Pfad laut Parametern initialisiert.
			\param arch		Die Archivnummer des Archives.
			\param path		Pfad zur Laufumgebung.
			\brief ctor.
		*/
		TTableInfo(const QDateTime& dt, long arch, const QString& path)
		: m_Table(NULL)
		, m_AccessDT(dt)
		{
			setValue(TTable::entryArchive, arch);
			setValue(entryPath, path);
		}
		/*!	Erzeugt einen neuen Eintrag einer Statistinformation. Es werden das Datum, die
			Archivnummer sowie der Pfad laut Parameter initialisiert.
			\param date		Datum des Statist-Verzeichnisses.
			\param arch		Archivnummer innerhalb des Statist-Verzeichnisses.
			\param path		Pfad zur Laufumgebung.
			\brief ctor.
		*/
		TTableInfo(const QDateTime& dt, const QDate& date, long arch, const QString& path)
		: m_Table(NULL)
		, m_AccessDT(dt)
		{
			setValue(TTable::entryArchive, arch);
			setValue(entryDate, date);
			setValue(entryPath, path);
		}
		/*!	Zerstört die Instanz der Tischinformation und gibt einen eventuell geöffneten Tisch wieder
			frei.
			\note Der Tisch sollte schon vorher mit freeTable() von der Applikation geschlossen worden
				sein um Speicher zu sparen.
			\brief dtor.
		*/
		~TTableInfo()
		{
			freeTable();
		}
		QString		getFileName();
		TTable*		loadTable(bool all, int mask, TWaiter* waiter, TWaiters* waiters, TWaiterTeams* teams, const char* path);
		void		freeTable()
		{
			if( m_Table )
			{
				delete m_Table;
				m_Table = NULL;
			}
		}
		/*!	\return Liefert TRUE; wenn es sich bei der Dateiinformation um einen Tisch handelt.
			\brief Info zu einem Tisch?
		*/
		bool		isTable() const
		{
			return hasValue(TTable::entryTable);
		}
		/*!	\return Liefert TRUE, wenn es sich bei der Dateiinformation um ein Archiv handelt.
			\brief Info zu einem Archiv?
		*/
		bool		isArchive() const
		{
			return hasValue(TTable::entryArchive) && !hasValue(entryDate);
		}
		/*!	\return Liefert TRUE, wenn es sich bei der Dateiinformation um ein Statistfile handelt.
			\brief Info zu einem Statistfile?
		*/
		bool		isStatist() const
		{
			return hasValue(TTable::entryArchive) && hasValue(entryDate);
		}
		/*!	\return Liefert die Tischnummer dieses Tisches. Wenn es sich bei der Dateiinformation um keinen Tisch handelt,
			ist die Tischnummer nicht gesetzt und die Funktion liefert 0.
			\brief Tischnummer ermitteln.
		*/
		long		getTable() const
		{
			return getValue(TTable::entryTable, 0L);
		}
		/*!	\return Liefert die Parteinummer dieses Tisches. Wenn es sich bei der Dateiinformation um keinen Tisch handelt,
			ist die Parteinummer nicht gesetzt und die Funktion liefert 0.
			\brief Parteinummer ermitteln.
		*/
		int			getParty() const
		{
			return getValue(TTable::entryParty, 0);
		}
		/*!	\return Liefert die Archivnummer des Archives oder des Statistfiles. Falls es sich um einen Tisch handelt, ist
			die Archivnummer nicht gesetzt und die Funktion liefert 0.
			\brief Archivnummer ermitteln.
		*/
		long		getArchive() const
		{
			return getValue(TTable::entryArchive, 0L);
		}
		/*!	\return Liefert das Datum des Statistpfades dieses Archives. Falls es sich um kein Statistfile handelt, ist das
			zurückgegebene Datum ungültig.
			\brief Statistfile-Datum ermitteln.
		*/
		QDate		getDate() const
		{
			return TValue::getDate(entryDate);
		}
		/*!	\return Liefert den Pfad zur Laufumgebung wie im ctor übergeben. Wird zum Laden des Tisches benötigt.
			\brief Pfad zur Laufumgebung ermitteln.
		*/
		QString		getPath() const
		{
			return getString(entryPath);
		}
		/*!	\return Liefert den Zeitpunkt der letzten Modifikation wie im ctor übergeben (und letztendlich wie vom
			Betriebssystem übergeben.
			\brief Zeitpunkt der letzten Änderung ermitteln.
		*/
		QDateTime	getAccessDT()
		{
			return m_AccessDT;
		}
		void		setTable(TTable* tab)
		{
			m_Table = tab;
		}
	protected:
		TTable*		m_Table;								//!< Handle für den Tischzugriff
		QDateTime	m_AccessDT;								//!< Zeitpunkt der letzten Änderung der Datei
	};

	/*!	Die Liste der Tischinformationen über alle offenen Tische wird in dieser Klasse
		gespeichert. Haupt-Verwendungszweck ist die Anzeige der Offenstände und der abgerechneten
		Tische.
		Der Index der Liste ist generisch, alle gefundenen Tische werden einfach appendet.
		Über diese Liste kann mit Hilfe von TTableInfoIt oder TTableIt iteriert werden. Hierbei
		kann man mit TTableInfoIt nur die Dateiinformationen selbst auswerten, was enorme
		Geschwindigkeitsvorteil bringt, mit TTableIt kann man auf den gesameten Tischnihalt
		zugreifen.
		\brief Hilfsklasse, um Informationen über alle Tische/Archive/Statists zu sammeln.
	*/
	class			TTableList
	: public TValueList
	{
		static const char	fileChache[];
	public:
		static const char	fileTurn[];
	public:
		/*!	Erzeugt eine leere Tischinformationsliste.
			\brief C'tor
		*/
		TTableList(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zersört die Instanz dieser Tischinormationsliste. Das freigeben des Speichers erfolgt in Abhängigkeit
			des Autodelete-Flags.
			\brief dtor.
		*/
		~TTableList()
		{
		}
		/*!	Fügt die Elemente der Liste list dieser Liste hinzu.
			\note Hierbei wird das Autodelete-Flag von list gelöscht, es sollten also keine weiteren Tischinfos zu
				list hinzugefügt werden.
			\param list		Hinzuzufügende Liste mit Tischinformationen.
			\brief Liste hinzufügen.
		*/
		TTableList&	operator += (TTableList& list);
		/*!	Ermittelt die Liste der offenen Tische. Hierzu wird das Table-Verzeichnis eingelesen und die
			sich daraus ergebenden Dateiinformationen jeweils in einer TTableInfo-Instanz gespeichert. Ein Dateizugriff
			findet nicht statt. Aus diesem Grund kann auch keine Überprüfung von etwaigen Kellnerrechten erfolgen.
			\return Liefert die Liste aller gefundenen offenstehenden Tische.
			\note Die Liste ist <b>nicht</b> sortiert!
			\param path		Pfad zur aktuellen Umgebung
			\brief Liste der offenen Tische ermitteln.
		*/
		static TTableList*	getTables(const char* path="");
		/*!	Ermittelt die Liste der bezahlten Tische. Hierzu wird das Archiv-Verzeichnis eingelesen und die
			sich daraus ergebenden Dateiinformationen jeweils in einer TTableInfo-Instanz gespeichert. Ein Datezugriff
			findet nicht statt. Aus diesem Grund kann auch keine Überprüfung von etwaigen Kellnerrechten erfolgen.
			\return Liefert die Liste aller gefunden abgerechneten Tische.
			\note Die Liste ist <b>nicht</b> sortiert!
			\param path		Pfad zur aktuellen Umgebung
			\brief Liste der abgerechneten Tische ermitteln.
		*/
		static TTableList*	getArchives(const char* path="");
		static TTableList*	getSortedArchives(const char* path="");
		/*!	Ermittelt die Liste der Statistfiles für das Datum date. Hierzu wird das entsprechende Statist-Verzeichnis
			eingelesen und die sich daraus ergebenden Dateiinforamtionen jeweils in einer TTableInfo-Instanz gespeichert.
			Ein Dateizugriff findet nicht statt. Aus diesem Grund kann auch keine Überprüfung von etwaigen Kellnerrechten
			erfolgen.
			\return Liefert die Liste aller gefunden abgerechneten Tische.
			\note Die Liste ist <b>nicht</b> sortiert!
			\param date		Das Datum der Statist-Files
			\param path		Pfad zur aktuellen Umgebung
			\brief Liste von Statistfiles ermitteln.
		*/
		static TTableList*	getStatists(const QDate& date, const char* path="")
		{
			return getStatists(date, date, path);
		}
		/*!	Ermittelt die Liste der Statistfiles im Zeitraum from bis to. Hierzu werden die entsprechenden Statist-
			Verzeichnisse eingelesen und die sich daraus ergebenden Dateiinformationen in einer TTableInfo-Instanz
			gespeichert. Ein Dateizugriff findet nicht statt. Aus diesem Grund kann auch keine Überprüfung von etwaigen
			Kellnerrechten erfolgen.
			\return Liefert die Liste aller gefunden Statistfiles für den angegebenen Zeitraum.
			\note Die Liste ist <b>nicht</b> sortiert!
			\param from		Beginn des Zeitrausm der Statist-Files
			\param to		Ende des Zeitrausm der Statist-Files
			\param path		Pfad zur aktuellen Umgebung
			\brief Liste von Statistfiles für einen Zeitraums ermitteln.
		*/
		static TTableList*	getStatists(const QDate& from, const QDate& to, const char* path="");
		static int			turnOver(int z, const QDate& date, TWaiter* waiter, const char* path="");
	};

	class			TTableInfoIt
	: public TValueListIt
	{
	public:
		TTableInfoIt(TTableList& list)
		: TValueListIt(list)
		{
		}
		TTableInfo*	operator () ()
		{
			return (TTableInfo*) TValueListIt::operator()();
		}
		TTableInfo*	toFirst()
		{
			return (TTableInfo*) TValueListIt::toFirst();
		}
		TTableInfo*	current()
		{
			return (TTableInfo*) TValueListIt::current();
		}
		TTableInfo*	operator ++ ()
		{
			return (TTableInfo*) TValueListIt:: operator ++();
		}
	};

	class			TTableIt
	{
	public:
		TTableIt(TTableList& list, bool all, int mask, TWaiter* waiter, const char* path)
		: m_It(list)
		, m_All(all)
		, m_Mask(mask)
		, m_Waiter(waiter)
		, m_Path(path)
		, m_Waiters(NULL)
		, m_Teams(NULL)
		, m_ShowLocked(FALSE)
		{
		}
		void		setShowLocked(bool b)
		{
			m_ShowLocked=b;
		}
		void		setTeams(TWaiters* waiters, TWaiterTeams* teams)
		{
			m_Waiters = waiters;
			m_Teams = teams;
		}
		TTableInfo*	info()
		{
			return m_It.current();
		}
		TTable*		operator () ()
		{
			return current();
		}
		TTable*		toFirst()
		{
			TTableInfo* info = m_It.toFirst();
			if( !info )
				return NULL;
			TTable* table = info->loadTable(m_All, m_Mask, m_Waiter, m_Waiters, m_Teams, m_Path);
			int state = table->getState();
			if(state==TTable::stAccess)
				return table;
			if( state==TTable::stLocked&&m_ShowLocked )
				return table;
			info->freeTable();
			return operator ++();
		}
		TTable*		current()
		{
			TTableInfo* info = m_It.current();
			if( !info )
				return NULL;
			TTable* table = info->loadTable(m_All, m_Mask, m_Waiter, m_Waiters, m_Teams, m_Path);
			int state = table->getState();
			if(state==TTable::stAccess)
				return table;
			if( state==TTable::stLocked&&m_ShowLocked )
				return table;
			info->freeTable();
			return operator ++();
		}
		TTable*		operator ++ ();
	protected:
		bool		checkMask(TTable* table);
	protected:
		TTableInfoIt	m_It;
		bool			m_All;
		int				m_Mask;
		TWaiter*		m_Waiter;
		QString			m_Path;
		TWaiters*		m_Waiters;
		TWaiterTeams*	m_Teams;
		bool			m_ShowLocked; // damit auch tische die stLocked haben vom iterator geliefert werden
	};
}

using namespace PosLib;

#endif
