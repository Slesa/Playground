#ifndef				POSLIB_TWAITER_H
#define				POSLIB_TWAITER_H
#include			"poslib/ttablecfg.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Kellner.
		Alle verfgbaren Kellner werden in einer Instanz von TWaiters
		zur Verf�gung gestellt.
		\brief POS-Klassen: Kellner.
	*/
	class			TWaiter
	: public TNValue
	{
	public: // ---- Einstellungen -----------------------------------------------------------------
		static const char	entryDescr[];					//!< Eintrag Beschreibung
		static const char	entryPassword[];				//!< Eintrag Password
		static const char	entryKeylock[];					//!< Eintrag Kellnerstift
		static const char	entryPrefLng[];					//!< Eintrag bevorzugte Sprache
		static const char	entryRightGroup[];				//!< Eintrag Berechtigungsgruppen
		static const char	entryTeams[];					//!< Eintrag Kellnerteams
		static const char	entryModules[];					//!< Eintrag erlaubte Office-Module
		static const char	entryLevels[];					//!< Eintrag erlaubte Preisebenen
		static const char	entryPayforms[];				//!< Eintrag erlaubte Abrechnungsarten
		static const char	entryDefTable[];				//!< Eintrag Default-Tisch
		static const char	entrySelDrawer[];				//!< Eintrag Flag, ob er sich eine Schublade auswählen muß
		static const char	entryAccount[];					//!< 
		static const char	entryCheckAccount[];			//!< Soll das accountfeld gecheckt werden
	public: // ---- Flags -------------------------------------------------------------------------
		static const char	entryTrainee[];					//!< Eintrag Trainingskellner
		static const char	entryLeftHand[];				//!< Eintrag Linksh�nder
		static const char	entryMaster[];					//!< Eintrag Kellnerrecht Chefkellner
		static const char	entryCreateTable[];				//!< Eintrag Kellnerrecht Tische neu anlegen
		static const char	entryAllTables[];				//!< Eintrag Kellnerrecht Zugriff auf alle Tische
		static const char	entryOrders[];					//!< Eintrag Kellnerrecht Bestellen
		static const char	entryVoids[];					//!< Eintrag Kellnerrecht Stornieren
		static const char	entryPays[];					//!< Eintrag Kellnerrecht Bezahlen
		static const char	entrySplits[];					//!< Eintrag Kellnerrecht Splitten
		static const char	entryCloak[];					//!< Eintrag Kellnerrecht Kellner-Cloak
		static const char	entryNegative[];				//!< Eintrag Kellnerrecht Darf ins negative buchen
		static const char	entryDivide[];					//!< Eintrag Kellnerrecht Darf isch aufteilen
	public:
		/*!	Erzeuge eine leere Instanz eines Kellners.
		*/
		TWaiter()
		: TNValue()
		{
		}
	public: // ---- Private Einstellungen ---------------------------------------------------------
		/*!	\return die Beschreibung des Kellners.
			\brief Kellner-Beschreibung abfragen.
		*/
		QString		getDescription() const
		{
			return getString(entryDescr);
		}
		/*!	Ändert die Beschreibung des Kellners.
			\param d		Die neue Beschreibung. Ist der String leer, wird das Attribut gelöscht.
			\brief Kellner-Beschreibung ändern.
		*/
		void		setDescription(const QString& d)
		{
			if( d.isEmpty() )
				clrValue(entryDescr);
			else
				setValue(entryDescr, d);
		}
		/*!	\return das Pa�wort des Kellners.
			\brief Kellner-Pa�wort abfragen.
			\sa setPassword
		*/
		QString		getPassword() const
		{
			return getString(entryPassword);
		}
		/*!	�ndert das Pa�wort des Kellners.
			\param pass		Das neue Pa�wort. Ist der String leer, wird das Attribut
							gel�scht.
			\brief Kellner-Pa�wort �ndern.
			\sa getPassword
		*/
		void		setPassword(const QString& pass)
		{
			if( pass.isEmpty() )
				clrValue(entryPassword);
			else
				setValue(entryPassword, pass);
		}
		/*!	\return der Schl�ssel des Kellners.
			\brief Kellner-Schl�ssel abfragen.
			\sa setKeylock
		*/
		QString		getKeylock() const
		{
			return getString(entryKeylock);
		}
		/*!	�ndert den Schl�ssel des Kellners.
			\param key		Der neue Schl�ssel. Ist der String leer, wird das Attribut
							gel�scht.
			\brief Kellner-Schl�ssel �ndern.
			\sa getKeylock
		*/
		void		setKeylock(const QString& key)
		{
			if( key.isEmpty() )
				clrValue(entryKeylock);
			else
				setValue(entryKeylock, key);
		}
		/*!	\return Liefert die bevorzugte Sprache des Kellners f�r die Oberfl�che. Der R�ckgabewert
			ist das L�nderk�rzel der zu ladenden Sprachdateien.
			\brief Kellnersprache ermitteln.
		*/
		QString		getPrefLng() const
		{
			return getString(entryPrefLng);
		}
		/*!	�ndert die bevorzugte Sprache des Kellners f�r die Oberfl�che auf lng. Wenn lng leer
			ist, wird das Attribut gel�scht.
			\param lng		das L�nderk�rzel der zu ladenden Sprachdateien.
			\brief Kellnersprache �ndern.
		*/
		void		setPrefLng(const QString& lng)
		{
			if( lng.isEmpty() )
				clrValue(entryPrefLng);
			else
				setValue(entryPrefLng, lng);
		}
		/*!	\return Liefert die Berechtigungsgruppe zu der dieser Kellner geh�rt. �nderungen
			der Flags der Berechtigungsgruppe wirken sich direkt auf alle zugeh�rigen Kellner
			aus.
			\brief Berechtigungsgruppe ermitteln.
		*/
		int			getRightGroup() const
		{
			return getValue(entryRightGroup, 0);
		}
		/*!	�ndert die Berechtigungsgruppe zu der dieser Kellner geh�rt. �nderungen
			der Flags der Berechtigungsgruppe wirken sich direkt auf alle zugeh�rigen Kellner
			aus. Ist group 0, wird das Attribut gel�scht.
			\param group	Neue Berechtigungsgruppe.
			\brief Berechtigungsgruppe �ndern.
		*/
		void		setRightGroup(int group)
		{
			if( !group )
				clrValue(entryRightGroup);
			else
				setValue(entryRightGroup, group);
		}
		/*!	\return Liefert die Indizes der Kellnerteams. Der R�ckgabewert ist eine Stringliste
			durch ; getrennt.
			\brief Kellnerteams ermitteln.
		*/
		QString		getTeams() const
		{
			return getValue(entryTeams).toString();
		}
		/*!	�ndert die Zugerh�rigkeit des Kellners zu den Kellnerteams. teams mu� eine Stringliste
			sein, die Teams mit ; getrennt. Wenn teams leer ist, wird das Attribut gel�scht.
			\param teams		Die neue Zugeh�rigkeitstabelle des Kellners.
			\brief Kellnerteams �ndern.
		*/
		void		setTeams(const QString& teams)
		{
			if( teams.isEmpty() )
				clrValue(entryTeams);
			else
				setValue(entryTeams, teams);
		}
		/*!	\return Pr�ft nach, ob der Kellner einer der in teams angegebenen Kellnerteams zugeh�rt 
			oder nicht. Wenn er dazugeh�rt, liefert die Funktion TRUE.
			\param teams	Eine mit ; getrennte Stringliste der zu pr�fenden Teams.
			\brief Teamzugeh�rigkeit testen.
		*/
		bool		inTeams(const QString& teams);
		/*!	\return Liefert die f�r diesen Kellner erlaubten Office-Module als Stringliste
			zur�ck.
			\note Ist nur aktiv, wenn das Office-Login angeschaltet ist.
			\brief Erlaubte Office-Module ermitteln.
		*/
		QString		getModules() const
		{
			return getString(entryModules);
		}
		/*!	�ndert die f�r diesen Kellner erlaubten Office-Module auf mods. Wenn mods leer ist,
			wird das Attribut gel�scht.
			\param mods		Stringliste der erlaubten Module.
			\note Ist nur aktiv, wenn das Office-Login angeschaltet ist.
			\brief Erlaubte Office-Module �ndern.
		*/
		void		setModules(const QString& mods)
		{
			if( mods.isEmpty() )
				clrValue(entryModules);
			else
				setValue(entryModules, mods);
		}
		/*!	\return Liefert die f�r diesen Kellner erlaubten Preisebenen als Stringliste zur�ck.
			\brief Erlaubte Preisebenen ermitteln.
		*/
		QString		getLevels() const
		{
			return getString(entryLevels);
		}
		/*!	�ndert die f�r diesen Kellner erlaubten Preisebenen auf levels. Wenn levels leer ist,
			wird das Attribut gel�scht.
			\param levels	Stringliste der erlaubten Preisebenen.
			\brief Erlaubte Preisebenen �ndern.
		*/
		void		setLevels(const QString& levels)
		{
			if( levels.isEmpty() )
				clrValue(entryLevels);
			else
				setValue(entryLevels, levels);
		}
		/*!	\return Liefert die f�r diesen Kellner erlaubten Abrechnungsarten als Stringliste zur�ck.
			\brief Erlaubte Abrechnungsarten ermitteln.
		*/
		QString		getPayforms() const
		{
			return getString(entryPayforms);
		}
		/*!	�ndert die f�r diesen Kellner erlaubten Abrechnungsarten auf forms. Wenn forms leer ist,
			wird das Attribut gel�scht.
			\param levels	Stringliste der erlaubten Abrechnungsarten.
			\brief Erlaubte Abrechnungsarten �ndern.
		*/
		void		setPayforms(const QString& forms)
		{
			if( forms.isEmpty() )
				clrValue(entryPayforms);
			else
				setValue(entryPayforms, forms);
		}
		/*!	\return Liefert den f�r diesen Kellner eingestellten Default-Tisch.
			\brief Default-Tisch ermitteln.
		*/
		long		getDefTable() const
		{
			return getValue(entryDefTable, 0L);
		}
		/*!	�ndert den Default-Tisch dieses Kellners auf l. Wenn l leer ist, wird das Attribut gel�scht.
			\param t		der neue Default-Tisch
			\brief Default-Tisch �ndern.
		*/
		void		setDefTable(long t)
		{
			if( !t )
				clrValue(entryDefTable);
			else
				setValue(entryDefTable, t);
		}
		bool		doSelectDrawer() const
		{
			return getValue(entrySelDrawer, FALSE);
		}
		void		setSelectDrawer(bool flag)
		{
			if( !flag )
				clrValue(entrySelDrawer);
			else
				setValue(entrySelDrawer, flag);
		}
		bool		doCheckAccount() const
		{
			return getValue(entryCheckAccount, FALSE);
		}
		void		setCheckAccount(bool flag)
		{
			if( !flag )
				clrValue(entryCheckAccount);
			else
				setValue(entryCheckAccount, flag);
		}
	public: // ---- Private Flags -----------------------------------------------------------------
		/*!	\return Liefert TRUE, wenn der Kellner ein Trainingskellner ist, d.h. sein Ums�tze nicht
			zur Schicht hinzugez�hlt werden sollen.
			Der Default ist FALSE:
			\brief Flag Trainingskellner abfragen.
		*/
		bool		isTrainee() const
		{
			return getValue(entryTrainee, FALSE);
		}
		/*!	�ndert das Flag, ob der Kellner ein Trainingskellner ist, d.h. sein Ums�tze nicht
			zur Schicht hinzugez�hlt werden sollen.
			\param flag		TRUE, wenn es sich um einen Trainingskellner handelt, ansonsten wird
							das Flag gel�scht.
			\brief Flag Trainingskellner �ndern.
		*/
		void		setTrainee(bool flag)
		{
			if( !flag )
				clrValue(entryTrainee);
			else
				setValue(entryTrainee, flag);
		}
		/*!	\return Liefert TRUE, wenn der Kellner ein Linksh�nder ist, d.h. die Oberfl�che ihre
			Elemente gespiegelt darstellen soll.
			Der Default ist FALSE.
			\brief Flag Linksh�nder ermitteln.
		*/
		bool		isLeftHand() const
		{
			return getValue(entryLeftHand, FALSE);
		}
		/*!	�ndert das Flag, ob der Kellner ein Linksh�nder ist, d.h. die Oberfl�che ihre
			Elemente gespiegelt darstellen soll.
			\param flag		TRUE, wenn es sich um einen Linksh�nder handelt, ansonsten wird
							das Flag gel�scht.
			\brief Flag Linksh�nder �ndern.
		*/
		void		setLeftHand(bool flag)
		{
			if( !flag )
				clrValue(entryLeftHand);
			else
				setValue(entryLeftHand, flag);
		}
		/*!	\return TRUE, wenn sich bei diesem Kellner um einen Chef-Kellner handelt.
			Der Default ist FALSE.
			\brief Kellner-Recht Chefkellner ermitteln.
		*/
		bool		isMaster() const
		{
			return getValue(entryMaster, FALSE);
		}
		/*!	�ndert das Flag, ob es sich bei diesem Kellner um einen Chef-Kellner handelt.
			\param flag		TRUE, wenn es ein Chefkellner ist, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Chefkellner �ndern.
		*/
		void		setMaster(bool flag)
		{
			if( !flag )
				clrValue(entryMaster);
			else
				setValue(entryMaster, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner Tische neu anlegen darf.
			Der Default ist TRUE.
			\brief Kellner-Recht Tisch neu anlegen ermitteln.
		*/
		bool		canCreateTable() const
		{
			return getValue(entryCreateTable, TRUE);
		}
		/*!	�ndert das Flag, ob dieser Kellner einen Tisch neu anlegen darf oder nicht.
			\param flag		TRUE, wenn er er einen Tisch neu anlegen darf..
			\brief Keller-Recht Tische anlegen �ndern.
		*/
		void		setCreateTable(bool flag)
		{
			if( flag )
				clrValue(entryCreateTable);
			else
				setValue(entryCreateTable, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner Zugriff auf alle Tische hat.
			Der Default ist FALSE.
			\brief Kellner-Recht Zugriff auf alle Tische ermitteln.
		*/
		bool		canAllTables() const
		{
			return getValue(entryAllTables, FALSE);
		}
		/*!	�ndert das Flag, ob dieser Kellner Zugriff auf alle Tische hat.
			\param flag		TRUE, wenn er Zugriff hat, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Zugriff auf alle Tische �ndern.
		*/
		void		setAllTables(bool flag)
		{
			if( !flag )
				clrValue(entryAllTables);
			else
				setValue(entryAllTables, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner das Recht zum Bestellen hat.
			Der Default ist TRUE.
			\brief Kellner-Recht Bestellen ermitteln.
		*/
		bool		canOrders() const
		{
			return getValue(entryOrders, TRUE);
		}
		/*!	�ndert das Flag, ob dieser Kellner das Recht zum Bestellen hat.
			\param flag		FALSE, wenn er nicht bestellen darf, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Bestellen �ndern.
		*/
		void		setOrders(bool flag)
		{
			if( flag )
				clrValue(entryOrders);
			else
				setValue(entryOrders, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner das Recht zum Stornieren hat.
			Der Default ist TRUE.
			\brief Kellner-Recht Stornieren ermitteln.
		*/
		bool		canVoids() const
		{
			return getValue(entryVoids, TRUE);
		}
		/*!	�ndert das Flag, ob dieser Kellner das Recht zum Stornieren hat.
			\param flag		FALSE, wenn er nicht stornieren darf, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Bestellen �ndern.
		*/
		void		setVoids(bool flag)
		{
			if( flag )
				clrValue(entryVoids);
			else
				setValue(entryVoids, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner das Recht zum Bezahlen hat.
			Der Default ist TRUE.
			\brief Kellner-Recht Bezahlen ermitteln.
		*/
		bool		canPays() const
		{
			return getValue(entryPays, TRUE);
		}
		/*!	�ndert das Flag, ob dieser Kellner das Recht zum Bezahlen hat.
			\param flag		FALSE, wenn er nicht bezahlen darf, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Bestellen �ndern.
		*/
		void		setPays(bool flag)
		{
			if( flag )
				clrValue(entryPays);
			else
				setValue(entryPays, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner das Recht zum Splitten hat.
			Der Default ist TRUE.
			\brief Kellner-Recht Splitten ermitteln.
		*/
		bool		canSplits() const
		{
			return getValue(entrySplits, TRUE);
		}
		/*!	�ndert das Flag, ob dieser Kellner das Recht zum Splitten hat.
			\param flag		FALSE, wenn er nicht splitten darf, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Splitten �ndern.
		*/
		void		setSplits(bool flag)
		{
			if( flag )
				clrValue(entrySplits);
			else
				setValue(entrySplits, flag);
		}
		/*!	\return TRUE, wenn dieser Kellner das Recht zum Kellner-Cloaking hat.
			Der Default ist TRUE.
			\brief Kellner-Recht Kellner-Cloaking ermitteln.
		*/
		bool		canCloak() const
		{
			return getValue(entryCloak, TRUE);
		}
		/*!	�ndert das Flag, ob dieser Kellner das Recht zum Kellner-Cloaking hat.
			\param flag		FALSE, wenn er nicht cloaken darf, ansonsten wird das Attribut gel�scht.
			\brief Keller-Recht Kellner-Cloaking �ndern.
		*/
		void		setCloak(bool flag)
		{
			if( flag )
				clrValue(entryCloak);
			else
				setValue(entryCloak, flag);
		}
	public: // ---- Gemeinsame Einstellungen ------------------------------------------------------
		/*!	\return die Kostenstelle des Kellners. Der zur�ckgegebene Wert ist ein Index auf die
			Kostenstellenliste.
			\brief Kostenstelle abfragen.
			\sa setCostCenter, TCostCenter, TCostCenterList
		*/
		int			getCostCenter() const
		{
			return getValue(TTableCfg::entryCostCenter, 0);
		}
		/*!	�ndert die Kostenstelle des Kellners.
			\param center	Die neue Kostenstelle als Index auf die Kostenstellenliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Kostenstelle des Kellners �ndern.
			\sa getCostCenter, TCostCenter, TCostCenterList
		*/
		void		setCostCenter(int center)
		{
			if( !center )
				clrValue(TTableCfg::entryCostCenter);
			else
				setValue(TTableCfg::entryCostCenter, center);
		}
		/*!	\return die voreingestellte Preisebene des Kellners.
			Der zur�ckgegebene Wert ist ein	Index auf die Preisebenenliste.
			\brief Preisebene abfragen.
			\sa setPricelevel, TPricelevel, TPricelevelList
		*/
		int			getPricelevel() const
		{
			return getValue(TTableCfg::entryPricelevel, 0);
		}
		/*!	�ndert die voreingestellte Preisebene des Kellners.
			\param level	Die neue Preisebene als Index auf die Preisebenenliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Preisebene des Kellners �ndern.
			\sa getPricelevel, TPricelevel, TPricelevelList
		*/
		void		setPricelevel(int level)
		{
			if( !level )
				clrValue(TTableCfg::entryPricelevel);
			else
				setValue(TTableCfg::entryPricelevel, level);
		}
		/*!	\return Liefert den Index in die Preisfindungstabelle f�r diesen Kellner.
			\brief Preisfindungsmodell ermitteln.
		*/
		int			getSubvention() const
		{
			return getValue(TTableCfg::entrySubvention, 0);
		}
		/*!	�ndert das Preisfindungsmodell f�r diesen Kellner. Wenn sub 0 ist, wird das
			Attribut gel�scht.
			\param sub		Neues Preisfindungsmodell.
			\brief Preisfindungsmodell �ndern.
		*/
		void		setSubvention(int sub)
		{
			if( !sub )
				clrValue(TTableCfg::entrySubvention);
			else
				setValue(TTableCfg::entrySubvention, sub);
		}
		int			getMenuCard() const
		{
			return getValue(TTableCfg::entryMenuCard, 0);
		}
		void		setMenuCard(int card)
		{
			if( !card )
				clrValue(TTableCfg::entryMenuCard);
			else
				setValue(TTableCfg::entryMenuCard, card);
		}
		QString		getAccount() const
		{
			return getString(entryAccount);
		}
		void		setAccount(const QString& acc)
		{
			if( acc.isEmpty() )
				clrValue(entryAccount);
			else
				setValue(entryAccount, acc);
		}
	public: // ---- Gemeinsame Flags --------------------------------------------------------------
	
	
	
	
	public:
		static const char	entryPayOnly[];
		static const char	entryRestores[];
		static const char	entryRescues[];
		static const char	entryReports[];
		static const char	entrySelRepWaiter[];
		static const char	entryPrices[];
		static const char	entryTurnOver[];
		static const char	entryConnects[];
		static const char	entryBitmap[];
		static const char	entryChOwner[];
		static const char	entryChCredits[];
		static const char	entryChPayform[];
		static const char	entryDiscounts[];
		public:
		bool		isPayOnly() const
		{
			return getValue(entryPayOnly, FALSE);
		}
		void		setPayOnly(bool flag)
		{
			if( !flag )
				clrValue(entryPayOnly);
			else
				setValue(entryPayOnly, flag);
		}
		/*	0 = gar nicht
			1 = immer
			2 = wenn keine Offenst�de
			3 = wenn keine eigenen Offenst�de
		*/
		int			canReports() const
		{
			int ret = getValue(entryReports, 1);
			if( !ret )
			{
				bool yes = getValue(entryReports, FALSE);
				if( yes )
					ret = 1;
			}
			return ret;
		}
		void		setReports(int flag)
		{
			if( flag==1 )
				clrValue(entryReports);
			else
				setValue(entryReports, flag);
		}
		bool		canSelRepWaiter() const
		{
			return getValue(entrySelRepWaiter, FALSE);
		}
		void		setSelRepWaiter(bool flag)
		{
			if( !flag )
				clrValue(entrySelRepWaiter);
			else
				setValue(entrySelRepWaiter, flag);
		}
		int			canTurnOver() const
		{
			int ret = getValue(entryTurnOver, 1);
			if( !ret )
			{
				bool yes = getValue(entryTurnOver, FALSE);
				if( yes )
					ret = 1;
			}
			return ret;
		}
		void		setTurnOver(int flag)
		{
			if( flag==1 )
				clrValue(entryTurnOver);
			else
				setValue(entryTurnOver, flag);
		}
		bool		canPrices() const
		{
			return getValue(entryPrices, TRUE);
		}
		void		setPrices(bool flag)
		{
			if( flag )
				clrValue(entryPrices);
			else
				setValue(entryPrices, flag);
		}
		bool		canConnects() const
		{
			return getValue(entryConnects, TRUE);
		}
		void		setConnects(bool flag)
		{
			if( flag )
				clrValue(entryConnects);
			else
				setValue(entryConnects, flag);
		}
		QString		getBitmap() const
		{
			return getString(entryBitmap);
		}
		void		setBitmap(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryBitmap);
			else
				setValue(entryBitmap, str);
		}
		bool		canChangeOwner() const
		{
			return getValue(entryChOwner, FALSE);
		}
		void		setChangeOwner(bool flag)
		{
			if( !flag )
				clrValue(entryChOwner);
			else
				setValue(entryChOwner, flag);
		}
		/*!	\return Liefert TRUE, wenn bei einem Tischwechsel dieses Kellners
			dessen Auslagen weitergegeben werden an die n�hste Schicht.
			\brief Auslagen verrerben?
		*/
		bool		doChangeCredits() const
		{
			return getValue(entryChCredits, FALSE);
		}
		/*!	�dert das Flag, ob bei einem Tischwechsel dieses Kellners
			dessen Auslagen weitergegeben werden an die n�hste Schicht.
			\param flag		Wenn TRUE, werden die Auslagen weitergegeben.
			\brief Flag Auslagen verrerben �dern
		*/
		void		setChangeCredits(bool flag)
		{
			if( !flag )
				clrValue(entryChCredits);
			else
				setValue(entryChCredits, flag);
		}
		bool		canDiscounts() const
		{
			return getValue(entryDiscounts, TRUE);
		}
		void		setDiscounts(bool flag)
		{
			if( flag )
				clrValue(entryDiscounts);
			else
				setValue(entryDiscounts, flag);
		}
		bool		canChangePayform() const
		{
			return getValue(entryChPayform, TRUE);
		}
		void		setChangePayform(bool flag)
		{
			if( flag )
				clrValue(entryChPayform);
			else
				setValue(entryChPayform, flag);
		}
		bool		canRestores() const
		{
			return getValue(entryRestores, TRUE);
		}
		void		setRestores(bool flag)
		{
			if( flag )
				clrValue(entryRestores);
			else
				setValue(entryRestores, flag);
		}
		bool		canRescues() const
		{
			return getValue(entryRescues, TRUE);
		}
		void		setRescues(bool flag)
		{
			if( flag )
				clrValue(entryRescues);
			else
				setValue(entryRescues, flag);
		}
		bool		canNegative() const
		{
			return getValue(entryNegative, TRUE);
		}
		void		setNegative(bool flag)
		{
			if( flag )
				clrValue(entryNegative);
			else
				setValue(entryNegative, flag);
		}
		bool		canDivide() const
		{
			return getValue(entryDivide, TRUE);
		}
		void		setDivide(bool flag)
		{
			if( flag )
				clrValue(entryDivide);
			else
				setValue(entryDivide, flag);
		}
	};

	class			TWaiters
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TWaiters(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TWaiters()
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
		TWaiter*	operator [] (int index)
		{
			return (TWaiter*) TValueList::operator [](index);
		}
		TWaiter*	findKey(const QString& key);
	};

	class			TWaiterIt
	: public TValueListIt
	{
	public:
		TWaiterIt(TWaiters& list)
		: TValueListIt(list)
		{
		}
		TWaiter*	operator () ()
		{
			return (TWaiter*) TValueListIt::operator()();
		}
		TWaiter*	toFirst()
		{
			return (TWaiter*) TValueListIt::toFirst();
		}
		TWaiter*	current()
		{
			return (TWaiter*) TValueListIt::current();
		}
		TWaiter*	operator ++ ()
		{
			return (TWaiter*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


