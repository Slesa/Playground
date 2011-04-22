#ifndef				_POSLIB_TREPORTNODE_

#define				_POSLIB_TREPORTNODE_
#include			"poslib/treportleaf.h"
#include			"poslib/tdepartment.h"
#include			"poslib/tfamgroup.h"
#include			"poslib/tdiscount.h"

namespace			PosLib
{
	/*!	Diese Klasse bildet die Basis fr alle Knoten eines Reportes.
		Knoten nehmen die Bl?ter des Reportes auf und akkumulieren die Anzahl
		und Betr?e aller ihrer Bl?ter.
		\brief POS-Klassen: Report-Knoten.
	*/
	class			TReportNode
	: public QDict<TReportLeaf>
	{
	public:
		/*!	Erzeuge eine Instanz eines Reportknotens.
			\brief ctor
		*/
		TReportNode(const QString& name)
		: QDict<TReportLeaf>(17/*8191*/, FALSE)
		, m_Name(name)
		{
			QDict<TReportLeaf>::setAutoDelete(TRUE);
		}
		/*!	Zerst?t eine Instanz eines Reportknotens.
			\brief dtor
		*/
		~TReportNode()
		{
			clear();
		}
		QString		getName() const
		{
			return m_Name;
		}
		virtual void	exportCSV(const QString& path, TValue& value);
		virtual void	exportCSV(const QString& /*path*/, TValue& /*value*/, QTextStream& /*st*/, QStringList& /*tags*/)
		{
		}
	#if QT_VERSION>=300
		virtual QDomElement	exportXml(QDomDocument& doc, QDomElement& root);
	#endif
	protected:
		QString		m_Name;
	};

	/*!	Diese Klasse bildet die Basis fr alle Knoten-Iteratoren eines Reportes.
		\brief POS-Klassen: Iterator fr Report-Knoten.
	*/
	class			TReportNodeIt
	: public QDictIterator<TReportLeaf>
	{
	public:
		/*!	Erzeuge eine Instanz eines Reportknoten-Iterators.
			\param node		Der Knoten, ber den iteriert wird.
			\brief ctor
		*/
		TReportNodeIt(const TReportNode& node)
		: QDictIterator<TReportLeaf>(node)
		{
		}
	};

	/*!	Diese Klasse bildet die Basis fr alle Bestell-Knoten eines Reportes.
		Diese Knoten akkumulieren jeweils die Anzahl und den Betrag von Bestellung-Bl?tern.
		\brief POS-Klassen: Report-Knoten, Bestellungen.
		\sa TRepOrderLeaf
	*/
	class			TRepOrderNode
	: public TReportNode
	, public TRepOrderLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Bestell-Reportknotens.
			\brief ctor
		*/
		TRepOrderNode(const QString& name)
		: TReportNode(name)
		, TRepOrderLeaf()
//		, m_Maximum(0)
		{
		}
		/*!	\return Liefert den Bestellknoten fr den Schlssel key, oder NULL, wenn
			noch kein solcher Knoten vorliegt.
			\param key		Schlssel des zu suchenden Knotens
			\brief Bestellknoten finden.
		*/
		TReportLeaf*	find(const QString& key)
		{
			return TReportNode::find(key);
		}
		/*!	Hilfsfunktion, um den gr?ten Schlsselwert zu finden. Dient zur Sortierung der
			Ausgabe.
			\return Liefert den gr?ten eingefgten Schlssel.
			\brief Gr?te Schlsselnummer finden.
		int			getMaximum()
		{
			return m_Maximum;
		}
		*/
		/*!	Akkumuliert eine Bestellung zu den vorliegenden Zahlen hinzu. Anhand der
			vorliegenden Bestellung wird das Blatt mit dem entsprechenden Schlssel
			gesucht. Falls dieses Blatt noch nicht existiert, wird es zum Knoten
			hinzugefgt. Danach wird die Akkumulierungsfunktion addOrder des Blattes
			aufgerufen.
			\param order		Zu akkumulierende Bestellung
			\param table		Der Tisch, zu der die Bestellnug geh?t.
			\brief Bestellung akkumulieren.
			\sa createLeaf, getKey
		*/
		virtual void	walkOrders(TTableOrder* order, TTable* table);
		virtual void	exportCSV(const QString& path, TValue& value, QTextStream &st, QStringList& tags)
		{
			TReportNode::exportCSV(path, value);
			TReportLeaf::exportCSV(value, st, tags);
		}
//		virtual void	exportCSV(QTextStream &st, QStringList& tags);
	#if QT_VERSION>=300
		QDomElement		exportXml(QDomDocument& doc, QDomElement& root)
		{
			QDomElement el = TReportNode::exportXml(doc, root);
//			el.setAttribute("Maximum", m_Maximum);
			TReportLeaf::exportXml(doc, el);
			return el;
		}
	#endif
		uint		count() const
		{
			return TReportNode::count();
		}
	protected:
		/*!	Zu berschreibende Funktion, die ein neues Bestellblatt anlegt in
			Anh?gigkeit der bergebenen Bestellung und des Tisches.
			Per default wird kein Blatt erzeugt.
			\param order		Die fr das Blatt zu verwendende Bestellung
			\param table		Der Tisch, zu der die Bestellnug geh?t.
			\return Liefert das erzeugte Blatt oder NULL.
			\note Wird nur von walkOrders ben?igt.
			\brief Bestellblatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable* table)
		{
			order = order; table=table;
			return NULL;
		}
		/*!	Zu berschreibende Funktion, die den Schlssel zu einem Blatt liefert.
			Die Schlssel der Bl?ter steuern die Anzahl der von einem Report
			erzeugten Daten.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			\param table		Der Tisch, zu der die Bestellung geh?t.
			\return Liefert den Schlssel fr ein Blatt.
			\note Wird nur von walkOrders ben?igt.
			\brief Bestellblatt-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable* table)
		{
			order=order; table=table;
			return QString::number(TReportNode::count()+1);
		}
//	protected:
//		int			m_Maximum;					//!< Gr?er eingefgter Schlssel
	};

	/*!	Diese Klasse akkumuliert alle Bestellungen. Zus?zlich zu den von TRepOrderNode
		bereitgestellten Daten stehen folgende Informationen zur Verfgung:
		- getOrderCount()	Liefert die Anzahl Bestellvorg?ge.
		\note Dieser Knoten besitzt keine Bl?ter.
		\brief POS-Klassen: Report-Knoten, Bestellungen.
	*/
	class			TRepOrders
	: public TRepOrderNode
	{
		static const char	entryOrderCount[];
	public:
		/*!	Erzeuge eine Instanz eines Bestell-Knotens.
			\brief ctor
		*/
		TRepOrders()
		: TRepOrderNode("orders")
		{
		}
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable* table)
		{
			return new TRepOrder(order, table);
		}
		virtual QString		getKey(TTableOrder* order, TTable* table)
		{
			int idx = 0;
			if( table->hasPayform() )
				idx = table->getPayform();
			else
			{
				TTableEntries* pays = table->getOldPays();
				if( pays )
				{
					int count = 0;
					TTableEntryIt it(*pays);
					for(TTablePay* pay = (TTablePay*) it.toFirst(); pay; pay=(TTablePay*)++it)
					{
						if( pay->wasVoided() )
							continue;
						if( count )
						{
							idx = 0;
							break;
						}
						count++;
						idx = pay->getPayform();
					}
					delete pays;
				}
			}
			return QString::number(order->getPlu()*1000+idx);
		}
		/*!	?erschreibt die add Funktion der Basisklasse, um die Anzahl entsprechend
			anzupassen.
			\param order		Zu akkumulierende Bestellung.
			\return Liefert FALSE, wenn die Bestellung nicht akkumuliert werden soll.
		*/
		virtual bool	addOrder(TTableOrder* order, TTable* tbl)
		{
			if( !TRepOrderNode::addOrder(order, tbl) )
				return FALSE;
			incOrderCount();
			return TRUE;
		}
		/*!	\return Liefert die Anazhl der Bestellvorg?ge, im Gegensatz zu getCount,
			welches die Anzahl der bestellten Posten zurckliefert.
			\brief Anzahl Bestellvorg?ge ermitteln.
		*/
		int			getOrderCount() const
		{
			return getValue(entryOrderCount, 0);
		}
	protected:
		/*!	?erschreibt die Funktion, um nur einen Eintrag fr alle Bestellungen
			zu erhalten. Diese werden im Knoten "0" gesammelt.
			param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Immer "0".
			\brief Bestellungen-Schlssel erzeugen.
		virtual QString		getKey(TTableOrder*, TTable*)
		{
			return "0";
		}
		*/
		/*!	Hilfsfnuktion, erh?t bei jedem Aufruf die Anzahl der Bestellungen um 1.
			\brief Anzahl der Bestellungen erh?en.
		*/
		void		incOrderCount()
		{
			inc(entryOrderCount);
		}
	};

	/*!	Diese Klasse bildet den Iterator ber die Bestell-Knoten
		\brief POS-Klassen: Iterator fr Bestell-Knoten.
	*/
	class			TRepOrdersIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Bestellknoten-Iterators.
			\param fams		Die Bestellungen, ber den iteriert wird.
			\brief ctor
		*/
		TRepOrdersIt(const TRepOrders& orders)
		: TReportNodeIt(orders)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepOrder*	operator () ()
		{
			return (TRepOrder*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepOrder*	toFirst()
		{
			return (TRepOrder*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepOrder*	current()
		{
			return (TRepOrder*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepOrder*	operator ++ ()
		{
			return (TRepOrder*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse akkumuliert alle Warengruppen. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepFamily.
		\brief POS-Klassen: Report-Knoten, Warengruppen.
	*/
	class			TRepFamilies
	: public TRepOrderNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Warengruppen-Knotens.
			\brief ctor
		*/
		TRepFamilies()
		: TRepOrderNode(TFamilies::listName)
		{
		}
	protected:
		/*!	Erzeugt ein neues Warengruppen-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Warengrupen-Informationen.
			\return Das neu erzeugte Warengruppen-Blatt.
			\brief Warengruppen-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable*)
		{
			return new TRepFamily(order);
		}
		/*!	?erschreibt die Funktion, der Schlssel der Warengruppe bildet dessen ID.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID der Warengruppe als Schlssel.
			\brief Warengruppen-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			return QString::number(order->getFamily());
		}
	};

	/*!	Diese Klasse bildet den Iterator ber die Warengruppen-Knoten
		\brief POS-Klassen: Iterator fr Warengruppen-Knoten.
	*/
	class			TRepFamiliesIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Warengruppenknoten-Iterators.
			\param fams		Die Warengruppen, ber den iteriert wird.
			\brief ctor
		*/
		TRepFamiliesIt(const TRepFamilies& fams)
		: TReportNodeIt(fams)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepFamily*	operator () ()
		{
			return (TRepFamily*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepFamily*	toFirst()
		{
			return (TRepFamily*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepFamily*	current()
		{
			return (TRepFamily*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepFamily*	operator ++ ()
		{
			return (TRepFamily*) TReportNodeIt::operator ++();
		}
	};
	/*!	Diese Klasse akkumuliert alle Oberwarengruppen. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepFamGroup.
		\brief POS-Klassen: Report-Knoten, Oberwarengruppen.
	*/
	class			TRepFamGroups
	: public TRepOrderNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Oberwarengruppen-Knotens.
			\brief ctor
		*/
		TRepFamGroups()
		: TRepOrderNode(TFamGroups::listName)
		{
		}
	protected:
		/*!	Erzeugt ein neues Oberwarengruppen-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Oberwarengrupen-Informationen.
			\return Das neu erzeugte Oberwarengruppen-Blatt.
			\brief Oberwarengruppen-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable*)
		{
			return new TRepFamGroup(order);
		}
		/*!	?erschreibt die Funktion, der Schlssel der Warengruppe bildet dessen ID.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID der Warengruppe als Schlssel.
			\brief Warengruppen-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			return QString::number(order->getFamGroup());
		}
	};
	/*!	Diese Klasse bildet den Iterator ber die Warengruppen-Knoten
		\brief POS-Klassen: Iterator fr Warengruppen-Knoten.
	*/
	class			TRepFamGroupsIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Warengruppenknoten-Iterators.
			\param fams		Die Warengruppen, ber den iteriert wird.
			\brief ctor
		*/
		TRepFamGroupsIt(const TRepFamGroups& fams)
		: TReportNodeIt(fams)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepFamGroup*	operator () ()
		{
			return (TRepFamGroup*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepFamGroup*	toFirst()
		{
			return (TRepFamGroup*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepFamGroup*	current()
		{
			return (TRepFamGroup*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepFamGroup*	operator ++ ()
		{
			return (TRepFamGroup*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse akkumuliert alle Oberwarengruppen. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepFamGroup.
		\brief POS-Klassen: Report-Knoten, Oberwarengruppen.
	*/
	class			TRepDiscounts
	: public TRepOrderNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Oberwarengruppen-Knotens.
			\brief ctor
		*/
		TRepDiscounts()
		: TRepOrderNode(TDiscounts::listName)
		{
		}
	protected:
		/*!	Erzeugt ein neues Oberwarengruppen-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Oberwarengrupen-Informationen.
			\return Das neu erzeugte Oberwarengruppen-Blatt.
			\brief Oberwarengruppen-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable*)
		{
			return new TRepDiscount(order);
		}
		/*!	?erschreibt die Funktion, der Schlssel der Warengruppe bildet dessen ID.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID der Warengruppe als Schlssel.
			\brief Warengruppen-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			QStringList lines = order->getDiscInfo();
			if( !lines.count() )
				return QString::null;
//			if( lines.count()>1 )
//				return QString("0");
			return QString::number(lines[1].toInt());
//			return QString::number(order->getPlu());
//			return QString::number(order->getPlu()*1000+lines[1].toInt());
		}
	};

	/*!	Diese Klasse bildet den Iterator ber die Warengruppen-Knoten
		\brief POS-Klassen: Iterator fr Warengruppen-Knoten.
	*/
	class			TRepDiscountsIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Warengruppenknoten-Iterators.
			\param fams		Die Warengruppen, ber den iteriert wird.
			\brief ctor
		*/
		TRepDiscountsIt(const TRepDiscounts& fams)
		: TReportNodeIt(fams)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepDiscount*	operator () ()
		{
			return (TRepDiscount*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepDiscount*	toFirst()
		{
			return (TRepDiscount*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepDiscount*	current()
		{
			return (TRepDiscount*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepDiscount*	operator ++ ()
		{
			return (TRepDiscount*) TReportNodeIt::operator ++();
		}
	};
//#if 0
	/*!	Diese Klasse akkumuliert alle Sparten. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepDepartment.
		\brief POS-Klassen: Report-Knoten, Sparten.
	*/
	class			TRepDepartments
	: public TRepOrderNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Sparten-Knotens.
			\brief ctor
		*/
		TRepDepartments(const char* path)
		: TRepOrderNode(TDepartments::listName)
		{
			m_Departments.load(path);
		}
		virtual void	walkOrders(TTableOrder* order, TTable* table);
	protected:
		bool		checkDepartments(TTableOrder* order, TDepartment* dep);
		/*!	Erzeugt ein neues Sparten-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Warengrupen-Informationen.
			\return Das neu erzeugte Warengruppen-Blatt.
			\brief Warengruppen-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable*)
		{
			return new TRepDepartment(order);
		}
		/*!	?erschreibt die Funktion, der Schlssel der Warengruppe bildet dessen ID.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID der Warengruppe als Schlssel.
			\brief Warengruppen-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			return QString::number((int)order->getValue("department", 0));
		}
	protected:
		TDepartments	m_Departments;
	};

	/*!	Diese Klasse bildet den Iterator ber die Sparten-Knoten
		\brief POS-Klassen: Iterator fr Sparten-Knoten.
	*/
	class			TRepDepartmentsIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Spartenknoten-Iterators.
			\param fams		Die Sparten, ber den iteriert wird.
			\brief ctor
		*/
		TRepDepartmentsIt(const TRepDepartments& deps)
		: TReportNodeIt(deps)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepDepartment*	operator () ()
		{
			return (TRepDepartment*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepDepartment*	toFirst()
		{
			return (TRepDepartment*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepDepartment*	current()
		{
			return (TRepDepartment*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepDepartment*	operator ++ ()
		{
			return (TRepDepartment*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse akkumuliert alle Artikel. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepArticle.
		\brief POS-Klassen: Report-Knoten, Artikel.
	*/
	class			TRepArticles
	: public TRepOrderNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Artikel-Knotens.
			\brief ctor
		*/
		TRepArticles()
		: TRepOrderNode(TArticles::listName)
		{
		}
	protected:
		/*!	Erzeugt ein neues Artikel-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Artikel-Informationen.
			\return Das neu erzeugte Artikel-Blatt.
			\brief Artikel-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable*)
		{
			return new TRepArticle(order);
		}
		/*!	?erschreibt die Funktion, der Schlssel des Artikels bildet dessen ID.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID des Artikels als Schlssel.
			\brief Artikel-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			QString key = QString::number(order->getPlu()).rightJustify(10, '0');
			QString pre;
			if( order->getValue(TTableOrder::entryDiscount, 0) )
				pre = "_2";
			else
			{
				TRepArticle* tmp = (TRepArticle*) find(key+"_0");
				if( !tmp )
					tmp = (TRepArticle*) find(key+"_2");
				if( tmp && tmp->getPrice()!=order->getPrice() )
					pre = "_1";
			}
			if( pre.isEmpty() )
				pre = "_0";
			return key+pre;
		}
	};

	class			TRepNegOrders
	: public TRepOrderNode
	{
	public:
		TRepNegOrders()
		: TRepOrderNode("negorders")
		{
		}
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable* /*table*/)
		{
//		qDebug(QString("have order %1: %2 / %3").arg(order->getPlu()).arg(order->getCount()).arg(getMaximum()));
			return new TRepArticle(order);
		}
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			if( order->getCount()>0 )
				return QString::null;
			QString key = QString::number(order->getPlu());
//		qDebug(QString("Have key %1").arg(key));
			return key;
		}
	};


	/*!	Diese Klasse bildet den Iterator ber die Artikel-Knoten
		\brief POS-Klassen: Iterator fr Artikel-Knoten.
	*/
	class			TRepArticlesIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Artikel-Iterators.
			\param arts		Die Artikel, ber den iteriert wird.
			\brief ctor
		*/
		TRepArticlesIt(const TRepArticles& arts)
		: TReportNodeIt(arts)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepArticle*	operator () ()
		{
			return (TRepArticle*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepArticle*	toFirst()
		{
			return (TRepArticle*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepArticle*	current()
		{
			return (TRepArticle*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepArticle*	operator ++ ()
		{
			return (TRepArticle*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse akkumuliert alle Artikel. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepHourLeaf.\n
		Stunden-Bl?ter und -Knoten beinhalten eine Auswertung der Daten pro Stunde.
		\brief POS-Klassen: Report-Knoten, Stunden.
	*/
	class			TRepHours
	: public TRepOrderNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Stunden-Knotens.
			\brief ctor
		*/
		TRepHours()
		: TRepOrderNode("Hours")
		{
		}
	protected:
		/*!	Erzeugt ein neues Stunden-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Stunden-Informationen.
			\return Das neu erzeugte Stunden-Blatt.
			\brief Stunden-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable*)
		{
			return new TRepHour(order);
		}
		/*!	?erschreibt die Funktion, der Schlssel der Stunde ist die Stunde selbst.
			\param order		Die fr den Schlssel zu verwendende Bestellung
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die Stunde als Schlssel.
			\brief Stunden-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable*)
		{
			return QString::number(order->getTime().hour());
		}
	};

	/*!	Diese Klasse bildet den Iterator ber die Stunden-Knoten
		\brief POS-Klassen: Iterator fr Stunden-Knoten.
	*/
	class			TRepHoursIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Stunden-Iterators.
			\param hours		Die Stunden, ber die iteriert wird.
			\brief ctor
		*/
		TRepHoursIt(const TRepHours& hours)
		: TReportNodeIt(hours)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepHour*	operator () ()
		{
			return (TRepHour*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepHour*	toFirst()
		{
			return (TRepHour*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepHour*	current()
		{
			return (TRepHour*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepHour*	operator ++ ()
		{
			return (TRepHour*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse akkumuliert alle Mws-S?ze. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepVatRate.\n
		Mwst-Bl?ter und -Knoten beinhalten eine Auswertung der Daten pro Mehrwert
		steuersatz.
		\brief POS-Klassen: Report-Knoten, Mehrwertsteuers?ze.
	*/
	class			TRepVatRates
	: public TRepOrderNode
	{
		static const char	entrySumIH[];
		static const char	entrySumOH[];
	public:
		/*!	Erzeuge eine Instanz eines Mwst-Steuer-Knotens.
			\brief ctor
		*/
		TRepVatRates()
		: TRepOrderNode(TVatRates::listName)
		, m_Taxes(FALSE)
		{
		}
		bool		hasTaxes() const
		{
			return m_Taxes;
		}
		long		getSumIH()
		{
			return getValue(entrySumIH);
		}
		long		getSumOH()
		{
			return getValue(entrySumOH);
		}
		virtual void	walkOrders(TTableOrder* order, TTable* table);
		virtual bool	addOrder(TTableOrder* order, TTable*);
		void		addTax(TTable* table, TTaxGroup* grp);
	protected:
		/*!	Erzeugt ein neues Mwstsatz-Blatt anhand der in order angegebenen
			Daten.
			\param order		Bestellvorgang mit den Stunden-Informationen.
			\return Das neu erzeugte Mwst-Satz-Blatt.
			\brief Mwst-Satz-Blatt erzeugen.
		*/
		virtual TRepOrderLeaf*	createLeaf(TTableOrder* order, TTable* tbl)
		{
			return new TRepVatRate(order, tbl);
		}
		/*!	?erschreibt die Funktion, der Schlssel ist das Flag, ob der Tisch auf
			Au?r-Haus oder auf In-Haus bezahlt wurde.
			param order		Die fr den Schlssel zu verwendende Bestellung
			\param table	Der Tisch, zu der die Bestellung geh?t.
			\return 1 wenn .
			\brief Mwstsatz-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableOrder* order, TTable* table)
		{
			bool oh = table->isVatOH();
			if( order->hasVatOH() )
				oh = order->isVatOH();
			return QString::number(oh?order->getRateOH():order->getRateIH());
		}
		void		doTaxes(TTable* table, TTableOrder* order=NULL);
	protected:
		bool		m_Taxes;
	};

	/*!	Diese Klasse bildet den Iterator ber die Mwst-Knoten
		\brief POS-Klassen: Iterator fr Mwst-Knoten.
	*/
	class			TRepVatRatesIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Mwst-Iterators.
			\param arts		Die Mwst-S?ze, ber die iteriert wird.
			\brief ctor
		*/
		TRepVatRatesIt(const TRepVatRates& rates)
		: TReportNodeIt(rates)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepVatRate*	operator () ()
		{
			return (TRepVatRate*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepVatRate*	toFirst()
		{
			return (TRepVatRate*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepVatRate*	current()
		{
			return (TRepVatRate*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepVatRate*	operator ++ ()
		{
			return (TRepVatRate*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse bildet die Basis fr alle Storno-Knoten eines Reportes.
		Diese Knoten akkumulieren jeweils die Anzahl und den Betrag von Storno-Bl?tern.
		\brief POS-Klassen: Report-Knoten, Stornierungen.
		\sa TRepVoidLeaf
	*/
	class			TRepVoidNode
	: public TReportNode
	, public TRepVoidLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Reportknotens.
			\brief ctor
		*/
		TRepVoidNode(const QString& name)
		: TReportNode(name)
		, TRepVoidLeaf()
		{
		}
		/*!	Akkumuliert ein Storno zu den vorliegenden Zahlen hinzu. Anhand des
			vorliegenden Stornos wird das Blatt mit dem entsprechenden Schlssel
			gesucht. Falls dieses Blatt noch nicht existiert, wird es zum Knoten
			hinzugefgt. Danach wird die Akkumulierungsfunktion addVoid des Blattes
			aufgerufen.
			\param _void		Zu akkumulierendes Storno
			\param table		Der Tisch, zu der das Storno geh?t.
			\brief Stornos akkumulieren.
			\sa createLeaf, getKey
		*/
		virtual void	walkVoids(TTableVoid* _void, TTable* table);
		virtual void	exportCSV(const QString& path, TValue& value, QTextStream &st, QStringList& tags)
		{
			TReportNode::exportCSV(path, value);
			TReportLeaf::exportCSV(value, st, tags);
		}
	#if QT_VERSION>=300
		QDomElement		exportXml(QDomDocument& doc, QDomElement& root)
		{
			QDomElement el = TReportNode::exportXml(doc, root);
//			el.setAttribute("Maximum", m_Maximum);
			TReportLeaf::exportXml(doc, el);
			return el;
		}
	#endif
	protected:
		/*!	Zu berschreibende Funktion, die ein neues Stornoblatt anlegt in
			Anh?gigkeit des bergebenen Stornos und des Tisches.
			Per default wird kein Blatt erzeugt.
			\param order		Die fr das Blatt zu verwendende Bestellung
			\param table		Der Tisch, zu der die Bestellnug geh?t.
			\return Liefert das erzeugte Blatt oder NULL.
			\note Wird nur von walkVoids ben?igt.
			\brief Stornoblatt erzeugen.
		*/
		virtual TRepVoidLeaf*	createLeaf(TTableVoid* _void, TTable* table)
		{
			_void=_void; table=table;
			return NULL;
		}
		/*!	Zu berschreibende Funktion, die den Schlssel zu einem Blatt liefert.
			Die Schlssel der Bl?ter steuern die Anzahl der von einem Report
			erzeugten Daten.
			\param _void		Das fr den Schlssel zu verwendende Storno
			\param table		Der Tisch, zu der das Storno geh?t.
			\return Liefert den Schlssel fr ein Blatt.
			\note Wird nur von walkVoids ben?igt.
			\brief Stornoblatt-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableVoid* _void, TTable* table)
		{
			_void=_void; table=table;
			return QString::number(TReportNode::count()+1);
		}
	};

	/*!	Diese Klasse akkumuliert alle Stornos. Zus?zlich zu den von TRepVoidNode
		bereitgestellten Daten stehen folgende Informationen zur Verfgung:
		- getVoidCount()	Liefert die Anzahl Stornovorg?ge.
		\note Dieser Knoten besitzt keine Bl?ter.
		\brief POS-Klassen: Report-Knoten, Stornos.
	*/
	class			TRepVoids
	: public TRepVoidNode
	{
		static const char	entryVoidCount[];
	public:
		/*!	Erzeuge eine Instanz eines Storno-Knotens.
			\brief ctor
		*/
		TRepVoids()
		: TRepVoidNode("Voids")
		{
		}
		/*!	?erschreibt die addVoid Funktion der Basisklasse, um die Anzahl entsprechend
			anzupassen.
			\param _void		Zu akkumulierendes Storno.
			\return Liefert FALSE, wenn das Storno nicht akkumuliert werden soll.
		*/
		virtual bool	addVoid(TTableVoid* _void)
		{
			if( !TRepVoidNode::addVoid(_void) )
				return FALSE;
			incVoidCount();
			return TRUE;
		}
		/*!	\return Liefert die Anazhl der Stornovorg?ge, im Gegensatz zu getCount,
			welches die Anzahl der stornierten Posten zurckliefert.
			\brief Anzahl Stornovorg?ge ermitteln.
		*/
		long		getVoidCount() const
		{
			return getValue(entryVoidCount, 0);
		}
	protected:
		/*!	?erschreibt die Funktion, um nur einen Eintrag fr alle Stornos
			zu erhalten. Diese werden im Knoten "0" gesammelt.
			param _void		Das fr den Schlssel zu verwendende Storno
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Immer "0".
			\brief Storno-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableVoid* _void, TTable* table)
		{
			_void = _void; table=table;
			return "0";
		}
		/*!	Hilfsfnuktion, erh?t bei jedem Aufruf die Anzahl der Stornos um 1.
			\brief Anzahl der Stornos erh?en.
		*/
		void		incVoidCount()
		{
			inc(entryVoidCount);
		}
	};

	/*!	Diese Klasse akkumuliert alle stornierten Artikel. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepVArticle.
		\brief POS-Klassen: Report-Knoten, stornierte Artikel.
	*/
	class			TRepVArticles
	: public TRepVoidNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Artikel-Knotens.
			\brief ctor
		*/
		TRepVArticles()
		: TRepVoidNode("VArticles")
		{
		}
	protected:
		/*!	Erzeugt ein neues Storno-Artikel-Blatt anhand der in _void angegebenen
			Daten.
			\param _void		Stornovorgang mit den Artikel-Informationen.
			\return Das neu erzeugte Storno-Artikel-Blatt.
			\brief Storno-Artikel-Blatt erzeugen.
		*/
		virtual TRepVoidLeaf*	createLeaf(TTableVoid* _void, TTable*)
		{
			if( !_void )
				return NULL;
			return new TRepVArticle(_void);
		}
		/*!	?erschreibt die Funktion, der Schlssel des Artikels bildet dessen ID.
			\param _void		Das fr den Schlssel zu verwendende Storno
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID des Artikels als Schlssel.
			\brief Storno-Artikel-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableVoid* _void, TTable*)
		{
			return QString::number(_void->getPlu());
		}
	};

	/*!	Diese Klasse bildet den Iterator ber die Storno-Artikel-Knoten
		\brief POS-Klassen: Iterator fr Storno-Artikel-Knoten.
	*/
	class			TRepVArticlesIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Artikel-Iterators.
			\param arts		Die Artikel, ber den iteriert wird.
			\brief ctor
		*/
		TRepVArticlesIt(const TRepVArticles& arts)
		: TReportNodeIt(arts)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepVArticle*	operator () ()
		{
			return (TRepVArticle*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepVArticle*	toFirst()
		{
			return (TRepVArticle*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepVArticle*	current()
		{
			return (TRepVArticle*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepVArticle*	operator ++ ()
		{
			return (TRepVArticle*) TReportNodeIt::operator ++();
		}
	};

	/*!	Diese Klasse sammelt alle Storno-Vorg?ge. Dieser Knoten umfa? alle Bl?ter
		vom Typ TRepVoid.
		\brief POS-Klassen: Report-Knoten, stornierte Artikel.
	*/
	class			TRepAllVoids
	: public TRepVoidNode
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Knotens.
			\brief ctor
		*/
		TRepAllVoids()
		: TRepVoidNode("AllVoids")
		{
		}
	protected:
		/*!	Erzeugt ein neues Storno-Artikel-Blatt anhand der in _void angegebenen
			Daten.
			\param _void		Stornovorgang mit den Artikel-Informationen.
			\return Das neu erzeugte Storno-Artikel-Blatt.
			\brief Storno-Artikel-Blatt erzeugen.
		*/
		virtual TRepVoidLeaf*	createLeaf(TTableVoid* _void, TTable* table)
		{
			if( !_void )
				return NULL;
			return new TRepVoid(_void, table);
		}
		/*!	?erschreibt die Funktion, der Schlssel des Artikels bildet dessen ID.
			\param _void		Das fr den Schlssel zu verwendende Storno
			param table		Der Tisch, zu der die Bestellung geh?t.
			\return Die ID des Artikels als Schlssel.
			\brief Storno-Artikel-Schlssel erzeugen.
		*/
		virtual QString		getKey(TTableVoid* _void, TTable*)
		{
			QString tmp;
			tmp.sprintf("%02d%02d%02d%02d%02d%02d"
				, _void->getDate().year(), _void->getDate().month(), _void->getDate().day()
				, _void->getTime().hour(), _void->getTime().minute(), _void->getTime().second()
				);
			return tmp;
		}
	};

	/*!	Diese Klasse bildet den Iterator ber die Storno-Knoten
		\brief POS-Klassen: Iterator fr Storno-Knoten.
	*/
	class			TRepAllVoidsIt
	: public TReportNodeIt
	{
	public:
		/*!	Erzeuge eine Instanz eines Storno-Iterators.
			\param voids		Die Stornos, ber die iteriert wird.
			\brief ctor
		*/
		TRepAllVoidsIt(const TRepAllVoids& voids)
		: TReportNodeIt(voids)
		{
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepVoid*		operator () ()
		{
			return (TRepVoid*) TReportNodeIt::operator()();
		}
		/*!	\return Liefert das erste Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Erstes Element abrufen.
		*/
		TRepVoid*		toFirst()
		{
			return (TRepVoid*) TReportNodeIt::toFirst();
		}
		/*!	\return Liefert das aktuelle Element des Iterators, oder NULL, falls
			kein Element vorliegt.
			\brief Aktuelles Element abrufen.
		*/
		TRepVoid*		current()
		{
			return (TRepVoid*) TReportNodeIt::current();
		}
		/*!	\return Liefert das n?hste Element des Iterators, oder NULL, falls
			kein mehr Element vorliegt.
			\brief N?hstes Element abrufen.
		*/
		TRepVoid*		operator ++ ()
		{
			return (TRepVoid*) TReportNodeIt::operator ++();
		}
	};





	/*!	Diese Klasse bildet die Basis fr alle Bezahl-Knoten eines Reportes.
		Diese Knoten akkumulieren jeweils die Anzahl und den Betrag von Bezahl-Bl?tern.
		\brief POS-Klassen: Report-Knoten, Bezahlungen.
	*/
	class			TRepPayNode
	: public TReportNode
	, public TRepPayLeaf
	{
	public:
		/*!	Erzeuge eine Instanz eines Bezahl-Reportknotens.
			\brief ctor
		*/
		TRepPayNode(const QString& name)
		: TReportNode(name)
//		, m_Maximum(0)
		{
		}
		TReportLeaf*	find(const QString& key)
		{
			return TReportNode::find(key);
		}
/*		int			getMaximum()
		{
			return m_Maximum;
		}*/
		virtual void	walkPays(TTablePay* pay, TTable* table);
		virtual void	exportCSV(const QString& path, TValue& value, QTextStream &st, QStringList& tags)
		{
			TReportNode::exportCSV(path, value);
			TReportLeaf::exportCSV(value, st, tags);
		}
	#if QT_VERSION>=300
		QDomElement		exportXml(QDomDocument& doc, QDomElement& root)
		{
			QDomElement el = TReportNode::exportXml(doc, root);
//			el.setAttribute("Maximum", m_Maximum);
			TReportLeaf::exportXml(doc, el);
			return el;
		}
	#endif
		uint		count() const
		{
			return TReportNode::count();
		}
	protected:
		virtual TRepPayLeaf*	createLeaf(TTablePay*, TTable*)
		{
			return NULL;
		}
		virtual QString		getKey(TTablePay*, TTable*)
		{
			return QString::number(TReportNode::count()+1);
		}
//	protected:
//		int			m_Maximum;
	};

	/*!	Report-Eintrag aller Bezahlvorg?ge, beinhaltet:
		* Anzahl der Bezahlungen
		* Betrag aller Bezahlungen
	*/
	class			TRepPays
	: public TRepPayNode
	{
		static const char	entrySum[];
		static const char	entryCashSum[];
	public:
		TRepPays()
		: TRepPayNode("Payments")
		{
		}
		long		getTip() const
		{
			return getValue(TTablePay::entryTip, 0);
		}
		long		getSum() const
		{
			return getValue(entrySum, 0);
		}
		long		getCashSum() const
		{
			return getValue(entryCashSum, 0);
		}
		virtual bool	addPay(TTablePay* pay)
		{
			if( !TRepPayNode::addPay(pay) )
				return FALSE;
			addTip(pay->getTip());
			addGiven(pay->getGiven());
			addReturn(pay->getReturn());
/*
			if(pay->inSummary() )
				add(pay->getAmount(), entrySum);
*/
			if(pay->inCashSummary() )
				add(pay->getAmount(), entryCashSum);
			return TRUE;
		}
	protected:
		void		addTip(long tip)
		{
			add(tip, TTablePay::entryTip);
		}
		void		addGiven(long given)
		{
			add(given, TTablePay::entryGiven);
		}
		void		addReturn(long ret)
		{
			add(ret, TTablePay::entryReturn);
		}
	};

	class			TRepTables
	: public TRepPayNode
	{
	public:
		TRepTables()
		: TRepPayNode("Tables")
		{
		}
	protected:
		virtual TRepPayLeaf*	createLeaf(TTablePay* pay, TTable* table)
		{
			return new TRepTable(pay, table);
		}
		virtual QString		getKey(TTablePay* pay, TTable* table)
		{
			QString tmp;
			tmp.sprintf("%4d%02d%02d%8ld"
				, pay->getDate().year(), pay->getDate().month(), pay->getDate().day()
				, table->getArchive());
			return tmp;
		}
	};

	class			TRepTablesIt
	: public TReportNodeIt
	{
	public:
		TRepTablesIt(const TRepTables& tables)
		: TReportNodeIt(tables)
		{
		}
		TRepTable*		operator () ()
		{
			return (TRepTable*) TReportNodeIt::operator()();
		}
		TRepTable*		toFirst()
		{
			return (TRepTable*) TReportNodeIt::toFirst();
		}
		TRepTable*		current()
		{
			return (TRepTable*) TReportNodeIt::current();
		}
		TRepTable*		operator ++ ()
		{
			return (TRepTable*) TReportNodeIt::operator ++();
		}
	};

	class			TRepSubvents
	: public TRepPayNode
	{
	public:
		TRepSubvents()
		: TRepPayNode("Subventions")
		, count(0)
		{
		}
	int getcount()
	{
		return count;
	}
	protected:
		virtual TRepPayLeaf*	createLeaf(TTablePay* pay, TTable* table)
		{
			return new TRepSubvent(pay, table);
		}
		virtual QString		getKey(TTablePay* pay, TTable* /*table*/)
		{
			if( !pay->getSubId() )
				return QString::null;
			count++;
			int idx = pay->getPayform();
			/*
			if( table->hasPayform() )
				idx = table->getPayform();*/
			return QString::number(idx*1000+pay->getSubId());
		}
	int count;
	};

	class			TRepSubventsIt
	: public TReportNodeIt
	{
	public:
		TRepSubventsIt(const TRepSubvents& subs)
		: TReportNodeIt(subs)
		{
		}
		TRepSubvent*	operator () ()
		{
			return (TRepSubvent*) TReportNodeIt::operator()();
		}
		TRepSubvent*	toFirst()
		{
			return (TRepSubvent*) TReportNodeIt::toFirst();
		}
		TRepSubvent*	current()
		{
			return (TRepSubvent*) TReportNodeIt::current();
		}
		TRepSubvent*	operator ++ ()
		{
			return (TRepSubvent*) TReportNodeIt::operator ++();
		}
	};

	class			TRepPayforms
	: public TRepPayNode
	{
	public:
		TRepPayforms()
		: TRepPayNode(TPayforms::listName)
		{
		}
		TRepPayforms(const QString& name)
		: TRepPayNode(name)
		{
		}
	protected:
		virtual TRepPayLeaf*	createLeaf(TTablePay* pay, TTable* tbl)
		{
			return new TRepPayform(pay, tbl);
		}
		virtual QString		getKey(TTablePay* pay, TTable* /*table*/)
		{
			/*if( table->hasPayform() )
				return table->getPayformName().replace("\n", "")+"_"+QString::number(table->getPayform());*/
			return pay->getPayformName().replace("\n", "")+"_"+QString::number(pay->getPayform());
			/*+"_"+*//*+"_"+QString::number(pay->getCurrency())*/
		}
	};

	class			TRepPayformsIt
	: public TReportNodeIt
	{
	public:
		TRepPayformsIt(const TRepPayforms& forms)
		: TReportNodeIt(forms)
		{
		}
		TRepPayform*	operator () ()
		{
			return (TRepPayform*) TReportNodeIt::operator()();
		}
		TRepPayform*	toFirst()
		{
			return (TRepPayform*) TReportNodeIt::toFirst();
		}
		TRepPayform*	current()
		{
			return (TRepPayform*) TReportNodeIt::current();
		}
		TRepPayform*	operator ++ ()
		{
			return (TRepPayform*) TReportNodeIt::operator ++();
		}
	};

	class			TRepCurrencies
	: public TRepPayforms
	{
	public:
		TRepCurrencies()
		: TRepPayforms(TCurrencies::listName)
		{
		}
	protected:
		virtual QString		getKey(TTablePay* pay, TTable*)
		{
			if( !pay->inCashSummary() ) {
			qDebug("Is NULL not in sum gedöhns");
				return QString::null;
			}

			QString ret = QString::number(count()+1);
//			qDebug(QString("REPCURRENCIES *%1 Amount=%2: %3, %4").arg(ret).arg(pay->getAmount()).arg(pay->getGiven()).arg(pay->getReturn()));
			return ret;
/* Seltsame Sache: mit den "richtigen" Schlüsseln gehts nicht
			QString ret = QString::number(pay->getPayform())+"_";
			int idx = pay->getCurrency();
			ret = ret+QString::number(idx);
		qDebug(QString("REPCURRENCIES *%1 Key=%2: %3, %4, %5").arg(count()+1).arg(ret).arg(pay->getAmount()).arg(pay->getGiven()).arg(pay->getReturn()));
			return ret;
*/
		}
	};

	class			TRepCurrReturns
	: public TRepPayforms
	{
	public:
		TRepCurrReturns()
		: TRepPayforms("currreturns")
		{
		}
	protected:
		virtual TRepPayLeaf*	createLeaf(TTablePay* pay, TTable* tbl)
		{
			return new TRepPayform(pay, tbl, TRUE);
		}
		virtual QString		getKey(TTablePay* /*pay*/, TTable*)
		{
			QString ret = QString::number(count()+1);
//			qDebug(QString("REPCURRETURNS *%1 Amount=%2: %3, %4").arg(ret).arg(pay->getAmount()).arg(pay->getGiven()).arg(pay->getReturn()));
			return ret;
/* Seltsame Sache: mit den "richtigen" Schlüsseln gehts nicht
			QString ret = QString::number(pay->getPayform())+"_";
			int idx = pay->getRetCurrency();
			if( !idx )
				idx = pay->getCurrency();
			ret = ret+QString::number(idx);
		qDebug(QString("REPCURRETURNS *%1 Key=%2: %3, %4, %5").arg(count()+1).arg(ret).arg(pay->getAmount()).arg(pay->getGiven()).arg(pay->getReturn()));
			return ret;
			*/
//			return QString::number(pay->getCurrency())+"_"+QString::number(pay->getPayform());
//			return QString::number(pay->getCurrency());
		}
	};

	class			TRepHotelRooms
	: public TRepPayNode
	{
	public:
		TRepHotelRooms()
		: TRepPayNode("hotelrooms")
		{
		}
	protected:
		virtual TRepPayLeaf*	createLeaf(TTablePay* pay, TTable* tbl)
		{
			return new TRepHotelRoom(pay, tbl);
		}
		virtual QString		getKey(TTablePay* pay, TTable* /*table*/)
		{
			if( !pay->hasHotelRoom() )
				return QString::null;
			return pay->getHotelRoom()+"_"+QString::number(pay->getHotelParty())+"_"+QString::number(QDict<TReportLeaf>::count());
		}
	};

	class			TRepHotelRoomIt
	: public TReportNodeIt
	{
	public:
		TRepHotelRoomIt(const TRepHotelRooms& rooms)
		: TReportNodeIt(rooms)
		{
		}
		TRepHotelRoom*	operator () ()
		{			return (TRepHotelRoom*) TReportNodeIt::operator()();
		}
		TRepHotelRoom*	toFirst()
		{
			return (TRepHotelRoom*) TReportNodeIt::toFirst();
		}
		TRepHotelRoom*	current()
		{
			return (TRepHotelRoom*) TReportNodeIt::current();
		}
		TRepHotelRoom*	operator ++ ()
		{
			return (TRepHotelRoom*) TReportNodeIt::operator ++();
		}
	};

	class			TRepCashNode
	: public TReportNode
//	, public TRepCashLeaf
	{
	public:
		TRepCashNode(const QString& name)
		: TReportNode(name)
//		, m_Maximum(0)
		{
		}
		TReportLeaf*	find(const QString& key)
		{
			return TReportNode::find(key);
		}
/*		int			getMaximum()
		{
			return m_Maximum;
		}*/
		virtual void	walkCashs(TTableCashing* cash, TTable* table);
/*		virtual void	exportCSV(const QString& path, TValue& value, QTextStream &st, QStringList& tags)
		{
			TReportNode::exportCSV(path, value);
			TReportLeaf::exportCSV(value, st, tags);
		}
	#if QT_VERSION>=300
		QDomElement		exportXml(QDomDocument& doc, QDomElement& root)
		{
			QDomElement el = TReportNode::exportXml(doc, root);
			el.setAttribute("Maximum", m_Maximum);
			TReportLeaf::exportXml(doc, el);
			return el;
		}
	#endif
*/
	protected:
		virtual TRepCashLeaf*	createLeaf(TTableCashing*, TTable*)
		{
			return NULL;
		}
		virtual QString		getKey(TTableCashing*, TTable*)
		{
			return QString::number(TReportNode::count()+1);
		}
//	protected:
//		int			m_Maximum;
	};

	class			TRepCashNodeIt
	: public TReportNodeIt
	{
	public:
		TRepCashNodeIt(const TRepCashNode& cashs)
		: TReportNodeIt(cashs)
		{
		}
		TRepCashLeaf*	operator () ()
		{			
			return (TRepCashLeaf*) TReportNodeIt::operator()();
		}
		TRepCashLeaf*	toFirst()
		{
			return (TRepCashLeaf*) TReportNodeIt::toFirst();
		}
		TRepCashLeaf*	current()
		{
			return (TRepCashLeaf*) TReportNodeIt::current();
		}
		TRepCashLeaf*	operator ++ ()
		{
			return (TRepCashLeaf*) TReportNodeIt::operator ++();
		}
	};
	
	class			TRepCashIns
	: public TRepCashNode
	{
	public:
		TRepCashIns()
		: TRepCashNode("cashins")
		{
		}
	protected:
		virtual TRepCashLeaf*	createLeaf(TTableCashing* cash, TTable*)
		{
			return new TRepCashLeaf(cash);
		}
		virtual QString		getKey(TTableCashing* cash, TTable* table)
		{
			if( cash->getAmount()<0L )
				return QString::null;
			return TRepCashNode::getKey(cash, table);
//			return QString::number(cash->getRetCurrency());
		}
	};

	class			TRepCashOuts
	: public TRepCashNode
	{
	public:
		TRepCashOuts()
		: TRepCashNode("cashouts")
		{
		}
	protected:
		virtual TRepCashLeaf*	createLeaf(TTableCashing* cash, TTable*)
		{
			return new TRepCashLeaf(cash);
		}
		virtual QString		getKey(TTableCashing* cash, TTable* table)
		{
			if( cash->getAmount()>0L )
				return QString::null;
			return TRepCashNode::getKey(cash, table);
//			return QString::number(cash->getRetCurrency());
		}
	};
}

using namespace PosLib;

#endif

