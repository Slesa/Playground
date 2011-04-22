#ifndef				_POSLIB_TTABLE_

#define				_POSLIB_TTABLE_
#include			"poslib/ttableaction.h"
#include			"poslib/ttableentry.h"
#include			"poslib/ttablebalance.h"
#include			"poslib/ttaxgroup.h"
#include			"poslib/twaiter.h"
#include			"poslib/twaiterteam.h"
#include			"poslib/tcounter.h"
#include			"poslib/tdiscount.h"
#include			"poslib/tsubvention.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\todo Beim retten eines Tisches Signal fr Restbetrag werfen.
		\todo Tischlisten aufbauen
		\todo Archivnummer erh?en beim Zugriff
	*/
	class			TTable
	: public QObject
	, public TValue
	{
		Q_OBJECT
	public:
		// -------- Enums --------------------------------------------------------
		enum		Masks
		{
			mskNone			= 0
		,	mskLocked		= bit0				//!< Tisch ist gelockt
		,	mskOwn			= bit1				//!< Tisch geh?t dem Kellner
		,	mskAccess		= bit2				//!< Tisch geh?t anderem, aber Zugriff m?lich
		,	mskNoAccess		= bit3				//!< Tisch geh?t anderem, Zugriff nicht m?lich
		,	mskTeam			= bit4				//!< Tisch geh?t anderem aus dem Kellnerteam
		};
		/*!	Folgende Arten von Typen k?nen auftreten:
			\brief Die m?lichen Typen von Tischen.
		*/
		enum		Types
		{
			Unknown			= 0					//!< Typ noch nicht zugeordnet
		,	Table			= 1					//!< Tisch liegt unter var/tables
		,	Archive			= 2					//!< Tisch liegt unter var/archive
		,	Statist			= 3					//!< Tisch liegt unter var/statist/<datum>
		,	Cashing			= 4					//!< Archiv fr Ein- bzw Auszahlungen
		};
		/*!	Folgende Zust?de k?nen bei einem Tisch auftreten:
			\brief Die m?lichen Zust?de eines Tisches.
		*/
		enum		States
		{
			stUnknown		= 0					//!< Status unbekannt
		,	stAccess		= 1					//!< Tisch kann bearbeitet werden
		,	stLocked		= 2					//!< Tisch ist gesperrt
		,	stCrashed		= 3					//!< Tisch ist kaputt
		,	stMasked		= 4					//!< Tisch wurde ausmaskiert
		};
	public:
		// -------- Constants ----------------------------------------------------
		static const char	entryNulled[];			//!< Leerstorniert / -gesplittet
		static const char	entryTable[];
		static const char	entryParty[];
		static const char	entryArchive[];
		static const char	entryRound[];
		static const char	entryAmount[];
		static const char	entryMaxAmount[];
		static const char	entryDiscSum[];
		static const char	entryInvisible[];

		static const char	entryVatOH[];
		static const char	entryInSummary[];
		static const char	entryBillNum[];
		static const char	entryRestored[];
		static const char	entryClient[];
		static const char	entryClientName[];
		static const char	entryGuestAddr[];
		static const char	entryPrfinding[];
		static const char	entryClfinding[];
		static const char	entryPricelevel[];

		static const char	entryBATResDate[];
		static const char	entryBATResNo[];
		static const char	entryBATLastName[];
		static const char	entryBATFirstName[];
		static const char	entryBATCustomerNo[];
		static const char	entryBATSex[];
		static const char	entryBATCompany[];
		static const char	entryBATNumberOfPersons[];
		static const char	entryBATNumberOfChildren[];
		static const char	entryBATFonNr[];
		static const char	entryBATFonNoG[];
		static const char	entryBATMobilNr[];
		static const char	entryBATEmail[];
		static const char	entryBATStreet[];
		static const char	entryBATZipCode[];
		static const char	entryBATCity[];
		static const char	entryBATResTime[];
		static const char	entryBATSmokerTable[];
		static const char	entryBATState[];
		static const char	entryBATResState[];
		static const char	entryBATComment[];
		static const char	entryBATUserInt1[];
		static const char	entryBATUserInt2[];
		static const char	entryBATUserInt3[];
		static const char	entryBATUserString1[];
		static const char	entryBATUserString2[];
		static const char	entryBATUserString3[];
		static const char	entryBATFoodConv[];
		static const char	entryBATBeverageConv[];
		static const char	entryBATNonFoodConv[];
		static const char	entryBATTotalConv[];
		static const char	entryBATTableNo[];
		static const char	entryBATIdent[];

		static const char	entryPayform[];
		static const char	entryPayformName[];
//		static const char	entryPayVat[];

		static const char	entryCheckLine[];		//!< USA: Guestcheck gedruckt bis Zeile
		static const char	entryCheckEntry[];		//!< USA: Guestcheck gedruckt bis Tischeintrag
//		static const char	entryIgnoreVats[];		//!< USA: Gewisse Mwst-Sätze ignorieren

		static const char	entryLimitMinGuest[];	//!< Minimum pro Gast inkl. H?e der selben / Gesamt einstellbarmachen
		static const char	entryScannerCard[];		//!< Bei manuellem Rabatt mit Karte die Kartennummer
	public:
		static const char	pathActiveTables[];		//!< Verzeichnis der tische die gerade geöffnet sind
		static const char	pathTables[];			//!< Tisch-Verzeichnis
		static const char	pathArch[];				//!< Archiv-Verzeichnis
		static const char	pathStatist[];			//!< Statist-Verzeichnis
	public:
		// -------- ctors/dtors --------------------------------------------------
		/*!	Erzeugt eine Instanz von TTable als Typ Table.
			\param table	Tischnummer
			\param party	Parteinummer
			\param waiter	Zugreifender Kellner
			\brief ctor.
		*/
		TTable(long table, int party, TWaiter* waiter=NULL, const char* path="");
		/*!	Erzeugt eine Instanz von TTable als Typ Archive.
			\param arch		Archivnummer
			\param waiter	Zugreifender Kellner
			\brief ctor.
		*/
		TTable(long arch, TWaiter* waiter=NULL, const char* path="");
		/*!	Erzeugt eine Instanz von TTable als Typ Statist.
			\param date		Datum des Statist-Ordners
			\param arch		Archivnummer
			\param waiter	Zugreifender Kellner
			\brief ctor.
		*/
		TTable(const QDate& date, long arch, TWaiter* waiter=NULL, const char* path="");
		/*!	Erzeugt eine Instanz von TTable als Typ Cashing.
			\param waiter	Zugreifender Kellner
			\brief ctor.
		*/
		TTable(TWaiter* waiter, const char* path="");
		/*!	Zerst?t die Instanz von TTable.
			\brief dtor.
		*/
		~TTable();
		/*!	Exportiert die Liste in eine XML-Datei unterhalb des Knotens root.
			\param root		Root-Knoten, in den die Liste eingefgt wird.
			\brief Liste nach XML exportieren.
			\note getListName() und getElementName() mssen hierfr berschrieben worden sein.
		*/
		virtual void	importXml(const QDomElement& root);
		/*!	Importiert die Liste aus der XML-Datei doc aus dem Knoten root.
			\param doc		Die XML-Datei
			\param root		Root-Knoten, aus dem die Liste importiert wird.
			\brief Liste aus XML importieren.
			\note getListName() und getElementName() mssen hierfr berschrieben worden sein.
		*/
		virtual void	exportXml(QDomDocument& doc, QDomElement& root);
		double		calcMaxAmount();
		void		getTaxes(TTaxGroups& grps);
		void		recalcTaxes(bool oh);
		void		setAsOrdered(bool on)
		{
			m_AsOrdered = on;
		}
	public:
		// -------- Zugriff auf Members ------------------------------------------
		static int	isActive(long t,int p,const QString path);
		
		int			activeTerminalId()
		{
			return m_ActiveTermId;
		}
		bool		hasPayform() const
		{
			return hasValue(entryPayform);
		}
		int			getPayform() const
		{
			return getValue(entryPayform, 0);
		}
		QString		getPayformName() const
		{
			return getString(entryPayformName);
		}
		void		setPayform(int id, const QString& name)
		{
			setValue(entryPayform, id);
			setValue(entryPayformName, name);
		}
	/*
		bool		getPayVat() const
		{
			return getValue(entryPayVat, FALSE);
		}
		void		setPayVat(bool oh)
		{
			if( !oh )
				clrValue(entryPayVat);
			else
				setValue(entryPayVat, oh);
			emit vatChanged(this, oh);
		}
	*/
		/*!	\return den Status des Tisches wie unter States definiert.
			\brief Status des Tisches ermitteln.
		*/
		int			getState() const
		{
			return m_State;
		}
		/*!	\return Liefert den Typ des Tisches wie unter Types definiert.
			\brief Typ des Tisches ermitteln.
		*/
		int			getType() const
		{
			return m_Type;
		}
		int			hasPriceFinding() const
		{
			return hasValue(entryPrfinding);
		}
		int			getPriceFinding() const
		{
			return getValue(entryPrfinding, 0);
		}
		bool		hasClientFinding() const
		{
			return hasValue(entryClfinding);
		}
		int			getClientFinding() const
		{
			return getValue(entryClfinding, 0);
		}
		int			getPricelevel() const
		{
			return getValue(entryPricelevel, 0);
		}
		void		setPricelevel(int level)
		{
			if( !level )
				clrValue(entryPricelevel);
			else
				setValue(entryPricelevel, level);
		}
		/*!	\return Liefert TRUE, wenn der Tisch ge?dert wurde, d.h. wenn er
			gespeichert werden mu?
			\brief Tischinhalt ge?dert?
		*/
		bool		wasChanged() const
		{
			return m_Changed;
		}
		/*!	?dert das Flag, ob der Tisch ge?dert wurde und damit gespeichert
			werden mu?
			\param flag		TRUE hei?, Tisch wurde ge?dert.
			\brief Flag setzen, ob Tischinhalt ge?dert wurde.
		*/
		void		setChanged(bool flag=TRUE)
		{
			m_Changed = flag;
		}
		/*!	\return Liefert den Kellner, der gerade auf den Tisch zugreift.
			\brief Zugriffskellner ermitteln.
		*/
		TWaiter*	getWaiter() const
		{
			return m_Waiter;
		}
		void		setWaiter(TWaiter* waiter)
		{
			if( waiter )
				m_Waiter = waiter;
		}
				/*!	\return den Archivkellner des Tisches.
			\brief Archivkellner des Tisches ermitteln.
		*/
		int				getArchWaiter();
		/*!	\return Liefert den Create-Vorgang des Tisches.
			\note Vor dem Zugriff auf diese Funktion ist der Status des Tisches mit getState()
			auf stAccess zu prfen.
			\brief Create-Vorgang ermitteln.
		*/
		TTableCreate&	getCreate()
		{
			return m_Create;
		}
		/*!	\return Liefert die bereits verbuchten Vorg?ge des Tisches.
			\note Vor dem Zugriff auf diese Funktion ist der Status des Tisches mit getState()
			auf stAccess zu prfen.
			\brief Verbuchte Vorg?ge ermitteln.
		*/
		TTableEntries&	getEntries()
 		{
			return m_Entries;
		}
		/*!	\return Liefert die noch nicht verbuchten Vorg?ge des Tisches.
			\note Vor dem Zugriff auf diese Funktion ist der Status des Tisches mit getState()
			auf stAccess zu prfen.
			\brief Unverbuchte Vorg?ge ermitteln.
		*/
		TTableEntries&	getCurrent()
		{
			return m_Current;
		}
		/*!	\return Liefert alle Bestell-Vorg?ge, die noch nicht verbucht wurden.
			\param controls	Wenn TRUE, werden auch Drucker-Anweisungen mitgelifert.
			\note Die zurckgelieferte Liste mu?nach dem Zugriff gel?cht werden.
			\brief Unverbuchte Bestell-Vorg?ge ermitteln.
		*/
		TTableEntries*	getOrders(bool controls=FALSE)
		{
			return m_Current.getEntries(TTableEntry::Order, controls);
		}
		/*!	\return Liefert alle Storno-Vorg?ge, die noch nicht verbucht wurden.
			\note Die zurckgelieferte Liste mu?nach dem Zugriff gel?cht werden.
			\brief Unverbuchte Storno-Vorg?ge ermitteln.
		*/
		TTableEntries*	getVoids()
		{
			return m_Current.getEntries(TTableEntry::Void);
		}
		TTableEntries*	getSplits()
		{
			return m_Current.getEntries(TTableEntry::Split);
		}
		/*!	\return Liefert alle Bestell-Vorg?ge, die bereits verbucht wurden.
			\param controls	Wenn TRUE, werden auch Drucker-Anweisungen mitgelifert.
			\note Die zurckgelieferte Liste mu?nach dem Zugriff gel?cht werden.
			\brief Verbuchte Bestell-Vorg?ge ermitteln.
		*/
		TTableEntries*	getOldOrders(bool controls=FALSE)
		{
			return m_Entries.getEntries(TTableEntry::Order, controls);
		}
		TTableEntries*	getOldVoids()
		{
			return m_Entries.getEntries(TTableEntry::Void);
		}
		/*!	\return Liefert alle Bezahl-Vorg?ge, die bereits verbucht wurden.
			\note Die zurckgelieferte Liste mu?nach dem Zugriff gel?cht werden.
			\brief Verbuchte Bezahl-Vorg?ge ermitteln.
		*/
		TTableEntries*	getOldPays()
		{
			return m_Entries.getEntries(TTableEntry::Pay);
		}
		/*!	\return Liefert alle Archivierungs-Vorg?ge, die bereits verbucht wurden.
			\note Die zurckgelieferte Liste mu?nach dem Zugriff gel?cht werden.
			\brief Verbuchte Archivier-Vorg?ge ermitteln.
		*/
		TTableEntries*	getOldArchiveds()
		{
			return m_Entries.getEntries(TTableEntry::Archived);
		}
		/*!	\return Liefert das Datum und die Uhrzeit des letzten gültigen Archivvorgangs.
			\note Der letzte gültige Archivvorgang ist der der nicht 'gevoided' wurde.
			\brief Zeit und Datum des letzten gültigen Archivvorgangs.
		*/
		QDateTime		getArchDate();
		QDate			getStatDate()
		{
			return m_Date;
		}
	public:
		// -------- Zugriff auf Tischinhalt --------------------------------------
		/*!	\return Liefert die gespeicherte Tischnummer.
			\note Falls der Typ von TTable Types::Table ist, wird beim Laden die Tischnummer
			der des ctors angepa?.
			\brief Tischnummer ermitteln.
		*/
		long		getTable() const
		{
			return getValue(entryTable, 0);
		}
		/*
		void		setTable(long table)
		{
			setValue(entryTable, table);
			emit tableChanged(table);
		}
		*/
		/*!	\return Liefert die gespeicherte Parteinummer.
			\note Falls der Typ von TTable Types::Table ist, wird beim Laden die Parteinummer
			der des ctors angepa?.
			\brief Parteinummer ermitteln.
		*/
		int			getParty() const
		{
			return getValue(entryParty, 0);
		}
		/*
		void		setParty(int party)
		{
			setValue(entryParty, party);
			emit partyChanged(party);
		}
		*/
		QDate		getTableDate() const
		{
			return m_Date;
		}
		/*!	\return Liefert die gespeicherte Archivnummer.
			\note Falls der Typ von TTable Types::Archive oder Types::Statist ist, wird beim
			laden die Archivnummer der des ctors angepa?.
			\brief Archivnummer ermitteln.
		*/
		long		getArchive() const
		{
			return getValue(entryArchive, 0);
		}
		/*!	?dert die Archivnummer auf arch. Diese Funktion wird nur von load bzw open aufgerufen.
			\param arch		Neue Archivnummer.
			\note Diese Funktion sollte nur im Office verwendet werden!
			\brief Tischnummer ?dern.
		*/
		void		setArchive(long arch)
		{
			setValue(entryArchive, arch);
		}
		/*!	\return Liefert die aktuelle Rundennummer des Tisches. Bei jedem Aufruf von commit()
			wird die Rundennummer erh?t, somit werden einzelne Vorg?ge zu Runden zusammengfa?.
			\brief Rundennummer ermitteln.
		*/
		int			getRound() const
		{
			return getValue(entryRound, 1);
		}
		/*!	\return Liefert den noch zu zahlenden Restbetrag des Tisches.
			\brief Restbetrag ermitteln.
		*/
		double		getDAmount() const
		{
			return getValue(entryAmount, 0.0);
		}
		long		getAmount() const
		{
			return lRound(getValue(entryAmount, 0.0));
		}
		/*!	?dert den Restbetrag des Tisches auf amount.
			\param amount	Neuer Restbetrag des Tisches.
			\brief Tisch-Restbetrag ?dern.
		*/
		void		setAmount(double amount)
		{
			setValue(entryAmount, amount);
			emit amountChanged(this, amount);
		}
		/*!	\return Liefert den Gesamtwert des Tisches. Dieser berechnet sich aus der Summe der
			Orders abzglich der bereits get?igten Zahlvorg?ge. Der Tischbetrag wird bei jedem
			Aufruf von close() neu berechnet.
			\brief Tischwert ermitteln.
		*/
		double		getDMaxAmount()
		{
			return getValue(entryMaxAmount, 0.0);
		}
		long		getMaxAmount()
		{
			return lRound(getValue(entryMaxAmount, 0.0));
		}
		long		getDiscSum()
		{
			return getValue(entryDiscSum, 0L);
		}
		void		setDiscSum(long amount)
		{
			setValue(entryDiscSum, amount);
			emit discountChanged(this, amount);
		}
		/*!	\return Liefert TRUE, wenn der Tisch nicht archiviert werden soll. Nachdem alle
			Vorg?ge abgeschlossen sind und der Tisch komplett bezahlt wurde, wird der Tischinhalt
			verworfen.
			\brief Tischinhalt nur tempor??
		*/
		bool		isInvisible() const
		{
			return getValue(entryInvisible, FALSE);
		}
		/*!	?dert das Tempor?-Flag des Tisches auf flag. Wenn ein Tisch als tempor? markiert wird,
			wird dessen Tischinhalt nach dem vollst?digen Bezahlen verworfen und erscheint nicht
			unter Archive.
			\param flag	TRUE hei?, der Tischinhalt ist tempor?.
			\brief Tisch als tempor? markieren.
		*/
		void		setInvisible(bool flag)
		{
			m_Changed = TRUE;
			setValue(entryInvisible, flag);
		}
		/*!	\return Liefert TRUE, wenn der Tischinhalt aus Archive restauriert worden ist, d.h. der
			Tisch bereits einmal vollst?dig bezahlt wurde.
			\brief Tisch restauriert?
		*/
		bool		wasRestored() const
		{
			return getValue(entryRestored, FALSE);
		}
		/*!	?dert das Flag, ob der Tisch bereits einmal vollst?dig bezahlt worden ist und danach
			gerettet wurde. Nach dem Retten eines Tisches werden bei allen Tischaktion andere
			Kellnerrechte abgefragt.
			\param flag	Wenn TRUE, wurde der Tisch restauriert.
			\brief Tisch als bereits gerettet markieren.
		*/
		void		setRestored(bool flag)
		{
			setValue(entryRestored, flag);
		}
		/*!	\return Liefert TRUE, wenn der Tisch auf "Au?r Haus" bezahlt worden ist. Beim Bezahlen
			k?nen nicht "Im Haus" und "Au?r Haus" auf dem selben Tisch gemischt werden, da sonst
			die MWST der Bestellungen nicht ermittelt werden kann.
			\brief Tisch bereits auf "Au?r Haus" bezahlt worden?
		*/
		bool		isVatOH() const
		{
			return getValue(entryVatOH, FALSE);
		}
		void		setVatOH(bool flag)
		{
			bool old = isVatOH();
			setValue(entryVatOH, flag);
			if( old!=flag )
				recalcTaxes(flag);
		}

		bool		hasBillNum() const
		{
			return hasValue(entryBillNum);
		}
		long		getBillNum() const
		{
			return getValue(entryBillNum, 0);
		}
		void		setBillNum(long num)
		{
			setValue(entryBillNum, num);
		}
		bool		hasInSummary() const
		{
			return hasValue(entryInSummary);
		}
		bool		inSummary() const
		{
			return getValue(entryInSummary, FALSE);
		}
		void		setInSummary(bool flag)
		{
			setValue(entryInSummary, flag);
		}
		bool		hasClient() const
		{
			return hasValue(entryClient);
		}
		long		getClient() const
		{
			return getValue(entryClient, 0);
		}
		void		setClient(long client)
		{
			setValue(entryClient, client);
		}
		QString		getClientName() const
		{
			return getString(entryClientName);
		}
		void		setClientName(const QString& name)
		{
			setValue(entryClientName, name);
		}
		QString		getGuestAddr() const
		{
			return getString(entryGuestAddr);
		}
		void		setGuestAddr(const QString& str)
		{
			setValue(entryGuestAddr, str);
		}
		bool		hasGuestCount() const
		{
			return hasValue(TTablePay::entryGuestCount);
		}
		int			getGuestCount() const
		{
			return getValue(TTablePay::entryGuestCount, 1);
		}
		void		setGuestCount(int count);
		int			getCheckLine() const
		{
			return getValue(entryCheckLine, 0);
		}
		int			getCheckEntry() const
		{
			return getValue(entryCheckEntry, 0);
		}
		void		setGuestcheck(int line, int entry)
		{
			setValue(entryCheckLine, line);
			setValue(entryCheckEntry, entry);
			setChanged(TRUE);
		}
		bool		hasTaxGroups()
		{
			return hasValue(TTableOrder::entryTaxGroupsIH) || hasValue(TTableOrder::entryTaxGroupsOH);
		}
		long		getTaxAmount() const
		{
			long ret = getValue(TTableOrder::entryTaxAmount, 0L);
			return ret*getGuestCount();
		}
		bool		hasLimitMinGuest()
		{
			return hasValue(entryLimitMinGuest);
		}
		long		getLimitMinGuest()
		{
			return getValue(entryLimitMinGuest, 0L);
		}
		void		setLimitMinGuest(long sum)
		{
			if( !sum )
				clrValue(entryLimitMinGuest);
			else
				setValue(entryLimitMinGuest, sum);
			setChanged();
		}

/*		QStringList	getIgnoreVats() const
		{
			return QStringList::split(":", getString(entryIgnoreVats));
		}
		void		setIgnoreVats(const QStringList& vats)
		{
			if( !vats.count() )
				clrValue(entryIgnoreVats);
			else
				setValue(entryIgnoreVats, vats.join(":"));
		}*/
		long		getNetAmount();
		QStringList	getTaxAmounts();
		void		clrTaxGroups();
		void		addTaxGroupIH(TVatRate* vat);
//		void		addTaxGroupIH(int id, const QString& name, int rate, int type, bool disc);
		void		addTaxGroupOH(TVatRate* vat);
//		void		addTaxGroupOH(int id, const QString& name, int rate, int type, bool disc);
		QStringList	getTaxGroups(bool oh);
		void		removeTaxes();
		void		calcTaxes(bool oh=FALSE);
		void		setActive(bool active,int termid); // erzeugt oder löscht das lockfile für schnelle offenstände
		bool		checkActive();
		int			isActive(); // check ob das lockfile vorhanden ist und liefert die terminalnummer zurück oder 0 wenn tisch frei ist
	public:
		// -------- Statische Dateinamens-Funktionen -----------------------------
		/*!	\return Liefert den Pfadnamen der aktivdateien für geöffnete tische
			\param path	Root-Pfad der Laufumgebung
			\note Der Pfad wird ggf angelegt.
			\brief Pfad der aktivdateien für geöffnetet tische ermitteln.
		*/
		static QString	getActivePath(const char* path="")
		{
			QString p = TDir::checkPath(TDir::checkPath(path)+pathActiveTables);
			return p;
		}
		/*!	\return Liefert den Pfadnamen fr einen Tisch im Verzeichnis path.
			\param path	Root-Pfad der Laufumgebung
			\note Der Pfad wird ggf angelegt.
			\brief Pfad fr einen Tisch ermitteln.
		*/
		static QString	getTablePath(const char* path="")
		{
			QString p = TDir::checkPath(TDir::checkPath(path)+pathTables);
			return p;
		}
		/*!	\return Liefert den Pfadnamen fr ein Archiv im Verzeichnis path.
			\param path	Root-Pfad der Laufumgebung
			\note Der Pfad wird ggf angelegt.
			\brief Pfad fr ein Archiv ermitteln.
		*/
		static QString	getArchPath(const char* path="")
		{
			QString p = TDir::checkPath(TDir::checkPath(path)+pathArch);
			return p;
		}
		/*!	\return Liefert den Pfadnamen fr ein Statist-File im Verzeichnis path.
			\param date	Datum des Statists
			\param path	Root-Pfad der Laufumgebung
			\note Der Pfad wird ggf angelegt.
			\brief Pfad fr ein Statist-File ermitteln.
		*/
		static QString	getStatPath(const QDate& date, const char* path="")
		{
			QString tmp;
			tmp.sprintf("tag%02d.%02d.%04d", date.day(), date.month(), date.year());
			QString p = TDir::checkPath(TDir::checkPath(TDir::checkPath(path, FALSE)+pathStatist, FALSE)+tmp, FALSE);
			return p;
		}
		/*!	\return Liefert den Dateinamen fr ein Table-File im Verzeichnis path.
			\param table	Tischnummer
			\param party	Parteinummer
			\param path		Root-Pfad der Laufumgebung
			\brief Dateinamen fr ein table-File ermitteln.
		*/
		static QString	getFileName(long table, int party, const char* path="")
		{
			QString file = "t"+QString::number(table)+"."+QString::number(party).rightJustify(3, '0');
//			file.sprintf("t%ld.%03d", table, party);
			return getTablePath(path)+file;
		}
		/*!	\return Liefert den Dateinamen fr ein Archiv-File im Verzeichnis path.
			\param arch		Archivnummer
			\param path		Root-Pfad der Laufumgebung
			\brief Dateinamen fr ein Archiv-File ermitteln.
		*/
		static QString	getFileName(long arch, const char* path="");
		/*!	\return Liefert den Dateinamen fr ein Statist-File im Verzeichnis path.
			\param date		Datum des Statists
			\param arch		Archivnummer
			\param path		Root-Pfad der Laufumgebung
			\brief Dateinamen fr ein Statist-File ermitteln.
		*/
		static QString	getFileName(const QDate& date, long arch, const char* path="");
		/*!	\return Liefert TRUE, wenn ein Tisch mit den angegebenen Parametern im Pfad path
			bereits existiert.
			\param table	Tischnummer
			\param party	Parteinummer
			\param path		Root-Pfad der Laufumgebung
			\brief Existiert der Tisch?
		*/
		static bool	exists(long table, int party, const char* path="")
		{
			return QFile::exists(getFileName(table, party, path));
		}
		/*!	\return Liefert TRUE, wenn ein Archiv mit den angegebenen Parametern im Pfad path
			bereits existiert.
			\param arch		Archivnummer
			\param path		Root-Pfad der Laufumgebung
			\brief Existiert das Archiv?
		*/
		static bool	exists(long arch, const char* path="")
		{
			return QFile::exists(getFileName(arch, path));
		}
		/*!	\return Liefert TRUE, wenn ein Statist mit den angegebenen Parametern im Pfad path
			bereits existiert.
			\param date		Datum des Statists
			\param arch		Archivnummer
			\param path		Root-Pfad der Laufumgebung
			\brief Existiert das Statist?
		*/
		static bool	exists(const QDate& date, long arch, const char* path="")
		{
			return QFile::exists(getFileName(date, arch, path));
		}
	public:
		// -------- Nicht-Statische Dateinamens-Funktionen -----------------------
		/*!	\return Liefert je nach Typ des Tisches den Pfadnamen fr den Tisch.
			Wenn der Typ des Tisches unbekannt ist, liefert die Funktion QString::null.
			\note Der Pfad wird ggf angelegt.
			\brief Pfad fr einen Tisch ermitteln.
		*/
		QString		getPath();
		/*!	\return Liefert je nach Typ des Tisches den Dateinamen fr den Tisch.
			Wenn der Typ des Tisches unbekannt ist, liefert die Funktion QString::null.
			\brief Dateinamen fr einen Tisch ermitteln.
		*/
		QString		getFileName();
		/*!	\return Liefert TRUE, wenn der Tisch je nach Typ bereits existiert.
			\brief Existiert der Tisch?
		*/
		bool		exists();
		// -------- Laden und speichern ------------------------------------------
		/*!	Liest den Tischinhalt je nach Typ.
			\note Der Tisch wird dabei nicht gelockt.
			\return Liefert einen err...-Wert, falls die Datei nicht gelesen werden konnte oder nicht
			gefunden wurde. Falls die Datei gelockt ist, liefert sie trotzdem errNone, setzt aber den
			Status der Datei auf stLocked.
			\brief Tisch aus Datei lesen.
		*/
		int			load(bool all, int mask, TWaiters* waiters=NULL, TWaiterTeams* teams=NULL)
		{
			return load(getFileName(), all, mask, waiters, teams);
		}
		/*!	Speichert den Tischinhalt je nach Typ.
			\return Liefert einen err...-Wert, falls die Datei nicht geschrieben werden konnte.
			\brief Tisch in Datei schreiben.
		*/
		int			save()
		{
			return save(getFileName());
		}
		/*!	?fnet den Tisch zum Lesen und Schreiben, so da?der Tisch gelockt
			bleibt bis zum Aufruf von close(). Wenn create TRUE ist und der Tisch
			noch nicht angelegt wurde, wird er erzeugt. Ist create FALSE und der
			Tisch existiert noch nicht, liefert die Funktion einen Fehler.
			\note Im Gegensatz zum Load-Befehl werden hier die Kellner-Rechte abgefragt.
			\param create	Flag, ob der Tisch ggf anzulegen ist.
			\param term		Terminal, das den Tisch ?fnet, wird beim Erzeugen ben?igt.
			\param center	Nummer derKostenstelle, an der der Tisch ge?fnet wird, wird beim Erzeugen ben?igt.
			\param ccenter	Die komplette Kostenstellen-Information, sofern vorhanden. Wird beim Erzeugen ben?igt.
			\param cfg		Tisch-Informationen, sofern vorhanden. Wird beim Erzeugen ben?igt.
			\return errNone, falls der Tisch ge?fnet werden konnte.
			\brief Tisch ?fnen.
		*/
		int			open(bool create, int term=0, int center=0, TCostCenter* ccenter=0, TTableCfg* cfg=0);
		/*!	?fnet den Tisch als Archiv zum Lesen und Schreiben, so da?das Archiv gelockt bleibt
			bis zum Aufruf von close().
			\note Im Gegensatz zum Load-Befehl werden hier die Kellner-Rechte abgefragt.
			\return errNone, falls das Archiv ge?fnet werden konnte.
			\brief Archiv ?fnen.
		*/
		int			openArch();
		/*!	?ernimmt alle Elemente aus der Liste der neuen Eintr?e m_Current in die
			Liste der bestehenden Eintr?e m_Entries und schlie? damit alle Aktionen
			ab. Falls unter den abgeschlossenen Aktion Orders waren, wird die Rundenzahl um 1
			erh?t.
			\note Wird automatisch von close aufgerufen.
			\return Liefert TRUE, wenn der Tisch bisher noch keine Eintr?e hatte, m_Entries also
			leer war.
			\brief Alle Aktionen auf den Tisch bernehmen.
		*/
		bool		commit();
		/*!	Schlie? einen mit open ge?fneten Tisch bzw ein mit openArch ge?fnetes Archiv
			und gibt den Tisch dadurch wieder zur Bearbeitung frei. Neu hinzugekommene Aktionen
			werden mittels commit in den Tischpuffer bertragen.
			\brief Tisch schlie?n.
		*/
		void		close();
	public:
		// -------- POS-Funktionen -----------------------------------------------
		int			cash(long amount, const QString& reason, long foreign, TCurrency* curr, int term=0, int center=0, TCostCenter* ccenter=0);
		int			balance(TTableBalance* entry, int term=0, int center=0, TCostCenter* ccenter=0);
		/*!	?dert den Besitzer des Tisches auf waiter. Besitzer ist der im Create-Vorgang
			angegebene Kellner.
			\param waiter	Der neue Besitzer des Tisches.
		*/
		void		changeOwner(TWaiter* waiter);
		/*!	Wechselt den Tisch auf die in dest angegebene Tisch- und Parteinummer.
			\param change	Tischwechsel-Informationen
			\param dest		Zieltisch
			\brief Tisch wechseln.
			\note Wenn der Zieltisch bereits existiert, wird der aktuelle Tischinhalt angefgt.
			\return Liefert errNone wenn der Tisch gewechselt werden konnte.
		*/
		int			change(TTableChange* change, TTable* dest);
		/*!	Fgt die Bestellung order hinzu. Wenn der selbe Artikel noch einmal bestellt wird,
			werden alte und neue Bestellung zusammengefa?.
			Die Bestellung wird mit commit() im Tisch eingetragen.
			\param order		Die neue Bestellung.
			\param copied		Flag, ob die Orderdaten von einer bestehenden Order kopiert wurden.
			\brief Bestellung zum Tisch hinzufgen.
		*/
		void		order(TTableOrder* order, bool copied=FALSE);
		/*!	Fgt das Storno Void zum Tischinhalt hinzu.
			\param Void		Der neue Storno-Vorgang.
			\brief Storno zum Tisch hinzufgen.
		*/
		void		doVoid(TTableVoid* Void)
		{
//			setData(Void);
			Void->setDate(QDate::currentDate());
			Void->setTime(QTime::currentTime());
			m_Current.append(Void);
			setAmount(getAmount()-Void->getAmount());
			emit voided(this, Void);
		}
		/*!	Fgt den Splitt-Vorgang split zum Tischinhalt hinzu.
			\param split	Der neue Splitt-Vorgang.
			\brief Splitting zum Tisch hinzufgen.
		*/
		void		doSplit(TTableSplit* split)
		{
			setData(split);
			m_Current.append(split);
			setAmount(getAmount()-split->getAmount());
			emit splitted(this, split);
		}
		/*!	Fgt den Drucker-Kontrollartikel contr zum Tischinhalt hinzu. Wenn der selbe
			Kontrollartikel bereits eingefgt wurde, wird nichts hinzugefgt.
			\param contr	Der neue Kontrollartikel.
			\brief Kontrollartikel zum Tisch hinzufgen.
		*/
		void		control(TTableControl* contr)
		{
			if( m_Current.getMaximum()>1 )
			{
				TTableControl* entry = (TTableControl*) m_Current[m_Current.getMaximum()-1];
				if( entry && entry->getType()==TTableEntry::Control && entry->getControl()==contr->getControl() )
				{
					delete contr;
					return;
				}
			}
			m_Current.append(contr);
			emit control(this, contr);
		}
		/*!	Fgt den Zahlvorgang _pay zum aktuellen Tischinhalt hinzu. Wenn dadurch der Restbetrag des
			Tisches 0 wird, wird der Tisch archiviert, ausgenommen _pay ist NULL. In diesem Fall mu
			der Rckgabewert von pay ausgewertet werden und ggf die Funktion archive aufgerufen werden.
			Die Unterscheidung, ob _pay NULL ist, mu?deswegen vorgenommen werden, da beim Komplettstorno
			eines Tisches darauf gebaut wird, da?der Tisch danach weg ist.
			\return Liefert TRUE, wenn der Tisch komplett bezahlt wurde und archive aufgerufen
			werden mu?
			\param waiter	Kellner, der den Zahlvorgang durchfhrt
			\param _pay		Der neue Zahlvorgang.
			\param center	Kostenstelle an der bezahlt wird.
			\param ccenter	Weitere Konstenstelleninformationen (sofern verfgbar)
			\param path		Root-Pfad zur Laufumgebung.
			\brief Zahlvorgang zum Tisch hinzufgen.
		*/
		bool		pay(TWaiter* waiter,TTablePay* _pay, int center, TCostCenter* ccenter=NULL);
		bool		unpay(TTablePay* _pay);
		void		archive(TWaiter* waiter,TTablePay* _pay, int center, TCostCenter* ccenter=NULL);
		/*!	Holt den Tisch aus Archive wieder nach Tables zurck. Wenn table und party nicht 0 sind,
			bekommt er diese neue Tischnummer. Wernn der Tisch bereits existiert, liefert die Funktion
			einen Fehler.
			Nach dem Retten sind alle Zahlungs- und Archivierungsvorg?ge als storniert gekennzeichnet.
			\return errNone, wenn der Tisch wieder hergestellt wurde.
			\param rest		Der Rette-Vorgang.
			\param table	0 oder die neue Tischnummer.
			\param party	0 oder die neue Parteinummer
			\brief Tisch wieder herstellen.
		*/
		int			restore(TTableRestore* rest, long table=0L, int party=0, bool rescue=FALSE);
		/*!	?dert den Preis des Artikels der Bestellung order auf price. Wenn free TRUE ist, wird
			die ?derung als freie Preiseingabe gewertet, ansonsten als Rabatt.
			\return errNone, wenn der Preis ge?dert werden konnte.
			\param order	Die zu ?dernde Order
			\param price	Der neue Preis des Artikels
			\param free		Flag, ob Rabatt (FALSE) oder freie Preiseingabe (TRUE)
			\brief Preis eine Artikels nachtr?lich ?dern.
		*/
		int			changePrice(TTableOrder* order, long price, bool free=FALSE);
		long		calcDiscount(long base, TDiscount* disc);
		int			changeDiscount(TTableOrder* order, long base, TDiscount* disc, TSubvention* sub=NULL);
		/*!	?dert den Namen des Artikels der Bestellung order auf texte. Wenn free TRUE ist, wird
			die ?derung als freie Texteingabe gewertet, ansonsten wird der Memotext ge?dert.
			\return errNone, wenn der Text ge?dert werden konnte.
			\param order	Die zu ?dernde Order
			\param texte	Der neue Name des Artikels
			\param free		Flag, ob Memotext (FALSE) oder Name (TRUE) ge?dert werden soll
			\brief Text eine Artikels nachtr?lich ?dern.
		*/
		int			changeText(TTableOrder* order, const QString& text, bool free=FALSE);
		int			park(const QString& path);
		int			regain(const QString& path);
		void		setModifiers(TTableOrder* order, TTableEntries* childs);
	signals:
		void		loaded(long amount);
		// -------- Signale ------------------------------------------------------
		/*!	Dieses Signal wird geworfen, wenn sich der Restbetrag des Tisches ?dert.
			\param table	Tisch, dessen Restbetrag sich ?dert.
			\param amount	Neuer Restbetrag des Tisches.
			\brief Tisch-Restbetrag ge?dert.
		*/
		void		amountChanged(TTable* table, double amount);
		void		discountChanged(TTable* table, long amount);
		/*!	Dieses Signal wird geworfen, wenn beim Befehl open ein Tisch neu angelegt worden ist.
			\param table	Tisch, der neu angelegt wurde.
			\brief Tisch neu erzeugt.
		*/
		void		created(TTable* table);
		/*!	Dieses Signal wird geworfen, wenn auf den Tisch table gewechselt wurde.
			\param dest		Tisch, auf den gewechselt wurde.
			\param change	Der Wechsel-Vorgang.
			\brief Tisch wurde gewechselt.
		*/
		void		tableChanged(TTable* table, TTableChange* change);
		/*!	Dieses Signal wird geworfen, wenn der Tisch table als Datei gel?cht wurde und somit
			nicht mehr vorhanden ist.
			\param table	Gel?chter Tisch.
			\brief Tisch wurde gel?cht.
		*/
		void		tableRemoved(TTable* table);
		/*!	Dieses Signal wird geworfen, wenn sich ein Eintrag des Tisches ge?dert hat.
			\param table	Tisch, der ge?dert wurde
			\param entry	Eintrag, der sich ge?dert hat
			\brief Tisch wurde ver?dert.
		*/
		void		changed(TTable* table, TTableEntry* entry);
		/*!	Dieses Signal wird geworfen, wenn eine neue Bestellung zum Tisch hinzugefgt wurde.
			\param table	Tisch, der ge?dert wurde
			\param order	Die neue Bestellung
			\brief Bestellung hinzugefgt.
		*/
		void		ordered(TTable* table, TTableOrder* order);
		/*!	Dieses Signal wird geworfen, wenn ein neuer Storno-Eintrag zum Tisch hinzugefgt wurde.
			\param table	Tisch, der ge?dert wurde
			\param _void	Der neue Storno-Vorgang.
			\brief Storno hinzugefgt.
		*/
		void		voided(TTable* table, TTableVoid* _void);
		/*!	Dieses Signal wird geworfen, wenn ein neuer Splitt-Eintrag zum Tisch hinzugefgt wurde.
			\param table	Tisch, der ge?dert wurde
			\param split	Der neue Splitt-Vorgang.
			\brief Splitting hinzugefgt.
		*/
		void		splitted(TTable* table, TTableSplit* split);
		/*!	Dieses Signal wird geworfen, wenn ein neuer Drucker-Kontrollartikel zum Tisch hinzugefgt
			wurde.
			\param table	Tisch, der ge?dert wurde
			\param contr	Der neue Kontroll-Artikel.
			\brief Drucker-Kontrollartikel hinzugefgt.
		*/
		void		control(TTable* table, TTableControl* contr);
		/*!	Dieses Signal wird geworfen, wenn ein neuer Zahlvorgang zum Tisch hinzugefgt wurde.
			\param table	Tisch, der ge?dert wurde
			\param pay		Der Neue Zahlvorgang.
			\brief Zahlvorgang hinzugefgt.
		*/
		void		paid(TTable* table, TTablePay* pay);
		void		unpaid(TTable* table, TTablePay* pay);
		/*!	Dieses Signal wird geworfen, wenn durch einen Zahlvorgang der Tisch archiviert wurde.
			\param table	Der archivierte Tisch.
			\brief Tisch wurde archiviert.
		*/
		void		archived(TTable* table);
		/*!	Dieses Signal wird geworfen, wenn ein bereits bezahlter Tisch wieder gerettet wurde.
			\param table	Der restaurierte Tisch.
			\brief Tisch wurde gerettet.
		*/
		void		restored(TTable* table);
		void		vatChanged(TTable* table, bool oh);
	protected:
		/*!	?dert die Tischnummer auf table. Diese Funktion wird nur von load bzw open aufgerufen.
			\param table	Neue Tischnummer.
			\brief Tischnummer ?dern.
		*/
		void		setTable(long table)
		{
			setValue(entryTable, table);
		}
		/*!	?dert die Parteinummer auf party. Diese Funktion wird nur von load bzw open aufgerufen.
			\param party	Neue Parteinummer.
			\brief Parteinummer ?dern.
		*/
		void		setParty(long party)
		{
			setValue(entryParty, party);
		}
		/*!	?dert den Tischwert auf amount. Der Wert ees Tisches wird bei jedem Aufruf von close()
			neu berechnet und setzt sich zusammen aus der Summe der Orders abzglich der bereits
			get?igten Zahlungen.
			\param amount	Neuer Tischwert.
			\brief Tischwert ?dern.
		*/
		void		setMaxAmount(double amount)
		{
			setValue(entryMaxAmount, amount);
		}
		/*!	Initialisert die n?igen Werte der Tisch-Aktion entry.
			Bisher werden gesetzt:
			- das Datum
			- die Uhrzeit
			- der Kellner
			\param entry	Zu initialisierende Tischaktion
			\brief Hilfsfunktion, setzen der Werte einer TTableAction.
		*/
		void		setData(TTableAction* entry)
		{
			if( !entry->hasValue(TTableEntry::entryDate) )
				entry->setDate(QDate::currentDate());
			if( !entry->hasValue(TTableEntry::entryTime) )
				entry->setTime(QTime::currentTime());
			entry->setWaiter(getWaiter());
		}
		/*!	Initialisert die n?igen Werte des Tisch-Eintrags entry.
			Bisher werden gesetzt:
			- das Datum
			- die Uhrzeit
			\param entry	Zu initialisierender Tischeintrag
			\brief Hilfsfunktion, setzen der Werte eines TTableEntry.
		*/
		void		setData(TTableEntry* entry)
		{
			if( !entry->hasValue(TTableEntry::entryDate) )
				entry->setDate(QDate::currentDate());
			if( !entry->hasValue(TTableEntry::entryTime) )
				entry->setTime(QTime::currentTime());
		}
		/*!	Setzt m_Archive auf die n?hste freie Archivnummer. Dabei wird der aktuelle Z?ler aus
			der Counter-Datei gelesen und solange erh?t, bis die Datei nicht existiert.
			\param path		Root-Pfad zur Laufumgebung
			\note Der Typ des Tisches wir dabei auf Archive gesetzt.
			\brief Archivnummer ermitteln.
		*/
		void		getNextArch();
		int			load(const QString& name, bool all, int mask, TWaiters* waiters=NULL, TWaiterTeams* teams=NULL);
		int			save(const QString& name);
	private:
		TTable()
		: TValue()
		{
		}
	#ifdef Q_OS_WIN32
		void			closeTheFuckingFile(TFile& fh);
	#endif
	protected:
		int				m_ActiveTermId;			//!< Terminal das den tisch geöffnet hat
		int				m_State;				//!< Status des Tisches wie in States definiert
		int				m_Type;					//!< Art des Tisches wie in Types definiert
		bool			m_Changed;				//!< Flag, ob der Tisch gespeichert werden mu?
		long			m_Table;				//!< Die Tischnummer, mit der der Tisch initialisert wurde (Typ Table)
		int				m_Party;				//!< Die Parteinummer, mit der der Tisch initialisert wurde (Typ Table)
		long			m_Archive;				//!< Die Archivnummer, mit der der Tisch initialisiert wurde (Typ Archive oder Statist)
		QDate			m_Date;					//!< Das Datum, mit dem der Tisch initialisiert wurde (Typ Statist)
		TWaiter*		m_Waiter;				//!< Zugreifender Kellner
		TTableCreate	m_Create;				//!< Der Create-Vorgang des Tisches
		TTableEntries	m_Entries;				//!< Bisherigen Eintr?e des Tisches
		TTableEntries	m_Current;				//!< Aktuelle Eintr?e des Tisches
		TFile			m_File;					//!< Zugriffshandle fr Datei-Locking
		QString			m_Path;
		bool			m_AsOrdered;			//!< Hilfsvar: beim Bestellen nicht zusammenfassen
		int				m_Terminal;
	protected:
		/*!	Leert alle Entries und schlie? ggf das zugeh?ige Filehandle.
			\brief Hilfsfunktion, aufr?men.
		*/
		void		cleanup();
	};
}

using namespace PosLib;

#endif

