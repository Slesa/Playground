#ifndef				POSLIB_TTERMCFG_H
#define				POSLIB_TTERMCFG_H
#include			"poslib/ttermdrv.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/bits.h"
#include			"qtwidgets/tprogram.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Terminalkonfigurationen.
		Die Terminalkonfiguration umfaßt z.Z. folgende Informationen:
		* Die Einstellungen des Hintergrundsystems unter etc/possystem.ini
		* Die Einstellungen der Oberfläche unter etc/postouch.ini
		* Die Geräteeinstellungen unter etc/devices/
		* Die Layout-Einstellungen unter etc/layout
		Alle verfügbaren Terminalkonfigurationen werden in einer Instanz von TTermCfgs
		zur Verfügung gestellt.
		\brief POS-Klassen: Terminalkonfigurationen.
	*/
	class			TTermCfg
	: public TNValue
	{
	public:
		// enums
	public: // ---- Files ------------------------------------------------------------------------------
/*		static const char	fileSystem[];						//!< etc/possystem.ini
		static const char	fileTouch[];						//!< etc/postouch.ini
		static const char	fileElpay[];						//!< etc/elpay.ini
*/
		public: // ---- possystem.ini ----------------------------------------------------------------------
		static const char	sectDongle[];						//!< possystem, Sektion [Dongle]
		static const char	entryServer[];						//!< [Dongle], server=IP des Dongle-Servers
		static const char	entryPort[];						//!< [Dongle], port=Port des Dongle-Servers
		static const char	entryMaxCount[];					//!< Maximal zu bestellende Artikel
	public:
		// paths
		static const char	pathThemes[];						//!< Default-Pfad für usr/themes
		static const char	pathTimeReg[];						//!< Default-Pfad für var/timereg
		static const char	pathAudio[];						//!< Pfad für audio verzeichnis im theme
		static const char	pathRooms[];						//!< Pfad für rooms verzeichnis im theme
		static const char	pathGroups[];						//!< Pfad für groups verzeichnis im theme
		static const char	pathWaiters[];						//!< Pfad für waiters verzeichnis im theme
		static const char	fileTimeReg[];						//!< Default Datei der Zeiterfassung
		static const char	fileGlobals[];						//!< datei der themetemplates und tapandhold einstellungen
		// ---- ex postouch.ini -----------------------------------------------------------------------
		static const int	entrySlotlogSizeDefault;			//!< Default filesize für die slot.log datei
		static const int	entryMaxGuestCountDefault;			//!< Default fur die anzahl der Gäste
		static const int	entryPrnLineSizeDefault;			//!< Default fur die anzahl der zeichen einer druckzeile
		static const int	entryMaxLastTablesDefault;			//!< Default fur die anzahl der zuletzt geöffneteten tische
		static const int	entryAutoLogOffDefault;				//!< Default in minuten für die autom. kellner abmeldung
		static const int	entryWaiterReminderDefault;			//!< Default in minuten für die akust. kellner erinnerung
		static const char	entryThemePath[];					//!< Themepath wenn typ = other ist
		static const char	entryTheme[];						//!< Theme
		static const char	entryThemePathEnv[];				//!< themepath ist eine environment variable
		static const char	entryThemeEnv[];					//!< theme ist eine environment variable
		static const char	entryThemeUseDPath[];				//!< Themepath = Datapath+/usr/themes
		static const char	entryAudio[];						//!< Soundfeatures ein/aus
		static const char	entryStartupAudio[];				//!< Soundfile beim starten
		static const char	entryShutdownAudio[];				//!< Soundfile beim beenden
		static const char	entryShowCursor[];					//!< Mauscursor an/aus
		static const char	entryWaiterHistorySave[];			//!< waiterhistory sichern an/aus
		static const char	entryWaiterHistoryLoad[];			//!< waiterhistory laden an/aus
		static const char	entryWaiterHistoryDPath[];			//!< Waiterhistoryfile wird nicht lokal sondern im datapath gesucht
		static const char	entryAskTable[];					//!< Tisch nachfragen wenn der kellner sich anmeldet an/aus
		static const char	entryVoidReason[];					//!< Stornogrund nachfragen an/aus
		static const char	entryCommaMode[];					//!< Automatisch komma an 2ter Stelle annehmen an/aus
		static const char	entryPartCounter[];					//!< Eingabe von kommawerten als Anzahl an/aus
		static const char	entryShowTooltip[];					//!< Tooltip wenn maus über einem button ist an/aus
		static const char	entryEANMinLength[];				//!< Die EAN Minimal Länge
		static const char	entryAutoLogoffDelay[];				//!< Die Logoffzeit des Kellners in Minuten
		static const char	entryWaiterReminderDelay[];			//!< Die Zeit nachdem ein beep an der kasse erzeugt wird in minuten
		static const char	entryMaxLastTables[];				//!< Maximalanzahl der Tische in den Createbutons der LastTables
		static const char	entryPrnLineSize[];					//!< Länge einer Zeile eines Listenausdrucks
		static const char	entryFreezeCustomerDisplay[];		//!< Kundendisplay aktualisierung beim bezahlen an/aus
		static const char	entryNextSplitParty[];				//!< Nächste frei partei beim splitten öffnen an/aus
		static const char	entryHCCDepartments[];				//!< Department Createbuttons Handling für HCC an/aus
		static const char	entryMaxGuestCount[];				//!< Maximale Gastanzahl
		static const char	entryDefaultGuestCount[];			//!< Default wert der Gastanzahl
		static const char	entryDefaultSeat[];					//!< Default wert des Seats
		static const char	entryUseOfficeBitmaps[];			//!< Bitmaps nicht aus screen sonder aus office verwenden an/aus
		static const char	entryNoThemeCloaking[];				//!< Theme beim kellnercloak nicht wechsel
		static const char	entryClearArticlesByFamgroup[];		//!< Artikel Createbuttons beim wechsel der Oberwarengruppe löschen an/aus
		static const char	entrySlotlog[];						//!< Slotlogging an/aus
		static const char	entrySlotlogFileSize[];				//!< Ab der dateigröße wird eine neue slotlog.log datei angelegt
		static const char	entrySlotlogPacking[];				//!< Packt slots.old in gz archiv.
		static const char	entryTimeRegFile[];					//!< Die Exportdatei der Zeiterfassung
		static const char	entryCreatebuttonSort[];
		static const char	entryLasttablesSort[];
		static const char	entryShowArticleDivide[];			//!< auf dem split/multisplit button kann divide angewählt werden

	public:
		/*!	Erzeuge eine leere Instanz einer Terminalkonfiguration.
			\brief ctor.
		*/
		TTermCfg()
		: TNValue()
		{
		}
		/*!	Lädt die Terminal-Konfiguration aus dem Verzeichnis path. In Abhängigkeit davon, ob das Terminal
			lokal oder vom Server geladen wird, wird als path entweder das Unterverzeichnis dieses Terminals
			oder der lokale Pfad des laufenden Programms (bzw QString::null) angegeben.
			In jedem Fall wird an den Pfad noch "etc" angehängt.
			\param path		Pfad zur Terminal-Konfiguration.
			\brief Terminalkonfiguration laden.
		*/
		//void			load(const QString& path=QString::null);
		/*!	Speichert die Terminal-Konfiguration in das Verzeichnis path. In Abhängigkeit davon, ob das
			Terminal lokal oder auf den Server gespeichert wird, wird als path entweder das Unterverzeichnis
			dieses Terminals oder der lokale Pfad des laufenden Programms (bzw QString::null) angegeben.
			In jedem Fall wird an den Pfad noch "etc" angehängt.
			\param path		Pfad zur Terminal-Konfiguration.
			\brief Terminalkonfiguration speichern.
		*/
		//void			save(const QString& path=QString::null);
	public: // -------- possystem -------------------------------------------------------
			/*!	possystem.ini, Sektion Dongle.
			\return Liefert die Adresse des Dongleservers für dieses Terminal.
			\brief Dongleserver ermitteln.
		*/
		QString			getDongleServer() const
		{
			return getString(entryServer);
		}
		/*!	possystem.ini, Sektion Dongle.
			Ändert die Adresse des Dongleservers für dieses Terminal auf ip.
			\param ip		Die neue Adresse des Servers.
			\brief Dongleserver ändern.
		*/
		void			setDongleServer(const QString& ip)
		{
			if( ip.isEmpty() )
				clrValue(entryServer);
			else
				setValue(entryServer, ip);
		}
		/*!	possystem.ini, Sektion Dongle.
			\return Liefert den Port des Dongleservers für dieses Terminal.
			\brief Dongleserver-Port ermitteln.
		*/
		int				getDonglePort() const
		{
			return getValue(entryPort);
		}
		/*!	possystem.ini, Sektion Dongle.
			Ändert den Port des Dongleservers für dieses Terminal auf port.
			\param port		Der neue Port des Servers.
			\brief Dongleserver-Port ändern.
		*/
		void			setDonglePort(int port)
		{
			if( !port )
				clrValue(entryPort);
			else
				setValue(entryPort, port);
		}
		/*!	possystem.ini, Sektion Settings.
			\return Liefert die größt-bestellbare Anzahl, die ohne Nachfrage bestellt werden kann.
			\brief MaxCount bei Orders ermitteln.
		*/
		int				getMaxCount() const
		{
			return getValue(entryMaxCount, 99);
		}
		/*!	possystem.ini, Sektion Settings.
			Ändert die größt-bestellbare Anzahl, die ohne Nachfrage bestellt werden kann.
			\param count	die neue Maximalanzahl
			\brief MaxCount bei Orders ändern.
		*/
		void			setMaxCount(int cnt)
		{
			setValue(entryMaxCount, cnt);
		}
		TTermDrvs&		getDrivers()
		{
			return m_Drivers;
		}

	public:
		/*!	postouch.ini
			\return Liefert den sortmodus der createbuttons
			\brief Sortmodus der createbuttons ermitteln
		*/
		QString			getCreatebuttonSort() const
		{
			return getString(entryCreatebuttonSort,"ID");
		}
		/*!	postouch.ini
			Ändert den sortmode der createbuttons
			\param mode	Der neue sortmode
			\brief Sortmode der createbuttons Ändern
		*/
		void			setCreatebuttonSort(const QString& mode)
		{
			setValue(entryCreatebuttonSort,mode);
		}
		
		/*!	postouch.ini
			\return Liefert den sortmodus der lasttables createbuttons
			\brief Sortmodus der lasttables createbuttons ermitteln
		*/
		QString			getLasttablesSort() const
		{
			return getString(entryLasttablesSort,"NADA");
		}
		/*!	postouch.ini
			Ändert den sortmode der lasttablescreatebuttons
			\param mode	Der neue sortmode
			\brief Sortmode der lasttables createbuttons Ändern
		*/
		void			setLasttablesSort(const QString& mode)
		{
			setValue(entryLasttablesSort,mode);
		}
		/*!	postouch.ini, Sektion timeregistration
			\return Liefert die exportdatei der Zeiterfassung
			\brief Exportdatei der Zeiterfassung ermitteln.
		*/
		QString			getTimeRegFile() const
		{
			return getString(entryTimeRegFile,"");
		}
		/*!	postouch.ini, Sektion timeregistrration
			Ändert die Exportdatei der Zeiterfassung
			\param file	Die neue Exportdatei
			\brief Exportdatei der Zeiterfassung Ändern
		*/
		void			setTimeRegFile(const QString& file)
		{
			setValue(entryTimeRegFile,file);
		}
		/*!
			Ändert die max. dateigröße der slotlog datei
			\param val	die neue max. dateigröße
			\brief die max. dateigröße des slotlogs ändern
		*/
		void			setSlotlogFileSize(int val)
		{
			setValue(entrySlotlogFileSize, val);
		}
		/*!
			\return Liefert die max. dateigröße der slotlog datei
			\brief Die max. dateigröße der slotlog datei
		*/
		int				getSlotlogFileSize() const
		{
			return getValue(entrySlotlogFileSize,1024);
		}
		/*!
			\return Liefert TRUE wenn auf den split togglebuttons Article Divide verfügbar ist
			\brief Article Divide durch togglebuttons verfügbar
		*/
		bool			showArticleDivide() const
		{
			return getValue(entryShowArticleDivide,FALSE);
		}
		/*!
			Anzeigen oder nicht anzeigen der articledivide funktion auf dem splittogglebutton
			\param show	true anzeigen false nicht anzeigen
		*/
		void			setShowArticleDivide(bool show)
		{
			setValue(entryShowArticleDivide,show);
		}
		/*!
			\return Liefert TRUE wenn die slots.old dateien in ein gz archiv gepackt werden sollen
			\brief slots.old dazeien in gz archive packen ermitteln
		*/
		bool			slotlogPacking() const
		{
			return getValue(entrySlotlogPacking,FALSE);
		}
		/*!
			TRUE, wenn die slots.old dateien in ein gz archiv gepackt werden sollen
			\param pack	TRUE wenn die slots.old dateien in ein gz archiv gepackt werden sollen
			\brief Slots.old dateien in gz archviv packen log an/aus
		*/
		void			setSlotlogPacking(bool pack)
		{
			setValue(entrySlotlogPacking,pack);
		}
		/*!
			\return Liefert TRUE wenn die benutzten slots geloggt werden sollen
			\brief Logging der Slots ermitteln
		*/
		bool			slotlogActiv() const
		{
			return getValue(entrySlotlog,TRUE);
		}
		/*!
			TRUE, wenn die benutzten slots geloggt werden sollen
			\param use	TRUE wenn benutzten slots geloggt werden sollen
			\brief Slotlog an/aus
		*/
		void			setSlotlogActiv(bool use)
		{
			setValue(entrySlotlog,use);
		}
		/*!
			\return Liefert TRUE wenn die Waiterhistory die daten im datenpfad suchen soll
			\brief Waiterhistory sucht daten im datenpfad
		*/
		bool			useWaiterHistoryDPath() const
		{
			return getValue(entryWaiterHistoryDPath,FALSE);
		}
		/*!
			TRUE, wenn die Waiterhistory die daten im datenpfad suchen soll
			\param use	TRUE wenn die Waiterhistory die daten im datenpfad suchen soll
			\brief Waiterhistory datenpfad verwenden
		*/
		void			setUseWaiterHistoryDPath(bool use)
		{
			setValue(entryWaiterHistoryDPath,use);
		}
		/*!
			\return Liefert TRUE wenn der themepath aus dem datenpfad ermittelt werden soll
			\brief Themepath=Datenpfad +usr/themes
		*/
		bool			useThemeDPath() const
		{
			return getValue(entryThemeUseDPath,FALSE);
		}
		/*!
			TRUE, wenn der themepath aus dem datenpfad ermittelt werden soll
			\param use	TRUE wenn der themepath aus dem datenpfad ermittelt werden soll
			\brief Themepath=Datenpfad +usr/themes
		*/
		void			setUseThemeDPath(bool use)
		{
			setValue(entryThemeUseDPath,use);
		}
		/*!
			\return Liefert TRUE wenn die Waiterhistory geladen werden soll
			\brief Waiterhistory laden
		*/
		bool			loadWaiterHistory() const
		{
			return getValue(entryWaiterHistoryLoad,TRUE);
		}
		/*!
			TRUE, wenn die Waiterhistory geladen werden soll
			\param use	TRUE wenn die Waiterhistory geladen werden soll
			\brief Waiterhistory laden an/aus
		*/
		void			setLoadWaiterHistory(bool use)
		{
			setValue(entryWaiterHistoryLoad,use);
		}
		/*!
			\return Liefert TRUE wenn die Waiterhistory gesichert werden soll
			\brief Waiterhistory sichern
		*/
		bool			saveWaiterHistory() const
		{
			return getValue(entryWaiterHistorySave,TRUE);
		}
		/*!
			TRUE, wenn die Waiterhistory gesichert werden soll
			\param use	TRUE wenn die Waiterhistory gesichert werden soll
			\brief Waiterhistory sichern an/aus
		*/
		void			setSaveWaiterHistory(bool use)
		{
			setValue(entryWaiterHistorySave,use);
		}
		/*!
			\return Liefert TRUE wenn die artikelbilder aus dem office verwendet werden sollen
			\brief artikelbilder aus dem office verwenden
		*/
		bool			useOfficeBitmaps() const
		{
			return getValue(entryUseOfficeBitmaps,TRUE);
		}
		/*!
			TRUE, wenn die artikelbilder aus dem office verwendet werden sollen
			\param use	TRUE wenn artikelbilder aus dem office verwendet werden sollen
			\brief artikelbilder aus dem office verwenden
		*/
		void			setUseOfficeBitmaps(bool use)
		{
			setValue(entryUseOfficeBitmaps,use);
		}
		/*!
			\return Liefert TRUE wenn der wechsel einer create oberwarengruppe die artikelcreatebuttons löschen soll
			\brief Articlecreatebuttons löschen bei wechesel einer Oberwarengruppe
		*/
		bool			clearArticlesByFamgroup() const
		{
			return getValue(entryClearArticlesByFamgroup,FALSE);
		}
		/*!
			TRUE, wenn der wechsel einer create oberwarengruppe die artikelcreatebuttons löschen soll
			\param clear	TRUE wenn der wechsel einer create oberwarengruppe die artikelcreatebuttons löschen soll
			\brief wechsel einer create oberwarengruppe löscht die artikelcreatebuttons
		*/
		void			setClearArticlesByFamgroup(bool clear)
		{
			setValue(entryClearArticlesByFamgroup,clear);
		}
		/*!
			\return Liefert TRUE wenn beim Kellnercloaking das Theme nicht an den cloakkellner angepasst werden soll
			\brief beim Kellnercloaking das Theme nicht an den cloakkellner anpassen
		*/
		bool			noThemeCloaking() const
		{
			return getValue(entryNoThemeCloaking,FALSE);
		}
		/*!
			TRUE, wenn wenn beim Kellnercloaking das Theme nicht an den cloakkellner angepasst werden soll
			\param cloak	TRUE wenn wenn beim Kellnercloaking das Theme nicht an den cloakkellner angepasst werden soll
			\brief beim Kellnercloaking das Theme nicht an den cloakkellner anpassen
		*/
		void			setNoThemeCloaking(bool cloak)
		{
			setValue(entryNoThemeCloaking,cloak);
		}
		/*!
			\return Liefert die maximale anzahl des guestcounts
			\brief die maximale anzahl des guestcounts
		*/
		int				getMaxGuestCount() const
		{
			return getValue(entryMaxGuestCount,entryMaxGuestCountDefault);
		}
		/*!
			Ändert die maximale anzahl des guestcounts
			\param max	die maximale anzahl des guestcounts
			\brief die maximale anzahl des guestcounts ermitteln
		*/
		void			setMaxGuestCount(int max)
		{
			setValue(entryMaxGuestCount, max);
		}
		/*!
			\return Liefert den defaultwert des guestcounts
			\brief den defaultwert des guestcounts
		*/
		int				getDefaultGuestCount() const
		{
			return getValue(entryDefaultGuestCount,1);
		}
		/*!
			Ändert den defaultwert des guestcounts
			\param val	der defaultwert des guestcounts
			\brief den defaultwert des guestcounts ermitteln
		*/
		void			setDefaultGuestCount(int val)
		{
			setValue(entryDefaultGuestCount, val);
		}
		/*!
			\return Liefert den defaultwert des seats
			\brief den defaultwert des seats
		*/
		int				getDefaultSeat() const
		{
			return getValue(entryDefaultSeat,0);
		}
		/*!
			Ändert den defaultwert des Seats
			\param val	der defaultwert des sesats
			\brief den defaultwert des seats ermitteln
		*/
		void			setDefaultSeat(int val)
		{
			setValue(entryDefaultSeat, val);
		}
		/*!
			\return Liefert TRUE wenn das Department Createbuttons Handling für HCC aktiv ist
			\brief Department Createbuttons Handling für HCC an/aus
		*/
		bool			hccDepartmentHandling() const
		{
			return getValue(entryHCCDepartments,FALSE);
		}
		/*!
			TRUE, wenn das Department Createbuttons Handling für HCC aktiv sein soll
			\param flag	TRUE wenn das Department Createbuttons Handling für HCC aktiv sein soll
			\brief Department Createbuttons Handling für HCC an/aus
		*/
		void			setHCCDepartmentHandling(bool flag)
		{
			setValue(entryHCCDepartments,flag);
		}
		/*!
			\return Liefert TRUE wenn beim splitten die nächste frei partei des tisches anstatt 999 geöffnet werden soll
			\brief beim splitten die nächste frei partei des tisches anstatt 999 öffnen
		*/
		bool			nextSplitParty() const
		{
			return getValue(entryNextSplitParty,FALSE);
		}
		/*!
			TRUE, wenn beim splitten die nächste frei partei des tisches anstatt 999 geöffnet werden soll
			\param flag	TRUE wenn beim splitten die nächste frei partei des tisches anstatt 999 geöffnet werden soll
			\brief beim splitten die nächste frei partei des tisches anstatt 999 öffnen ermittreln
		*/
		void			setNextSplitParty(bool flag)
		{
			setValue(entryNextSplitParty,flag);
		}
		/*!
			\return Liefert TRUE wenn beim bezahlen das Kundendisplay nicht mit dem betrag aktualisiert werden soll
			\brief Kundendisplay nicht aktualisieren beim bezahlen eines Tisches an/aus
		*/
		bool			freezeCustomerDisplay() const
		{
			return getValue(entryFreezeCustomerDisplay,FALSE);
		}
		/*!
			TRUE, wenn beim bezahlen das Kundendisplay nicht mit dem betrag aktualisiert werden soll
			\param show	TRUE wenn beim bezahlen das Kundendisplay nicht mit dem betrag aktualisiert werden soll
			\brief kundendisplay aktualisierug beim bezahlen an/aus
		*/
		void			setFreezeCustomerDisplay(bool show)
		{
			setValue(entryFreezeCustomerDisplay,show);
		}
		/*!
			\return Liefert die Zeilenlänge einer ausgedruckten Liste
			\brief die Zeilenlänge einer ausgedruckten Liste ermitteln
		*/
		int				getPrnLineSize() const
		{
			return getValue(entryPrnLineSize,entryPrnLineSizeDefault);
		}
		/*!
			Ändert die Zeilenlänge einer ausgedruckten Liste
			\param len	Die neue Zeilenlänge einer ausgedruckten Liste
			\brief die Zeilenlänge einer ausgedruckten Liste
		*/
		void			setPrnLineSize(int len)
		{
			setValue(entryPrnLineSize, len);
		}
		/*!
			\return Liefert die Maximale Menge der Tische für die Lasttables Createbuttons
			\brief Maximale Menge der Tische für die Lasttables Createbuttons ermitteln
		*/
		int				getMaxLastTables() const
		{
			return getValue(entryMaxLastTables,entryMaxLastTablesDefault);
		}
		/*!
			Ändert die Maximale Menge der Tische für die Lasttables Createbuttons
			\param max	Die neue Maximale Menge der Tische für die Lasttables Createbuttons
			\brief die Maximale Menge der Tische für die Lasttables Createbuttons ändern
		*/
		void			setMaxLastTables(int max)
		{
			setValue(entryMaxLastTables, max);
		}
		/*!
			\return Liefert TRUE wenn ein tooltip beim maushoover über einem button angezeigt werden soll
			\brief Buttontooltip anzeigen
		*/
		bool			showTooltip() const
		{
			return getValue(entryShowTooltip,TRUE);
		}
		/*!
			TRUE wenn ein tooltip für einen button beim maushoover angezeigt werden soll
			\param show	TRUE wenn der tooltip angezeigt werden soll
			\brief tooltip für button an/aus
		*/
		void			setTooltip(bool show)
		{
			setValue(entryShowTooltip,show);
		}
		/*!
			\return Liefert die Minimallänge einer EAN
			\brief Minimallänge einer EAN ermitteln
		*/
		int				getEANMinLength() const
		{
			return getValue(entryEANMinLength,8);
		}
		/*!
			Ändert die Minimallänge einer EAN
			\param len	Die neue minimallänge
			\brief EAN Minimallänge ändern
		*/
		void			setEANMinLength(int len)
		{
			setValue(entryEANMinLength, len);
		}
		/*!
			\return Liefert das Zeit Delay des Kellners Autologoff in minuten. 0 bedeutet die 
			funktion ist deaktivert.
			\brief das Zeit Delay des Kellners Autologoff ermitteln
		*/
		int				getAutoLogoffDelay() const
		{
			return getValue(entryAutoLogoffDelay,0);
		}
		/*!
			Ändert das Zeit Delay des Kellners Autologoff. 0 bedeuted die funktion ist deaktiviert.
			\param min	Das neue Zeit Delay des Kellners in minuten
			\brief Zeit Delay des Kellners Autologoff ändern
		*/
		void			setAutoLogoffDelay(int min)
		{
			setValue(entryAutoLogoffDelay, min);
		}
		/*!
			\return Liefert die zeit in minuten bis an der kasse ein beep ertönt. 0 bedeutet die
			funktion deaktiviert ist.
			\brief Zeit Delay der Kellner Erinnerung ermitteln
		*/
		int				getWaiterReminderDelay() const
		{
			return getValue(entryWaiterReminderDelay,0);
		}
		/*!
			Ändert das Kellner Reminder Delay 0 bedeutet die funtion ist deaktiviert.
			\param min	Das neue Kellner Reminder Delay in minuten
			\brief Kellner Reminder Delay ändern
		*/
		void			setWaiterReminderDelay(int min)
		{
			setValue(entryWaiterReminderDelay, min);
		}
		/*!
			\return Liefert TRUE wenn der Themepath eine Umgebungsvariable ist
			\brief Themepathherkunft ermitteln.
		*/
		bool			isThemePathEnv() const
		{
			return getValue(entryThemePathEnv,FALSE);
		}
		/*!
			TRUE wenn der Themepath eine Umgebungsvariable ist
			\param env	TRUE wenn der Themepath eine Umgebungsvariable ist
			\brief Themepath ist eine Umgebungsvariable
		*/
		void			setThemePathEnv(bool env)
		{
			setValue(entryThemePathEnv,env);
		}
		/*!
			\return Liefert TRUE wenn die Theme eine Umgebungsvariable ist
			\brief Themeherkunft ermitteln.
		*/
		bool			isThemeEnv() const
		{
			return getValue(entryThemeEnv,FALSE);
		}
		/*!
			TRUE wenn die Theme eine Umgebungsvariable ist
			\param env	TRUE wenn die Theme eine Umgebungsvariable ist
			\brief Theme ist eine Umgebungsvariable
		*/
		void			setThemeEnv(bool env)
		{
			setValue(entryThemeEnv,env);
		}
		/*!	postouch.ini, Sektion Login.
			\return Liefert den Themepath der Oberfläche für dieses Terminal.
			\brief Themepath ermitteln.
		*/
		QString			getThemePath() const
		{
			return getString(entryThemePath,QString("%1/%2/").arg(TProgram::pathUsr).arg(pathThemes));
		}
		/*!	postouch.ini, Sektion Login.
			Ändert den Themepath der Oberfläche für dieses Terminal auf path.
			\param path	Pfad zum neuen Theme.
			\brief Themepath ändern.
		*/
		void			setThemePath(const QString& path)
		{
			setValue(entryThemePath, path);
		}
		/*!	postouch.ini, Sektion Login.
			\return Liefert das Theme der Oberfläche für dieses Terminal.
			\brief Theme ermitteln.
		*/
		QString			getTheme() const
		{
			return getString(entryTheme,"");
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Theme der Oberfläche für dieses Terminal auf path.
			\param theme	Das neue Theme.
			\brief Theme ändern.
		*/
		void			setTheme(const QString& path)
		{
			setValue(entryTheme, path);
		}
		/*!	postouch.ini, Sektion Login.
			\return TRUE, wenn das Terminal Sound unterstützt, also bei Signalen WAVs hinterlegt werden können.
			\brief Flag Audio-Unterstützung ermitteln.
		*/
		bool			audioEnabled() const
		{
			return getValue(entryAudio, TRUE);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Flag, daß das Terminal Sound unterstützt, also bei Signalen WAVs hinterlegt werden können.
			\param flag		TRUE, wenn Sounds unterstützt werden.
			\brief Flag Audio-Unterstützung ändern.
		*/
		void			setAudioEnabled(bool flag)
		{
			setValue(entryAudio, flag);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert den Das audiofile das beim starten abgespielt wird.
			\param file	Audiofile
			\brief Audiofile beim starten der Kasse ändern.
		*/
		void			setStartupAudio(const QString& file)
		{
			setValue(entryStartupAudio, file);
		}
		/*!	postouch.ini, Sektion Login.
			\return Liefert das Audiofile das beim starten der kasse gespielt wird
			\brief Audiofile beim starten der Kasse ermitteln
		*/
		QString			getStartupAudio() const
		{
			return getString(entryStartupAudio,"");
		}
		/*!	postouch.ini, Sektion Login.
			Ändert den Das audiofile das beim beenden abgespielt wird.
			\param file	Audiofile
			\brief Audiofile beim beenden der kasse ändern.
		*/
		void			setShutdownAudio(const QString& file)
		{
			setValue(entryShutdownAudio, file);
		}
		/*!	postouch.ini, Sektion Login.
			\return Liefert das Audiofile das beim beenden der kasse gespielt wird
			\brief Audiofile beim beenden der Kasse ermitteln
		*/
		QString			getShutdownAudio() const
		{
			return getString(entryShutdownAudio,"");
		}
		/*!	postouch.ini, Sektion Login.
			\return TRUE, wenn der Mauscursor an diesem Terminal sichtbar ist.
			\brief Flag Mauscursor status ermitteln.
		*/
		bool			isCursorAvailable() const
		{
			return getValue(entryShowCursor, TRUE);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Flag, daß der Mauscursor an diesem Terminal sichtbar ist.
			\param flag		TRUE, wenn der Mauscursor an ist.
			\brief Flag Mauscursor ändern.
		*/
		void			setCursorAvailable(bool flag)
		{
			setValue(entryShowCursor, flag);
		}
		/*!	postouch.ini, Sektion Login.
			\return TRUE, wenn beim Anmelden eines Kellners nach dem Eröffnungstisch gefragt werden soll.
			\brief Flag Kellnertisch ermitteln.
		*/
		bool			askTable() const
		{
			return getValue(entryAskTable, FALSE);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Flag, daß beim Anmelden eines Kellners nach dem Eröffnungstisch gefragt werden soll.
			\param flag		TRUE, wenn nach einem Tisch gefragt werden soll.
			\brief Flag Kellnertisch ändern.
		*/
		void			setAskTable(bool flag)
		{
			setValue(entryAskTable, flag);
		}
		/*!	postouch.ini, Sektion Login.
			\return TRUE, wenn bei einem Storno nach dem Stornogrund gefragt werden soll.
			\brief Flag Stornogrund ermitteln.
		*/
		bool			askVoidReason() const
		{
			return getValue(entryVoidReason, FALSE);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Flag, daß bei einem Storno nach dem Stornogrund gefragt werden soll.
			\brief Flag Stornogrund ändern.
		*/
		void			setVoidReason(bool flag)
		{
			setValue(entryVoidReason, flag);
		}
		/*!	postouch.ini, Sektion Login.
			\return TRUE, wenn kein Komma bei Beträgen benutzt werden muß.
			\brief Flag Komma-Modus ermitteln.
		*/
		bool			commaMode() const
		{
			return getValue(entryCommaMode, FALSE);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Flag, daß kein Komma bei Beträgen benutzt werden muß.
			\param flag		TRUE, wenn der Cursor an ist.
			\brief Flag Cursor ändern.
		*/
		void			setCommaMode(bool flag)
		{
			setValue(entryCommaMode, flag);
		}
		/*!	postouch.ini, Sektion Login.
			\return TRUE, wenn als Anzahl ein kommawert eingegeben werden kann
			\brief Flag Partcounter Modus ermitteln.
		*/
		bool			partCounter() const
		{
			return getValue(entryPartCounter, FALSE);
		}
		/*!	postouch.ini, Sektion Login.
			Ändert das Flag, daß bei der Anzahl kein Kommawert eigegeben werden kann
			\param flag		TRUE, wenn der Partcounter Modus aktiv ist
			\brief Flag Partcounter modus ändern.
		*/
		void			setPartCounter(bool flag)
		{
			setValue(entryPartCounter, flag);
		}
	protected:
		TTermDrvs		m_Drivers;
	};

	/*!	\ingroup PosLib
		\brief POS-Klassen: Terminal konfigureieren
	*/
	class			TTermCfgs
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste
		static const char	elementName[];				//!< Name eines Elements der Liste
	public:
		/*!	Erzeugt eine neue Instanz einer Terminalkonfigurations-Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste gelöscht.
			\brief ctor.
		*/
		TTermCfgs(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Erzeugt ein neues Element der Liste der konfiguratioenn
			\return das neu erzeugte Element.
			\brief Terminalkonfiguration erzeugen.
		*/
		virtual TValue*	createValue()
		{
			return new TTermCfg();
		}
		virtual int		load(const char* path="")
		{
			return TValueList::load(path);
		}
		virtual int		load(const QString& file, const char* path);
		/*!	\return Liefert den Default-Dateinamen.
			\brief Default-Dateinamen ermitteln.
		*/
		virtual QString	getFileName() const
		{
			return fileName;
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		/*!	\return Liefert den Namen eines Elements der Liste innerhalb des XML-Baums.
			\brief Elementnamen ermitteln.
		*/
		virtual QString	getElementName()
		{
			return elementName;
		}
		/*!	\return Liefert die termcfg mit ID index oder NULL, falls kein Element mit
			dieser ID existiert.
			\param index		ID der zu suchenden termcfg
			\brief terminalkonfig suchen.
		*/
		TTermCfg*	operator [] (int index)
		{
			return (TTermCfg*) TValueList::operator [](index);
		}
	};

	class			TTermCfgIt
	: public TValueListIt
	{
	public:
		TTermCfgIt(TTermCfgs& list)
		: TValueListIt(list)
		{
		}
		TTermCfg*	operator () ()
		{
			return (TTermCfg*) TValueListIt::operator()();
		}
		TTermCfg*	toFirst()
		{
			return (TTermCfg*) TValueListIt::toFirst();
		}
		TTermCfg*	current()
		{
			return (TTermCfg*) TValueListIt::current();
		}
		TTermCfg*	operator ++ ()
		{
			return (TTermCfg*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif
/*
	alte ttermcfg klasse die in die inifiles gesichert hat
class			TTermCfgs
	: public TValueList
	{
		Q_OBJECT
	public:
		static const char	defPath[];					//!< Der Default-Pfad der Terminal-Konfigurationen
		static const char	pathTerm[];					//!< Pfad für die Terminals (term)
		static const char	listName[];					//!< Name der Liste (termcfgs)
		static const char	elementName[];				//!< Name eines Elements der Liste (termcfg)
	public:
		TTermCfgs(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		virtual TValue*	createValue()
		{
			return new TTermCfg();
		}
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		virtual QString	getDefPath() const
		{
			return defPath;
		}
		TTermCfg*		load(int id, const char* _path);
		bool			save(int id, const char* _path);
		virtual int		load(const char* path="");
		virtual void	save(const char* path="");
		virtual bool	remove(TValue* item);
		TTermCfg*	operator [] (int index)
		{
			return (TTermCfg*) TValueList::operator [](index);
		}
//	protected:
		QString		getTermPath(int id, const char* path="");
		QString		mkPath(int id)
		{
			return pathTerm+QString::number(id);
		}
	};
*/

