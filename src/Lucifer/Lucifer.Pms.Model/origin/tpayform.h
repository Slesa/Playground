#ifndef				POSLIB_TPAYFORM_H
#define				POSLIB_TPAYFORM_H
#include			"poslib/ttablecfg.h"
#include			"poslib/tarticle.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen fr Abrechnungsarten.
		Alle verfgbaren Abrechnungsarten werden in einer Instanz von TPayforms
		zur Verfgung gestellt.
		\brief POS-Klassen: Abrechnungsart.
	*/
	class			TPayform
	: public TNSValue
	{
	public:
		enum		Roundings
		{
			rndNone		= 0
		,	rndMath		= 1
		,	rndUp		= 2
		,	rndDown		= 3
		,	rndSwiss	= 4
		};
	public: // ---- Defaults ----------------------------------------------------------------------
		static const char	billCounter[];					// Default-Bill-Counter
	public: // ---- Einstellungen -----------------------------------------------------------------
		static const char	entryPrio[];
		static const char	entryPrinters[];
		static const char	entryPPrinters[];
		static const char	entryLimitMin[];
		static const char	entryLimitMinGuest[];
		static const char	entryLimitMax[];
	public: // ---- Flags -------------------------------------------------------------------------
		static const char	entryKostSummary[];
		static const char	entryInSummary[];
		static const char	entryInCashSum[];
		static const char	entryInJournal[];
		static const char	entryIsReadBill[];
		static const char	entryNoPaySplit[];
		static const char	entryNoFinalSplit[];
		static const char	entryNoOverPay[];
		static const char	entryIsNetOnly[];
		static const char	entryIsRetour[];
		static const char	entryBillCounter[];				// Anderen Rechnungszähler verwenden

		static const char	entryAccount[];


		static const char	entryIsNoCreditArt[];			// Keine Auslagen erlauben
		static const char	entryIsNoFiscal[];
		static const char	entryIsNoReturn[];				// Kein Rückgeld
		static const char	entryIsNoTip[];					// Kein Tip
		static const char	entryIsHotel[];
		static const char	entryIsClients[];
		static const char	entryIsElpay[];
		static const char	entryNoBillCopy[];
		static const char	entryPayCurr[];					// Zahlen in Fremdwährung erlauben
		static const char	entryReturnCurr[];				// Rückgeld in Fremdwährung erlauben
	
		static const char	entryRndType[];					// Welche Art der Rundung?
		static const char	entryRndVal[];					// Auf was Runden
		static const char	entryRndPlu[];					// Ausgleichs-PLU
	public:
		/*!	Erzeuge eine leere Instanz einer Abrechnungsart.
			\brief ctor
		*/
		TPayform()
		: TNSValue()
		{
		}
		/*!	\return Die Priorit?fr den Touch innerhalb einer Warengruppe. Warengruppen mit
			hherer Prio sollten weiter vorne stehen.
			\brief Touch, Gruppen-Priorit?ermitteln.
		*/
		int			getPrio() const
		{
			return getValue(entryPrio, 0);
		}
		void		setPrio(int prio)
		{
			if( !prio )
				clrValue(entryPrio);
			else
				setValue(entryPrio, prio);
		}
	public: // ---- Private Einstellungen ---------------------------------------------------------
		QString		getButton() const
		{
			QString ret = getValue(TArticle::entryButton);
			if( ret.isEmpty() )
				return getName();
			return ret;
		}
		void		setButton(const QString& txt)
		{
			if( txt.isEmpty() || txt==getName() )
				clrValue(TArticle::entryButton);
			else
				setValue(TArticle::entryButton, txt);
		}
		/*!	\return Liefert als Stringliste die Kombinationen von Drucker und Layout
			als Index in die entsprechenden Tabellen fr den Rechnungsdruck.
			Falls Der String leer ist, werden die Voreinstellungen von TTableCfg genommen.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert berschreibt den Tisch-Rechnungsdrucker.
			\code
			QStringList list = QStringList::split(";", fam->getPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Abrechnungsart fr Rechnungen ermitteln
			\sa setPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPrinters() const
		{
			return getString(entryPrinters);
		}
		/*!	�dert die Drucker/Layout-Kombinationen, mit denen Zahlungen dieser Zahlart
			ausgedruckt werden sollen. Siehe getPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gel�cht.
			\note Dieser Wert berschreibt den Tisch-Rechnungsdrucker.
			\brief Ausgabedrucker und -layout der Abrechnungsart fr Rechnungen �dern
			\sa getPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		void		setPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entryPrinters);
			else
				setValue(entryPrinters, prns);
		}
		/*!	\return Liefert als Stringliste die Kombinationen von Drucker und Layout
			als Index in die entsprechenden Tabellen fr den Rechnungsdruck bei Teilzahlungen.
			Falls Der String leer ist, werden die Voreinstellungen von TTableCfg genommen.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert berschreibt den Tisch-Rechnungsdrucker fr Teilzahlungen.
			\code
			QStringList list = QStringList::split(";", fam->getPPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Abrechnungsart fr Teilzahlungen ermitteln
			\sa setPPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPPrinters() const
		{
			return getString(entryPPrinters);
		}
		/*!	�dert die Drucker/Layout-Kombinationen, mit denen Teilzahlungen dieser Zahlart
			ausgedruckt werden sollen. Siehe getPPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gel�cht.
			\note Dieser Wert berschreibt den Tisch-Rechnungsdrucker fr Teilzahlungen.
			\brief Ausgabedrucker und -layout der Abrechnungsart fr Teilzahlungen �dern
			\sa getPPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		void		setPPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entryPPrinters);
			else
				setValue(entryPPrinters, prns);
		}
		/*!	\return Liefert die Kostenstellen-Zuordnung fr KOST. Diese Schnittstelle ist nur
			aktiv, wenn in der Office-Konfiguration die Kostenstellen-Krzel angegeben wurde.
			\brief KOST-Zuordnung ermitteln.
		*/
		QChar		getKostSummary() const
		{
			return getString(entryKostSummary)[0];
		}
		/*!	�dert die Kostenstellen-Zuordnung fr KOST. Diese Schnittstelle ist nur
			aktiv, wenn in der Office-Konfiguration die Kostenstellen-Krzel angegeben wurde.
			\param ch	Neue Zuordnung
			\brief KOST-Zuordnung �dern.
		*/
		void		setKostSummary(const QChar& ch)
		{
			setValue(entryKostSummary, QString(ch));
		}

	public: // ---- Private Flags -----------------------------------------------------------------
		/*!	\return TRUE, wenn die Abrechnungsart im Gesamtumsatz bercksichtigt werden
			soll. Der Default-Wert ist TRUE.
			\brief Abrechnungsart im Gesamtumsatz?
			\sa setInSummary
		*/
		bool		inSummary() const
		{
			return getValue(entryInSummary, TRUE);
		}
		/*!	�dert das Flag, ob die Abrechnungsart im Gesamtumsatz bercksichtigt werden
			soll.
			\param flag		TRUE, wenn auf diese Abrechnungsart abgerechneten Tische zum
							Gesamtumsatz hinzugez�lt werden sollen.
			\brief Flag �dern, ob Abrechnungsart im Gesamtumsatz.
			\sa inSummary
		*/
		void		setInSummary(bool flag)
		{
			if( flag )
				clrValue(entryInSummary);
			else
				setValue(entryInSummary, flag);
		}
		/*!	\return TRUE, wenn die Abrechnungsart im Kassenumsatz bercksichtigt werden
			soll. Der Default-Wert ist TRUE.
			\brief Abrechnungsart im Kassenumsatz?
			\sa setInCashSummary
		*/
		bool		inCashSummary() const
		{
			return getValue(entryInCashSum, TRUE);
		}
		/*!	�dert das Flag, ob die Abrechnungsart im Kassenumsatz bercksichtigt werden
			soll.
			\param flag		TRUE, wenn auf diese Abrechnungsart abgerechneten Tische zum
							Kassenumsatz hinzugez�lt werden sollen.
			\brief Flag �dern, ob Abrechnungsart im Kassenumsatz.
			\sa inCashSummary
		*/
		void		setInCashSummary(bool flag)
		{
			if( flag )
				clrValue(entryInCashSum);
			else
				setValue(entryInCashSum, flag);
		}
		/*!	\return TRUE, wenn das Abrechnen auf diese Abrechnungsart protokolliert werden
			soll. Der Default-Wert ist FALSE.
			\note �erschreibt die globalen Journal-Einstellungen.
			\brief Abrechnen auf diese Abrechnungsart protokollieren?
			\sa setInJournal
		*/
		bool		inJournal() const
		{
			return getValue(entryInJournal, TRUE);
		}
		/*!	�dert das Flag, ob das Abrechnen auf diese Abrechnungsart protokolliert werden
			soll.
			\param flag		TRUE, wenn auf das Abrechnen in das Journal eingetragen werden soll.
			\note �erschreibt die globalen Journal-Einstellungen.
			\brief Flag �dern, ob Abrechnungsart im Journal.
			\sa inJournal
		*/
		void		setInJournal(bool flag)
		{
			if( flag )
				clrValue(entryInJournal);
			else
				setValue(entryInJournal, flag);
		}
		/*!	\return TRUE, wenn diese Abrechnungsart nur Rechnung lesen bedeutet, d.h der
			Tisch nicht abgerechnet wird, aber eine Rechnung gedruckt werden soll.
			Der Default-Wert ist FALSE.
			\brief Abrechnungsart Rechnung lesen?
			\sa setIsReadBill
		*/
		bool		isReadBill() const
		{
			return getValue(entryIsReadBill, FALSE);
		}
		/*!	�dert das Flag, ob diese Abrechnungsart Rechnung lesen bedeutet.
			\param flag		TRUE, wenn das Abrechnen nur die Rechnung druckt.
			\brief Flag �dern, ob Abrechnungsart Rechnung lesen.
			\sa isReadBill
		*/
		void		setIsReadBill(bool flag)
		{
			if( !flag )
				clrValue(entryIsReadBill);
			else
				setValue(entryIsReadBill, flag);
		}
		/*!	\return TRUE, wenn beim Bezahlen auf diese Abrechnungsart der Betrag nicht gesplittet werden darf, d.h. der
			Tisch nur komplett bezahlt werden kann. Der Default ist FALSE.
			\note Man beachte die doppelte Verneinung.
			\brief Zahlart splitten erlaubt?
		*/
		bool		noPaySplit() const
		{
			return getValue(entryNoPaySplit, FALSE);
		}
		/*!	�dert das Flag, da�beim Bezahlen auf diese Abrechnungsart der Betrag nicht gesplittet werden darf, d.h. der
			Tisch nur komplett bezahlt werden kann.
			\param flag		TRUE bedeutet splitten verbieten.
			\note Man beachte die doppelte Verneinung.
			\brief Flag Zahlart splitten erlaubt �dern.
		*/
		void		setNoPaySplit(bool flag)
		{
			if( !flag )
				clrValue(entryNoPaySplit);
			else
				setValue(entryNoPaySplit, flag);
		}
		bool		noFinalSplit() const
		{
			return getValue(entryNoFinalSplit, TRUE);
		}
		void		setNoFinalSplit(bool flag)
		{
			if( flag )
				clrValue(entryNoFinalSplit);
			else
				setValue(entryNoFinalSplit, flag);
		}
		bool		noOverPay() const
		{
			return getValue(entryNoOverPay, FALSE);
		}
		void		setNoOverPay(bool flag)
		{
			if( !flag )
				clrValue(entryNoOverPay);
			else
				setValue(entryNoOverPay, flag);
		}
		/*!	\return TRUE, wenn die Abrechnungsart eine Netto-Abrechnung erzwingt, d.h. die Mwst
			aus allen Bestellungen abgezogen wird und nur der Netto-Betrag in Rechnung gestellt wird.
			\brief Flag Netto-Abrechnung abfragen
		*/
		bool		isNetOnly() const
		{
			return getValue(entryIsNetOnly, FALSE);
		}
		/*!	�dert das Flag, ob diese Abrechnungsatr eine Netto-Abrechnung erzwingt, d.h. die Mwst
			aus allen Bestellungen abgezogen wird und nur der Netto-Betrag in Rechnung gestellt wird.
			\param flag		Wenn TRUE, handelt es sich um eine Netto-Abrechnung.
			\brief Flag Netto-Abrechnung �dern
		*/
		void		setNetOnly(bool flag)
		{
			if( !flag )
				clrValue(entryIsNetOnly);
			else
				setValue(entryIsNetOnly, flag);
		}

	public: // ---- Gemeinsame Einstellungen ------------------------------------------------------
		/*!	\return die fest eingestellte Mwst beim bezahlen auf diese Abrechnungsart.
			\brief Mwst fr Abrechnungsart abfragen
			\sa setVat
		*/
		int			getVat() const
		{
			return getValue(TTableCfg::entryVat, 0);
		}
		/*!	�dert die fest eingestellte Mwst beim Bezahlen auf diese Abrechnungsart.
			\param vat		die neue Mwst
							Ist der Wert 0, wird das Attribut gel�cht.
			\brief Mwst fr Abrechnungsart �dern
			\sa getVatRate
		*/
		void		setVat(int vat)
		{
			if( !vat )
				clrValue(TTableCfg::entryVat);
			else
				setValue(TTableCfg::entryVat, vat);
		}
		/*!	\return den Mindestbetrag den ein Tisch haben mu�damit auf diese Abrechnungsart
			abgerechnet werden kann.
			\brief Mindestbetrag fr Abrechnungsart abfragen
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
			\brief Mindestbetrag fr Abrechnungsart setzen
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
			\brief H�hstbetrag fr Abrechnungsart abfragen
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
			\brief H�hstbetrag fr Abrechnungsart setzen
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
		/*!	\return das externe Programm, das beim Abrechnen auf diese Abrechnungsart gestartet
			werden soll.
			\note �erschreibt die globalen Einstellungen.
			\brief Externes Programm fr Abrechnungsart abfragen.
			\sa setExtern
		*/
		QString		getExtern() const
		{
			return getString(TTableCfg::entryExtern);
		}
		/*!	�dert das externe Programm, das beim Abrechnen auf diese Abrechnungsart gestartet
			werden soll.
			\param ext		Das neue externe Programm. Ist der String leer, wird das Attribut
							gel�cht.
			\note �erschreibt die globalen Einstellungen.
			\brief Externes Programm fr Abrechnungsart �dern.
			\sa getExtern
		*/
		void		setExtern(const QString& ext)
		{
			if( ext.isEmpty() )
				clrValue(TTableCfg::entryExtern);
			else
				setValue(TTableCfg::entryExtern, ext);
		}
		/*!	\return Liefert den Index in die Preisfindungstabelle fr diese Abrechnungsart.
			\note �erschreibt die Einstellungen des Terminals, des Kellners und des Tisches.
			\brief Preisfindungsmodell ermitteln.
		*/
		int			getSubvention() const
		{
			return getValue(TTableCfg::entrySubvention, 0);
		}
		/*!	�dert das Preisfindungsmodell fr diese Abrechnungsart. Wenn sub 0 ist, wird das
			Attribut gel�cht.
			\param sub		Neues Preisfindungsmodell.
			\note �erschreibt die Einstellungen des Terminals, des Kellners und des Tisches.
			\brief Preisfindungsmodell �dern.
		*/
		void		setSubvention(int sub)
		{
			if( !sub )
				clrValue(TTableCfg::entrySubvention);
			else
				setValue(TTableCfg::entrySubvention, sub);
		}
	public: // ---- Gemeinsame Flags --------------------------------------------------------------
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart die Kassenschublade
			ge�fnet werden soll. Der Default-Wert ist TRUE.
			\note �erschreibt die globalen Schubladen-Einstellungen.
			\brief Beim Abrechnen auf diese Abrechnungsart Schublade �fnen?
			\sa setDoDrawer
		*/
		bool		doDrawer() const
		{
			return getValue(TTableCfg::entryDoDrawer, TRUE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart die Kassenschublade
			ge�fnet werden soll.
			\param flag		TRUE, wenn die Schublade ge�fnet werden soll
			\note �erschreibt die globalen Schubladen-Einstellungen.
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
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart der Schubladenzwang
			aktiv sein soll. Der Default-Wert ist FALSE.
			\note �erschreibt die globalen Schubladen-Einstellungen.
			\brief Beim Abrechnen auf diese Abrechnungsart warten bis Schublade wieder zu?
			\sa setDoDrawerConstr
		*/
		bool		doDrawerConstr() const
		{
			return getValue(TTableCfg::entryDoDrawerConstr, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart der Schubladenzwang
			aktiv sein soll.
			\param flag		TRUE, wenn auf die Schublade gewartet werden soll
			\note �erschreibt die globalen Schubladen-Einstellungen.
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
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart das Geldrckgabeger�
			aktiviert werden soll. Der Default-Wert ist TRUE.
			\note �erschreibt die globalen Geldrckgabe-Einstellungen.
			\brief Beim Abrechnen auf diese Abrechnungsart Geldrckgabe aktivieren?
			\sa setDoChanger
		*/
		bool		doChanger() const
		{
			return getValue(TTableCfg::entryDoChanger, TRUE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart die Geldrckgabe
			aktiviert werden soll.
			\param flag		TRUE, wenn Geldrckgabe aktiviert werden soll
			\note �erschreibt die globalen Geldrckgabe-Einstellungen.
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
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart nach dem gegebenen
			Betrag gefragt werden soll. Der Default-Wert ist FALSE.
			\note �erschreibt die globalen Einstellungen.
			\brief Beim Abrechnen auf diese Abrechnungsart nach gegebenem Betrag fragen?
			\sa setDoAskGiven
		*/
		bool		doAskGiven() const
		{
			return getValue(TTableCfg::entryDoAskGiven, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart nach dem gegebenen
			Betrag gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �erschreibt die globalen Einstellungen.
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
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart nach dem Trinkgeld
			gefragt werden soll. Der Default-Wert ist FALSE.
			\note �erschreibt die globalen Einstellungen.
			\brief Beim Abrechnen auf diese Abrechnungsart nach Trinkgeld fragen?
			\sa setDoAskTip
		*/
		bool		doAskTip() const
		{
			return getValue(TTableCfg::entryDoAskTip, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart nach dem Trinkgeld
			gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �erschreibt die globalen Einstellungen.
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
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart nach der Anzahl
			der G�te gefragt werden soll. Der Default-Wert ist FALSE.
			\note �erschreibt die globalen Einstellungen.
			\brief Beim Abrechnen auf diese Abrechnungsart nach Anzahl G�te fragen?
			\sa setDoAskGuests
		*/
		bool		doAskGuests() const
		{
			return getValue(TTableCfg::entryDoAskGuests, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart nach der Anzahl
			der G�te gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �erschreibt die globalen Einstellungen.
			\brief Flag �dern, ob nach Gastanzahl fragen.
			\sa doAskGuests
		*/
		void		setDoAskGuests(bool flag)
		{
			if( !flag )
				clrValue(TTableCfg::entryDoAskGuests);
			else
				setValue(TTableCfg::entryDoAskGuests, flag);
		}
		/*!	\return TRUE, wenn beim Abrechnen auf diese Abrechnungsart nach der Gastanschrift
			gefragt werden soll. Der Default-Wert ist FALSE.
			\note �erschreibt die globalen Einstellungen.
			\note Kann durch setMinGuestAddr eingeschr�kt werden.
			\brief Beim Abrechnen auf diese Abrechnungsart nach Gastanschrift fragen?
			\sa setDoAskGuestAddr, getMinGuestAddr, setMinGuestAddr
		*/
		bool		doAskGuestAddr() const
		{
			return getValue(TTableCfg::entryDoAskGuestAddr, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart nach der Gastanschrift
			gefragt werden soll.
			\param flag		TRUE, wenn gefragt werden soll
			\note �erschreibt die globalen Einstellungen.
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
		/*!	\return Liefert TRUE, wenn die Gastanschrift beim Abrechnen auf diese Abrechnungsart erfragt werden soll, in Abh�gigkeit
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
		/*!	\return true, wenn beim Abrechnen auf diese Abrechnungsart die Fiskalschnittstelle
			<b>nicht</b> aktiv sein soll. Der Default-Wert ist false.
			\brief Beim Abrechnen auf diese Abrechnungsart Fiskalschnittstelle aktivieren?
			\sa setNoFiscal
		*/
		bool		isNoFiscal() const
		{
			return getValue(entryIsNoFiscal, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart die Fiskalschnittstelle
			<b>nicht</b> aktiv sein soll.
			\param flag		true, wenn die Abrechnung <b>nicht</b> an das Fiskalsystem bermittelt wird.
			\brief Flag �dern, ob Fiskalschnittstelle aktiv..
			\sa isNoFiscal
		*/
		void		setNoFiscal(bool flag)
		{
			if( !flag )
				clrValue(entryIsNoFiscal);
			else
				setValue(entryIsNoFiscal, flag);
		}
		bool		isNoReturn() const
		{
			return getValue(entryIsNoReturn, FALSE);
		}
		void		setNoReturn(bool flag)
		{
			if( !flag )
				clrValue(entryIsNoReturn);
			else
				setValue(entryIsNoReturn, flag);
		}
		bool		isNoTip() const
		{
			return getValue(entryIsNoTip, FALSE);
		}
		void		setNoTip(bool flag)
		{
			if( !flag )
				clrValue(entryIsNoTip);
			else
				setValue(entryIsNoTip, flag);
		}
		bool		isNoCreditArt() const
		{
			return getValue(entryIsNoCreditArt, FALSE);
		}
		void		setNoCreditArt(bool flag)
		{
			if( !flag )
				clrValue(entryIsNoCreditArt);
			else
				setValue(entryIsNoCreditArt, flag);
		}










		/*!	\return true, wenn beim Abrechnen auf diese Abrechnungsart die Hotelschnittstelle
			aktiv sein soll. Der Default-Wert ist false.
			\brief Beim Abrechnen auf diese Abrechnungsart Hotelschnittstelle aktivieren?
			\sa setIsHotel
		*/
		bool		isHotel() const
		{
			return getValue(entryIsHotel, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart die Hotelschnittstelle
			aktiv sein soll.
			\param flag		true, wenn die Abrechnung auf das Hotel erfolgt
			\brief Flag �dern, ob Hotelschnittstelle aktiv..
			\sa isHotel
		*/
		void		setIsHotel(bool flag)
		{
			if( !flag )
				clrValue(entryIsHotel);
			else
				setValue(entryIsHotel, flag);
		}
		/*!	\return true, wenn beim Abrechnen auf diese Abrechnungsart die Kundendatenbank
			aktiv sein soll. Der Default-Wert ist false.
			\brief Beim Abrechnen auf diese Abrechnungsart Kundendatenbank aktivieren?
			\sa setIsHotel
		*/
		bool		isClients() const
		{
			return getValue(entryIsClients, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart die Kundendatenbank
			aktiv sein soll.
			\param flag		true, wenn die Abrechnung auf die Kundendatenbank erfolgt
			\brief Flag �dern, ob Kundendatenbank aktiv..
			\sa isHotel
		*/
		void		setIsClients(bool flag)
		{
			if( !flag )
				clrValue(entryIsClients);
			else
				setValue(entryIsClients, flag);
		}
		/*!	\return true, wenn beim Abrechnen auf diese Abrechnungsart ElPay
			aktiv sein soll. Der Default-Wert ist false.
			\brief Beim Abrechnen auf diese Abrechnungsart ElPay aktivieren?
			\sa setIsHotel
		*/
		bool		isElpay() const
		{
			return getValue(entryIsElpay, FALSE);
		}
		/*!	�dert das Flag, ob beim Abrechnen auf diese Abrechnungsart ElPay
			aktiv sein soll.
			\param flag		true, wenn die Abrechnung auf ElPay erfolgt
			\brief Flag �dern, ob ElPay aktiv..
			\sa isHotel
		*/
		void		setIsElpay(bool flag)
		{
			if( !flag )
				clrValue(entryIsElpay);
			else
				setValue(entryIsElpay, flag);
		}
		/*!	\return true, wenn der Rechnungsdrucker per Callback ausgew�lt werden soll,
			anstatt eine Rechnungskopie an jeden Drucker zu schicken.
			Der Default-Wert ist false.
			\brief Rechnungskopien drucken?
			\sa setNoBillCopy
		*/
		bool		noBillCopy() const
		{
			return getValue(entryNoBillCopy, FALSE);
		}
		/*!	�dert das Flag, ob der Rechnungsdrucker per Callback ausgew�lt werden soll,
			anstatt eine Rechnungskopie an jeden Drucker zu schicken.
			\param flag		true, wenn der Drucker ausgew�lt werden soll.
			\sa noBillCopy
		*/
		void		setNoBillCopy(bool flag)
		{
			if( !flag )
				clrValue(entryNoBillCopy);
			else
				setValue(entryNoBillCopy, flag);
		}
		bool		isRetour() const
		{
			return getValue(entryIsRetour, FALSE);
		}
		void		setIsRetour(bool flag)
		{
			if( !flag )
				clrValue(entryIsRetour);
			else
				setValue(entryIsRetour, flag);
		}
		bool		isPayCurr() const
		{
			return getValue(entryPayCurr, inCashSummary());
		}
		void		setPayCurr(bool flag)
		{
			if( !flag )
				clrValue(entryPayCurr);
			else
				setValue(entryPayCurr, flag);
		}
		bool		isReturnCurr() const
		{
			return getValue(entryReturnCurr, FALSE);
		}
		void		setReturnCurr(bool flag)
		{
			if( !flag )
				clrValue(entryReturnCurr);
			else
				setValue(entryReturnCurr, flag);
		}
		QString		getBillCounter() const
		{
			return getString(entryBillCounter, billCounter);
		}
		void		setBillCounter(const QString& cnt)
		{
			if( cnt.isEmpty() || cnt==billCounter )
				clrValue(entryBillCounter);
			else
				setValue(entryBillCounter, cnt);
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
		int			getRndType() const
		{
			return getValue(entryRndType, rndNone);
		}
		void		setRndType(int type)
		{
			if( !type )
				clrValue(entryRndType);
			else
				setValue(entryRndType, type);
		}
		int			getRndVal() const
		{
			return getValue(entryRndVal, 0);
		}
		void		setRndVal(int val)
		{
			if( !val )
				clrValue(entryRndVal);
			else
				setValue(entryRndVal, val);
		}
		int			getRndPlu() const
		{
			return getValue(entryRndPlu, 0);
		}
		void		setRndPlu(int plu)
		{
			if( !plu )
				clrValue(entryRndPlu);
			else
				setValue(entryRndPlu, plu);
		}
		long		doRoundings(long amount);
	};


}

using namespace PosLib;

#endif


