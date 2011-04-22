#ifndef				POSLIB_TABLECFG_H
#define				POSLIB_TABLECFG_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Tischinformationen.
		Alle verf�gbaren Tischnformationen werden in einer Instanz von TTableCfgs
		zur Verfgung gestellt.
		\note Die Liste der Tischinformationen mu� nicht alle verf�gbaren Tische
		umfassen.
		\brief POS-Klassen: Tischinformationen.
	*/
	class			TTableCfg
	: public TNValue
	{
	public: // ---- Einstellungen -----------------------------------------------------------------
		static const char	entryDescr[];
		static const char	entryParty[];
		static const char	entryCostCenter[];
		static const char	entryWaiter[];
		static const char	entryCurrency[];
		static const char	entryPayform[];
		static const char	entryVat[];
		static const char	entryDiscount[];
		static const char	entryPricelevel[];
		static const char	entryLimitMin[];
		static const char	entryLimitMinGuest[];
		static const char	entryOrderMin[];
		static const char	entryLimitMax[];
		static const char	entryOrderMax[];
		static const char	entryExtern[];
		static const char	entryCreateArt[];
		static const char	entrySubvention[];
		static const char	entryMenuCard[];
		static const char	entryMinAmGuestAddr[];
		static const char	entryOrdercard[]; // bei loga Import steht hier die Kartennummer drin!
		static const char	entryMandant[]; // loga import: Mandant (im office nicht editierbar!)
		static const char	entryPayrollSubunit[]; // loga import: Abrechnungskreis (im office nicht editierbar!)
		static const char	entryContractExpire[]; // loga import: Vertragsende (im office nicht editierbar!)
	public: // ---- Flags -------------------------------------------------------------------------
		static const char	entryDoDiscount[]; // loga import: Rabattberechtigung
		static const char	entryDoDrawer[];
		static const char	entryDoDrawerConstr[];
		static const char	entryDoChanger[];
		static const char	entryDoAskGiven[];
		static const char	entryDoAskTip[];
		static const char	entryDoAskGuests[];
		static const char	entryDoAskClient[];
		static const char	entryDoAskSeat[];
		static const char	entryDoComprTable[];
		static const char	entryDoAsOrdered[];				//!< Bestellungen nicht zusammenfassen
		static const char	entryDoAskGuestAddr[];
		static const char	entryDoBill[];
	public:
		/*!	Erzeuge eine leere Instanz einer Tischinformation.
		*/
		TTableCfg()
		: TNValue()
		{
		}
		/*!	Erzeuge eine Instanz eine Tischinformation als Kopie von cfg.
			\param cfg		die zu kopierende Information.
		TTableCfg(const TTableCfg& cfg)
		: TNValue(cfg)
		{
		}
		*/
	public: // ---- Private Einstellungen ---------------------------------------------------------
		/*!	\return Das Vertragsende des Loga Import Mandanten
			\brief Vertragsende (Loga import) erfragen
		*/
		QDate			getContractExpire() const
		{
			return getDate(entryContractExpire);
		}
		/*!	Aendert das Datum des Vertragsendes
			\param date		Das neue dateum
							Ist der Wert leer, wird das atribut gelöscht
			\brief Vertragsendedatum (Loga Import) setzen
		*/
		void		setContractExpire(const QDate& date)
		{
			if(!date.isValid())
				clrValue(entryContractExpire);
			else
				setValue(entryContractExpire,date);
		}
		/*!	\return Der Abrechnungskreis des Loga Import Mandanten
			\brief Abrechnungskreis (Loga import) erfragen
		*/
		int				getPayrollSubunit() const
		{
			return getValue(entryPayrollSubunit);
		}
		/*!	Aendert den Abrechnungskreis
			\param prsu		Der neue Abrechnungskreis
							Ist der Wert 0, wird das atribut gelöscht
			\brief Abrechnungskreis (Loga Import) setzen
		*/
		void		setPayrollSubunit(int prsu)
		{
			if(!prsu)
				clrValue(entryPayrollSubunit);
			else
				setValue(entryPayrollSubunit,prsu);
		}
		/*!	\return Der Mandant
			\brief Mandanten (Loga import) erfragen
		*/
		long 		getMandant() const
		{
			return getValue(entryMandant);
		}
		/*!	Aendert den Mandanten
			\param client	Der neue Mandant
							Ist der Wert 0, wird das atribut gelöscht
			\brief Mandant (Loga Import) setzen
		*/
		void		setMandant(long mandant)
		{
			if(!mandant)
				clrValue(entryMandant);
			else
				setValue(entryMandant,mandant);
		}
		/*!	\return die Ordercardnummer fr den Orderman
			\brief Ordercard Nummer erfragen
		*/
		QString		getOrdercard() const
		{
			return getString(entryOrdercard);
		}
		/*!	�ndert die voreingestellte Ordercard Nummer fr den orderman
			\param str		Die neue Nummer
							Ist der Wert leer, wird das Attribut gel�scht.
			\brief Ordercardnummer des Tisches �ndern.
		*/
		void		setOrdercard(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryOrdercard);
			else
				setValue(entryOrdercard, str);
		}
		/*!	\return die Beschreibung des Tisches.
			\brief Beschreibung abfragen.
		*/
		QString		getDescription() const
		{
			return getString(entryDescr);
		}
		/*!	�ndert die voreingestellte Beschreibung des Tisches.
			\param str		Die neue Beschreibung.
							Ist der Wert leer, wird das Attribut gel�scht.
			\brief Beschreibung des Tisches �ndern.
		*/
		void		setDescription(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryDescr);
			else
				setValue(entryDescr, str);
		}
	public: // ---- Private Flags -----------------------------------------------------------------
	public: // ---- Gemeinsame Einstellungen ------------------------------------------------------
		/*!	\return die voreingestellte Partei des Tisches.
			\brief Partei abfragen.
			\sa setParty
		*/
		int			getParty() const
		{
			return getValue(entryParty, 0);
		}
		/*!	�ndert die voreingestellte Partei des Tisches.
			\param party	Die neue Partei.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Partei des Tisches �ndern.
		*/
		void		setParty(int party)
		{
			if( !party )
				clrValue(entryParty);
			else
				setValue(entryParty, party);
		}
		/*!	\return die Kostenstelle des Tisches. Der zur�ckgegebene Wert ist ein Index auf die
			Kostenstellenliste.
			\brief Kostenstelle abfragen.
			\sa setCostCenter, TCostCenter, TCostCenterList
		*/
		int			getCostCenter() const
		{
			return getValue(entryCostCenter, 0);
		}
		/*!	�ndert die Kostenstelle des Tisches.
			\param center	Die neue Kostenstelle als Index auf die Kostenstellenliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Kostenstelle des Tisches �ndern.
			\sa getCostCenter, TCostCenter, TCostCenterList
		*/
		void		setCostCenter(int center)
		{
			if( !center )
				clrValue(entryCostCenter);
			else
				setValue(entryCostCenter, center);
		}
		/*!	\return den erzwungenen Kellner des Tisches. Der zur�ckgegebene Wert ist ein
			Index auf die Kellnerliste.
			\brief Kellner abfragen.
			\sa setWaiter, TWaiter, TWaiterList
		*/
		int			getWaiter() const
		{
			return getValue(entryWaiter, 0);
		}
		/*!	�ndert den voreingestellten Kellner des Tisches.
			\param waiter	Der neue Kellner als Index auf die Kellnerliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Kellner des Tisches �ndern.
			\sa getWaiter, TWaiter, TWaiterList
		*/
		void		setWaiter(int waiter)
		{
			if( !waiter )
				clrValue(entryWaiter);
			else
				setValue(entryWaiter, waiter);
		}
		/*!	\return die voreingestellte W�hrung des Tisches. Der zur�ckgegebene Wert ist ein
			Index auf die W�hrungsliste.
			\brief W�hrung abfragen.
			\sa setCurrency, TCurrency, TCurrencyList
		*/
		int			getCurrency() const
		{
			return getValue(entryCurrency, 0);
		}
		/*!	�ndert die voreingestellte W�hrung des Tisches.
			\param curr		Die neue W�hrung als Index auf die W�hrungsliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief W�hrung des Tisches �ndern.
			\sa getCurrency, TCurrency, TCurrencyList
		*/
		void		setCurrency(int curr)
		{
			if( !curr )
				clrValue(entryCurrency);
			else
				setValue(entryCurrency, curr);
		}
		/*!	\return die voreingestellte Abrechnungsart des Tisches. Der zur�ckgegebene Wert ist ein
			Index auf die Abrechnungsartenliste.
			\brief Abrechnungsart abfragen.
			\sa setPayform, TPayform, TPayformList
		*/
		int			getPayform() const
		{
			return getValue(entryPayform, 0);
		}
		/*!	�ndert die voreingestellte Abrechnungsart des Tisches.
			\param form		Die neue Abrechnungsart als Index auf die Abrechnungsartenliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Abrechnungsart des Tisches �ndern.
			\sa getPayform, TPayform, TPayformList
		*/
		void		setPayform(int form)
		{
			if( !form )
				clrValue(entryPayform);
			else
				setValue(entryPayform, form);
		}
		/*!	\return der voreingestellte Mehrwertsteuersatz des Tisches.
			Der zur�ckgegebene Wert ist ein	Index auf die Mehrwertsteuerliste.
			\brief Mehrwertsteuer abfragen.
			\sa setVatRate, TVatRate, TVatRateList
		*/
		int			getVat() const
		{
			return getValue(entryVat, 0);
		}
		/*!	�ndert die voreingestellte Mehrwertsteuer des Tisches.
			\param rate		Die neue Mehrwertsteuer als Index auf die Mehrwertsteuerliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Mehrwertsteuer des Tisches �ndern.
			\sa setVatRate, TVatRate, TVatRateList
		*/
		void		setVat(int rate)
		{
			if( !rate )
				clrValue(entryVat);
			else
				setValue(entryVat, rate);
		}
		/*!	\return der voreingestellte Rabattsatz des Tisches.
			Der zur�ckgegebene Wert ist ein	Index auf die Rabattsatzliste.
			\brief Rabatt abfragen.
			\sa setDiscount, TDiscount, TDiscountList
		*/
		int			getDiscount() const
		{
			return getValue(entryDiscount, 0);
		}
		/*!	�ndert den voreingestellten Rabattsatz des Tisches.
			\param disc		Der neue Rabattsatz als Index auf die Rabattliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Rabatt des Tisches �ndern.
			\sa getDiscount, TDiscount, TDiscountList
		*/
		void		setDiscount(int disc)
		{
			if( !disc )
				clrValue(entryDiscount);
			else
				setValue(entryDiscount, disc);
		}
		/*!	\return die voreingestellte Preisebene des Tisches.
			Der zur�ckgegebene Wert ist ein	Index auf die Preisebenenliste.
			\brief Preisebene abfragen.
			\sa setPricelevel, TPricelevel, TPricelevelList
		*/
		int			getPricelevel() const
		{
			return getValue(entryPricelevel, 0);
		}
		/*!	�ndert die voreingestellte Preisebene des Tisches.
			\param level	Die neue Preisebene als Index auf die Preisebenenliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Preisebene des Tisches �ndern.
			\sa getPricelevel, TPricelevel, TPricelevelList
		*/
		void		setPricelevel(int level)
		{
			if( !level )
				clrValue(entryPricelevel);
			else
				setValue(entryPricelevel, level);
		}
		/*!	\return den voreingestellten Mindestverzehr des Tisches.
			\brief Mindestverzehr abfragen.
			\sa setLimitMin
		*/
		long		getLimitMin() const
		{
			return getValue(entryLimitMin, 0);
		}
		/*!	�ndert den voreingestellten Mindestverzehr des Tisches.
			\param amount	Der neue Mindestverzehr.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Mindestverzehr des Tisches �ndern.
			\sa getLimitMin
		*/
		void		setLimitMin(long amount)
		{
			if( !amount )
				clrValue(entryLimitMin);
			else
				setValue(entryLimitMin, amount);
		}
		bool		isLimitMinGuest() const
		{
			return getValue(entryLimitMinGuest, FALSE);
		}
		void		setLimitMinGuest(bool on)
		{
			if( !on )
				clrValue(entryLimitMinGuest);
			else
				setValue(entryLimitMinGuest, TRUE);
		}
		/*!	\return den Artikel, der bestellt werden soll, wenn der Minimum-Betrag des Tisches nicht erreicht wurde. Ist der Minimumartikel
			nicht gesetzt, kann der Tisch nicht bezahlt werden.
			\brief Minimumartikel des Tisches abfragen.
		*/
		int			getOrderMin() const
		{
			return getValue(entryOrderMin, 0);
		}
		/*!	�ndert den Artikel, der bestellt werden soll, wenn der Minimum-Betrag des Tisches nicht erreicht wurde.
			\param art		Der Minimum-Artikel.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Minimumartikel des Tisches �ndern.
		*/
		void		setOrderMin(int art)
		{
			if( !art )
				clrValue(entryOrderMin);
			else
				setValue(entryOrderMin, art);
		}
		/*!	\return den voreingestellten Maximalbetrag des Tisches.
			\brief Maximalbetrag abfragen.
			\sa setLimitMax
		*/
		long		getLimitMax() const
		{
			return getValue(entryLimitMax, 0);
		}
		/*!	�dert den voreingestellten Maximalbetrag des Tisches.
			\param amount	Der neue Maximalbetrag.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Maximalbetrag des Tisches �ndern.
			\sa getLimitMax
		*/
		void		setLimitMax(long amount)
		{
			if( !amount )
				clrValue(entryLimitMax);
			else
				setValue(entryLimitMax, amount);
		}
		/*!	\return den Artikel, der bestellt werden soll, wenn der Maximum-Betrag des Tisches erreicht wurde. Ist der Maximumartikel
			nicht gesetzt, kann der Tisch nicht bezahlt werden.
			\brief Maximumartikel des Tisches abfragen.
		*/
		int			getOrderMax() const
		{
			return getValue(entryOrderMax, 0);
		}
		/*!	�ndert den Artikel, der bestellt werden soll, wenn der Maximum-Betrag des Tisches erreicht wurde.
			\param art		Der Maximum-Artikel. Ist der Wert 0, wird das Attribut gel�scht.
			\brief Maximumartikel des Tisches �ndern.
		*/
		void		setOrderMax(int art)
		{
			if( !art )
				clrValue(entryOrderMax);
			else
				setValue(entryOrderMax, art);
		}
		/*!	\return das externe Programm, das beim Abrechnen auf diesen Tisch gestartet
			werden soll.
			\note �berschreibt die globalen Einstellungen.
			\brief Externes Programm f�r Tisch abfragen.
			\sa setExtern
		*/
		QString		getExtern() const
		{
			return getString(entryExtern);
		}
		/*!	�ndert das externe Programm, das beim Abrechnen auf diesen Tisch gestartet
			werden soll.
			\param ext		Das neue externe Programm. Ist der String leer, wird das Attribut
							gel�scht.
			\note �berschreibt die globalen Einstellungen.
			\brief Externes Programm f�r Tisch �ndern.
			\sa getExtern
		*/
		void		setExtern(const QString& ext)
		{
			if( ext.isEmpty() )
				clrValue(entryExtern);
			else
				setValue(entryExtern, ext);
		}
		/*!	\return den Artikel, der beim Erzeugen des Tisches bestellt werden soll.
			\brief Erzeugungsartikel des Tisches abfragen.
		*/
		int			getCreateArt() const
		{
			return getValue(entryCreateArt, 0);
		}
		/*!	�ndert den Artikel, der beim Erzeugen des Tisches bestellt werden soll.
			\param art		Der Erzeugungsartikel.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Erzeugungsartikel des Tisches �ndern.
		*/
		void		setCreateArt(int art)
		{
			if( !art )
				clrValue(entryCreateArt);
			else
				setValue(entryCreateArt, art);
		}
		/*!	\return Liefert den Index in die Preisfindungstabelle f�r diesen Tisch.
			\note �berschreibt die Einstellungen des Terminals und des Kellners.
			\brief Preisfindungsmodell ermitteln.
		*/
		int			getSubvention() const
		{
			return getValue(entrySubvention, 0);
		}
		/*!	�ndert das Preisfindungsmodell f�r diesen Tisch. Wenn sub 0 ist, wird das
			Attribut gel�scht.
			\param sub		Neues Preisfindungsmodell.
			\note �berschreibt die Einstellungen des Terminals und des Kellners.
			\brief Preisfindungsmodell �ndern.
		*/
		void		setSubvention(int sub)
		{
			if( !sub )
				clrValue(entrySubvention);
			else
				setValue(entrySubvention, sub);
		}
		int			getMenuCard() const
		{
			return getValue(entryMenuCard, 0);
		}
		void		setMenuCard(int card)
		{
			if( !card )
				clrValue(entryMenuCard);
			else
				setValue(entryMenuCard, card);
		}
	public: // ---- Gemeinsame Flags --------------------------------------------------------------
		/*!	\return Liefert TRUE, wenn beim Erzeugen dieses Tisches nach einem Kunden aus der Kunden-DB
			gefragt werden soll. Wir f�r den Pizza-Modus gebraucht, bei dem die Lieferanschrift auf der
			Rechnung stehen soll.
			\brief Kunden beim Erzeugen des Tisches erfragen?
		*/
		bool		doAskClient() const
		{
			return getValue(entryDoAskClient, FALSE);
		}
		/*!	�ndert das Flag, ob beim Erzeugen dieses Tisches nach einem Kunden aus der Kunden-DB
			gefragt werden soll. Wir f�r den Pizza-Modus gebraucht, bei dem die Lieferanschrift auf der
			Rechnung stehen soll.
			\brief Flag �ndern, ob beim Erzeugen des Tisches Kunden erfragen.
		*/
		void		setDoAskClient(bool flag)
		{
			if( !flag )
				clrValue(entryDoAskClient);
			else
				setValue(entryDoAskClient, flag);
		}

		/*!	\return Liefert TRUE, wenn bei jeder Order nach dem Seat gefragt werden soll.
			\brief Kunden beim Erzeugen des Tisches erfragen?
		*/
		int			doAskSeat() const
		{
			return getValue(entryDoAskSeat, 0);
		}
		/*!	Ändert das Flag zur Seatabfrage.
			\brief Flag Ändern, ob beim Erzeugen der Order eine Seatnummer abgefragt werden soll
		*/
		void		setDoAskSeat(int flag)
		{
			if( !flag )
				clrValue(entryDoAskSeat);
			else
				setValue(entryDoAskSeat, flag);
		}

		/*!	\return Liefert TRUE, wenn der Tisch auf jeden Fall zusammengefa�t
			werden soll, zB weil es ein Sammeltisch f�r eine Schankanlage ist.
			\note Kann pro Tisch oder pro Terminal festgelegt werden.
			\brief Tischinhalt zusammenfassen?
		*/
		bool		doCompressTable() const
		{
			return getValue(entryDoComprTable, FALSE);
		}
		/*!	�ndert das Flag, ob der Tischinhalt zusammengefa�t werden soll.
			\param flag		Wenn TRUE, wird der Tischinhalt komprimiert.
			\note Kann pro Tisch oder pro Terminal festgelegt werden.
			\brief Flag �ndern, ob Tisch zusammenfassen.
		*/
		void		setDoCompressTable(bool flag)
		{
			if( !flag )
				clrValue(entryDoComprTable);
			else
				setValue(entryDoComprTable, flag);
		}
		bool		doAsOrdered() const
		{
			return getValue(entryDoAsOrdered, FALSE);
		}
		void		setDoAsOrdered(bool flag)
		{
			if( !flag )
				clrValue(entryDoAsOrdered);
			else
				setValue(entryDoAsOrdered, flag);
		}
		/*!	\return TRUE, wenn der Tisch rabattieren darf
			Der Default-Wert ist TRUE.
			\brief Rabatt erlaubt?
			\sa setDoDiscount
		*/
		bool		doDiscount() const
		{
			return getValue(entryDoDiscount, TRUE);
		}
		/*!	Aendert das Flag, ob der Tisch Rabattieren darf.
			\param flag		TRUE, wenn Rabatt erlaubt sein soll
			\brief Flag Aendern, ob rabattiert werden darf
			\sa doDiscount
		*/
		void		setDoDiscount(bool flag)
		{
			if( flag )
				clrValue(entryDoDiscount);
			else
				setValue(entryDoDiscount,flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesen Tisch die Kassenschublade
			ge�ffnet werden soll. Der Default-Wert ist TRUE.
			\note �berschreibt die globalen Schubladen-Einstellungen.
			\brief Beim Abrechnen auf diesen Tisch Schublade �ffnen?
			\sa setDoDrawer
		*/
		bool		doDrawer() const
		{
			return getValue(entryDoDrawer, TRUE);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch die Kassenschublade
			ge�ffnet werden soll.
			\param flag		TRUE, wenn die Schublade ge�ffnet werden soll
			\note �berschreibt die globalen Schubladen-Einstellungen.
			\brief Flag �ndern, ob Schublade �ffnen.
			\sa doDrawer
		*/
		void		setDoDrawer(bool flag)
		{
			if( flag )
				clrValue(entryDoDrawer);
			else
				setValue(entryDoDrawer, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesen Tisch der Schubladenzwang
			aktiv sein soll. Der Default-Wert ist FALSE.
			\note �berschreibt die globalen Schubladen-Einstellungen.
			\brief Beim Abrechnen auf diesen Tisch warten bis Schublade wieder zu?
			\sa setDoDrawerConstr
		*/
		bool		doDrawerConstr() const
		{
			return getValue(entryDoDrawerConstr, FALSE);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch der Schubladenzwang
			aktiv sein soll.
			\param flag		TRUE, wenn auf die Schublade gewartet werden soll
			\note �berschreibt die globalen Schubladen-Einstellungen.
			\brief Flag �ndern, ob warten bis Schublade wieder zu.
			\sa doDrawerConstr
		*/
		void		setDoDrawerConstr(bool flag)
		{
			if( !flag )
				clrValue(entryDoDrawerConstr);
			else
				setValue(entryDoDrawerConstr, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesen Tisch das Geldr�ckgabeger�t
			aktiviert werden soll. Der Default-Wert ist TRUE.
			\note �berschreibt die globalen Geld�rckgabe-Einstellungen.
			\brief Beim Abrechnen auf diesen Tisch Geldr�ckgabe aktivieren?
			\sa setDoChanger
		*/
		bool		doChanger() const
		{
			return getValue(entryDoChanger, TRUE);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch die Geldr�ckgabe
			aktiviert werden soll.
			\param flag		TRUE, wenn Geldr�ckgabe aktiviert werden soll
			\note �berschreibt die globalen Geldr�ckgabe-Einstellungen.
			\brief Flag �ndern, ob Geldr�ckgabe aktiv.
			\sa doChanger
		*/
		void		setDoChanger(bool flag)
		{
			if( flag )
				clrValue(entryDoChanger);
			else
				setValue(entryDoChanger, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesem Tisch nach dem gegebenen
			Betrag gefragt werden soll. Der Default-Wert ist FALSE.
			\note �berschreibt die globalen Einstellungen.
			\brief Beim Abrechnen auf diesen Tisch nach gegebenem Betrag fragen?
			\sa setDoAskGiven
		*/
		bool		doAskGiven() const
		{
			return getValue(entryDoAskGiven, FALSE);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch nach dem gegebenen
			Betrag gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �berschreibt die globalen Einstellungen.
			\brief Flag �ndern, ob nach gegebenen Betrag fragen.
			\sa doAskGiven
		*/
		void		setDoAskGiven(bool flag)
		{
			if( !flag )
				clrValue(entryDoAskGiven);
			else
				setValue(entryDoAskGiven, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesen Tisch nach dem Trinkgeld
			gefragt werden soll. Der Default-Wert ist FALSE.
			\note �berschreibt die globalen Einstellungen.
			\brief Beim Abrechnen auf diesen Tisch nach Trinkgeld fragen?
			\sa setDoAskTip
		*/
		bool		doAskTip() const
		{
			return getValue(entryDoAskTip, FALSE);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch nach dem Trinkgeld
			gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �berschreibt die globalen Einstellungen.
			\brief Flag �ndern, ob nach Trinkgeld fragen.
			\sa doAskTip
		*/
		void		setDoAskTip(bool flag)
		{
			if( !flag )
				clrValue(entryDoAskTip);
			else
				setValue(entryDoAskTip, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesen Tisch nach der Anzahl
			der G�ste gefragt werden soll. Der Default-Wert ist FALSE.
			\note �berschreibt die globalen Einstellungen.
			\brief Beim Abrechnen auf diesen Tisch nach Anzahl G�ste fragen?
			\sa setDoAskGuests
		*/
		int			doAskGuests() const
		{
			return getValue(entryDoAskGuests, 0);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch nach der Anzahl
			der G�ste gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �berschreibt die globalen Einstellungen.
			\brief Flag �ndern, ob nach Gastanzahl fragen.
			\sa doAskGuests
		*/
		void		setDoAskGuests(int flag)
		{
			if( !flag )
				clrValue(entryDoAskGuests);
			else
				setValue(entryDoAskGuests, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diesen Tisch nach der Gastanschrift
			gefragt werden soll. Der Default-Wert ist FALSE.
			\note �berschreibt die globalen Einstellungen.
			\note Kann durch setMinGuestAddr eingeschr�nkt werden.
			\brief Beim Abrechnen auf diesen Tisch nach Gastanschrift fragen?
			\sa setDoAskGuestAddr, getMinGuestAddr, setMinGuestAddr
		*/
		bool		doAskGuestAddr() const
		{
			return getValue(entryDoAskGuestAddr, FALSE);
		}
		/*!	�ndert das Flag, ob beim Abrechnen auf diesen Tisch nach der Gastanschrift
			gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �berschreibt die globalen Einstellungen.
			\note Kann durch setMinGuestAddr eingeschr�nkt werden.
			\brief Flag �ndern, ob nach Gastanschrift fragen.
			\sa doAskGuestAddr, getMinGuestAddr, setMinGuestAddr
		*/
		void		setDoAskGuestAddr(bool flag)
		{
			if( !flag )
				clrValue(entryDoAskGuestAddr);
			else
				setValue(entryDoAskGuestAddr, flag);
		}
		/*!	\return den Betrag, ab dem das Flag "Gastanschrift erfragen" aktiv werden soll. Ist der Wert 0, wird die Anschrift
			immer erfragt, sobald das Flag gesetzt ist.
			\brief Minimumbetrag f�r Gastanschrift ermitteln.
		*/
		long		getMinGuestAddr() const
		{
			return getValue(entryMinAmGuestAddr, 0L);
		}
		/*!	�ndert den Betrag, ab dem das Flag "Gastanschrift erfragen" aktiv werden soll. Ist der Wert 0, wird die Anschrift
			immer erfragt, sobald das Flag gesetzt ist.
			\param am		Der Mindestbetrag f�r das Flag.
			\brief Minimumbetrag f�r Gastanschrift �ndern.
		*/
		void		setMinGuestAddr(long am)
		{
			if( !am )
				clrValue(entryMinAmGuestAddr);
			else
				setValue(entryMinAmGuestAddr, am);
		}
		/*!	\return Liefert TRUE, wenn die Gastanschrift beim Abrechnen auf diesen Tisch erfragt werden soll, in Abh�ngigkeit
			des Tischbetrages am. Fa�t die Einstellungen "Gastanschrift erfragen" und "Minimumbetrag f�r Gastanschrift" zusammen.
			\param am		Der zu zahlende Betrag.
			\brief Gastanschrift erfragen aktiv?
		*/
		bool		doAskGuestAddr(long am) const
		{
			long lim = getMinGuestAddr();
			if( lim && am<lim )
				return FALSE;
			return doAskGuestAddr();
		}





		bool		doBill() const
		{
			return getValue(entryDoBill, TRUE);
		}
		void		setDoBill(bool flag)
		{
			if( flag )
				clrValue(entryDoBill);
			else
				setValue(entryDoBill, flag);
		}
	};

	class			TTableCfgs
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TTableCfgs(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TTableCfgs()
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
		TTableCfg*	operator [] (int index)
		{
			return (TTableCfg*) TValueList::operator [](index);
		}
	};

	class			TTableCfgIt
	: public TValueListIt
	{
	public:
		TTableCfgIt(TTableCfgs& list)
		: TValueListIt(list)
		{
		}
		TTableCfg*	operator () ()
		{
			return (TTableCfg*) TValueListIt::operator()();
		}
		TTableCfg*	toFirst()
		{
			return (TTableCfg*) TValueListIt::toFirst();
		}
		TTableCfg*	current()
		{
			return (TTableCfg*) TValueListIt::current();
		}
		TTableCfg*	operator ++ ()
		{
			return (TTableCfg*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


