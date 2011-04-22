#ifndef				POSLIB_TTABLEORDER_H
#define				POSLIB_TTABLEORDER_H
#include			"poslib/tfamgroup.h"

namespace PosLib
{
	/*!	Diese Klasse umfa? alle bentigten Informationen fr die Tischaktion "Bestellung".
		\brief POS-Klassen: Tischaktionen, Artikel bestellt.
	*/
	class			TTableOrder
	: public TTableArtAction
	{
	public:
		static const char	entryMemo[];
		static const char	entryRound[];
		static const char	entrySeat[];
		static const char	entryEan[];
		static const char	entryVoided[];
		static const char	entryMoved[];
		static const char	entryDiscard[];
		static const char	entryCredit[];
		static const char	entryFreePrice[];
		static const char	entryTimePaid[];
		static const char	entryTimeArt[];
		static const char	entryFromSplit[];
		static const char	entryIndexIH[];
		static const char	entryCompIH[];
		static const char	entryRateIH[];
		static const char	entryNameIH[];
		static const char	entryNetIH[];
		static const char	entryVatIH[];
		static const char	entryIndexOH[];
		static const char	entryCompOH[];
		static const char	entryRateOH[];
		static const char	entryNameOH[];
		static const char	entryNetOH[];
		static const char	entryVatOH[];
		static const char	entryIsVatOH[];
		static const char	entrySavedVat[];
		static const char	entryStock[];		
		static const char	entryTare[];
		static const char	entryPriceKg[];
		static const char	entryTaxAmount[];
		static const char	entryTaxAmounts[];
		static const char	entryNetWeight[];
		static const char	entryTaxGroupsIH[];
		static const char	entryTaxGroupsOH[];
// -----------------------
		static const char	entryDiscount[];
		static const char	entryDiscId[];
		static const char	entryDiscBase[];
		static const char	entryDiscName[];
		static const char	entryDiscRate[];
		static const char	entryDiscAbs[];
		static const char	entryDiscSubId[];
		static const char	entryDiscSubName[];
		static const char	entrySubvent[];
		static const char	entryLength[]; // Meterware
		static const char	entryWidth[]; // Meterware
		static const char	entryHeight[]; // Meterware
		static const char	entryOrdermanFax[];

		
		//		static const char	entryModifiers[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Order.
			\brief ctor
		*/
		TTableOrder()
		: TTableArtAction(Order)
		{
		}
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ type.
			\brief ctor
		*/
		TTableOrder(int type)
		: TTableArtAction(type)
		{
		}
		static void	combi2order(TTableOrder* combi, const TTableOrder* order);
		/*!	?ernimmt alle Daten aus der Bestellung order bis auf die Eintr? Stornoanzahl,
			Splitanzahl und Rundennummer.
			\return Eine Referenz auf die neuen Bestelldaten.
			\brief Copy-Operator.
		*/
		TTableOrder& operator = (const TTableOrder& order);
		/*!	\return Liefert das bei der Bestellung hinterlegte Orderman Faximage file.
			\brief Orderman Fax Imagefile ermitteln.
		*/
		QString		getOrdermanFax() const
		{
			return getString(entryOrdermanFax);
		}
		/*!	Ändert das bei der Bestellung hinterlegte Orderman Fax Image file
			\param faxfile		das neue faximage
			\brief Orderman Fax Image ändern.
		*/
		void		setOrdermanFax(const QString& fi)
		{
			setValue(entryOrdermanFax,fi);
		}
		/*!	\return Liefert den bei der Bestellung hinterlegten Memotext.
			\brief Memotext ermitteln.
		*/
		QString		getMemo() const
		{
			return getString(entryMemo);
		}
		/*!	Idert den bei der Bestellung hinterlegten Memotext.
			\param memo		der neue Memotext
			\brief Memotext ?ern.
		*/
		void		setMemo(const QString& memo)
		{
			setValue(entryMemo, memo);
		}
		QString		getEan() const
		{
			return getString(entryEan);
		}
		void		setEan(const QString& ean)
		{
			setValue(entryEan, ean);
		}
		QString		getAccount() const
		{
			return getString(TFamGroup::entryAccount);
		}
		void		setAccount(const QString& acc)
		{
			if( acc.isEmpty() )
				clrValue(TFamGroup::entryAccount);
			else
				setValue(TFamGroup::entryAccount, acc);
		}
		/*!	\overload TTableArtAction::setCount(double count)
			?erschreibt die entsprechende Funktion von TTableArtAction, da die Mehrwertsteuer neu
			berechnet werden mu?
			\param count	die neue Anzahl
			\brief Anzahl ?ern.
		*/
		void		setCount(double count)
		{
			TTableArtAction::setCount(count);
			calcVat();
		}
		void		setVending(double count)
		{
			if( 0.0==count )
				clrValue(TArticle::entryVending);
			else
				setValue(TArticle::entryVending, count);
		}
		double		getVending() const
		{
			return getValue(TArticle::entryVending, 0.0);
		}
		/*!	\overload TTableArtAction::setPrice(long pr)
			?erschreibt die entsprechende Funktion von TTableArtAction, da die Mehrwertsteuer neu
			berechnet werden mu?
			\param pr		der neue Preis
			\brief ArtikelPreis ?ern.
		*/
		void		setPrice(long pr)
		{
			TTableArtAction::setPrice(pr);
			if( isClientDB() && isFreePrice() )
				setValue(TArticle::entryClientAmount, pr);
			calcVat();
		}
		virtual void	setArticle(const QString& art)
		{
			TTableArtAction::setArticle(art);
		}
		/*!	\overload TTableArtAction::setArticle(TArticle* art)
			?erschreibt die entsprechende Funktion von TTableArtAction, da mehr Daten fr eine Order
			bernommen werden mssen.
			\param art		Der Artikel dieser Aktion
			\brief Artikeldaten bernehmen.
		*/
		void		setArticle(TArticle* art);
		/*!	\return Liefert die Rundennummer dieser Bestellung. Alle Bestellungen, die zwischen "Tisch ffnen"
			und "Tisch committen" bestellt werden, gehren zur selben Runde.
			\brief Rundennummer ermitteln.
		*/
		int			getRound() const
		{
			return getValue(entryRound, 0);
		}
		/*!	Idert die Rundennummer dieser Bestellung. Alle Bestellungen, die zwischen "Tisch ffnen"
			und "Tisch committen" bestellt werden, gehren zur selben Runde.
			\param _round		die neue Rundennummer
			\brief Rundennummer ?ern.
		*/
		void		setRound(long _round)
		{
			setValue(entryRound, _round);
		}
		/*!	\return Liefert die Sitznummer dieser Bestellung. Sitznummern dienen dazu, G?e, die keinem festen
			Tisch zugeordnet werden knnen oder die den Tisch wechseln (Disko vor allem), per Bondruck
			identifizieren zu knnen.
			\brief Sitznummer ermitteln.
		*/
		int			getSeat() const
		{
			return getValue(entrySeat, 0);
		}
		/*!	Idert die Sitznummer dieser Bestellung. Sitznummern dienen dazu, G?e, die keinem festen
			Tisch zugeordnet werden knnen oder die den Tisch wechseln (Disko vor allem), per Bondruck
			identifizieren zu knnen.
			\param seat		die neu Sitznummer
			\brief Sitznummer ?ern.
		*/
		void		setSeat(int seat)
		{
			if( seat )
				setValue(entrySeat, seat);
			else
				clrValue(entrySeat);
		}
		/*!	\return Liefert die Sitznummer dieser Bestellung. Sitznummern dienen dazu, G?e, die keinem festen
			Tisch zugeordnet werden knnen oder die den Tisch wechseln (Disko vor allem), per Bondruck
			identifizieren zu knnen.
			\brief Sitznummer ermitteln.
		*/
		int			getStock() const
		{
			return getValue(entryStock, 0);
		}
		
		
		
			/*!	Merkt sich die den Stock Count bei der Order
			\param aktuellen Stock Count
			\brief StockCount
		*/
		void		setStock(int stock)
		{
			if( stock )
				setValue(entryStock, stock);
			else
				clrValue(entryStock);
		}
		
		
		

		/*!	\return Liefert die Anzahl der von dieser Bestellung stornierten Artikel.
			\brief Stornierte Artikel ermitteln.
		*/
		double		getVoided() const
		{
			return getValue(entryVoided, 0.0);
		}
		/*!	Idert die Anzahl der von dieser Bestellung stornierten Artikel.
			\param voided		die neue Stornoanzahl
			\brief Stornierte Artikel ?ern.
		*/
		void		setVoided(double voided)
		{
			setValue(entryVoided, getVoided()+voided);
		}
		/*!	\return Liefert die Anzahl der von dieser Bestellung gesplitteten Artikel.
			\brief Gesplittete Artikel ermitteln.
		*/
		double		getMoved() const
		{
			return getValue(entryMoved, 0.0);
		}
		/*!	Idert die Anzahl der von dieser Bestellung verschobenen, d.h. gesplitteten, Artikel.
			\param moved		die neue Splitanzahl
			\brief Gesplittete Artikel ?ern.
		*/
		void		setMoved(double moved)
		{
			setValue(entryMoved, getMoved()+moved);
		}
		/*!	\return Liefert TRUE, wenn diese Bestellung durch den Abschlu?des Tisches hinzugefgt wurde und
			bei einem Tisch-Retten wieder vom Tisch genommen werden mu?
			\brief Bestellung beim Retten verwerfen?
		*/
		bool		isDiscard() const
		{
			return getValue(entryDiscard, FALSE);
		}
		/*!	Idert das Flag, da?diese Bestellung durch den Abschlu?des Tisches hinzugefgt wurde und
			bei einem Tisch-Retten wieder vom Tisch genommen werden mu?
			\param d		TRUE, wenn der Artikel verworfen werden mu?
			\brief Flag Bestellung beim Retten ?ern.
		*/
		void		setDiscard(bool d)
		{
			if( !d )
				clrValue(entryDiscard);
			else
				setValue(entryDiscard, d);
		}
		/*!	\return Liefert TRUE, wenn es sich bei dieser Bestellung um einen Auslagenartikel handelt.
			\brief Flag Auslagenartikel ermitteln.
		*/
		bool		isCredit() const
		{
			return getValue(entryCredit, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dieser Bestellung um einen Auslagenartikel handelt.
			\param cr		TRUE, wenn es sich um eine Auslage handelt.
			\brief Flag Auslagenartikel ?ern.
		*/
		void		setCredit(bool cr)
		{
			setValue(entryCredit, cr);
		}
		/*!	\return Liefert TRUE, wenn es sich bei dieser Bestellung um einen Artikel mit freier Preiseingabe
			handelt.
			\brief Flag Freie Preiseingabe ermitteln.
		*/
		bool		isFreePrice() const
		{
			return getValue(entryFreePrice, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dieser Bestellung um einen Artikel mit freier Preiseingabe handelt.
			\param fp		TRUE, wenn es sich um einen Artikel mit freier Preiseingabe handelt.
			\brief Flag Freie Preiseingabe ?ern.
		*/
		void		setFreePrice(bool fp)
		{
			setValue(entryFreePrice, fp);
		}
		/*!	\return Liefert TRUE, wenn es sich bei dieser Bestellung um einen Kontrollartikel handelt.
			\brief Flag Kontrollartikel ermitteln.
		*/
		bool		isControl() const
		{
			return getValue(TArticle::entryIsControl, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dieser Bestellung um einen Kontrollartikel handelt.
			\param cr		TRUE, wenn es sich um einen Kontrollartikel handelt.
			\brief Flag Kontrollartikel ?ern.
		*/
		void		setControl(bool cr)
		{
			setValue(TArticle::entryIsControl, cr);
		}
		/*!	\return Liefert TRUE, wenn es sich bei dieser Bestellung um einen Zeitsteuerungs-Artikel handelt.
			Bei der Zeitsteuerung wird der Artikel zum Ermitteln des Beginns einer Zeit bestellt. Beim Bezahlen
			werden dann, je nach konfiguration, die entsprechenden Nachfolge-Artikel je nach Zeitvolumen
			zum Tisch hinzugefgt.
			\brief Flag Zeitsteuerungs-Artikel ermitteln.
		*/
		bool		isTime() const
		{
			return getValue(TArticle::entryIsTime, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dieser Bestellung um einen Zeitsteuerungs-Artikel handelt.
			Bei der Zeitsteuerung wird der Artikel zum Ermitteln des Beginns einer Zeit bestellt. Beim Bezahlen
			werden dann, je nach konfiguration, die entsprechenden Nachfolge-Artikel je nach Zeitvolumen
			zum Tisch hinzugefgt.
			\param time		TRUE, wenn die Zeitsteuerung aktiv ist.
			\brief Flag Zeitsteuerungs-Artikel ?ern.
		*/
		void		setIsTime(bool time)
		{
			setValue(TArticle::entryIsTime, time);
		}
		bool		stopsTime() const
		{
			return getValue(TArticle::entryStopsTime, FALSE);
		}
		void		setStopsTime(bool time)
		{
			setValue(TArticle::entryStopsTime, time);
		}
		/*!	\return Liefert TRUE, wenn die Zeitsteuerung fr diese Bestellung bereits abgeschlossen wurde, der
			Tisch also bereits bezahlt und die Nachfolgeartikel hinzugefgt wurden.
			\brief Flag Zeitsteuerung abgeschlossen ermitteln.
		*/
		bool		isTimePaid() const
		{
			return getValue(entryTimePaid, FALSE);
		}
		/*!	Idert das Flag, ob die Zeitsteuerung fr diese Bestellung bereits abgeschlossen wurde, der
			Tisch also bereits bezahlt und die Nachfolgeartikel hinzugefgt wurden.
			\param val		TRUE, wenn die Zeitsteuerung abgeschlossen wurde.
			\brief Flag Zeitsteuerung abgeschlossen ?ern.
		*/
		void		setTimePaid(bool val)
		{
			if( !val )
				clrValue(entryTimePaid);
			else
				setValue(entryTimePaid, val);
		}
		bool		isTimeArt() const
		{
			return getValue(entryTimeArt, FALSE);
		}
		void		setTimeArt(bool val)
		{
			if( !val )
				clrValue(entryTimeArt);
			else
				setValue(entryTimeArt, val);
		}
		/*!	\return Liefert TRUE, wenn diese Bestellung durch einen Splitvorgang hinzugefgt wurde, also nicht
			per Bedienereingabe bestellt wurde.
			\brief Flag Splitartikel ermitteln.
		*/
		bool		isFromSplit() const
		{
			return getValue(entryFromSplit, FALSE);
		}
		/*!	Idert das Flag, ob diese Bestellung durch einen Splitvorgang hinzugefgt wurde, also nicht
			per Bedienereingabe bestellt wurde.
			\param spl		TRUE, wenn es sich um einen Splitvorgang handelt
			\brief Flag Splitartikel ?ern.
		*/
		void		setFromSplit(bool spl=TRUE)
		{
			setValue(entryFromSplit, spl);
		}
		void		setFoodBev(int fb)
		{
			if( 0==fb )
				clrValue(TArticle::entryFoodBev);
			else
				setValue(TArticle::entryFoodBev, fb);
		}
		int			getFoodBev() const
		{
			return getValue(TArticle::entryFoodBev, 0);
		}
	public: // ---- Preisberechnung ------------------------------------------------------------------------
		double		getDAmount()
		{
			return (double)getPrice() * getCount();
		}
		/*!	\return Liefert den Betrag dieser Bestellung. Im Gegensatz zu TTableVoid wird der Betrag in dieser
			Funktion berechnet, da sich die Daten im Nachhinein noch ?ern knnen, z.B. durch Preisfindung
			oder freie Preiseingabe.
			\brief Bestellbetrag ermitteln.
		*/
		long		getAmount()
		{
			double ret = (double)getPrice() * getCount();
			return lRound(ret);
		}
		/*!	\return Liefert den Index des Steuersatzes fr die Bezahlung auf Im Haus.
			\brief Index des Im-Haus-Steuersatzes ermitteln.
		*/
		int			getIndexIH() const
		{
			return getValue(entryIndexIH, 0);
		}
		/*!	Idert den Index des Steuersatzes fr die Bezahlung auf Im Haus.
			\brief Index des Im-Haus-Steuersatzes ?ern.
		*/
		void		setIndexIH(int id)
		{
			setValue(entryIndexIH, id);
		}
		/*!	\return Liefert den Steuersatz fr die Bezahlung auf Im Haus.
			\brief Im-Haus-Steuersatz ermitteln.
		*/
		int			getRateIH() const
		{
			return getValue(entryRateIH, 0);
		}
		/*!	Idert den Steuersatz fr die Bezahlung auf Im Haus.
			\brief Im-Haus-Steuersatz ?ern.
		*/
		void		setRateIH(int rate)
		{
			setValue(entryRateIH, rate);
		}
		QString		getNameIH() const
		{
			return getString(entryNameIH);
		}
		void		setNameIH(const QString& name)
		{
			setValue(entryNameIH, name);
		}
		/*!	\return Liefert den Nettobetrag der Bestellung fr eine Bezahlung auf Im Haus.
			\brief Nettobetrag Im-Haus ermitteln.
		*/
		long		getNetIH() const
		{
			return getValue(entryNetIH, 0);
		}
		/*!	\return Liefert den Steuerbetrag der Bestellung fr eine Bezahlung auf Im Haus.
			\brief Steuerbetrag Im-Haus ermitteln.
		*/
		long		getVatIH() const
		{
			return getValue(entryVatIH, 0);
		}
		/*!	\return Liefert den Index des Steuersatzes fr die Bezahlung auf Im Haus.
			\brief Index des Au?r-Haus-Steuersatzes ermitteln.
		*/
		int			getIndexOH() const
		{
			return getValue(entryIndexOH, 0);
		}
		/*!	Idert den Index des Steuersatzes fr die Bezahlung auf Im Haus.
			\brief Index des Au?r-Haus-Steuersatzes ?ern.
		*/
		void		setIndexOH(int id)
		{
			setValue(entryIndexOH, id);
		}
		/*!	\return Liefert den Steuersatz fr die Bezahlung auf Au?r Haus.
			\brief Au?r-Haus-Steuersatz ermitteln.
		*/
		int			getRateOH() const
		{
			return getValue(entryRateOH, 0);
		}
		/*!	Idert den Steuersatz fr die Bezahlung auf Au?r Haus.
			\brief Im-Au?r-Steuersatz ?ern.
		*/
		void		setRateOH(int rate)
		{
			setValue(entryRateOH, rate);
		}
		QString		getNameOH() const
		{
			return getString(entryNameOH);
		}
		void		setNameOH(const QString& name)
		{
			setValue(entryNameOH, name);
		}
		/*!	\return Liefert den Nettobetrag der Bestellung fr eine Bezahlung auf Au?r Haus.
			\brief Nettobetrag Au?r-Haus ermitteln.
		*/
		long		getNetOH() const
		{
			return getValue(entryNetOH, 0);
		}
		/*!	\return Liefert den Steuerbetrag der Bestellung fr eine Bezahlung auf Au?r Haus.
			\brief Steuerbetrag Au?r-Haus ermitteln.
		*/
		long		getVatOH() const
		{
			return getValue(entryVatOH, 0);
		}
		/*!	\return Liefert TRUE, wenn das Im/Au?r-Haus-Flag des Zahlvorgangs in dieser Bestellung berschrieben
			wurde.
			\brief Im/Au?r-Haus berschrieben?
		*/
		bool		hasVatOH() const
		{
			return hasValue(entryIsVatOH);
		}
		/*!	Lscht das Flag, da?das Im/Au?r-Haus-Flag in dieser Bestellung berschrieben wird. Die Bestellung
			wird in dem Fall wieder dem Steuersatz zugewiesen, der im Zahlvorgang angegeben wird.
			\brief ?erschreiben von Im/Au?r-Haus zurcknehmen.
		*/
		void		clrVatOH()
		{
			clrValue(entryIsVatOH);
		}
		/*!	\return Liefert TRUE, wenn die Bestellung fest auf Au?r-Haus-Bezahlung berschrieben wurde.
			\warning Dieser Wert ist nur gltig, wenn das Steuer-Flag berschrieben wurde. Vor Aufruf dieser
			Funktion ist also mittels hasVatOH() zu prfen, da?der Bestellvorgang den Wert wirklich
			berschreibt.
			\brief Bestellung fest auf Au?r-Haus?
		*/
		bool		isVatOH() const
		{
			return getValue(entryIsVatOH, FALSE);
		}
		/*!	?erschreibt das Mwst-Flag des Zahlvorgangs und legt eine Zahlmethode fr diese Bestellung fest.
			\param fl		TRUE, wenn die Bestellung fest auf Au?r-Haus l?t, FALSE wenn die Bestellung fest
							auf Im-Haus l?t.
			\brief Mwst-Flag berschreiben.
		*/
		void		doVatOH(bool fl)
		{
			setValue(entryIsVatOH, fl);
		}
		/*!	?ernimmt aus ih und oh die bentigten Mwst-Informationen fr In Haus und Au?r Haus.
			\param ih		die Mwst-Infos fr Bezahlung auf Im Haus
			\param oh		die Mwst-Infos fr Bezahlung auf Au?r Haus
			\brief Mwst-nformationen bernehmen.
		*/
		void		setVat(TVatRate* ih, TVatRate* oh)
		{
			if( ih )
			{
				setValue(entryIndexIH, ih->getID());
				setValue(entryRateIH, ih->getRate());
				setValue(entryNameIH, ih->getName());
			}
			if( oh )
			{
				setValue(entryIndexOH, oh->getID());
				setValue(entryRateOH, oh->getRate());
				setValue(entryNameOH, oh->getName());
			}
			calcVat();
		}
		void		setVatComposites(const QString& ih, const QString& oh)
		{
			if( !ih.isEmpty() )
				setValue(entryCompIH, ih);
			if( !oh.isEmpty() )
				setValue(entryCompOH, oh);
		}
		QString		getVatComposites(bool oh) const
		{
			if( oh )
				return getString(entryCompOH);
			return getString(entryCompIH);
		}
	public: // ---- Preisfindung ---------------------------------------------------------------------------
	public: // ---- Kundendatenbank ------------------------------------------------------------------------
		bool		isClientDB() const
		{
			return getValue(TArticle::entryIsClient, FALSE);
		}
		/*!	Kundendatenbank, Stck-Verbuchung. Der entsprechende Wert wird bei setAtricle(TArticle* art)
			bernommen
			\return Liefert die Stckzahl, die auf dem Stckkonto des Kunden verbucht werden soll.
			\brief Stckkonto-Verbuchung ermitteln.
		*/
		long		getClientPieces() const
		{
			return getValue(TArticle::entryClientPieces, 0L);
		}
		/*!	Kundendatenbank, Betrags-Verbuchung. Der entsprechende Wert wird bei setAtricle(TArticle* art)
			bernommen
			\return Liefert den Betrag, der auf dem Betragskonto des Kunden verbucht werden soll.
			\brief Betragskonto-Verbuchung ermitteln.
		*/
		long		getClientAmount() const
		{
			return getValue(TArticle::entryClientAmount, 0L);
		}
		/*!	Kundendatenbank, Zeit-Verbuchung. Der entsprechende Wert wird bei setAtricle(TArticle* art)
			bernommen
			\return Liefert die Tage, die zum Ablaufdatum des Kunden verbucht werden sollen.
			\brief Ablaufdatum-Verbuchung ermitteln.
		*/
		int			getClientDays()  const
		{
			return getValue(TArticle::entryClientDays, 0);
		}
		/*!	Kundendatenbank, Zeit-Verbuchung. Der entsprechende Wert wird bei setAtricle(TArticle* art)
			bernommen
			\return Liefert die Stunden, die zum Ablaufdatum des Kunden verbucht werden sollen.
			\brief Ablaufzeit-Verbuchung ermitteln.
		*/
		int			getClientHours() const
		{
			return getValue(TArticle::entryClientHours, 0);
		}
	public: // ---- Waage ----------------------------------------------------------------------------------
		bool		hasWeight() const
		{
			return hasValue(TArticle::entryWeight);
		}
		/*!	\return Liefert das Gewicht des bestellten Artikels inklusive Tara.
			\brief Gesamt-Gewicht ermitteln.
		*/
		long		getWeight() const
		{
			return getValue(TArticle::entryWeight, 0);
		}
		/*!	\return Liefert das Gewicht des bestellten Artikels exklusive Tara.
			\brief Netto-Gewicht ermitteln.
		*/
		long		getNetWeight() const
		{
			return getValue(entryNetWeight, 0);
		}
		/*!	\return Liefert das Tara beim Wiegevorgang der Bestellung.
			\brief Tara ermitteln.
		*/
		long		getTare() const
		{
			return getValue(entryTare, 0);
		}
		/*!	Idert das Gewicht des bestellten Artikels und den Tara beim Wiegevorgang.
			\param weight	das neue Gewicht
			\param tare		das neu Tara
			\brief Gewicht und Tara ?ern.
		*/
		void		setWeight(long weight, long tare)
		{
			setValue(TArticle::entryWeight, weight);
			setValue(entryTare, tare);
			setValue(entryNetWeight, weight-tare);
		}
// -----------------------------------
		void		setSubvention(int sub)
		{
			if( sub )
				setValue(entrySubvent, sub);
			else
				clrValue(entrySubvent);
		}
		int			getSubvention() const
		{
			return getValue(entrySubvent, 0);
		}
		/*
		void		clrModifiers()
		{
			clrValue(entryModifiers);
		}
		*/
		void		clrDiscInfo();
		void		addDiscInfo(long base, TDiscount* disc, TSubvention* sub);
		QStringList	getDiscInfo();
		/*
		QStringList	getModifiers() const
		{
			return QStringList::split(";", getValue(entryModifiers));
		}
		void		addModifier(TModifier* mod)
		{
			QStringList ent;
			ent << QString::number(mod->getID());
			ent << mod->getName();
			ent << QString::number(mod->getPrice());
//			setPrice(getPrice()+mod->getPrice());
			QString tmp = getValue(entryModifiers);
			setValue(entryModifiers, tmp+";"+ent.join(":"));
		}
		*/
		int			getPricelevel() const
		{
			return getValue(TSubvention::entryPricelevel, 0);
		}
		void		setPricelevel(int level)
		{
			setValue(TSubvention::entryPricelevel, level);
		}
		long		getDiscount() const
		{
			return getValue(entryDiscount, 0L);
		}
		void		setDiscount(long disc)
		{
			setValue(entryDiscount, disc);
		}
		bool		noDiscount() const
		{
			return getValue(TArticle::entryNoDiscount, FALSE);
		}
		void		setNoDiscount(bool flag)
		{
			setValue(TArticle::entryNoDiscount, flag);
		}
		void		clrModifiers();
		bool		hasTaxGroups()
		{
			return hasValue(entryTaxGroupsIH) || hasValue(entryTaxGroupsOH);
		}
		long		getTaxAmount(bool single=FALSE)
		{
			double ret = getValue(entryTaxAmount, 0.0);
			if( single )
				return lRound(ret);
			return lRound(ret*getCount());
		}
		QStringList	getTaxAmounts();
		void		addTaxGroupIH(TVatRate* vat);
//		void		addTaxGroupIH(int id, const QString& name, int rate, int type, bool disc);
		void		addTaxGroupOH(TVatRate* vat);
//		void		addTaxGroupOH(int id, const QString& name, int rate, int type, bool disc);
		QStringList	getTaxGroups(/*const QStringList& ign, */bool oh);
		void		calcTaxes(/*const QStringList& ign, */bool oh=FALSE);
	protected:
		/*!	Berechnet die Mwst-S?e dieser Bestellung. Wird bei jeder Iderung von Anzahl, Preis oder
			Mwst-Satz aufgerufen.
			\brief Mwst-S?e berechnen.
		*/
		void		calcVat();
	public:
		void		saveVat();
		void		restoreVat();
	};
}

#endif
