#ifndef				POSLIB_TTERMINAL_H
#define				POSLIB_TTERMINAL_H
#include			"poslib/ttablecfg.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa� alle ben�igten Informationen fr Terminals.
		Alle verfgbaren Terminal-Informationen werden in einer Instanz von TTerminals
		zur Verfgung gestellt.
		\note Die Liste der Terminals mu�nicht alle verfgbaren Terminals
		umfassen.
		\brief POS-Klassen: Terminals.
	*/
	class			TTerminal
	: public TNValue
	{
	public: // ---- Einstellungen -----------------------------------------------------------------
		static const char	entryTable[];
		static const char	entryLimitMin[];
		static const char	entryLimitMinGuest[];
		static const char	entryLimitMax[];
		static const char	entryResOutlet[];
		static const char	entryResRooms[];
		static const char	entryShiftGroup[];
		static const char	entryWaiters[];
		static const char	entryOMMenuCard[];				// index der TOMMenucard
		static const char	entryOMProfile[];				// index des TOMProfiles
		static const char	entryOMBeltPath[];				// spolpath des ordermangrteldruckers
		static const char	entryOMSplitParty[];			// die partei auf die beim schnellsplit hingesplittet wird
		static const char	entryOMTransponder[];			// der Orderman Transpondertyp 0=none 1 = ordercard 2=hitag
		static const char	entryPOSConfig[];				// link zu der kassenkonfiguration
	public: // ---- Flags -------------------------------------------------------------------------
		static const char	entryDoBill[];
		static const char	entryWireless[];					// orderman terminal
		static const char	entrySerialNo[];					// die serial nummer des devices
		static const char	entryAutoTurn[];
		static const char	entryPrnGuest[];					// Guestcheckdrucker (USE-Version)
		static const char	entryNetDrawers[];					// Liste der Schubladenzuordnung pro Kellner vom Server laden statt lokal
	public:
		/*!	Erzeuge eine leere Instanz eines Terminals.
		*/
		TTerminal()
		: TNValue()
		{
		}
	public: // ---- Private Einstellungen ---------------------------------------------------------
		/*!	\return den voreingestellten Tisch des Terminals.
			Der zurckgegebene Wert kann als Index auf die Tischinfoliste benutzt werden.
			\brief Tisch abfragen.
			\sa setTable, TTableCfg, TTableCfgList
		*/
		long		getTable() const
		{
			return getValue(entryTable, 0);
		}
		/*!	�dert den voreingestellten Tisch des Terminals.
			\param table	Der neue Tisch.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Tisch des Terminals �dern.
			\sa getTable, TTableCfg, TTableCfgList
		*/
		void		setTable(long table)
		{
			if( !table )
				clrValue(entryTable);
			else
				setValue(entryTable, table);
		}
		/*!	\return Liefert die Uhrzeit des automatischen Tagesbertrags an diesem Terminal im Format h*100+m, bzw einen Wert
			kleiner 0 wenn kein Tagesbertrag stattfinden soll.
			\brief �ertragszeit ermitteln.
		*/
		int			getAutoTurn() const
		{
			return getValue(entryAutoTurn, -1);
		}
		/*!	�dert die Uhrzeit des automatischen Tagesbertrags an diesem Terminal.
			\param time		Uhrzeit im Format h*100+m. Wenn der Wert kleiner 0 ist, findet kein �ertrag statt.
			\brief �ertragszeit �dern.
		*/
		void		setAutoTurn(int time)
		{
			if( time==-1 )
				clrValue(entryAutoTurn);
			else
				setValue(entryAutoTurn, time);
		}
		/*!	\return Liefert die ID des zugehörigen Guestcheck-Druckers für dieses Terminal (Für USA-Version).
			\brief Guestcheckdrucker ermitteln.
		*/
		int			getPrnGuest() const
		{
			return getValue(entryPrnGuest, 0);
		}
		/*! Ändert die ID des zugehörigen Guestcheck-Druckers für dieses Terminal auf prn (Für USA-Version).
			\param prn		Neuer Guestcheckdrucker.
			\brief Guestcheckdrucker ändern.
		*/
		void		setPrnGuest(int prn)
		{
			if( !prn )
				clrValue(entryPrnGuest);
			else
				setValue(entryPrnGuest, prn);
		}
		QString		getWaiters() const
		{
			return getString(entryWaiters);
		}
		void		setWaiters(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryWaiters);
			else
				setValue(entryWaiters, str);
		}
	public: // ---- Private Flags -----------------------------------------------------------------
		int			getResOutlet() const
		{
			return getValue(entryResOutlet, 0);
		}
		void		setResOutlet(int outlet)
		{
			if( !outlet )
				clrValue(entryResOutlet);
			else
				setValue(entryResOutlet, outlet);
		}
		QStringList	getResRooms() const
		{
			return QStringList::split(":", getString(entryResRooms));
		}
		void		setResRooms(const QStringList& rooms)
		{
			if( !rooms.count() )
				clrValue(entryResRooms);
			else
				setValue(entryResRooms, rooms.join(":"));
		}
		int			getShiftGroup() const
		{
			return getValue(entryShiftGroup, 0);
		}
		void		setShiftGroup(int group)
		{
			if( !group )
				clrValue(entryShiftGroup);
			else
				setValue(entryShiftGroup, group);
		}
	public: // ---- Gemeinsame Einstellungen ------------------------------------------------------
		/*!	\return die voreingestellte Partei des Terminals.
			\brief Partei abfragen.
			\sa setParty
		*/
		int			getParty() const
		{
			return getValue(TTableCfg::entryParty, 0);
		}
		/*!	�dert die voreingestellte Partei des Terminals.
			\param party	Die neue Partei.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Partei des Terminals �dern.
			\sa getParty
		*/
		void		setParty(int party)
		{
			if( !party )
				clrValue(TTableCfg::entryParty);
			else
				setValue(TTableCfg::entryParty, party);
		}
		/*!	\return die Kostenstelle des Terminals. Der zurckgegebene Wert ist ein Index auf die
			Kostenstellenliste.
			\brief Kostenstelle abfragen.
			\sa setCostCenter, TCostCenter, TCostCenterList
		*/
		int			getCostCenter() const
		{
			return getValue(TTableCfg::entryCostCenter, 0);
		}
		/*!	�dert die Kostenstelle des Terminals.
			\param center	Die neue Kostenstelle als Index auf die Kostenstellenliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Kostenstelle des Terminals �dern.
			\sa getCostCenter, TCostCenter, TCostCenterList
		*/
		void		setCostCenter(int center)
		{
			if( !center )
				clrValue(TTableCfg::entryCostCenter);
			else
				setValue(TTableCfg::entryCostCenter, center);
		}
		/*!	\return den voreingestellten Kellner des Terminals. Der zurckgegebene Wert ist ein
			Index auf die Kellnerliste.
			\brief Kellner abfragen.
			\sa setWaiter, TWaiter, TWaiterList
		*/
		int			getWaiter() const
		{
			return getValue(TTableCfg::entryWaiter, 0);
		}
		/*!	�dert den voreingestellten Kellner des Terminals.
			\param waiter	Der neue Kellner als Index auf die Kellnerliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Kellner des Terminals �dern.
			\sa getWaiter, TWaiter, TWaiterList
		*/
		void		setWaiter(int waiter)
		{
			if( !waiter )
				clrValue(TTableCfg::entryWaiter);
			else
				setValue(TTableCfg::entryWaiter, waiter);
		}
		/*!	\return die voreingestellte W�rung des Terminals. Der zurckgegebene Wert ist ein
			Index auf die W�rungsliste.
			\brief W�rung abfragen.
			\sa setCurrency, TCurrency, TCurrencyList
		*/
		int			getCurrency() const
		{
			return getValue(TTableCfg::entryCurrency, 0);
		}
		/*!	�dert die voreingestellte W�rung des Terminals.
			\param curr		Die neue W�rung als Index auf die W�rungsliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief W�rung des Terminals �dern.
			\sa getCurrency, TCurrency, TCurrencyList
		*/
		void		setCurrency(int curr)
		{
			if( !curr )
				clrValue(TTableCfg::entryCurrency);
			else
				setValue(TTableCfg::entryCurrency, curr);
		}
		/*!	\return die voreingestellte Abrechnungsart des Terminals. Der zurckgegebene Wert ist ein
			Index auf die Abrechnungsartenliste.
			\brief Abrechnungsart abfragen.
			\sa setPayform, TPayform, TPayformList
		*/
		int			getPayform() const
		{
			return getValue(TTableCfg::entryPayform, 0);
		}
		/*!	�dert die voreingestellte Abrechnungsart des Terminals.
			\param form		Die neue Abrechnungsart als Index auf die Abrechnungsartenliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Abrechnungsart des Terminals �dern.
			\sa getPayform, TPayform, TPayformList
		*/
		void		setPayform(int form)
		{
			if( !form )
				clrValue(TTableCfg::entryPayform);
			else
				setValue(TTableCfg::entryPayform, form);
		}
		/*!	\return der voreingestellte Mehrwertsteuersatz des Terminals.
			Der zurckgegebene Wert ist ein	Index auf die Mehrwertsteuerliste.
			\brief Mehrwertsteuer abfragen.
			\sa setVatRate, TVatRate, TVatRateList
		*/
		int			getVat() const
		{
			return getValue(TTableCfg::entryVat, 0);
		}
		/*!	�dert die voreingestellte Mehrwertsteuer des Terminals.
			\param rate		Die neue Mehrwertsteuer als Index auf die Mehrwertsteuerliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Mehrwertsteuer des Terminals �dern.
			\sa setVatRate, TVatRate, TVatRateList
		*/
		void		setVat(int rate)
		{
			if( !rate )
				clrValue(TTableCfg::entryVat);
			else
				setValue(TTableCfg::entryVat, rate);
		}
		/*!	\return der voreingestellte Rabattsatz des Terminals.
			Der zurckgegebene Wert ist ein	Index auf die Rabattsatzliste.
			\brief Rabatt abfragen.
			\sa setDiscount, TDiscount, TDiscountList
		*/
		int			getDiscount() const
		{
			return getValue(TTableCfg::entryDiscount, 0);
		}
		/*!	�dert den voreingestellten Rabattsatz des Terminals.
			\param disc		Der neue Rabattsatz als Index auf die Rabattliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Rabatt des Terminals �dern.
			\sa getDiscount, TDiscount, TDiscountList
		*/
		void		setDiscount(int disc)
		{
			if( !disc )
				clrValue(TTableCfg::entryDiscount);
			else
				setValue(TTableCfg::entryDiscount, disc);
		}
		/*!	\return die voreingestellte Preisebene des Terminals.
			Der zurckgegebene Wert ist ein	Index auf die Preisebenenliste.
			\brief Preisebene abfragen.
			\sa setPricelevel, TPricelevel, TPricelevelList
		*/
		int			getPricelevel() const
		{
			return getValue(TTableCfg::entryPricelevel, 0);
		}
		/*!	�dert die voreingestellte Preisebene des Terminals.
			\param level	Die neue Preisebene als Index auf die Preisebenenliste.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Preisebene des Terminals �dern.
			\sa getPricelevel, TPricelevel, TPricelevelList
		*/
		void		setPricelevel(int level)
		{
			if( !level )
				clrValue(TTableCfg::entryPricelevel);
			else
				setValue(TTableCfg::entryPricelevel, level);
		}
		/*!	\return den Mindestbetrag den ein Tisch haben mu�damit auf diese Abrechnungsart
			abgerechnet werden kann.
			\note Das Attribut mu�auch bei Wert 0 explizit gesetzt werden.
			\brief Mindestbetrag fr Terminal abfragen
			\sa setLimitMin, getLimitMax, setLimitMax
		*/
		long		getLimitMin() const
		{
			return getValue(entryLimitMin, 0);
		}
		/*!	�dert den Mindestbetrag den ein Tisch haben mu�damit auf diese Abrechnungsart
			abgerechnet werden kann.
			\param limit	das neue Limit
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Mindestbetrag fr Terminal setzen
			\sa getLimitMin, getLimitMax, setLimitMax
		*/
		void		setLimitMin(long limit)
		{
			if( !limit )
				clrValue(entryLimitMin);
			else
				setValue(entryLimitMin, limit);
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
			return getValue(TTableCfg::entryOrderMin, 0);
		}
		/*!	�dert den Artikel, der bestellt werden soll, wenn der Minimum-Betrag des Tisches nicht erreicht wurde.
			\param art		Der Minimum-Artikel.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Minimumartikel des Tisches �dern.
		*/
		void		setOrderMin(int art)
		{
			if( !art )
				clrValue(TTableCfg::entryOrderMin);
			else
				setValue(TTableCfg::entryOrderMin, art);
		}
		/*!	\return den H�hstbetrag den ein Tisch haben darf, damit auf diese Abrechnungsart
			abgerechnet werden kann.
			\note Das Attribut mu�auch bei Wert 0 explizit gesetzt werden.
			\brief H�hstbetrag fr Terminal abfragen
			\sa getLimitMin, setLimitMin, setLimitMax
		*/
		long		getLimitMax() const
		{
			return getValue(entryLimitMax, 0);
		}
		/*!	�dert den Maximal-Betrag den ein Tisch haben darf, damit auf diese Abrechnungsart
			abgerechnet werden kann.
			\param limit	das neue Limit
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief H�hstbetrag fr Terminal setzen
			\sa getLimitMin, setLimitMin, getLimitMax
		*/
		void		setLimitMax(long limit)
		{
			if( !limit )
				clrValue(entryLimitMax);
			else
				setValue(entryLimitMax, limit);
		}
		/*!	\return den Artikel, der bestellt werden soll, wenn der Maximum-Betrag des Tisches erreicht wurde. Ist der Maximumartikel
			nicht gesetzt, kann der Tisch nicht bezahlt werden.
			\brief Maximumartikel des Tisches abfragen.
		*/
		int			getOrderMax() const
		{
			return getValue(TTableCfg::entryOrderMax, 0);
		}
		/*!	�dert den Artikel, der bestellt werden soll, wenn der Maximum-Betrag des Tisches erreicht wurde.
			\param art		Der Maximum-Artikel. Ist der Wert 0, wird das Attribut gel�cht.
			\brief Maximumartikel des Tisches �dern.
		*/
		void		setOrderMax(int art)
		{
			if( !art )
				clrValue(TTableCfg::entryOrderMax);
			else
				setValue(TTableCfg::entryOrderMax, art);
		}
		/*!	\return das externe Programm, das beim Abrechnen an diesem Terminal gestartet
			werden soll.
			\note �erschreibt die globalen Einstellungen.
			\brief Externes Programm fr Terminal abfragen.
			\sa setExtern
		*/
		QString		getExtern() const
		{
			return getString(TTableCfg::entryExtern);
		}
		/*!	�dert das externe Programm, das beim Abrechnen an diesem Terminal gestartet
			werden soll.
			\param ext		Das neue externe Programm. Ist der String leer, wird das Attribut
							gel�cht.
			\note �erschreibt die globalen Einstellungen.
			\brief Externes Programm fr Terminal �dern.
			\sa getExtern
		*/
		void		setExtern(const QString& ext)
		{
			if( ext.isEmpty() )
				clrValue(TTableCfg::entryExtern);
			else
				setValue(TTableCfg::entryExtern, ext);
		}
		/*!	\return den Artikel, der beim Erzeugen von Tischen an diesem Terminal bestellt werden soll.
			\brief Erzeugungsartikel von Tischen abfragen.
		*/
		int			getCreateArt() const
		{
			return getValue(TTableCfg::entryCreateArt, 0);
		}
		/*!	�dert den Artikel, der beim Erzeugen von Tischen an diesem Terminal  bestellt werden soll.
			\param art		Der Erzeugungsartikel.
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Erzeugungsartikel von Tischen �dern.
		*/
		void		setCreateArt(int art)
		{
			if( !art )
				clrValue(TTableCfg::entryCreateArt);
			else
				setValue(TTableCfg::entryCreateArt, art);
		}
		/*!	\return Liefert den Index in die Preisfindungstabelle fr dieses Terminal.
			\brief Preisfindungsmodell ermitteln.
		*/
		int			getSubvention() const
		{
			return getValue(TTableCfg::entrySubvention, 0);
		}
		/*!	�dert das Preisfindungsmodell fr dieses Terminal. Wenn sub 0 ist, wird das
			Attribut gel�cht.
			\param sub		Neues Preisfindungsmodell.
			\brief Preisfindungsmodell �dern.
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
	public: // ---- Gemeinsame Flags --------------------------------------------------------------
		/*!	\return Liefert TRUE, wenn beim Erzeugen von Tischen an diesem Terminal  nach einem Kunden aus der Kunden-DB
			gefragt werden soll. Wir fr den Pizza-Modus gebraucht, bei dem die Lieferanschrift auf der
			Rechnung stehen soll.
			\brief Kunden beim Erzeugen eines Tisches erfragen?
		*/
		bool		doAskClient() const
		{
			return getValue(TTableCfg::entryDoAskClient, FALSE);
		}
		/*!	�dert das Flag, ob beim Erzeugen von Tischen an diesem Terminal nach einem Kunden aus der Kunden-DB
			gefragt werden soll. Wir fr den Pizza-Modus gebraucht, bei dem die Lieferanschrift auf der
			Rechnung stehen soll.
			\brief Flag �dern, ob beim Erzeugen eines Tisches Kunden erfragen.
		*/
		void		setDoAskClient(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskClient);
			else
				setValue(TTableCfg::entryDoAskClient, flag);
		}
			/*!	\return Liefert TRUE, wenn die SeatNummer bei jeder Order abgefragt werden soll
			\brief Seatnummer bei Order erfragen
		*/
		int			doAskSeat() const
		{
			return getValue(TTableCfg::entryDoAskSeat, 0);
		}
		/*!	ändert das Flag, ob beim Erzeugen von Tischen an diesem Terminal nach der SeatNummer gefragt werden soll.
		\brief Flag ändern, ob beim Erzeugen eines Tisches Kunden erfragen.
		*/
		void		setDoAskSeat(int flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskSeat);
			else
				setValue(TTableCfg::entryDoAskSeat, flag);
		}

		/*!	\return Liefert TRUE, wenn Tischinhalte dieses Terminals auf jeden Fall zusammengefa�
			werden sollen, zB weil es sich um eine Schankanlage handelt.
			\note Kann pro Tisch oder pro Terminal festgelegt werden.
			\brief Tischinhalte zusammenfassen?
		*/
		bool		doCompressTable() const
		{
			return getValue(TTableCfg::entryDoComprTable, FALSE);
		}
		/*!	�dert das Flag, ob die Tischinhalte an diesem Terminal zusammengefa� werden sollen.
			\param flag		Wenn TRUE, werden alle Tische komprimiert.
			\note Kann pro Tisch oder pro Terminal festgelegt werden.
			\brief Flag �dern, ob Tische zusammenfassen.
		*/
		void		setDoCompressTable(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoComprTable);
			else
				setValue(TTableCfg::entryDoComprTable, flag);
		}
		bool		doAsOrdered() const
		{
			return getValue(TTableCfg::entryDoAsOrdered, FALSE);
		}
		void		setDoAsOrdered(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAsOrdered);
			else
				setValue(TTableCfg::entryDoAsOrdered, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen an diesem Terminal die Kassenschublade
			ge�fnet werden soll. Der Default-Wert ist TRUE.
			\brief Beim Abrechnen an diesem Terminal Schublade �fnen?
			\sa setDoDrawer
		*/
		bool		doDrawer() const
		{
			return getValue(TTableCfg::entryDoDrawer, TRUE);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal die Kassenschublade
			ge�fnet werden soll.
			\param flag		TRUE, wenn die Schublade ge�fnet werden soll
			\brief Flag �dern, ob Schublade �fnen.
			\sa doDrawer
		*/
		void		setDoDrawer(bool flag)
		{
			if( flag )
				clrValue(TTableCfg::entryDoDrawer);
			else
				setValue(TTableCfg::entryDoDrawer, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen n diesem Terminal der Schubladenzwang
			aktiv sein soll. Der Default-Wert ist FALSE.
			\brief Beim Abrechnen an diesem Terminal warten bis Schublade wieder zu?
			\sa setDoDrawerConstr
		*/
		bool		doDrawerConstr() const
		{
			return getValue(TTableCfg::entryDoDrawerConstr, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal der Schubladenzwang
			aktiv sein soll.
			\param flag		TRUE, wenn auf die Schublade gewartet werden soll
			\brief Flag �dern, ob warten bis Schublade wieder zu.
			\sa doDrawerConstr
		*/
		void		setDoDrawerConstr(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoDrawerConstr);
			else
				setValue(TTableCfg::entryDoDrawerConstr, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen an diesem Terminal das Geldrckgabeger�
			aktiviert werden soll. Der Default-Wert ist TRUE.
			\brief Beim Abrechnen an diesem Terminal Geldrckgabe aktivieren?
			\sa setDoChanger
		*/
		bool		doChanger() const
		{
			return getValue(TTableCfg::entryDoChanger, TRUE);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal die Geldrckgabe
			aktiviert werden soll.
			\param flag		TRUE, wenn Geldrckgabe aktiviert werden soll
			\brief Flag �dern, ob Geldrckgabe aktiv.
			\sa doChanger
		*/
		void		setDoChanger(bool flag)
		{
			if( flag )
				clrValue(TTableCfg::entryDoChanger);
			else
				setValue(TTableCfg::entryDoChanger, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen an diesem Terminal nach dem gegebenen
			Betrag gefragt werden soll. Der Default-Wert ist FALSE.
			\brief Beim Abrechnen an diesem Terminal nach gegebenem Betrag fragen?
			\sa setDoAskGiven
		*/
		bool		doAskGiven() const
		{
			return getValue(TTableCfg::entryDoAskGiven, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal nach dem gegebenen
			Betrag gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\brief Flag �dern, ob nach gegebenen Betrag fragen.
			\sa doAskGiven
		*/
		void		setDoAskGiven(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskGiven);
			else
				setValue(TTableCfg::entryDoAskGiven, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen an diesem Terminal nach dem Trinkgeld
			gefragt werden soll. Der Default-Wert ist FALSE.
			\brief Beim Abrechnen an diesem Terminal nach Trinkgeld fragen?
			\sa setDoAskTip
		*/
		bool		doAskTip() const
		{
			return getValue(TTableCfg::entryDoAskTip, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal nach dem Trinkgeld
			gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\brief Flag �dern, ob nach Trinkgeld fragen.
			\sa doAskTip
		*/
		void		setDoAskTip(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskTip);
			else
				setValue(TTableCfg::entryDoAskTip, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen an diesem Terminal nach der Anzahl
			der G�te gefragt werden soll. Der Default-Wert ist FALSE.
			\brief Beim Abrechnen an diesem Terminal nach Anzahl G�te fragen?
			\sa setDoAskGuests
		*/
		int			doAskGuests() const
		{
			return getValue(TTableCfg::entryDoAskGuests, 0);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal nach der Anzahl
			der G�te gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\brief Flag �dern, ob nach Gastanzahl fragen.
			\sa doAskGuests
		*/
		void		setDoAskGuests(int flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskGuests);
			else
				setValue(TTableCfg::entryDoAskGuests, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen an diesem Terminal nach der Gastanschrift
			gefragt werden soll. Der Default-Wert ist FALSE.
			\note Kann durch setMinGuestAddr eingeschr�kt werden.
			\brief Beim Abrechnen an diesem Terminal nach Gastanschrift fragen?
			\sa setDoAskGuestAddr, getMinGuestAddr, setMinGuestAddr
		*/
		bool		doAskGuestAddr() const
		{
			return getValue(TTableCfg::entryDoAskGuestAddr, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen an diesem Terminal nach der Gastanschrift
			gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note Kann durch setMinGuestAddr eingeschr�kt werden.
			\brief Flag �dern, ob nach Gastanschrift fragen.
			\sa doAskGuestAddr, getMinGuestAddr, setMinGuestAddr
		*/
		void		setDoAskGuestAddr(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskGuestAddr);
			else
				setValue(TTableCfg::entryDoAskGuestAddr, flag);
		}
		/*!	\return den Betrag, ab dem das Flag "Gastanschrift erfragen" aktiv werden soll. Ist der Wert 0, wird die Anschrift
			immer erfragt, sobald das Flag gesetzt ist.
			\brief Minimumbetrag fr Gastanschrift ermitteln.
		*/
		long		getMinGuestAddr() const
		{
			return getValue(TTableCfg::entryMinAmGuestAddr, 0L);
		}
		/*!	�dert den Betrag, ab dem das Flag "Gastanschrift erfragen" aktiv werden soll. Ist der Wert 0, wird die Anschrift
			immer erfragt, sobald das Flag gesetzt ist.
			\param am		Der Mindestbetrag fr das Flag.
			\brief Minimumbetrag fr Gastanschrift �dern.
		*/
		void		setMinGuestAddr(long am)
		{
			if( !am )
				clrValue(TTableCfg::entryMinAmGuestAddr);
			else
				setValue(TTableCfg::entryMinAmGuestAddr, am);
		}
		/*!	\return Liefert TRUE, wenn die Gastanschrift beim Abrechnen auf diesen Tisch erfragt werden soll, in Abh�gigkeit
			des Tischbetrages am. Fa� die Einstellungen "Gastanschrift erfragen" und "Minimumbetrag fr Gastanschrift" zusammen.
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

		/*!	\return des voreingestellte Spoolverzeichnis fr den Orderman Grteldrucker
			\brief Spoolverzeichnis des Orderman Grtesdruckers ermitteln
			\sa setOMBeltPath
		*/
		QString		getOMBeltPath() const
		{
			return getString(entryOMBeltPath);
		}
		/*!	Ändert das voreingestellte Spoolverzeichnis des Orderman Grteldruckers
			\param str		Das neue Spoolverzeichnis des Orderman Grteldruckers
							Ist der Wert 0, wird das Attribut gelöscht.
			\brief Spoolverzeichnis des Orderman Grteldruckers ändern.
			\sa getOMBeltPath
		*/
		void		setOMBeltPath(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryOMBeltPath);
			else
				setValue(entryOMBeltPath, str);
		}
		/*!	\return die voreingestellte Split Partei beim schnellsplitten. Default ist 999
			eingestellt.
			\brief Orderman Split Partei abfragen.
			\sa setOMSplitParty
		*/
		int		getOMSplitParty() const
		{
			return getValue(entryOMSplitParty,999);
		}
		/*!	Ändert die voreingestellte Orderman Split Partei beim schnellsplitten.
			\param party		Die neue Orderman Splitpartei
								Ist der Wert 0, wird das Attribut gelöscht.
			\brief Orderman Split Partei des Terminals ändern.
			\sa getOMSplitParty
		*/
		void		setOMSplitParty(int party)
		{
			if(!party)
				clrValue(entryOMSplitParty);
			else
				setValue(entryOMSplitParty,party);
		}
		/*!	\return die voreingestellte Orderman Menukarte des Terminals.
			Der zurckgegebene Wert ist ein	Index auf die Orderman Menucardliste.
			\brief Orderman Menukarte abfragen.
			\sa setOMMenuCard, TOMMenuCard
		*/
		int		getOMMenuCard() const
		{
			return getValue(entryOMMenuCard, 0);
		}
		/*!	Ändert die voreingestellte Orderman Menukarte des Terminals.
			\param card		Die neue Orderman Menukarte als Index auf die Orderman Menucardliste.
							Ist der Wert 0, wird das Attribut gelöscht.
			\brief Orderman Menukarte des Terminals ändern.
			\sa getOMMenuCard, TOMMMenuCard
		*/
		void		setOMMenuCard(int card)
		{
			if(!card)
				clrValue(entryOMMenuCard);
			else
				setValue(entryOMMenuCard,card);
		}
		/*!	\return das voreingestellte Orderman Profil des Terminals.
			Der zurckgegebene Wert ist ein Index auf die Orderman Profiliste.
			\brief Orderman Profilabfragen.
			\sa setOMProfile, TOMProfile
		*/
		int		getOMProfile() const
		{
			return getValue(entryOMProfile, 0);
		}
		/*!	Ändert das voreingestellte Orderman Profil des Terminals.
			\param profile		Das neue Orderman Profile als Index auf die Orderman Profilliste.
							Ist der Wert 0, wird das Attribut gelöscht.
			\brief Orderman Profil des Terminals ändern.
			\sa getOMPrifile, TOMProfile
		*/
		void		setOMProfile(int profile)
		{
			if(!profile)
				clrValue(entryOMProfile);
			else
				setValue(entryOMProfile,profile);
		}
		/*!	\return Liefert die Serial Nummer des Orderman Devices.
			\brief Orderman Serial Nummer erfragen
		*/
		int			getSerialNo() const
		{
			return getValue(entrySerialNo,0);
		}
		/*!	Ändert die Serial Nummer des Orderman Devices das mit dem Terminal verbunden ist.
			\param no		Die Serial Nummer.
							Ist der Wert 0, wird das Attribut gelöscht.
			\brief Orderman Serial Nummer ändern.
		*/
		void		setSerialNo(int nr)
		{
			if(!nr)
				clrValue(entrySerialNo);
			else
				setValue(entrySerialNo,nr);
		}
		/*!	\return Liefert TRUE, wenn die es sich um ein Terminal handelt das einem Orderman zugeordnet werden kann.
			\brief Orderman Funktionalität erfragen
		*/
		bool		isWireless() const
		{
			return getValue(entryWireless, FALSE);
		}
		/*!	Ändert die Eingenschaft des Terminals einem Orderman zugeordnet werden zu können.
			\param flag	Orderman Funktionalität erlauben JA/NEIN
			\brief Orderman Funktionalität ändern
		*/
		void		setWireless(bool flag)
		{
			if( !flag )
				clrValue(entryWireless);
			else
				setValue(entryWireless, flag);
		}
		bool		isNetDrawers() const
		{
			return getValue(entryNetDrawers, FALSE);
		}
		void		setNetDrawers(bool flag)
		{
			if( !flag )
				clrValue(entryNetDrawers);
			else
				setValue(entryNetDrawers, flag);
		}
		/*!	\return der voreingestellte Transponder typ eines Orderman Terminals.
			\brief Orderman Transponder Typ abfragen.
			\sa setTransponder
		*/
		int			getTransponder() const
		{
			return getValue(entryOMTransponder, 0);
		}
		/*!	ändert den voreingestellten Orderman Transpondertyp des Terminals.
			\param typ		Der neue Transpondertyp
							Ist der Wert 0, ist kein Transponder vorhanden
			\brief Orderman Transpondertyp des Terminals �dern.
			\sa getTransponder
		*/
		void		setTransponder(int typ)
		{
			if( !typ )
				clrValue(entryOMTransponder);
			else
				setValue(entryOMTransponder,typ);
		}
		/*!	\return die voreingestellte POS Konfiguration des Terminals.
			Der zurckgegebene Wert ist ein Index auf die POS Config Liste. (Nur für postouch)
			\brief Kasseneistellunges abfragen.
			\sa setPOSConfig, TTermCfg
		*/
		int		getPOSConfig() const
		{
			return getValue(entryPOSConfig, 0);
		}
		/*!	Ändert die voreingestellte Kassen Konfiguration des Terminals.
			\param cfg		Die neue Kassenkonfiguration als Index auf die POS Config Liste.
							Ist der Wert 0, wird das Attribut gelöscht.
			\brief Kasseneinstellungen des Terminals ändern.
			\sa getPOSConfig, TTermCgf
		*/
		void		setPOSConfig(int cfg)
		{
			if(!cfg)
				clrValue(entryPOSConfig);
			else
				setValue(entryPOSConfig,cfg);
		}

	};

	class			TTerminals
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TTerminals(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TTerminals()
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
		TTerminal*	operator [] (int index)
		{
			return (TTerminal*) TValueList::operator [](index);
		}
	};

	class			TTerminalIt
	: public TValueListIt
	{
	public:
		TTerminalIt(TTerminals& list)
		: TValueListIt(list)
		{
		}
		TTerminal*	operator () ()
		{
			return (TTerminal*) TValueListIt::operator()();
		}
		TTerminal*	toFirst()
		{
			return (TTerminal*) TValueListIt::toFirst();
		}
		TTerminal*	current()
		{
			return (TTerminal*) TValueListIt::current();
		}
		TTerminal*	operator ++ ()
		{
			return (TTerminal*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


