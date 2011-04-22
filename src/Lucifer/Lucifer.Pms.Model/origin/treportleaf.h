#ifndef				_POSLIB_TREPORTLEAF_

#define				_POSLIB_TREPORTLEAF_
#include			"poslib/ttableaction.h"
#include			"poslib/ttable.h"

namespace			PosLib
{
#if 0
	/*!	Definition eines Reports, [report].ini
		\code
		; Die Root-Definition bildet die Wurzel des Report-Baums
		[Root]
			; Die Root-Liste umfa? wie bisher Kellner und Kostenstellen
			lists = waiters,centers
		; Definition des Teilbaums Kellner
		[waiters]
			; Kellner soll mal die Liste der Stornos
			lists = voids
		\endcode
	*/

	/*!	Ein Blatt innerhalb des Report-Baumes umfa? aus den Tischeintr?n die gewnschten Attribute (wie name, preis) sowie
		die Summen Anzahl, Gesamtzahl und Gesamtbetrag der gew?ten Teilinformation.
		Beispiel: Das Report-Blatt "Artikel" knnte je nach Report umfassen "PLU", "Artikelname", "Preis", "Warengruppe",
		"Warengruppenname". In diesem Fall w? fr den zust?igen Knoten der Key "PLU", bei Knotentyp "Order" w?n die
		Summen die Anzahl der Orders fr diesen Artikel (count), Die Gesamtzahl g? an, wie oft der Artikel bestellt worden
		ist (sum), der Gesamtbetrag w? der Gesamtpreis der bestellten Artikel mit dieser PLU (amount). Bei Knotentyp "Void"
		entsprechen die Summen den jeweiligen Stornos.

		Ein Report-Blatt kann entweder zur weiteren Gruppierung dienen oder bereits die gewnschten Endinformationen enthalten.
		Ein Report-Blatt dient dann zur Gruppierung, wenn seine Unterlisten nicht leer sind. In dem Fall werden die
		Tischeintrags-Informationen genau dann den jeweiligen Unterknoten hinzugefgt, wenn diese Inormationen auch dem Blatt
		hinzugefgt werden.
		Beispiel: das Blatt enth? die Informationen fr "Kellner 3". Dann werden auch nur die Tischeintr? den Unterkategorien
		weitergereicht, die Kellner 3 betreffen. Ausnahme: Das Weiterreichen ist unabh?ig vom Typ des Eintrags. Wenn das
		Blatt respektive sein zugehriger Knoten (der Knoten der Kellner) den Typ "Orders" hat, d.h. dieser Knoten alle
		Bestellkellner umfa?, werden auch alle Storno-Eintr? an die jeweiligen Unterkategorien weitergereicht.
		\brief Reporting, Bl?er
		Wenn das Blatt keine weiteren Unterkategorien hat, werden nur die Informationen aus dem jeweiligen Tischeintrag
		bernommen, sofern der Tischeintrags-Typ dem Typ des zugehrigen Knotens entspricht.
		Beispiel: das Blatt ist Teil des Teilbaums Kellner, der den Typ "Orders" hat. In dem Fall bernimmt das Blatt die
		Informationen der jeweiligen Bestellung, nicht jedoch der Stornos.
		\brief Reporte, Report-Bl?er
	*/
	class			TRepLeaf
	: public TValue
	, public QDict<TRepNode>
	{
	public:
		TRepLeaf(TInifile& ini, const QString& name);
		TRepLeaf(int level, const QStringList& sublists);
	protected:
		int			m_Level;							//!< Die Tiefe des Blattes im Teilbaun
		int			m_Count[3];							//!< Vorl?ig mal 3 Anzhlfelder (count, sum, discount)
		long		m_Sum[3];							//!< Vorl?ig mal 3 Summenfelder (amount, discount, x)
		QStringList	m_SubLists;							//!< Die Unterkategorien des Blattes
	};

	/*!	Ein Knoten innerhalb des Report-Baumes umfa? zwei Funktionen: zum einen ist ein Knoten immer auch ein Blatt, das die
		Summen seiner Unterbl?er enth?. Zum Beispiel enth? ein Knoten "Kellner" vom Typ "Order" alle Kellnerbl?er mit
		ihren jeweiligen Summen, aber auch die Gesamtsumme aus allen Bl?ern, in diesem Fall die Summe aller Bestellungen.
		Zum zweiten ist ein Knoten eine Liste seiner Bl?er. Diese Bl?er knnen, je nach Definition des Blatt-Schlssels,
		unterschiedlich zusammengefa? werden. Z.B. knnen Artikelknoten mit dem Typ Order und dem Schlssel plu zusammen-
		gefa? werden, man erh? einen Teilbaum aller bestellten Artikel, Oder man definiert den Knotentyp als Pay und den
		Schlssel als payform, und man erh? die Summen aller Abrechnungsarten.
		Bei alldem entscheidend ist, da?ein Knoten jeweils seine Bl?er generiert, wobei jedes Blatt Untergruppierungen
		enthalten kann, n?ich eine Liste von Knoten.
		\brief Report, Report-Knoten
	*/
	class		TRepNode
	: public TRepLeaf
	, public QDict<TRepLeaf>
	{
	public:
		TRepNode(TInifile& ini, const QString& name);
		TRepNode(int level, const QStringList& sublists, int type, const QString& key, const QStringList& values);
	protected:
		int			m_Type;								//!< Der Eintragstyp des Knotens (Order, Void oder Pay im Wesentlichen)
		QString		m_Key;								//!< Der Schlssel fr die Bl?er
		QStringList	m_Values;							//!< Die zu ermittelnden Werte der Bl?er
	};

#endif

	/*!	Diese Klasse bildet die Basis fr alle Bl?er eines Reportes.
		Bl?er akkumulieren jeweils die Anzahl und den Betrag ihrer zugehrigen
		Daten.
		\brief POS-Klassen: Report-Bl?er.
	*/
	class			TReportLeaf
	: public TValue
	{
	public:
		static const char	entryPartCount[];
		static const char	entryCount[];
		static const char	entryAmount[];
	public:
		/*!	Erzeuge eine Instanz eines Reportblattes.
			\brief ctor
		*/
		TReportLeaf()
		: TValue()
		, m_Count(0.0)
		, m_Amount(0.0)
		{
		}
		bool		hasPartCount() const
		{
			if( lRound(m_Count*1000.0)!=lRound(m_Count)*1000.0 )
				return TRUE;
			return FALSE;
		}
		/*!	\returns Die aktuell akkumulierte Anzahl dieses Reportblattes.
			\brief Akkumulierte Anzahl ermitteln.
		*/
 		long		getCount() const
		{
 			return lRound(m_Count);
		}
		/*!	\returns Die aktuell akkumulierte Anzahl dieses Reportblattes.
			\brief Akkumulierte Anzahl ermitteln.
		*/
		double		getArtCount() const
		{
			return m_Count;
		}
		/*!	\returns Den aktuell akkumulierten Betrag dieses Reportblattes.
			\brief Akkumulierten Betrag ermitteln.
		*/
		long		getAmount() const
		{
			return lRound(m_Amount);
		}
		virtual void	exportCSV(TValue& keys, QTextStream &st, QStringList& tags)
		{
			TValue val;
//			val.setValue(entryCount, m_Count);
//			val.setValue(entryAmount, m_Amount);
			val.setValue(entryPartCount, hasPartCount());
			for(TValue::ConstIterator it=this->begin(); it!=this->end(); ++it)
			{
				QString key = it.key();
				val.setValue(key, *it);
			}
//			if( keys )
			{
				for(TValue::ConstIterator it=keys.begin(); it!=keys.end(); ++it)
				{
					QString key = it.key();
					val.setValue(key, *it);
				}
			}
			val.exportCSV(st, tags);
			st << "\n";
		}
	protected:
		/*!	Erhht die akkumulierte Anzahl dieses Reportblattes um count.
			\param count	Zu addierende Anzahl.
			\brief Akkumulierte Anzahl erhhen.
 		void		addCount(long count)
		{
 			add(count, entryCount);
		}
		*/
		/*!	Erhht die akkumulierte Anzahl dieses Reportblattes um count.
			\param count	Zu addierende Anzahl.
			\brief Akkumulierte Anzahl erhhen.
		void		addArtCount(double count)
		{
			if( lRound(count*1000.0)!=lRound(count)*1000.0 )
				setValue(entryPartCount, TRUE);
			addD(count, entryCount);
		}
		*/
		/*!	Erhht den akkumulierten Betrag dieses Reportblattes um amount.
			\param amount	Zu addierender Betrag.
			\brief Akkumulierten Betrag erhhen.
		void		addAmount(double amount)
		{
			if( lRound(amount*1000.0)!=lRound(amount)*1000.0 )
				setValue(entryPartCount, TRUE);
			addD(amount, entryAmount);
		}
		*/
		/*!	Hilfsfunktion zum Addieren von Value-Werten.
			\param val		Zu addierende Zahl.
			\param item		Zu erhhendes Element.
			\brief Element erhhen.
		*/
		void		add(long val, const char* item)
		{
			long l = getValue(item, 0L);
			setValue(item, l+val);
		}
		void		addD(double val, const char* item)
		{
			double d = getValue(item, 0.0);
			setValue(item, d+val);
		}
		/*!	Hilfsfunktion zum Inkrementieren von Value-Werten.
			\param item		Zu inkrementierendes Element.
			\brief Element Inkrementieren.
		*/
		void		inc(const char* item)
		{
			add(1L, item);
		}
	protected:
		double		m_Count;
		double		m_Amount;
	};

	/*!	Diese Klasse bildet die Basis fr alle Bestell-Bl?er eines Reportes.
		Diese Bl?er akkumulieren jeweils die Anzahl und den Betrag von Bestellungen.
		\brief POS-Klassen: Report-Bl?er, Bestellungen.
		\sa TRepOrderNode
	*/
	class			TRepOrderLeaf
	: public TReportLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Bestell-Reportblattes.
			\brief ctor
		*/
		TRepOrderLeaf()
		: TReportLeaf()
		{
		}
		/*! Akkumuliert die Anzahl der Bestellungen innerhalb dieses Blattes.
			\param order	Die zu akkumulierende Bestellung
			\returns TRUE, wenn die Bestelldaten aufsummiert werden sollen, sonst FALSE.
			\brief Testfunktion, ob die Bestellung relevant fr den Report ist.
		*/
		virtual bool	addOrder(TTableOrder* order, TTable*)
		{
			if( !order->hasCount() )
				return FALSE;
			m_Count += order->getCount();
			setValue(entryCount, m_Count);
			m_Amount += order->getDAmount();
			setValue(entryAmount, m_Amount);
			return TRUE;
		}
	};

	class			TRepOrder
	: public TRepOrderLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Warengrupen-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param order	Die Bestellung, aus der die Warengruppen-Daten entnommen werden
			\brief ctor
		*/
		TRepOrder(TTableOrder* order, TTable* table)
		: TRepOrderLeaf()
//		, m_Order(order)
//		, m_Payform(table->getPayform())
		{
			setValue(TTable::entryTable, table->getTable());
			setValue(TTable::entryParty, table->getParty());
			setValue(TTable::entryBillNum, table->getBillNum());
			setValue(TTable::entryVatOH, table->isVatOH());
			if( order->hasVatOH() )
				setValue(TTable::entryVatOH, order->isVatOH());
			// Fr das blde Hotel-Socket-system
			setValue(TTableOrder::entryFamGroup, order->getFamGroup());
			setValue(TTableOrder::entryFamily, order->getFamily());
			setValue(TTableOrder::entryFamilyName, order->getFamilyName());
			setValue(TTableOrder::entryPlu, order->getPlu());
			setValue(TTableOrder::entryArticle, order->getArticle());
			setValue(TTableOrder::entryPrice, order->getPrice());
			setValue(TTableOrder::entryTerminal, order->getTerminal());
			setValue(TTableOrder::entryWaiter, order->getWaiter());
			setValue(TTableOrder::entryRateIH, order->getRateIH());
			setValue(TTableOrder::entryRateOH, order->getRateOH());
		}
		long			getTable() const
		{
			return getValue(TTable::entryTable, 0);
		}
		long			getBillNum() const
		{
		 	return getValue(TTable::entryBillNum, 0);
		}
		bool			isVatOH() const
		{
			return getValue(TTable::entryVatOH, FALSE);
		}
		int				getParty() const
		{
			return getValue(TTable::entryParty, 0);
		}
		int				getFamGroup() const
		{
			return getValue(TTableOrder::entryFamGroup, 0);
		}
		int				getFamily() const
		{
			return getValue(TTableOrder::entryFamily, 0);
		}
		QString			getFamilyName() const
		{
			return getString(TTableOrder::entryFamilyName);
		}
		int				getPlu() const
		{
			return getValue(TTableOrder::entryPlu, 0);
		}
		QString			getArticle() const
		{
			return getString(TTableOrder::entryArticle);
		}
		long			getPrice() const
		{
			return getValue(TTableOrder::entryPrice, 0L);
		}
		int				getTerminal() const
		{
			return getValue(TTableOrder::entryTerminal, 0);
		}
		int				getWaiter() const
		{
			return getValue(TTableOrder::entryWaiter, 0);
		}
		int				getRateIH() const
		{
			return getValue(TTableOrder::entryRateIH, 0);
		}
		int				getRateOH() const
		{
			return getValue(TTableOrder::entryRateOH, 0);
		}
	};

	/*!	Diese Klasse akkumuliert die fr Warengruppen-Berichte bentigten Daten anhand
		der gegebenen Bestellungen. Jedes Blatt entspricht einer gefunden Warengruppe.
		Folgende Daten werden bereitgestellt:
		- getFamily()		Die Warengruppen-ID
		- getFamilyName()	Der Name der Warengruppe
		\brief POS-Klassen: Report-Bl?er, Warengruppen.
		\sa TRepFamilies
	*/
	class			TRepFamily
	: public TRepOrderLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Warengrupen-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param order	Die Bestellung, aus der die Warengruppen-Daten entnommen werden
			\brief ctor
		*/
		TRepFamily(TTableOrder* order)
		: TRepOrderLeaf()
		{
			setValue(TTableOrder::entryFamily, order->getFamily());
			setValue(TTableOrder::entryFamilyName, order->getFamilyName());
		}
		/*!	\return Liefert die Nummer der Warengruppe dieses Reportblattes.
			\brief Warengruppe ermitteln.
		*/
		int			getFamily() const
		{
			return getValue(TTableOrder::entryFamily, 0);
		}
		/*!	\return Liefert den Namen der Warengruppe dieses Reportblattes.
			\brief Warengruppennamen ermitteln.
		*/
		QString		getFamilyName() const
		{
			return getString(TTableOrder::entryFamilyName);
		}
	};

	class			TRepFamGroup
	: public TRepOrderLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Oberwarengrupen-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param order	Die Bestellung, aus der die Oberwarengruppen-Daten entnommen werden
			\brief ctor
		*/
		TRepFamGroup(TTableOrder* order)
		: TRepOrderLeaf()
		{
			setValue(TTableOrder::entryFamGroup, order->getFamGroup());
			setValue(TTableOrder::entryFamGroupName, order->getFamGroupName());
		}
		/*!	\return Liefert die Nummer der Oberwarengruppe dieses Reportblattes.
			\brief Warengruppe ermitteln.
		*/
		int			getFamGroup() const
		{
			return getValue(TTableOrder::entryFamGroup, 0);
		}
	};

	/*!	Diese Klasse akkumuliert die fr Artikel-Berichte bentigten Daten anhand
		der gegebenen Bestellungen. Jedes Blatt entspricht einem gefundenen Artikel.
		Folgende Daten werden bereitgestellt:
		- getPlu()			Die Nummer des Artikels.
		- getArticle()		Der Name des Artikels.
		- getPrice()		Der Einzelpreis des Artikels.
		- isControl()		Pseudo-Bestellung fr Druckerkontrolle?
		\brief POS-Klassen: Report-Bl?er, Artikel.
		\sa TRepArticles
	*/
	class			TRepArticle
	: public TRepOrderLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Artikel-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param order	Die Bestellung, aus der die Artikel-Daten entnommen werden
			\brief ctor
		*/
		TRepArticle(TTableOrder* order)
		: TRepOrderLeaf()
		{
			setValue(TTableOrder::entryPlu, order->getPlu());
			setValue(TTableOrder::entryArticle, order->getArticle());
			setValue(TTableOrder::entryArtShort, order->getArtShort());
			setValue(TTableOrder::entryPrice, order->getPrice());
			setValue(TArticle::entryIsControl, order->isControl());
		}
		/*!	\return Liefert die Nummer des Artikels dieses Reportblattes.
			\brief Artikel ermitteln.
		*/
		int			getPlu() const
		{
			return getValue(TTableOrder::entryPlu, 0);
		}
		/*!	\return Liefert den Namen des Artikels dieses Reportblattes.
			\brief Artikelnamen ermitteln.
		*/
		QString		getArticle() const
		{
			return getString(TTableOrder::entryArticle);
		}
		QString		getShortname() const
		{
			return getString(TTableOrder::entryArtShort);
		}
		/*!	\return Liefert den Einzelpreis des Artikels dieses Reportblattes.
			\brief Einzelpreis ermitteln.
		*/
		long		getPrice() const
		{
			return getValue(TTableOrder::entryPrice, 0);
		}
		/*!	\return Liefert TRUE, wenn es sich um eine Pseudo-Bestellung zur
			Druckerkontrolle handelt.
			\brief Drucker-Kontrolle?
		*/
		bool		isControl() const
		{
			return getValue(TArticle::entryIsControl, FALSE);
		}
	#if QT_VERSION>=300
		virtual void	exportXml(QDomDocument& doc, QDomElement& root)
		{
			TValue::exportXml(doc, root);
		}
	#endif
	};

	class			TRepDiscount
	: public TRepArticle
	{
	public:
		/*!	Erzeuge eine Instanz eines Rabatt-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param order	Die Bestellung, aus der die Rabatt-Daten entnommen werden
			\brief ctor
		*/
		TRepDiscount(TTableOrder* order)
		: TRepArticle(order)
		{
//			setFamGroup(order->getDiscount());
			m_DiscInfos = order->getDiscInfo();
		}
		long		getDiscount() const
		{
			return lRound(getValue(TTableOrder::entryDiscount, 0.0));
		}
		/*!	\return Liefert die Nummer der Oberwarengruppe dieses Reportblattes.
			\brief Warengruppe ermitteln.
		*/
		QStringList		getDiscInfos() const
		{
			return m_DiscInfos;
		}
		virtual bool	addOrder(TTableOrder* order, TTable* table)
		{
			if( !order->getDiscount() )
				return FALSE;
			if( !TRepArticle::addOrder(order, table) )
				return FALSE;
			addD((double)order->getDiscount()*order->getCount(), TTableOrder::entryDiscount);
			return TRUE;
		}
	private:
		QStringList		m_DiscInfos;
	};

	class			TRepDepartment
	: public TRepArticle
	{
	public:
		static const char	entryDepartment[];
		static const char	entryDepName[];
	public:
		TRepDepartment(TTableOrder* order)
		: TRepArticle(order)
		{
			setValue(entryDepartment, order->getValue(entryDepartment, 0));
			setValue(entryDepName, order->getValue(entryDepName));
		}
		int			getDepartment() const
		{
			return getValue(entryDepartment, 0);
		}
		QString		getDepName() const
		{
			return getString(entryDepName);
		}
	};

	/*!	Diese Klasse akkumuliert die fr Mwst-Satz-Berichte bentigten Daten anhand
		der gegebenen Bestellungen. Jedes Blatt entsprich einem gefundenen Mwst-Satz.
		Folgende Daten werden bereitgestellt:
		- isVatOH()			Flag, ob auf Au?r-Haus bezahlt wurde.
		- getVatRate()		Der Prozentsatz dieser Mehrwertsteuer.
		- getVatNet()		Der Netto-Umsatz dieses Mwst-Satzes.
		- getVatAmount()	Der Hhe der Mehrwert-Steuer selbst.
		\brief POS-Klassen: Report-Bl?er, Mwst-S?e.
		\sa RepVatRates
	*/
	class			TRepVatRate
	: public TRepOrderLeaf
	{
		static const char	entryVatRate[];
		static const char	entryVatNet[];
		static const char	entryVatAmount[];
	public:
		/*!	Erzeuge eine Instanz eines Mwst-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param order	Die Bestellung, aus der die Mwst-Daten entnommen werden
			\param table	Der zu der Bestellung gehrige Tisch
			\brief ctor
		*/
		TRepVatRate(TTableOrder* order, TTable* table)
		: TRepOrderLeaf()
		, m_VatNet(0.0)
		, m_VatAmount(0.0)
		{
			setValue(TTable::entryVatOH, table->isVatOH());
			if( order->hasVatOH() )
				setValue(TTable::entryVatOH, order->isVatOH());
			setValue(TTable::entryInSummary, table->inSummary());
			if( isVatOH() ) {
				setID(order->getIndexOH());
				setValue(entryVatRate, order->getRateOH());
			}
			else {
				setID(order->getIndexIH());
				setValue(entryVatRate, order->getRateIH());
			}
		}
		TRepVatRate(TTaxGroup* grp, TTable* table)
		: TRepOrderLeaf()
		, m_VatNet(0.0)
		, m_VatAmount(0.0)
		, m_IsTax(TRUE)
		{
			setValue(TNValue::entryName, grp->getName());
			setValue(TTable::entryVatOH, table->isVatOH());
			setValue(TTable::entryInSummary, table->inSummary());
			setValue(entryVatRate, grp->getValue(TVatRate::entryRate, 0));
		}
		/*!	\return Liefert TRUE, wenn der Tisch auf Au?r-Haus bezahlt wurde.
			\brief Zahlungsart ermitteln.
		*/
		bool		isVatOH() const
		{
			return getValue(TTable::entryVatOH, FALSE);
		}
		/*!	\return Liefert den Mehrwertsteuersatz dieses Reportblattes.
			\brief Mehrwertsteuersatz ermitteln.
		*/
		int			getVatRate() const
		{
			return getValue(entryVatRate, 0);
		}
		/*!	\return Liefert den Nettoumsatz des Steuersatzes dieses Reportblattes.
			\brief Nettoumsatz ermitteln.
		*/
		long		getVatNet() const
		{
			return lRound(m_VatNet);
		}
		/*!	\return Liefert den Steueranteil des Umsatzes des Steuersatzes dieses Reportblattes.
			\brief Steueranteil des Umsatzes ermitteln.
		*/
		long		getVatAmount() const
		{
			return lRound(m_VatAmount);
		}
		bool		inSummary() const
		{
			return getValue(TTable::entryInSummary, TRUE);
		}
		/*!	Akkumuliert die bentigten Werte der Bestellung order, sofern diese
			Bestellung bercksichtigt werden soll.
			\param order		Die zu akkumulierende Bestellung
			\return Liefert FALSE, wenn die Bestellung in diesem Report nicht bercksichtigt
			werden soll.
		*/
		virtual bool	addOrder(TTableOrder* order, TTable* tbl)
		{
			if( !tbl->inSummary() )
				return FALSE;
			if( !TRepOrderLeaf::addOrder(order, tbl) )
				return FALSE;
			double net;
			bool oh = tbl->isVatOH();
			if( order->hasVatOH() )
				oh = order->isVatOH();
			if( oh )
				net = (order->getAmount()*100.0)/(100.0+order->getRateOH()/100.0);
			else
				net = (order->getAmount()*100.0)/(100.0+order->getRateIH()/100.0);
			m_VatNet += net;
			m_VatAmount += order->getAmount()-net;
			return TRUE;
		}
		void		addTax(TTable* tbl, TTaxGroup* grp);
		bool		isTax() const
		{
			return m_IsTax;
		}
	private:
		double		m_VatNet;
		double		m_VatAmount;
		bool		m_Ignore;
		bool		m_IsTax;
	};

	/*!	Diese Klasse akkumuliert die fr Stunden-Berichte bentigten Daten anhand
		der gegebenen Bestellungen. Jedes Blatt entsprich einer Umsatz-Stunde.
		Folgende Daten werden bereitgestellt:
		- getHour()			Die Stunde der Bestellung.
		\brief POS-Klassen: Report-Bl?er, Stunden-Ums?e.
		\sa RepHours
	*/
	class			TRepHour
	: public TRepOrderLeaf
	{
	public:
		TRepHour(TTableOrder* order)
		: TRepOrderLeaf()
		{
			m_Hour = order->getTime().hour();
		}
		/*!	\return Liefert die Stunde dieses Reportblattes.
			\brief Stunde ermitteln.
		*/
		int			getHour() const
		{
			return m_Hour;
		}
	protected:
		int			m_Hour;
	};

	/*!	Diese Klasse bildet die Basis fr alle Storno-Bl?er eines Reportes.
		Diese Bl?er akkumulieren jeweils die Anzahl und den Betrag von Stornos.
		\brief POS-Klassen: Report-Bl?er, Stornierungen.
		\sa TRepVoidNode
	*/
	class			TRepVoidLeaf
	: public TReportLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Reportblattes.
			\brief ctor
		*/
		TRepVoidLeaf()
		: TReportLeaf()
		{
		}
		/*! Akkumuliert die Anzahl der Stornos innerhalb dieses Blattes.
			\param _void	Das zu akkumulierende Storno
			\returns TRUE, wenn die Stornodaten aufsummiert werden sollen, sonst FALSE.
			\brief Testfunktion, ob das Storno relevant fr den Report ist.
		*/
		virtual bool	addVoid(TTableVoid* _void)
		{
			if( !_void->hasCount() )
				return FALSE;
			m_Count += _void->getCount();
			setValue(entryCount, m_Count);
			m_Amount += _void->getDAmount();
			setValue(entryAmount, m_Amount);
			return TRUE;
		}
	};

	/*!	Diese Klasse akkumuliert die fr Storno-Artikel-Berichte bentigten Daten anhand
		der gegebenen Bestellungen. Jedes Blatt entspricht einem gefundenen Storno-Artikel.
		Folgende Daten werden bereitgestellt:
		- getPlu()			Die Nummer des Artikels.
		- getArticle()		Der Name des Artikels.
		- getPrice()		Der Einzelpreis des Artikels.
		\brief POS-Klassen: Report-Bl?er, Storno-Artikel.
		\sa TRepVArticles
	*/
	class			TRepVArticle
	: public TRepVoidLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Artikel-Reportblattes.
			?ernimmt aus order die vom Blatt bereitgestellten Daten.
			\param _void	Das Storno, aus dem die Storno-Artikel-Daten entnommen werden
			\brief ctor
		*/
		TRepVArticle(TTableVoid* _void)
		: TRepVoidLeaf()
		{
			setValue(TTableOrder::entryPlu, _void->getPlu());
			setValue(TTableOrder::entryArticle, _void->getArticle());
			setValue(TTableOrder::entryPrice, _void->getPrice());
		}
		/*!	\return Liefert die Nummer des Storno-Artikels dieses Reportblattes.
			\brief Storno-Artikel ermitteln.
		*/
		int			getPlu() const
		{
			return getValue(TTableOrder::entryPlu, 0);
		}
		/*!	\return Liefert den Namen des Storno-Artikels dieses Reportblattes.
			\brief Storno-Artikelnamen ermitteln.
		*/
		QString		getArticle() const
		{
			return getString(TTableOrder::entryArticle);
		}
		/*!	\return Liefert den Einzelpreis des Storno-Artikels dieses Reportblattes.
			\brief Einzelpreis ermitteln.
		*/
		long		getPrice() const
		{
			return getValue(TTableOrder::entryPrice, 0);
		}
	};

	/*!	Diese Klasse sammelt die fr Storno-Berichte bentigten Daten anhand
		der gegebenen Stornos. Jedes Blatt entspricht einem gefundenen Storno-Eintrag.
		Folgende Daten werden bereitgestellt:
		- getTable()		Die Tischnummer.
		- getParty()		Die Parteinummer.
		- getPlu()			Die Nummer des Artikels.
		- getArticle()		Der Name des Artikels.
		- getPrice()		Der Einzelpreis des Artikels.
		- getDate()			Das Datum des Stornos
		- getTime()			Die Uhrzeit des Stornos
		\brief POS-Klassen: Report-Bl?er, Storno-Vorg?e.
		\sa TRepAllVoids
	*/
	class			TRepVoid
	: public TRepVoidLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Reportblattes.
			\brief ctor
		*/
		TRepVoid(TTableVoid* _void, TTable* table)
		: TRepVoidLeaf()
		{
			setValue(TTableOrder::entryPlu, _void->getPlu());
			setValue(TTableOrder::entryArticle, _void->getArticle());
			setValue(TTableOrder::entryPrice, _void->getPrice());
			setValue(TTable::entryTable, table->getTable());
			setValue(TTable::entryParty, table->getParty());
			setValue(TTableVoid::entryVWaiter, _void->getVWaiter());
			setValue(TTableVoid::entryVWaiterName, _void->getVWaiterName());
			setValue(TTableVoid::entryReason, _void->getReason());
		}
		/*!	Akkumuliert die bentigten Werte des Stornos _void, sofern dieses
			Storno bercksichtigt werden soll.
			\param _void		Das zu akkumulierende Storno
			\return Liefert FALSE, wenn das Storno in diesem Report nicht bercksichtigt
			werden soll.
		*/
		virtual bool	addVoid(TTableVoid* _void)
		{
			if( !_void->hasCount() )
				return FALSE;
			m_Date = _void->getDate();
			m_Time = _void->getTime();
			m_Count += _void->getCount();
			setValue(entryCount, m_Count);
			m_Amount += _void->getDAmount();
			setValue(entryAmount, m_Amount);
			return TRUE;
		}
		/*!	\return Liefert die Tischnummer des Stornos.
			\brief Storno-Tisch ermitteln.
		*/
		long		getTable() const
		{
			return getValue(TTable::entryTable, 0);
		}
		/*!	\return Liefert die Parteinummer des Stornos.
			\brief Storno-Partei ermitteln.
		*/
		int			getParty() const
		{
			return getValue(TTable::entryParty, 0);
		}
		/*!	\return Liefert die Nummer des Storno-Artikels dieses Reportblattes.
			\brief Storno-Artikel ermitteln.
		*/
		int			getPlu() const
		{
			return getValue(TTableOrder::entryPlu, 0);
		}
		int			getVWaiter() const
		{
			return getValue(TTableVoid::entryVWaiter, 0);
		}
		QString		getVWaiterName() const
		{
			return getString(TTableVoid::entryVWaiterName);
		}
		/*!	\return Liefert den Namen des Storno-Artikels dieses Reportblattes.
			\brief Storno-Artikelnamen ermitteln.
		*/
		QString		getArticle() const
		{
			return getString(TTableOrder::entryArticle);
		}
		/*!	\return Liefert den Einzelpreis des Storno-Artikels dieses Reportblattes.
			\brief Einzelpreis ermitteln.
		*/
		long		getPrice() const
		{
			return getValue(TTableOrder::entryPrice, 0);
		}
		/*!	\return Liefert das Datum des Stornos dieses Reportblattes.
			\brief Storno-Datum ermitteln.
		*/
		QDate		getDate() const
		{
			return m_Date;
		}
		/*!	\return Liefert die Uhrzeit des Stornos dieses Reportblattes.
			\brief Storno-Uhrzeit ermitteln.
		*/
		QTime		getTime() const
		{
			return m_Time;
		}
		QString		getVoidReason() const
		{
			return getString(TTableVoid::entryReason);
		}
	protected:
		QDate		m_Date;					//!< Datum des Stornos
		QTime		m_Time;					//!< Uhrzeit des Stornos
	};

	/*!	Diese Klasse bildet die Basis fr alle Bezahl-Bl?er eines Reportes.
		Diese Bl?er akkumulieren jeweils die Anzahl und den Betrag von Zahlvorg?en.
		\brief POS-Klassen: Report-Bl?er, Bezahlungen.
		\sa TRepPayNode
	*/
	class			TRepPayLeaf
	: public TReportLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Bezahl-Reportblattes.
			\brief ctor
		*/
		TRepPayLeaf()
		: TReportLeaf()
		{
		}
		long			getTip() const
		{
			return getValue(TTablePay::entryTip, 0L);
		}
		long			getGuests() const
		{
			return getValue(TTablePay::entryGuestCount, 0L);
		}
		/*! Akkumuliert die bereitgestellten Bezahldaten innerhalb dieses Blattes.
			\param pay		Die zu akkumulierende Bezahlung
			\brief Testfunktion, ob der Zahlvorgang relevant fr den Report ist.
			\returns TRUE, wenn die Bezahldaten aufsummiert werden sollen, sonst FALSE.
		*/
		virtual bool	addPay(TTablePay* pay)
		{
			if( pay->wasVoided() )
				return FALSE;
			m_Count += 1.0;
			setValue(entryCount, m_Count);
			m_Amount += pay->getAmount();
			setValue(entryAmount, m_Amount);
			addTip(pay->getTip());
			addGuests(pay->getGuestCount());
			return TRUE;
		}
	protected:
		void		addTip(long tip)
		{
			add(tip, TTablePay::entryTip);
		}
		void		addGuests(int guests)
		{
			add(guests, TTablePay::entryGuestCount);
		}
	};

	/*!	Diese Klasse akkumuliert die fr Tisch-Berichte bentigten Daten anhand
		der gegebenen Zahlvorg?e.
		Folgende Daten werden bereitgestellt:
		- getTable()		Die Tischnummer.
		- getParty()		Die Parteinummer.
		- getTableName()	Der Tischname.
		- getDate()			Das Datum des Zahlvorgangs.
		- getTime()			Die Uhrzeit des Zahlvorgangs.
		- getCreateDate()	Das Erzeugungs-Datum des Tisches.
		- getCreateTime()	Die Erzeugungs-Uhrzeit des Tisches.
		- getLastDate()		Das Datum der Archivierung des Tisches.
		- getLastTime()		Die Uhrzeit der Archivierung des Tisches.
		\brief POS-Klassen: Report-Bl?er, Tischbericht.
		\sa TRepTables
	*/
	class			TRepTable
	: public TRepPayLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Tisch-Reportblattes.
			?ernimmt aus pay und table die vom Blatt bereitgestellten Daten.
			\param pay		Der Zahlvorgang, aus dem die Daten entnommen werden.
			\param table	Der Tisch zu dem der Zahlvorgang gehrt.
			\brief ctor
		*/
		TRepTable(TTablePay* pay, TTable* table)
		: TRepPayLeaf()
		{
			setValue(TTable::entryTable, table->getTable());
			setValue(TTable::entryParty, table->getParty());
			setValue(TTableCreate::entryTableName, table->getCreate().getTableName());
			m_Date = pay->getDate();
			m_Time = pay->getTime();
			m_CreateDate = table->getCreate().getDate();
			m_CreateTime = table->getCreate().getTime();
			if( pay->getAmount()==0 )
			{
				m_LastDate = pay->getDate();
				m_LastTime = pay->getTime();
			}
			bool help=table->isVatOH();
			if(help) m_IsInHouse=FALSE;
			    else m_IsInHouse=TRUE;
		}
		virtual bool	addPay(TTablePay* pay)
		{
			if( !TRepPayLeaf::addPay(pay) )
				return FALSE;
			if( !m_Payments.isEmpty() )
				m_Payments += ";";
			m_Payments += pay->getPayformName()+":"+QString::number(pay->getGiven()-pay->getReturn())+":"+QString::number(pay->getTip());
			return TRUE;
		}
		QStringList	getPayments() const
		{
			return QStringList::split(";", m_Payments);
		}
		/*!	\return Liefert die Tischnummer des Zahlvorgangs dieses Reportblattes.
			\brief Tischnummer ermitteln.
		*/
		long		getTable() const
		{
			return getValue(TTable::entryTable, 0);
		}
		/*!	\return Liefert die Parteinummer des Zahlvorgangs dieses Reportblattes.
			\brief Parteinummer ermitteln.
		*/
		int			getParty() const
		{
			return getValue(TTable::entryParty, 0);
		}
		/*!	\return Liefert den Tischnamen des Zahlvorgangs dieses Reportblattes.
			\brief Tischnamen ermitteln.
		*/
		QString		getTableName() const
		{
			return getString(TTableCreate::entryTableName);
		}
		/*!	\return Liefert das Erzeugungsdatum des Tisches mit dem Zahlvorgang
			dieses Reportblattes.
			\brief Erzeugungsdatum ermitteln.
		*/
		QDate		getCreateDate() const
		{
			return m_CreateDate;
		}
		/*!	\return Liefert die Erzeugungsuhrzeit des Tisches mit dem Zahlvorgang
			dieses Reportblattes.
			\brief Erzeugungsuhrzeit ermitteln.
		*/
		QTime		getCreateTime() const
		{
			return m_CreateTime;
		}
		/*!	\return Liefert das Datum des Zahlvorgangs dieses Reportblattes.
			\brief Zahlungsdatum ermitteln.
		*/
		QDate		getDate() const
		{
			return m_Date;
		}
		/*!	\return Liefert die Uhrzeit des Zahlvorgangs dieses Reportblattes.
			\brief Zahlungsuhrzeit ermitteln.
		*/
		QTime		getTime() const
		{
			return m_Time;
		}
		/*!	\return Liefert das Datum der Archivierung des Tisches dieses Reportblattes.
			\brief Archivierungsdatum ermitteln.
		*/
		QDate		getLastDate() const
		{
			return m_LastDate;
		}
		/*!	\return Liefert die Uhrzeit der Archivierung des Tisches dieses Reportblattes.
			\brief Archivierungsuhrzeit ermitteln.
		*/
		QTime		getLastTime() const
		{
			return m_LastTime;
		}
		bool		IsInHouse() const
		{
			return m_IsInHouse;
		}
	protected:
		QDate		m_Date;						//!< Das Datum des Zahlvorganges
		QTime		m_Time;						//!< Die Uhrzeit des Zahlvorganges
		QDate		m_CreateDate;				//!< Das Erzeugungsdatum des Tisches
		QTime		m_CreateTime;				//!< Die Erzeugungsuhrzeit des Tisches
		QDate		m_LastDate;					//!< Das Archivierungsdatum des Tisches
		QTime		m_LastTime;					//!< Die Archivierungsuhrzeit des Tisches
		QString		m_Payments;					//!< Die Zahlvorg?e, Aart:Betrag:Tip;...
        bool		m_IsInHouse;				//! Flag ob der Tisch Im/Haus oder Ausser/Haus bezahlt wurde
	};

	/*!	Diese Klasse akkumuliert die fr Abrechnungsart-Berichte bentigten Daten anhand
		der gegebenen Zahlvorg?e.
		Folgende Daten werden bereitgestellt:
		- getPayform()		Die Nummer der Abrechnungsart.
		- getPayformName()	Der Name der Abrechnungsart.
		- isVatOH()			Gibt an, ob auf Au?r-Haus bezahlt wurde.
		- inSummary()		Gibt an, ob die Abrechnungsart im Gesamtumsatz enthalten ist
		- inCashSummary()	Gibt an, ob die Abrechnungsart im Kassenumsatz enthalten ist
		- getCurrency()		Die Nummer der verwendeten W?ung.
		- getCurrencyName()	Der Name der verwendeten W?ung.
		- getCurrencyShort()	Das Krzel der verwendeten W?ung.
		- getCurrencyRate()	Der Umrechnungsfaktor der verwendeten W?ung.
		\brief POS-Klassen: Report-Bl?er, Abrechnungsartenbericht.
		\sa TRepPayforms
	*/
	class			TRepPayform
	: public TRepPayLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Abrechnungsarten-Reportblattes.
			?ernimmt aus pay und table die vom Blatt bereitgestellten Daten.
			\param pay		Der Zahlvorgang, aus dem die Daten entnommen werden.
			\param table	Der Tisch zu dem der Zahlvorgang gehrt.
			\brief ctor
		*/
		TRepPayform(TTablePay* pay, TTable* table, bool useret=FALSE)
		: TRepPayLeaf()
		{
			setValue(TTable::entryVatOH, table->isVatOH());
			setValue(TTablePay::entryPayform, pay->getPayform());
			setValue(TTablePay::entryPayformName, pay->getPayformName());
			if( useret ) {
				TCurrency curr;
				pay->getReturnCurrency(curr);
				setValue(TTablePay::entryCurrency, curr.getID());
				setValue(TTablePay::entryCurrencyName, curr.getName());
				setValue(TTablePay::entryCurrencyShort, curr.getShortname());
				setValue(TTablePay::entryCurrencyRate, curr.getRate());
				setValue(TTablePay::entryCurrencyDecPos, curr.getDecPos());
			} else {
				setValue(TTablePay::entryCurrency, pay->getCurrency());
				setValue(TTablePay::entryCurrencyName, pay->getCurrencyName());
				setValue(TTablePay::entryCurrencyShort, pay->getCurrencyShort());
				setValue(TTablePay::entryCurrencyRate, pay->getCurrencyRate());
				if( pay->hasValue(TTablePay::entryCurrencyDecPos) )
					setValue(TTablePay::entryCurrencyDecPos, pay->getCurrencyDecPos());
			}
			setValue(TTable::entryInSummary, table->inSummary());
			setValue(TTablePay::entryInCashSum, pay->inCashSummary());
			setValue(TTablePay::entryWaiter, pay->getWaiter());
			setValue(TTablePay::entryWaiterName, pay->getWaiterName());
			setValue(TTable::entryBillNum, table->getBillNum());
		}
		/*!	\return Liefert die Nummer der Abrechnungsart des Zahlvorgangs dieses Reportblattes.
			\brief Abrechnungsart ermitteln.
		*/
		int			getPayform() const
		{
			return getValue(TTablePay::entryPayform, 0);
		}
		/*!	\return Liefert den Namen der Abrechnungsart des Zahlvorgangs dieses Reportblattes.
			\brief Abrechnungsart, Name ermitteln.
		*/
		QString		getPayformName() const
		{
			return getString(TTablePay::entryPayformName);
		}
		/*!	\return Liefert TRUE, wenn der Tisch auf Au?r-Haus bezahlt worden ist, sonst
			FALSE.
			\brief Auf Au?r-Haus bezahlt?.
		*/
		bool		isVatOH() const
		{
			return getValue(TTable::entryVatOH, FALSE);
		}
		/*!	\return Liefert TRUE, wenn die Abrechnungsart im Gesamtumsatz enthalten ist, sonst
			FALSE.
			\brief Abrechnungsart im Gesamtumsatz enthalten?.
		*/
		bool		inSummary() const
		{
			return getValue(TTable::entryInSummary, TRUE);
		}
		/*!	\return Liefert TRUE, wenn die Abrechnungsart im Kassenumsatz enthalten ist, sonst
			FALSE.
			\brief Abrechnungsart im Kassenumsatz enthalten?.
		*/
		bool		inCashSummary() const
		{
			return getValue(TTablePay::entryInCashSum, TRUE);
		}
		bool			getCurrency(TCurrency& curr)
		{
			if( !getCurrency() )
				return false;
			curr.setID(getCurrency());
			curr.setName(getCurrencyName());
			curr.setShortname(getCurrencyShort());
			curr.setRate(getCurrencyRate());
			curr.setDecPos(getCurrencyDecPos());
			return true;
		}
		/*!	\return Liefert die Nummer der W?ung des Zahlvorgangs dieses Reportblattes.
			\brief W?ung ermitteln.
		*/
		int			getCurrency() const
		{
			return getValue(TTablePay::entryCurrency, 0);
		}
		/*!	\return Liefert den Namen der W?ung des Zahlvorgangs dieses Reportblattes.
			\brief W?ungsname ermitteln.
		*/
		QString		getCurrencyName() const
		{
			return getString(TTablePay::entryCurrencyName);
		}
		/*!	\return Liefert das Krzel der W?ung des Zahlvorgangs dieses Reportblattes.
			\brief W?ungskrzel ermitteln.
		*/
		QString		getCurrencyShort() const
		{
			return getString(TTablePay::entryCurrencyShort);
		}
		/*!	\return Liefert den Umrechnungsfaktor der W?ung des Zahlvorgangs dieses Reportblattes.
			\brief Umrechnungsfaktor ermitteln.
		*/
		double		getCurrencyRate() const
		{
			return getValue(TTablePay::entryCurrencyRate, 1.0);
		}
		int			getCurrencyDecPos() const
		{
			return getValue(TTablePay::entryCurrencyDecPos, 2);
		}
		/*!	\return Liefert die Nummer des Kellenrs des Zahlvorgangs dieses Reportblattes.
			\brief Kellner ermitteln.
		*/
		int			getWaiter() const
		{
			return getValue(TTablePay::entryWaiter, 0);
		}
		/*!	\return Liefert den Namen des Kellners des Zahlvorgangs dieses Reportblattes.
			\brief Kellner, Name ermitteln.
		*/
		QString		getWaiterName() const
		{
			return getString(TTablePay::entryWaiterName);
		}
		int			getBillNum() const
		{
			return getValue(TTable::entryBillNum, 0);
		}
		long		getOrgGiven() const
		{
			long ret = getValue(TTablePay::entryOrgGiven, 0L);
//			if( !ret )
//				ret = getGiven();
//		qDebug(QString("getOrgGiven: %1").arg(ret));
			return ret;
		}
		long		getGiven() const
		{
			long ret = getValue(TTablePay::entryGiven, 0L);
//		qDebug(QString("getGiven: %1").arg(ret));
			return ret;
		}
		long		getTip() const
		{
			return getValue(TTablePay::entryTip, 0);
		}
		long		getReturn() const
		{
			long ret = getValue(TTablePay::entryReturn, 0L);
//		qDebug(QString("getReturn: %1").arg(ret));
			return ret;
		}
		long		getRetAmount() const
		{
			long ret = getValue(TTablePay::entryRetAmount, 0L);
//			if( !ret )
//				ret = getReturn();
//		qDebug(QString("getRetAmount: %1").arg(ret));
			return ret;
		}
		/*! Z?t zus?lich den Tip-Betrag des Zahlvorgangs zum Umsatz hinzu.
			\param pay		Der zu akkumulierende Zahlvorgang
			\returns TRUE, wenn die Zahlungsdaten aufsummiert werden sollen, sonst FALSE.
			\brief Testfunktion, ob der Zahlvorgang relevant fr den Report ist.
		*/
		virtual bool	addPay(TTablePay* pay)
		{
			if( !TRepPayLeaf::addPay(pay) )
				return FALSE;
			m_Amount += pay->getTip();
			// steht schon in TRepPayLeaf::addPay // add(pay->getTip(), TTablePay::entryTip);
			add(pay->getReturn(), TTablePay::entryReturn);
			add(pay->getRetAmount(), TTablePay::entryRetAmount);
			long given = pay->getGiven();
			if( 0L==given )
				given = pay->getAmount();
			add(given, TTablePay::entryGiven);
			if( pay->hasOrgGiven() )
				add(pay->getOrgGiven(), TTablePay::entryOrgGiven);
			else
				add(given, TTablePay::entryOrgGiven);
			return TRUE;
		}
	};

	class			TRepSubvent
	: public TRepPayform
	{
	public:
		TRepSubvent(TTablePay* pay, TTable* table)
		: TRepPayform(pay, table)
		{
			setValue(TTablePay::entrySubvent, pay->getSubvent());
			setValue(TTablePay::entrySubId, pay->getSubId());
			setValue(TTablePay::entrySubName, pay->getSubName());
//			setValue(TTablePay::entrySubAmount, pay->getSubAmount());
		}
		long		getSubvent() const
		{
			return getValue(TTablePay::entrySubvent, 0L);
		}
		int			getSubId() const
		{
			return getValue(TTablePay::entrySubId, 0);
		}
		QString		getSubName() const
		{
			return getString(TTablePay::entrySubName);
		}
		long		getSubAmount() const
		{
			return getValue(TTablePay::entrySubAmount, 0L);
		}
		virtual bool	addPay(TTablePay* pay)
		{
			if( !TRepPayform::addPay(pay) )
				return FALSE;
			setValue(TTablePay::entrySubAmount, getSubAmount()+pay->getSubAmount());
			return TRUE;
		}
	};

	class			TRepHotelRoom
	: public TRepPayform
	{
	public:
		TRepHotelRoom(TTablePay* pay, TTable* table)
		: TRepPayform(pay, table)
		{
			setValue(TTable::entryTable, table->getTable());
			setValue(TTable::entryParty, table->getParty());
			setValue(TTable::entryBillNum, table->getBillNum());
			setValue(TTablePay::entryHotelRoom, pay->getHotelRoom());
			setValue(TTablePay::entryHotelParty, pay->getHotelParty());
			setValue(TTablePay::entryHotelAccount, pay->getHotelAccount());
			setValue(TTablePay::entryHotelGuest, pay->getHotelGuest());
			setValue(TTablePay::entryWaiterName, pay->getWaiterName());
			m_Date = pay->getDate();
			m_Time = pay->getTime();
		}
		long		getTable() const
		{
			return getValue(TTable::entryTable, 0);
		}
		long		getBillNum() const
		{
			return getValue(TTable::entryBillNum, 0);
		}
		int			getParty() const
		{
			return getValue(TTable::entryParty, 0);
		}
		QString		getHotelRoom() const
		{
			return getString(TTablePay::entryHotelRoom);
		}
		int			getHotelParty() const
		{
			return getValue(TTablePay::entryHotelParty, 0);
		}
		QString		getHotelAccount() const
		{
			return getString(TTablePay::entryHotelAccount);
		}
		QString		getHotelGuest() const
		{
			return getString(TTablePay::entryHotelGuest);
		}
		QDate		getDate() const
		{
			return m_Date;
		}
		QTime		getTime() const
		{
			return m_Time;
		}
		virtual bool	addPay(TTablePay* pay)
		{
			if( !pay->hasHotelRoom() || !TRepPayform::addPay(pay) )
				return FALSE;
			return TRUE;
		}
	protected:
		QDate		m_Date;						//!< Das Datum des Zahlvorganges
		QTime		m_Time;						//!< Die Uhrzeit des Zahlvorganges
	};

	class			TRepCashLeaf
	: public TReportLeaf
	{
	public:
		TRepCashLeaf(TTableCashing* cash)
		: TReportLeaf()
		, m_RetAmount(0L)
		{
			cash->getReturnCurrency(m_RetCurr);
		}
		bool			getRetCurrency(TCurrency& curr)
		{
			if( !m_RetCurr.getID() )
				return false;
			curr.setID(m_RetCurr.getID());
			curr.setName(m_RetCurr.getName());
			curr.setShortname(m_RetCurr.getShortname());
			curr.setRate(m_RetCurr.getRate());
            curr.setDecPos(m_RetCurr.getDecPos());
            return true;
		}
		long			getRetAmount() const
		{
			return m_RetAmount;
		}
		int				getRetCurr() const
		{
			return m_RetCurr.getID();
		}
		QString			getRetName() const
		{
			return m_RetCurr.getName();
		}
		QString			getRetShort()
		{
			return m_RetCurr.getShortname();
		}
		QString			getReason() const
		{
			return m_Reason;
		}
		virtual bool	addCash(TTableCashing* cash)
		{
			m_Count += 1.0;
			setValue(entryCount, m_Count);
			m_Amount += cash->getAmount();
			setValue(entryAmount, m_Amount);
			m_RetAmount += cash->getRetAmount();
			m_Reason = cash->getReason();
			return TRUE;
		}
	protected:
		long		m_RetAmount;
		QString		m_Reason;
		TCurrency	m_RetCurr;
	};

	// Hilfsklasse, um Pays und Cashins zusammenzukriegen
	class			TSumCurrencies
	{
	public:
		TSumCurrencies()
		: m_HasCurr(FALSE)
		, m_Amount(0L)
		{
		}
		TSumCurrencies(const TSumCurrencies& org)
		: m_HasCurr(org.m_HasCurr)
		, m_Amount(org.m_Amount)
		, m_Curr(org.m_Curr)
		{
		}
		TSumCurrencies&	operator = (const TSumCurrencies& org)
		{
			m_HasCurr = org.m_HasCurr;
			m_Amount = org.m_Amount;
			m_Curr = org.m_Curr;
			return *this;
		}
		void		setData(long amount, TCurrency& curr)
		{
			m_Amount += amount;
			if( m_HasCurr )
				return;
			m_Curr.setID(curr.getID());
			m_Curr.setName(curr.getName());
			m_Curr.setShortname(curr.getShortname());
			m_Curr.setRate(curr.getRate());
            m_Curr.setDecPos(curr.getDecPos());
            m_HasCurr = TRUE;
		}
		void		getCurrency(TCurrency& curr)
		{
			curr.setID(m_Curr.getID());
			curr.setName(m_Curr.getName());
			curr.setShortname(m_Curr.getShortname());
			curr.setRate(m_Curr.getRate());
            curr.setDecPos(m_Curr.getDecPos());
        }
		long		getAmount()
		{
			return m_Amount;
		}
	public:
		bool		m_HasCurr;
		long		m_Amount;
		TCurrency	m_Curr;
	};
}

using namespace PosLib;

#endif

