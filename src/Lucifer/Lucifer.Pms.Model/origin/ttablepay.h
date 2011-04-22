#ifndef				POSLIB_TTABLEPAY_H
#define				POSLIB_TTABLEPAY_H

namespace PosLib
{
	/*!	Diese Klasse umfa� alle ben�igten Informationen fr die Tischaktion "Bezahlen". Dies umfa� sowohl
		Komplettzahlungen eines Tisches wie auch Teilzahlungen. Wenn der Tisch bei dem Zahlvorgang komplett
		beglichen wird, wird zus�zlich eine Aktion TTableArchived in den Tisch eingetragen.
		\brief POS-Klassen: Tischaktionen, Tisch bezahlen.
	*/
	class			TTablePay
	: public TTableAction
	{
	public:
		// ------ Typen fr Kartendaten ---------------------------------------------------
		static const char	cardClients[];				//!< Kundendatenbank
		static const char	cardDevalue[];				//!< Abwerter-Karte
		static const char	cardHotel[];				//!< Hotel-Karte
		
		// ------ Zahlungsinformationen ---------------------------------------------------
		static const char	entryAmount[];				//!< Bezahlter Betrag
		static const char	entryOrgGiven[];			//!< Gegebener Betrag in der Originalwährung
		static const char	entryGiven[];				//!< Gegebener Betrag
		static const char	entryOrgTip[];				//!< Darin enthaltenes Trinkgeld in der Originalwährung
		static const char	entryTip[];					//!< Darin enthaltenes Trinkgeld
		static const char	entryReturn[];				//!< Rckgeld bei �erzahlung
		static const char	entryRetAmount[];			//!< Rckgeld bei �erzahlung in Fremdwährung
		static const char	entryRetCurrency[];			//!< W�rung des Rückgelds
		static const char	entryRetCurrName[];			//!< Name der W�rung des Rückgelds
		static const char	entryRetCurrShort[];		//!< W�rungskrzel der W�rung des Rückgelds
		static const char	entryRetCurrRate[];			//!< Umrechnungsfaktor der W�rung des Rückgelds
		static const char	entryRetCurrDecPos[];		//!<
		static const char	entryPayform[];				//!< Abrechnungsart des Vorgangs
		static const char	entryPayformName[];			//!< Name der Abrechnungsart
		static const char	entryPayformShort[];		//!< Kurzame der Abrechnungsart
		static const char	entryDiscount[];			//!< 
		static const char	entryGuestCount[];			//!< Anzahl der G�te an diesem Tisch
		static const char	entryPayNum[];				//!< Zah�organgs-Nummer
		static const char	entryVoided[];				//!< Flag ob durch Tisch-Retten storniert
//		static const char	entryInSum[];
		static const char	entryInCashSum[];			//!< Flag, ob im Kassenumsatz enthalten
		static const char	entryCurrency[];			//!< W�rung der Ber�e
		static const char	entryCurrencyName[];		//!< Name der W�rung
		static const char	entryCurrencyShort[];		//!< W�rungskrzel
		static const char	entryCurrencyRate[];		//!< Umrechnungsfaktor der W�rung
		static const char	entryCurrencyDecPos[];		//!<
		// ------ Kundeninformationen -----------------------------------------------------
		static const char	entryCompany[];				//!< Firmenzugeh�igkeit (falls vorhanden)
		static const char	entryCardType[];			//!< Art der Kundenkarte (KundenDB, Hotel,Abwerter)
		static const char	entryCardTrans[];			//!< Transaktionsnummer (sofern untersttzt)
		static const char	entryCardData[];			//!< Karteninformationen des Kunden
		static const char	entryCardAmount[];			//!< Alter Kontostand der Karte/des Zimmers
		static const char	entryCardNewAmount[];		//!< Neuer Kontostand der Karte/des Zimmers
		static const char	entryCardCenter[];			//!< Kostenstelle der Karte fr Fremdprogramme
		
		static const char	entryBayerPrice[];			//!< Bayer, benutzte Preisliste
		static const char	entryBayerGuest[];			//!< Bayer, Flag ob G�tebewirtungskarte
		static const char	entryBayerExtra[];			//!< Bayer, Aufwertungsbetrag ber .auf-Datei
		
		static const char	entryHotelRoom[];			//!< Hotel, Zimmernummer
		static const char	entryHotelParty[];			//!< Hotel, Zimmerpartei
		static const char	entryHotelAccount[];
		static const char	entryHotelCatering[];

		static const char	entryHotelGuest[];
		static const char	entryHotelGroup[];
		static const char	entryClient[];
		static const char	entryClientName[];
		static const char	entryElpayData[];
		static const char	entryElpayIdx[];
		static const char	entrySubvent[];
		static const char	entrySubId[];
		static const char	entrySubName[];
		static const char	entrySubAmount[];
		static const char	entryProfileNumber[];		//!< Profile id bei protel
		static const char	entryOldCredit[];			//!< Bayer: Deckel einer Karte vor der Zahlung
		static const char	entryNewCredit[];			//!< Bayer: Deckel einer Karte nach der Zahlung
		static const char	entryDrawers[];				//!< Welche Kassenschublade
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Pay.
			\brief ctor
		*/
		TTablePay()
		: TTableAction(Pay)
		{
		}
		/*!	\return Liefert die Nummer dieses Abrechnungsvorgangs. Jeder Vorgag sollte eine eindeutige Nummer
			haben, weshalb der aktuelle Wert innerhalb der counters.dat gespeichert wird und so im Netzwerk
			verteilt wird.
			\brief Nummer des Abrechnungsvorgangs ermitteln.
		*/
		long		getPayNum() const
		{
			return getValue(entryPayNum, 0);
		}
		/*!	�dert die Nummer dieses Abrechnungsvorgangs. Jeder Vorgag sollte eine eindeutige Nummer
			haben, weshalb der aktuelle Wert innerhalb der counters.dat gespeichert wird und so im Netzwerk
			verteilt wird.
			\param num		die neue Nummer des Abrechnungsvorgangs
			\brief Nummer des Abrechnungsvorgangs �dern.
		*/
		void		setPayNum(long num)
		{
			setValue(entryPayNum, num);
		}
		/*!	\return Liefert TRUE, wenn dieser Zahlvorgang storniert wurde, d.h. der Tisch gerettet wurde und
			somit alle bisherigen Zahlungen verworfen wurde.
			\brief Flag Zahlvorgang storniert ermitteln.
		*/
		bool		wasVoided() const
		{
			return getValue(entryVoided, FALSE);
		}
		/*!	�dert das Flag, ob dieser Zahlvorgang storniert wurde, d.h. der Tisch gerettet wurde und
			somit alle bisherigen Zahlungen verworfen wurde.
			\param flag		TRUE bedeutet storniert
			\brief Flag Zahlvorgang storniert �dern.
		*/
		void		setVoided(bool flag)
		{
			setValue(entryVoided, flag);
		}
		QChar		getKostSummary() const
		{
			return getString(TPayform::entryKostSummary)[0];
		}
		void		setKostSummary(const QChar& ch)
		{
			setValue(TPayform::entryKostSummary, QString(ch));
		}
		/*!	\return Liefert TRUE, wenn der Umsatz dieses Zahlvorgangs den Kassenbetrag des Kellners beeinflu�.
			\brief Flag Im Kassenumsatz ermitteln
		*/
		bool		inCashSummary() const
		{
			return getValue(entryInCashSum, TRUE);
		}
		/*!	�dert das Flag, ob der Umsatz dieses Zahlvorgangs den Kassenbetrag des Kellners beeinflu�.
			\param flag		TRUE bedeutet im Kassenumsatz enthalten
			\brief Flag Im Kassenumsatz ermitteln
		*/
		void		setCashSummary(bool flag)
		{
			setValue(entryInCashSum, flag);
		}
		/*!	\return Liefert den Betrag dieses Zahlvorgangs. Wenn dieser Betrag kleiner ist als die Gesamtsumme
			des Tisches, handelt es sich um eine Teilzahlung. Wenn der Betrag gr�er ist als der Tischbetrag,
			wird der berschssige Betrag dem Rckgeld zugeordnet.
			\brief Betrag des Zahlvorgangs ermitteln.
		*/
		long		getAmount() const
		{
			return getValue(entryAmount, 0);
		}
		/*!	�dert den Betrag dieses Zahlvorgangs.
			\param amount	der neue Zahlbetrag dieser Aktion
			\brief Betrag des Zahlvorgangs �dern.
		*/
		void		setAmount(long amount)
		{
			setValue(entryAmount, amount);
		}
		bool		hasGiven() const
		{
			return hasValue(entryGiven);
		}
		/*!	\return Liefert den Betrag, den der Gast bei diesem Zahlvorgang gegeben hat. Dieser Betrag
			entspricht im Normalfall dem Tischbetrag. Wenn er ihn berschreitet, wird das Rckgeld entsprechend
			gesetzt.
			\brief Gegebenen Betrag ermitteln.
		*/
		long		getGiven() const
		{
			return getValue(entryGiven, getAmount());
		}
		/*!	�dert den Betrag, den der Gast bei diesem Zahlvorgang gegeben hat. Dieser Betrag
			entspricht im Normalfall dem Tischbetrag. Wenn er ihn berschreitet, wird das Rckgeld entsprechend
			gesetzt.
			\param given	Der neue gegebene Betrag
			\brief Gegebenen Betrag �dern.
		*/
		void		setGiven(long given)
		{
			setValue(entryGiven, given);
		}
		bool		hasOrgGiven() const
		{
			return hasValue(entryOrgGiven);
		}
		long		getOrgGiven() const
		{
			return getValue(entryOrgGiven, 0L);
		}
		void		setOrgGiven(long given)
		{
			setValue(entryOrgGiven, given);
		}
		/*!	\return Liefert das Trinkgeld, das der Gast bei dieser Aktion dem Kellner gegeben hat.
			\brief Trinkgeld ermitteln.
		*/
		long		getTip() const
		{
			return getValue(entryTip, 0);
		}
		/*!	\return �dert das Trinkgeld, das der Gast bei dieser Aktion dem Kellner gegeben hat.
			\param tip		das neue Trinkgeld
			\brief Trinkgeld �dern.
		*/
		void		setTip(long tip)
		{
			// Achtung, Scheiss Crystal muss den Tip als "0" bekommen. Wegen ODBC-Müll. Deshalb kein clrValue!
			setValue(entryTip, tip);
		}
		bool		hasOrgTip() const
		{
			return hasValue(entryOrgTip);
		}
		long		getOrgTip() const
		{
			return getValue(entryOrgTip, 0L);
		}
		void		setOrgTip(long tip)
		{
/*			if( !tip )
				clrValue(entryOrgTip);
			else */
				setValue(entryOrgTip, tip);
		}
		/*!	\return Liefert das Rckgeld, das der Gast bei dieser Aktion zurckbekommt.
			\brief Rckgeld ermitteln.
		*/
		long		getReturn() const
		{
			return getValue(entryReturn, 0L);
		}
		/*!	�dert das Rckgeld, das der Gast bei dieser Aktion zurckbekommt.
			\param ret		das neue Rckgeld
			\brief Rckgeld �dern.
		*/
		void		setReturn(long ret)
		{
			setValue(entryReturn, ret);
		}
		long		getRetAmount() const
		{
			return getValue(entryRetAmount, 0L);
		}
		int			getRetCurrency() const
		{
			return getValue(entryRetCurrency, 0);
		}
		void		setRetAmount(long ret)
		{
			setValue(entryRetAmount, ret);
		}
		bool		getReturnCurrency(TCurrency& curr)
		{
			int id = getValue(entryRetCurrency);
			if( !id )
				return FALSE;
			curr.setID(id);
			curr.setName(getString(entryRetCurrName));
			curr.setShortname(getString(entryRetCurrShort));
			curr.setRate(getValue(entryRetCurrRate, 0.0));
            curr.setDecPos(getValue(entryRetCurrDecPos, 2));
            return TRUE;
		}
		void		setReturnCurrency(TCurrency* curr)
		{
			if( !curr ) {
				clrValue(entryRetCurrency);
				clrValue(entryRetCurrName);
				clrValue(entryRetCurrShort);
				clrValue(entryRetCurrRate);
                clrValue(entryRetCurrDecPos);
            } else {
				setValue(entryRetCurrency, curr->getID());
				setValue(entryRetCurrName, curr->getName());
				setValue(entryRetCurrShort, curr->getShortname());
				setValue(entryRetCurrRate, curr->getRate());
                setValue(entryRetCurrDecPos, curr->getDecPos());
            }
		}
		/*!	\return Liefert den Index der Abrechnungsart, auf die dieser Zahlvorgang erfolgt.
			\brief Zahlart ermitteln.
		*/
		int			getPayform() const
		{
			return getValue(entryPayform, 0);
		}
		/*!	�dert den Index der Abrechnungsart, auf die dieser Zahlvorgang erfolgt.
			\param form		die neue Zahlart
			\brief Zahlart �dern.
		*/
		void		setPayform(int form)
		{
			setValue(entryPayform, form);
		}
		/*!	\return Liefert den Namen der Abrechnungsart, auf die dieser Zahlvorgang erfolgt.
			\brief Zahlartnamen ermitteln.
		*/
		QString		getPayformName() const
		{
			return getString(entryPayformName);
		}
		/*!	�dert den Namen der Abrechnungsart, auf die dieser Zahlvorgang erfolgt.
			\param name		der neue Name der Zahlart
			\brief Zahlartnamen �dern.
		*/
		void		setPayformName(const QString& name)
		{
			setValue(entryPayformName, name);
		}
		void		setPayformShort(const QString& name)
		{
			setValue(entryPayformShort, name);
		}
		QString		getPayformAccount() const
		{
			return getString(TPayform::entryAccount);
		}
		void		setPayformAccount(const QString& acc)
		{
			if( acc.isEmpty() )
				clrValue(TPayform::entryAccount);
			else
				setValue(TPayform::entryAccount, acc);
		}
		/*!	�ernimmt die ben�igten Zahlartinformationen aus der Abrechnungsart form.
			\param form		die verwendete Abrechnungsart
			\brief Zahlartinformationen bernehmen.
		*/
		void		setPayform(TPayform* form)
		{
			setPayform(form->getID());
			setPayformName(form->getName());
			setPayformShort(form->getShortname());
			setPayformAccount(form->getAccount());
			setExtern(form->getExtern());
//			setSummary(form->inSummary());
			setNoFiscal(form->isNoFiscal());
			setCashSummary(form->inCashSummary());
			QChar ch = form->getKostSummary();
			if( !ch.isNull() )
				setKostSummary(ch);
		}
		/*!	\return Liefert den Index der W�rung, auf die dieser Zahlvorgang erfolgt.
			\brief W�rung ermitteln.
		*/
		int			getCurrency() const
		{
			return getValue(entryCurrency, 0);
		}
		/*!	�dert den Index der W�rung, auf die dieser Zahlvorgang erfolgt.
			\param curr		die neue W�rung
			\brief W�rung �dern.
		*/
		void		setCurrency(int curr)
		{
			setValue(entryCurrency, curr);
		}
		/*!	\return Liefert den Namen der W�rung, auf die dieser Zahlvorgang erfolgt.
			\brief W�rungsbezeichnung ermitteln.
		*/
		QString		getCurrencyName() const
		{
			return getString(entryCurrencyName);
		}
		/*!	�dert den Namen der W�rung, auf die dieser Zahlvorgang erfolgt.
			\param name		der neue Name der W�rung
			\brief W�rungsbezeichnung �dern.
		*/
		void		setCurrencyName(const QString& name)
		{
			setValue(entryCurrencyName, name);
		}
		/*!	\return Liefert das Krzel der W�rung, auf die dieser Zahlvorgang erfolgt.
			\brief W�rungskrzel ermitteln.
		*/
		QString		getCurrencyShort() const
		{
			return getString(entryCurrencyShort);
		}
		/*!	�dert das Krzel der W�rung, auf die dieser Zahlvorgang erfolgt.
			\param name		das neue Krzel der W�rung
			\brief W�rungskrzel �dern.
		*/
		void		setCurrencyShort(const QString& name)
		{
			setValue(entryCurrencyShort, name);
		}
		/*!	\return Liefert den Umrechnungsfaktor der W�rung, auf die dieser Zahlvorgang erfolgt.
			\brief Umrechnungsfaktor der W�rung ermitteln.
		*/
		double		getCurrencyRate() const
		{
			return getValue(entryCurrencyRate, 1.0);
		}
		/*!	�dert den Umrechnungsfaktor der W�rung, auf die dieser Zahlvorgang erfolgt.
			\param rate		der neue Faktor der W�rung
			\brief Umrechnungsfaktor der W�rung �dern.
		*/
		void		setCurrencyRate(double rate)
		{
			setValue(entryCurrencyRate, rate);
		}
        int 		getCurrencyDecPos() const
        {
            return getValue(entryCurrencyDecPos, 0);
        }
        void		setCurrencyDecPos(int pos)
        {
            setValue(entryCurrencyDecPos, pos);
        }
        /*!	�ernimmt die ben�igten W�rungsinformationen aus der W�rung curr.
			\param curr		die verwendete W�rung
			\brief W�rungsinformationen bernehmen.
		*/
		void		setCurrency(TCurrency* curr)
		{
			setCurrency(curr->getID());
			setCurrencyName(curr->getName());
			setCurrencyShort(curr->getShortname());
			setCurrencyRate(curr->getRate());
            setCurrencyDecPos(curr->getDecPos());
        }
		/*!	\return Liefert die Anzahl der G�te bei diesem Zahlvorgang. Dieser Wert dient prinzipiell der
			Auswertung, kann jedoch auch dazu benutzt werden, den Rechnungsbetrag auf die entsprechende
			Anzahl zu teilen.
			\brief Anzahl der G�te ermitteln.
		*/
		int			getGuestCount() const
		{
			return getValue(entryGuestCount, 0);
		}
		/*!	�dert die Anzahl der G�te bei diesem Zahlvorgang. Dieser Wert dient prinzipiell der
			Auswertung, kann jedoch auch dazu benutzt werden, den Rechnungsbetrag auf die entsprechende
			Anzahl zu teilen.
			\brief Anzahl der G�te �dern.
		*/
		void		setGuestCount(int count)
		{
			setValue(entryGuestCount, count);
		}
		/*!	\return Liefert den Namen des externen Moduls, das bei diesem Zahlvorgang zum Ermitteln von
			Kundendaten etc benutzt wurde. Wird vor allem fr das Tisch-Retten gebraucht, um den Zahlbetrag
			wieder gutzuschreiben.
			\brief Externes Modul ermitteln.
		*/
		QString		getExtern() const
		{
			return getString(TTableCfg::entryExtern);
		}
		/*!	�dert den Namen des externen Moduls, das bei diesem Zahlvorgang zum Ermitteln von
			Kundendaten etc benutzt wurde. Wird vor allem fr das Tisch-Retten gebraucht, um den Zahlbetrag
			wieder gutzuschreiben.
			\brief Externes Modul �dern.
		*/
		void		setExtern(const QString& ext)
		{
			if( !ext.isEmpty() )
				setValue(TTableCfg::entryExtern, ext);
			else
				clrValue(TTableCfg::entryExtern);
		}
		/*!	\return true, wenn bei dieser Zahlung die Fiskalschnittstelle
			<b>nicht</b> aktiv sein soll. Der Default-Wert ist false.
			\brief Die Zahlung an Fiskalschnittstelle bermitteln?
			\sa setNoFiscal
		*/
		bool		isNoFiscal() const
		{
			return getValue(TPayform::entryIsNoFiscal, FALSE);
		}
		/*!	�dert das Flag, ob bei Dieser zahlung die Fiskalschnittstelle
			<b>nicht</b> aktiv sein soll.
			\param flag		true, wenn die Abrechnung <b>nicht</b> an das Fiskalsystem bermittelt wird.
			\brief Flag �dern, ob Fiskalschnittstelle aktiv..
			\sa isNoFiscal
		*/
		void		setNoFiscal(bool flag)
		{
			if( !flag )
				clrValue(TPayform::entryIsNoFiscal);
			else
				setValue(TPayform::entryIsNoFiscal, flag);
		}


		int			getDiscount() const
		{
			return getValue(entryDiscount, 0);
		}
		void		setDiscount(int disc)
		{
			setValue(entryDiscount, disc);
		}
		long		getSubvent() const
		{
			return getValue(entrySubvent, 0L);
		}
		void		setSubvent(long sub)
		{
			setValue(entrySubvent, sub);
		}
		int			getSubId() const
		{
			return getValue(entrySubId, 0);
		}
		void		setSubId(int id)
		{
			setValue(entrySubId, id);
		}
		QString		getSubName() const
		{
			return getString(entrySubName);
		}
		void		setSubName(const QString& name)
		{
			setValue(entrySubName, name);
		}
		long		getSubAmount() const
		{
			return getValue(entrySubAmount, 0L);
		}
		void		setSubAmount(long am)
		{
			setValue(entrySubAmount, am);
		}
		void		setSubvention(TSubvention* sub)
		{
			if( !sub )
				return;
			setSubId(sub->getID());
			setSubName(sub->getName());
		}
/*
		bool		inSummary() const
		{
			return getValue(entryInSum, TRUE);
		}
		void		setSummary(bool flag)
		{
			setValue(entryInSum, flag);
		}
*/
// ---------------- Kundeninformationen -----------------------------------------------------
		/*!	\return Liefert die beim Zahlvorgang ermittelte Firmenzugeh�igkeit, sofern ein
			externes Modul diese Information bereitstellt (KundenDB, Abwerter...)
			\brief Firma eines Kunden ermitteln.
		*/
		QString		getCompany()
		{
			return getString(entryCompany);
		}
		/*!	�dert die Firmenzugeh�igkeit eines beim Zahlvorgang beteiligten Kunden. Diese
			Information mu�von einem externen Modul bereitgestellt werden.
			\param comp		Firma des Kunden
			\brief Firma eines Kunden �dern.
		*/
		void		setCompany(const QString& comp)
		{
			setValue(entryCompany, comp);
		}
		/*!	\return Liefert den Typ einer beim Zahlvorgang beteiligten Kundenkarte, sofern ein
			externes Modul diese Information bereitstellt. 
			Bisher kann die Karteninformation von folgenden Modulen stammen:
			- KundenDB
			- Hotelkarte
			- Abwerter-Karte
			\brief Kartentyp ermitteln.
		*/
		QString		getCardType()
		{
			return getString(entryCardType);
		}
		/*!	�dert den Typ der beim Zahlvorgang beteiligten Kundenkarte. Diese Information mu�
			von einem externen Modul bereitgestellt werden.
			\param type		Neuer Kartentyp
			\brief Kartentyp �dern-
		*/
		void		setCardType(const QString& type)
		{
			setValue(entryCardType, type);
		}
		/*!	\return Liefert TRUE, wenn die Karteninformationen fr diesen Zahlvorgang vorliegen,
			d.h. die Zuordnung des Kunden mittels Kartenauthentifizierung vorgenommen wurde.
			\brief Kartenzahlung erfolgt?
		*/
		bool		hasCardData() const
		{
			return hasValue(entryCardData);
		}
		/*!	\return Liefert den Inhalt einer beim Zahlvorgang beteiligten Kundenkarte, sofern ein
			externes Modul diese Information bereitstellt. 
			\brief Kartendaten ermitteln.
		*/
		QString		getCardData()
		{
			return getString(entryCardData);
		}
		/*!	�dert den Inhalt der beim Zahlvorgang beteiligten Kundenkarte. Diese Information mu�
			von einem externen Modul bereitgestellt werden.
			\param data		Neue Kartendaten
			\brief Kartendaten �dern-
		*/
		void		setCardData(const QString& data)
		{
			setValue(entryCardData, data);
		}
		/*!	\return Liefert den Kontostand des Kundenkontos, bevor diese Zahlung abgebucht wurde.
			\brief Kunden-Kontostand vor Zahlung ermitteln.
		*/
		long		getCardAmount() const
		{
			return getValue(entryCardAmount, 0L);
		}
		/*!	�dert den alten Kontostand des Kundenkontos auf am.
			\param am		Neuer alter Kontostand des Kontos
			\brief Kunden-Kontostand vor Zahlung �dern.
		*/
		void		setCardAmount(long am)
		{
			setValue(entryCardAmount, am);
		}
		/*!	\return Liefert den Kontostand des Kundenkontos, nachdem diese Zahlung abgebucht wurde.
			\brief Kunden-Kontostand nach Zahlung ermitteln.
		*/
		long		getCardNewAmount() const
		{
			return getValue(entryCardNewAmount, 0L);
		}
		/*!	�dert den neuen Kontostand des Kundenkontos auf am.
			\param am		Neuer Kontostand des Kontos
			\brief Kunden-Kontostand nach Zahlung �dern.
		*/
		void		setCardNewAmount(long am)
		{
			setValue(entryCardNewAmount, am);
		}
		/*!	\return Liefert die Kostenzustellen-Zuordnung fr Fremdprogramme fr diese Kundenkarte,
			sofern ein externes Modul diese Information bereitstellt.
			\brief Kostenstelle der Karte ermitteln.
		*/
		int			getCardCenter() const
		{
			return getValue(entryCardCenter, 0);
		}
		/*!	�dert die Kostenstellen-Zuordnung fr Fremdprogramme fr diese Kundenkarte.
			Diese Information mu�von einem externen Modul bereitgestellt werden.
			\param center	Neue Kostenstelle der Karte
			\brief Kostenstelle der Karte �dern.
		*/
		void		setCardCenter(int center)
		{
			if( !center )
				clrValue(entryCardCenter);
			else
				setValue(entryCardCenter, center);
		}
		
		int			getBayerPrice() const
		{
			return getValue(entryBayerPrice, 0);
		}
		void		setBayerPrice(int list)
		{
			if( !list )
				clrValue(entryBayerPrice);
			else
				setValue(entryBayerPrice, list);
		}
		bool		isBayerGuest() const
		{
			return getValue(entryBayerGuest, FALSE);
		}
		void		setBayerGuest(bool on)
		{
			if( !on )
				clrValue(entryBayerGuest);
			else
				setValue(entryBayerGuest, on);
		}
		
		
		bool		hasHotelRoom() const
		{
			return hasValue(entryHotelRoom);
		}
		QString		getHotelRoom() const
		{
			return getString(entryHotelRoom);
		}
		void		setHotelRoom(const QString& room)
		{
			setValue(entryHotelRoom, room);
		}
		int			getHotelParty() const
		{
			return getValue(entryHotelParty, 0);
		}
		void		setHotelParty(int party)
		{
			setValue(entryHotelParty, party);
		}
		bool		hasHotelAccount() const
		{
			return hasValue(entryHotelAccount);
		}
		QString		getHotelAccount()
		{
			return getString(entryHotelAccount);
		}
		void		setHotelAccount(const QString& acc)
		{
			setValue(entryHotelAccount, acc);
		}
		QString		getHotelGuest() const
		{
			return getString(entryHotelGuest);
		}
		void		setHotelGuest(const QString& guest)
		{
			setValue(entryHotelGuest, guest);
		}
		int			getHotelGroup()
		{
			return getValue(entryHotelGroup, 0);
		}
		void		setHotelGroup(int grp)
		{
			setValue(entryHotelGroup, grp);
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
		bool		hasElpayData() const
		{
			return hasValue(entryElpayData);
		}
		QString		getElpayData() const
		{
			return getString(entryElpayData);
		}
		void		setElpayData(const QString& data)
		{
			setValue(entryElpayData, data);
		}
		int			getElpayIndex() const
		{
			return getValue(entryElpayIdx, 0);
		}
		void		setElpayIndex(int idx)
		{
			if( !idx )
				clrValue(entryElpayIdx);
			else
				setValue(entryElpayIdx, idx);
		}
		/*!	\return [Bayer] Liefert den vor der Zahlung auf der Karte vorgefundenen Deckelbetrag.
			\note Deckelbetrag = Saarl�discher Deckel, also Kredit.
		*/
		long		getOldCredit() const
		{
			return getValue(entryOldCredit, 0);
		}
		void		setOldCredit(long cred)
		{
			setValue(entryOldCredit, cred);
		}
		/*!	\return [Bayer] Liefert den nach der Zahlung auf die Karte geschriebenen Deckelbetrag.
			\note Deckelbetrag = Saarl�discher Deckel, also Kredit.
		*/
		long		getNewCredit() const
		{
			return getValue(entryNewCredit, 0);
		}
		void		setNewCredit(long cred)
		{
			setValue(entryNewCredit, cred);
		}
		QStringList	getDrawers() const
		{
			return QStringList::split(",", getValue(entryDrawers));
		}
		void		setDrawers(const QStringList& drw)
		{
			if( drw.count() )
				setValue(entryDrawers, drw.join(","));
			else
				clrValue(entryDrawers);
		}
	};
}

#endif
