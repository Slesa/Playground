#ifndef				POSLIB_TARTICLE_H
#define				POSLIB_TARTICLE_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa? alle bentigten Informationen fr Artikel.
		Alle verfgbaren Artikel werden in einer Instanz von TArticles
		zur Verfgung gestellt.
		\brief POS-Klassen: Artikel.
	*/
	class			TArticle
	: public TNSValue
	{
	public:
		static const char	entryDisplay[];
		static const char	entryPrinter[];
		static const char	entryButton[];
		static const char	entryDescription[];
		static const char	entryPrice[];
		static const char	entryMinPrice[];
		static const char	entryMaxPrice[];
		static const char	entryOrderApp[];
		static const char	entryFamily[];
		static const char	entryPrio[];
		static const char	entryIH[];
		static const char	entryOH[];
		static const char	entryTaxIH[];
		static const char	entryTaxOH[];
		static const char	entryPrinters[];
		static const char	entryVPrinters[];
		static const char	entrySPrinters[];
		static const char	entryTares[];
		static const char	entryWeight[];
		static const char	entryIsTime[];
		static const char	entryStopsTime[];
		static const char	entryIsHint[];
		static const char	entryVending[];
		static const char	entryIsNoHelp[];
		static const char	entryNoDiscount[];
		static const char	entryNoSmartVoid[];
		static const char	entryIsFreePrice[];
		static const char	entryIsFreeText[];
		static const char	entryIsWeight[];
		static const char	entryIsBalance[];
		static const char	entryIsCredit[];
		static const char	entryIsInactive[];
		static const char	entryIsControl[];
		static const char	entryIsCutGood[];
		static const char	entryIsAreaGood[];
		static const char	entryIsBulkGood[];
		static const char	entryBitmap[];
		static const char	entryNoPrint[];
		static const char	entryTime1st[];
		static const char	entryTimeSlice[];
		static const char	entryTimeMaxSlice[];
		static const char	entryTimeFollow[];
		static const char	entryTimeMaxArt[];
		static const char	entryTimeTolerance[];
		static const char	entryIsClient[];
		static const char	entryClientPieces[];
		static const char	entryClientAmount[];
		static const char	entryClientDays[];
		static const char	entryClientHours[];
		static const char	entryFoodBev[];
		static const char	entryAccount[];
		static const char	entryStockcounterActive[];
	public:
		/*!	Erzeuge eine leere Instanz eines Artikels.
			\brief ctor.
		*/
		TArticle()
		: TNSValue()
		{
		}
		/*!	Erzeuge eine Instanz eines Artikels als Kopie von art.
			\param art		zu kopierender Artikel.
		TArticle(const TArticle& art)
		: TNSValue(art)
		{
		}
		*/
		/*!	Zerstre die Instanz von TArticle.
			\brief dtor.
		*/
		~TArticle()
		{
		}
		/*!	\return Die Priorit?fr den Touch innerhalb einer Artikelgruppe. Artikel mit
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
		/*!	\return den Ausgabetext fr das Kundendisplay. Wenn der Text selbst leer ist,
			wir der Name des Artikels zurckgegeben.
			\brief Kundendisplay-Text abfragen.
			\sa setDisplay
		*/
		QString		getDisplay() const
		{
			QString ret = getValue(entryDisplay);
			if( ret.isEmpty() )
				return getName();
			return ret;
		}
		/*!	Idert den Kundendisplay-Text des Artikels. Wenn txt leer ist, wird bei einem Aufruf von
			getDisplay() der Name des Artikels zurckgegeben.
			\param txt		der neue Kundendisplay-Text des Artikels.
			\brief Kundendisplay-Text ?ern.
		*/
		void		setDisplay(const QString& txt)
		{
			if( txt.isEmpty() || txt==getName() )
				clrValue(entryDisplay);
			else
				setValue(entryDisplay, txt);
		}
		/*!	\return den Ausgabetext fr den Drucker. Wenn der Text selbst leer ist,
			wir der Name des Artikels zurckgegeben.
			\brief Drucker-Text abfragen.
			\sa setPrinter
		*/
		QString		getPrinter() const
		{
			QString ret = getValue(entryPrinter);
			if( ret.isEmpty() )
				return getName();
			return ret;
		}
		/*!	Idert den Drucker-Text des Artikels. Wenn txt leer ist, wird bei einem Aufruf von
			getPrinter() der Name des Artikels zurckgegeben.
			\param txt		der neue Druckertext des Artikels.
			\brief Button-Text ?ern.
		*/
		void		setPrinter(const QString& txt)
		{
			if( txt.isEmpty() || txt==getName() )
				clrValue(entryPrinter);
			else
				setValue(entryPrinter, txt);
		}
		/*!	\return den Ausgabetext fr einen Touch-Button. Wenn der Text selbst leer ist,
			wir der Name des Artikels zurckgegeben.
			\brief Button-Text abfragen.
			\sa setButton
		*/
		QString		getButton() const
		{
			QString ret = getValue(entryButton);
			if( ret.isEmpty() )
				return getName();
			return ret;
		}
		/*!	Idert den Button-Text des Artikels. Wenn txt leer ist, wird bei einem Aufruf von
			getButton() der Name des Artikels zurckgegeben.
			\param txt		der neue Buttontext des Artikels.
			\brief Button-Text ?ern.
		*/
		void		setButton(const QString& txt)
		{
			if( txt.isEmpty() || txt==getName() )
				clrValue(entryButton);
			else
				setValue(entryButton, txt);
		}
		/*!	\return eine zus?liche Beschreibung zu dem Artikel.
			\brief Beschreibung abfragen.
			\sa setDescription
		*/
		QString		getDescription() const
		{
			return getString(entryDescription);
		}
		/*!	Idert die Beschreibung des Artikels.
			\param descr	Die neue Beschreibung.
			\brief Beschreibung des Artikels ?ern.
			\sa getDescription
		*/
		void		setDescription(const QString& descr)
		{
			if( descr.isEmpty() )
				clrValue(entryDescription);
			else
				setValue(entryDescription, descr);
		}
		/*!	\return der Preis des Artikels.
			\brief Preis abfragen.
			\sa setPrice
		*/
		long		getPrice() const
		{
			return getValue(entryPrice, 0L);
		}
		/*!	Idert den Preis des Artikels.
			\param price	Der neue Preis.
			\brief Preis des Artikels ?ern.
			\sa getPrice
		*/
		void		setPrice(long price)
		{
			if( !price )
				clrValue(entryPrice);
			else
				setValue(entryPrice, price);
		}
		QString		getMinPrice() const
		{
			return getString(entryMinPrice);
		}
		void		setMinPrice(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryMinPrice);
			else
				setValue(entryMinPrice, str);
		}
		QString		getMaxPrice() const
		{
			return getString(entryMaxPrice);
		}
		void		setMaxPrice(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryMaxPrice);
			else
				setValue(entryMaxPrice, str);
		}
		/*!	\return das Portionsgewicht des Artikels.
			\brief Portionsgewicht abfragen.
			\sa setWeight
		*/
		long		getWeight() const
		{
			long l = getValue(entryWeight, 1L);
			if( !l )
				l = 1L;
			return l;
		}
		/*!	Idert das Portiinsgewicht des Artikels.
			\param weight	Das neue Portionsgewicht.
			\brief Portionsgewicht des Artikels ?ern.
			\sa getWeight
		*/
		void		setWeight(long weight)
		{
			if( weight==1L )
				clrValue(entryWeight);
			else
				setValue(entryWeight, weight);
		}
		/*!	\return ein Programm, das beim Bestellen dieses Artikels aufgerufen wird.
			\brief Bestell-Programm abfragen.
			\sa setOrderApp
		*/
		QString		getOrderApp() const
		{
			return getString(entryOrderApp);
		}
		/*!	Idert das Programm, das beim Bestellen dieses Artikels aufgerufen wird.
			\param prog		Aufzurufendes Programm. Ist der String leer, wird das Attribut
							gelscht.
			\brief Bestell-Programm des Artikels ?ern.
			\sa getOrderApp
		*/
		void		setOrderApp(const QString& prog)
		{
			if( prog.isEmpty() )
				clrValue(entryOrderApp);
			else
				setValue(entryOrderApp, prog);
		}
		/*!	\return das Tara-Gewicht des Artikels. Dieser Wert ist nur von Bedeutung,
			wenn es sich um einen Waage-Artikel handelt.
			\brief Tara-Gewicht abfragen.
			\sa setTares, isWeight, setIsWeight
		*/
		QStringList	getTares() const
		{
			return QStringList::split(":", getString(entryTares));
		}
		/*!	Idert das Tara-Gewicht des Artikels. Dieser Wert ist nur von Bedeutung,
			wenn es sich um einen Waage-Artikel handelt.
			\param tare		Das neue Tara-Gewicht.
							Ist der Wert 0, wird das Attribut gelscht.
			\brief Tara-Gewicht des Artikels ?ern.
			\sa getTare, isWeight, setIsWeight
		*/
		void		setTares(const QStringList& tares)
		{
			if( !tares.count() )
				clrValue(entryTares);
			else
				setValue(entryTares, tares.join(":"));
		}
		/*!	\return die Warengruppe zu der dieser Artikel gehrt. Der zurckgegebene Wert ist ein Index auf die
			Warengruppenliste.
			\brief Warengruppe abfragen.
			\sa setFamily, TFamily, TFamilyList
		*/
		int			getFamily() const
		{
			return getValue(entryFamily, 0);
		}
		/*!	Idert die Warengruppe des Artikels.
			\param fam		Die neue Warengruppe als Index auf die Warengruppenliste.
							Ist der Wert 0, wird das Attribut gelscht.
			\brief Warengruppe des Artikels ?ern.
			\sa getFamily, TFamily, TFamilyList
		*/
		void		setFamily(int fam)
		{
			if( !fam )
				clrValue(entryFamily);
			else
				setValue(entryFamily, fam);
		}
		int			getIH() const
		{
			return getValue(entryIH, 0);
		}
		void		setIH(int ih)
		{
			if( !ih )
				clrValue(entryIH);
			else
				setValue(entryIH, ih);
		}
		int			getOH() const
		{
			return getValue(entryOH, 0);
		}
		void		setOH(int oh)
		{
			if( !oh )
				clrValue(entryOH);
			else
				setValue(entryOH, oh);
		}
		int			getTaxIH() const
		{
			return getValue(entryTaxIH, 0);
		}
		void		setTaxIH(int ih)
		{
			if( !ih )
				clrValue(entryTaxIH);
			else
				setValue(entryTaxIH, ih);
		}
		int			getTaxOH() const
		{
			return getValue(entryTaxOH, 0);
		}
		void		setTaxOH(int oh)
		{
			if( !oh )
				clrValue(entryTaxOH);
			else
				setValue(entryTaxOH, oh);
		}
		/*!	\return Liefert als Stringliste die Kombinationen von Drucker und Layout
			als Index in die entsprechenden Tabellen fr den Bondruck.
			Falls Der String leer ist, werden die Voreinstellungen von TFamily genommen.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert berschreibt den Warengruppen-Drucker.
			\code
			QStringList list = QStringList::split(";", art->getPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout des Artikels fr Bons ermitteln.
			\sa setPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPrinters() const
		{
			return getString(entryPrinters);
		}
		/*!	Idert die Drucker/Layout-Kombinationen, mit denen dieser Artikel beim
			Bestellen ausgedruckt werden soll. Siehe getPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelscht.
			\note Dieser Wert berschreibt den Warengruppen-Drucker.
			\brief Ausgabedrucker und -layout des Artikels fr Bons ?ern.
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
			als Index in die entsprechenden Tabellen fr den Stornodruck.
			Falls Der String leer ist, werden die Voreinstellungen von TFamily genommen.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert berschreibt den Warengruppen-Drucker.
			\code
			QStringList list = QStringList::split(";", art->getVPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout des Artikels fr Stornos ermitteln.
			\sa setVPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getVPrinters() const
		{
			return getString(entryVPrinters);
		}
		/*!	Idert die Drucker/Layout-Kombinationen, mit denen dieser Artikel beim
			Stornieren ausgedruckt werden soll. Siehe getVPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelscht.
			\note Dieser Wert berschreibt den Warengruppen-Drucker.
			\brief Ausgabedrucker und -layout des Artikels fr Storno-Bons ?ern.
			\sa getVPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		void		setVPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entryVPrinters);
			else
				setValue(entryVPrinters, prns);
		}
		QString		getSPrinters() const
		{
			return getString(entrySPrinters);
		}
		void		setSPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entrySPrinters);
			else
				setValue(entrySPrinters, prns);
		}
		/*!	\return das Flag, ob der Stockcounter des Artikels aktiv ist oder nicht
			\brief Stockcounter Aktiv?
			\sa setStockcounterActiv
		*/
		bool		stockcounterActive() const
		{
			return getValue(entryStockcounterActive, FALSE);
		}
		/*!	Ändert das Flag, ob der Stockcounter des Artikels aktiv ist oder nicht
			\param flag		true: Stockcounter aktiv
			\brief Stockcounter Flag Ändern.
			\sa stockcounterActiv
		*/
		void		setStockcounterActive(bool flag)
		{
			if( !flag )
				clrValue(entryStockcounterActive);
			else
				setValue(entryStockcounterActive,flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um einen Auslagenartikel handelt.
			\brief Auslagenartikel?
			\sa setIsCredit
		*/
		bool		isCredit() const
		{
			return getValue(entryIsCredit, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um einen Auslagenartikel handelt.
			\param flag		true: es handelt sich um eine Auslage.
			\brief Auslagenartikel-Flag ?ern.
			\sa isCredit
		*/
		void		setIsCredit(bool flag)
		{
			if( !flag )
				clrValue(entryIsCredit);
			else
				setValue(entryIsCredit, flag);
		}
		/*!	\return das Flag, ob der Artikel inaktiv ist.
			\brief Artikel inaktiv?
			\sa setInactive
		*/
		bool		isInactive() const
		{
			return getValue(entryIsInactive, FALSE);
		}
		/*!	Idert das Flag, ob der Artikel inaktiv ist.
			\param flag		true: Artikel inaktiv.
			\brief Artikel inaktiv, Flag ?ern.
			\sa isInactive
		*/
		void		setInactive(bool flag)
		{
			if( !flag )
				clrValue(entryIsInactive);
			else
				setValue(entryIsInactive, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um einen Kontrollartikel handelt.
			\brief Kontrollartikel?
			\sa setIsControl
		*/
		bool		isControl() const
		{
			return getValue(entryIsControl, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um einen Kontrollartikel handelt.
			\param flag		true: es handelt sich um einen Kontrollartikel.
			\brief Kontrollartikel-Flag ?ern.
			\sa isControl
		*/
		void		setIsControl(bool flag)
		{
			if( !flag )
				clrValue(entryIsControl);
			else
				setValue(entryIsControl, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um einen Zeitartikel handelt.
			\brief Zeitartikel?
			\sa setIsTime
		*/
		bool		isTime() const
		{
			return getValue(entryIsTime, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um einen Zeitartikel handelt.
			\param flag		true: es handelt sich um einen Zeitartikel.
			\brief Zeitartikel-Flag ?ern.
			\sa isTime
		*/
		void		setIsTime(bool flag)
		{
			if( !flag )
				clrValue(entryIsTime);
			else
				setValue(entryIsTime, flag);
		}
		bool		stopsTime() const
		{
			return getValue(entryStopsTime, FALSE);
		}
		void		setStopsTime(bool flag)
		{
			if( !flag )
				clrValue(entryStopsTime);
			else
				setValue(entryStopsTime, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um einen Hinweisartikel handelt.
			\brief Hinweisartikel?
			\sa setIsHint
		*/
		bool		isHint() const
		{
			return getValue(entryIsHint, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um einen Hinweisartikel handelt.
			\param flag		TRUE: es handelt sich um einen Hinweis.
			\brief Hinweisartikel-Flag ?ern.
			\sa isHint
		*/
		void		setIsHint(bool flag)
		{
			if( !flag )
				clrValue(entryIsHint);
			else
				setValue(entryIsHint, flag);
		}
		bool		isVending() const
		{
			return getValue(entryVending, FALSE);
		}
		void		setIsVending(bool flag)
		{
			if( !flag )
				clrValue(entryVending);
			else
				setValue(entryVending, flag);
		}
		bool		isNoHelp() const
		{
			return getValue(entryIsNoHelp, FALSE);
		}
		void		setNoHelp(bool flag)
		{
			if( !flag )
				clrValue(entryIsNoHelp);
			else
				setValue(entryIsNoHelp, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um Meterware handelt.
			\brief Meterware?
			\sa setIsCutGood
		*/
		bool		isCutGood() const
		{
			return getValue(entryIsCutGood, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um Meterware handelt.
			\param flag		TRUE: es handelt sich um Meterware.
			\brief Meterwaren-Flag ?ern.
			\sa isCutGood
		*/
		void		setIsCutGood(bool flag)
		{
			if( !flag )
				clrValue(entryIsCutGood);
			else
				setValue(entryIsCutGood, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um Fl?enware handelt.
			\brief Fl?enware?
			\sa setIsAreaGood
		*/
		bool		isAreaGood() const
		{
			return getValue(entryIsAreaGood, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um Fl?enware handelt.
			\param flag		TRUE: es handelt sich um Fl?enware.
			\brief Fl?enwaren-Flag ?ern.
			\sa isAreaGood
		*/
		void		setIsAreaGood(bool flag)
		{
			if( !flag )
				clrValue(entryIsAreaGood);
			else
				setValue(entryIsAreaGood, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um Massenenware handelt und damit immer die Anzahl nachgefragt
			wird.
			\brief Massenware?
			\sa setIsBulkGood
		*/
		bool		isBulkGood() const
		{
			return getValue(entryIsBulkGood, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um Massenware handelt und damit immer die Anzahl nachgefragt
			wird.
			\param flag		TRUE: es handelt sich um Massenware.
			\brief Massenwaren-Flag ?ern.
			\sa isBulkGood
		*/
		void		setIsBulkGood(bool flag)
		{
			if( !flag )
				clrValue(entryIsBulkGood);
			else
				setValue(entryIsBulkGood, flag);
		}
		bool		noDiscount() const
		{
			return getValue(entryNoDiscount, FALSE);
		}
		void		setNoDiscount(bool flag)
		{
			if( !flag )
				clrValue(entryNoDiscount);
			else
				setValue(entryNoDiscount, flag);
		}
		bool		noSmartVoid() const
		{
			return getValue(entryNoSmartVoid, FALSE);
		}
		void		setNoSmartVoid(bool flag)
		{
			if( !flag )
				clrValue(entryNoSmartVoid);
			else
				setValue(entryNoSmartVoid, flag);
		}
		/*!	\return das Flag, ob der Artikel bei einer Bestellung ausgedruckt werden soll oder nicht.
			\brief Artikel drucken?
			\sa setNoPrint
		*/
		bool		isNoPrint() const
		{
			int x= getValue(entryNoPrint, 0);
			return x&1?TRUE:FALSE;
		}
		/*!	Idert das Flag, ob der Artikel bei einer Bestellung ausgedruckt werden soll.
			\param no		TRUE: Artikel drucken.
			\brief Flag ?ern, ob Artikel bei Bestellung zu drucken ist.
			\sa isNoPrint
		*/
		void		setNoPrint(bool no)
		{
			int flag = getValue(entryNoPrint, 0);
			if( no )
				flag |= 1;
			else
				flag &= ~1;
			setValue(entryNoPrint, flag);
		}
		/*!	\return das Flag, ob der Artikel bei einem Storno ausgedruckt werden soll oder nicht.
			\param no		Wenn TRUE, wird der Artikel beim Storno nicht gedruckt.
			\brief Artikel drucken?
			\sa setNoVPrint
		*/
		void		setNoVPrint(bool no)
		{
			int flag = getValue(entryNoPrint, 0);
			if( no )
				flag |= 2;
			else
				flag &= ~2;
			setValue(entryNoPrint, flag);
		}
		/*!	Idert das Flag, ob der Artikel bei einem Storno ausgedruckt werden soll.
			\brief Flag ?ern, ob Artikel bei Storno zu drucken ist.
			\sa isNoVPrint
		*/
		bool		isNoVPrint() const
		{
			int x = getValue(entryNoPrint, 0);
			return x&2?TRUE:FALSE;
		}
		/*!	\return das Flag, ob bei dem Artikel eine freie Preiseingabe erlaubt ist.
			\brief Freie Preiseingabe?
			\sa setIsFreePrice
		*/
		bool		isFreePrice() const
		{
			return getValue(entryIsFreePrice, FALSE);
		}
		/*!	Idert das Flag, ob bei dem Artikel eine freie Preiseingabe erlaubt ist.
			\param flag		true: freie Preiseingabe erlaubt.
			\brief Flag fr freie Preiseingabe ?ern.
			\sa isFreePrice
		*/
		void		setIsFreePrice(bool flag)
		{
			if( !flag )
				clrValue(entryIsFreePrice);
			else
				setValue(entryIsFreePrice, flag);
		}
		/*!	\return das Flag, ob bei dem Artikel eine freie Texteingabe erlaubt ist.
			\brief Freie Texteingabe?
			\sa setIsFreeText
		*/
		bool		isFreeText() const
		{
			return getValue(entryIsFreeText, FALSE);
		}
		/*!	Idert das Flag, ob bei dem Artikel eine freie Texteingabe erlaubt ist.
			\param flag		true: freie Texteingabe erlaubt.
			\brief Flag fr freie Texteingabe ?ern.
			\sa isFreeText
		*/
		void		setIsFreeText(bool flag)
		{
			if( !flag )
				clrValue(entryIsFreeText);
			else
				setValue(entryIsFreeText, flag);
		}
		/*!	\return das Flag, ob es sich bei dem Artikel um einen Waageartikel handelt.
			\brief Waageartikel?
			\sa setIsWeight
		*/
		bool		isWeight() const
		{
			return getValue(entryIsWeight, FALSE);
		}
		/*!	Idert das Flag, ob es sich bei dem Artikel um eine Waageartikel handelt.
			\param flag		TRUE: Waageartikel.
			\brief Waageartikel-Flag ?ern .
			\sa isWeight
		*/
		void		setIsWeight(bool flag)
		{
			if( !flag )
				clrValue(entryIsWeight);
			else
				setValue(entryIsWeight, flag);
		}
		/*!	\return das Flag, ob das Gewicht des Artikels per Waage abgefragt werden
			soll. Falls das Flag FALSE ist, wird ist Gewicht manuell einzugeben.
			\brief Gewicht ber Waage abfragen?
			\sa setIsBalance
		*/
		bool		isBalance() const
		{
			return getValue(entryIsBalance, FALSE);
		}
		/*!	Idert das Flag, ob das Gewicht des Artikel per Waage abgefragt werden
			soll.
			\param flag		TRUE:	Gewicht per Waage ermitteln.
							FALSE:	Gewicht manuell eingeben.
			\brief Waagen-Flag ?ern .
			\sa isBalance
		*/
		void		setIsBalance(bool flag)
		{
			if( !flag )
				clrValue(entryIsBalance);
			else
				setValue(entryIsBalance, flag);
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
		int			getTime1st() const
		{
			return getValue(entryTime1st, 30);
		}
		void		setTime1st(int sl)
		{
			if( sl==30 )
				clrValue(entryTime1st);
			else
				setValue(entryTime1st, sl);
		}
		int			getTimeSlice() const
		{
			return getValue(entryTimeSlice, 30);
		}
		void		setTimeSlice(int sl)
		{
			if( sl==30 )
				clrValue(entryTimeSlice);
			else
				setValue(entryTimeSlice, sl);
		}
		int			getTimeMaxSlice() const
		{
			return getValue(entryTimeMaxSlice, 0);
		}
		void		setTimeMaxSlice(int sl)
		{
			if( !sl )
				clrValue(entryTimeMaxSlice);
			else
				setValue(entryTimeMaxSlice, sl);
		}
		int			getTimeFollow() const
		{
			return getValue(entryTimeFollow, 0);
		}
		void		setTimeFollow(int art)
		{
			if( !art )
				clrValue(entryTimeFollow);
			else
				setValue(entryTimeFollow, art);
		}
		int			getTimeMaxArt() const
		{
			return getValue(entryTimeMaxArt, 0);
		}
		void		setTimeMaxArt(int art)
		{
			if( !art )
				clrValue(entryTimeMaxArt);
			else
				setValue(entryTimeMaxArt, art);
		}
		int			getTimeTolerance() const
		{
			return getValue(entryTimeTolerance, 0);
		}
		void		setTimeTolerance(int tol)
		{
			if( !tol )
				clrValue(entryTimeTolerance);
			else
				setValue(entryTimeTolerance, tol);
		}
		/*!	\return das Flag, ob dieser Artikel die Kundendatenbank beeinflu?.
			\brief Kunden-DB?
			\sa setIsClientDB
		*/
		bool		isClientDB() const
		{
			return getValue(entryIsClient, FALSE);
		}
		/*!	Idert das Flag, ob dieser Artikel die Kundendatenbank beeinflu?.
			\param flag		true: ?ert ein Kundenkonto.
			\brief Kunden-DB-Flag ?ern.
			\sa isClientDB
		*/
		void		setIsClientDB(bool flag)
		{
			if( !flag )
				clrValue(entryIsClient);
			else
				setValue(entryIsClient, flag);
		}
		/*!	\return Die Anzahl, die auf dem Stckkonto des Kunden verbucht werden soll.
			\brief KundenDB, Stckkonto verbuchen.
			\sa setClientPieces
		*/
		long		getClientPieces() const
		{
			return getValue(entryClientPieces, 0L);
		}
		/*!	Idert die Anzahl, die auf dem Stckkonto des Kunden verbucht werden soll.
			\param st		Die zu verbuchende Stckzahl.
			\brief KundenDB, Stckkonto-Verbuchung des Artikels ?ern.
			\sa getClientPieces
		*/
		void		setClientPieces(long st)
		{
			if( !st )
				clrValue(entryClientPieces);
			else
				setValue(entryClientPieces, st);
		}
		/*!	\return Der Betrag, der auf dem Betragskonto des Kunden verbucht werden soll.
			\brief KundenDB, Betragskonto verbuchen.
			\sa setClientAmount
		*/
		long		getClientAmount() const
		{
			return getValue(entryClientAmount, 0L);
		}
		/*!	Idert den Betrag, der auf dem betragskonto des Kunden verbucht werden soll.
			\param am		Der zu verbuchende Betrag.
			\brief KundenDB, Betragskonto-Verbuchung des Artikels ?ern.
			\sa getClientAmount
		*/
		void		setClientAmount(long am)
		{
			if( !am )
				clrValue(entryClientAmount);
			else
				setValue(entryClientAmount, am);
		}
		/*!	\return Die Tage, die auf dem Gltigkeitskonto des Kunden verbucht werden soll.
			\brief KundenDB, Datumkonto verbuchen.
			\sa setClientDays
		*/
		int			getClientDays() const
		{
			return getValue(entryClientDays, 0);
		}
		/*!	Idert die Tage, die auf dem Gltigkeitskonto des Kunden verbucht werden soll.
			\param days		Die zu verbuchenden Tage.
			\brief KundenDB, Datum-Verbuchung des Artikels ?ern.
			\sa getClientDays
		*/
		void		setClientDays(int days)
		{
			if( !days )
				clrValue(entryClientDays);
			else
				setValue(entryClientDays, days);
		}
		/*!	\return Die Stunden, die auf dem Gltigkeitskonto des Kunden verbucht werden soll.
			\brief KundenDB, Tageskonto verbuchen.
			\sa setClientHours
		*/
		int			getClientHours() const
		{
			return getValue(entryClientHours, 0);
		}
		/*!	Idert die Stunden, die auf dem Gltigkeitskonto des Kunden verbucht werden soll.
			\param hrs		Die zu verbuchenden Stunden.
			\brief KundenDB, Stunden-Verbuchung des Artikels ?ern.
			\sa getClientHours
		*/
		void		setClientHours(int hrs)
		{
			if( !hrs )
				clrValue(entryClientHours);
			else
				setValue(entryClientHours, hrs);
		}
		int			getFoodBev() const
		{
			return getValue(entryFoodBev, 0);
		}
		void		setFoodBev(int fb)
		{
			if( !fb )
				clrValue(entryFoodBev);
			else
				setValue(entryFoodBev, fb);
		}
		QString			getAccount() const
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
	};

	/*!	\ingroup PosLib
		Diese Klasse fa? mehrere TArticle-Elemente zu einer Liste zusammen. Die fr die
		XML-Funktionen ntigen TValueList-Funktionen wurden berschrieben.
		\brief POS-Klassen: Liste von Artikeln.
	*/
	class			TArticles
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste (articles)
		static const char	elementName[];				//!< Name eines Elements der Liste (article)
	public:
		/*!	Erzeugt eine neue Instanz einer Artikel-Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste gelscht.
			\brief ctor.
		*/
		TArticles(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zerstrt die Instanz der Artikelliste.
			\brief dtor.
		*/
		~TArticles()
		{
		}
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
		/*!	\return Liefert den Artikel mit ID index oder NULL, falls kein Element mit
			dieser ID existiert.
			\param index		ID des zu suchenden Artikels.
			\brief Artikel suchen.
		*/
		TArticle*	operator [] (int index)
		{
			return (TArticle*) TValueList::operator [](index);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse definiert einen Iterator ber eine TArtikel-Liste.
		\brief TArtikels-Iterator.
	*/
	class			TArticleIt
	: public TValueListIt
	{
	public:
		/*!	Erzeugt eine Instanz eines TArticles-Iterators.
			\param list		Liste mit Artikeln, ber die iteriert werden soll.
			\brief ctor.
		*/
		TArticleIt(TArticles& list)
		: TValueListIt(list)
		{
		}
		TArticle*	operator () ()
		{
			return (TArticle*) TValueListIt::operator()();
		}
		TArticle*	toFirst()
		{
			return (TArticle*) TValueListIt::toFirst();
		}
		TArticle*	current()
		{
			return (TArticle*) TValueListIt::current();
		}
		TArticle*	operator ++ ()
		{
			return (TArticle*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


