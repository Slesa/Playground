#ifndef				_POSLIB_TREPORT_

#define				_POSLIB_TREPORT_
#include			"poslib/ttableaction.h"
#include			"poslib/tarchtable.h"
#include			"poslib/ttableinfo.h"
#include			"poslib/ttable.h"
#include			"poslib/treportnode.h"
#include			"poslib/tsumfilter.h"
#include			"basics/tinifile.h"

namespace			PosLib
{
	/*!	\defgroup Reporting Reporting
		Das Reporting der POS-Klassen wurde so konzipiert, da?alle zur Verfgung
		stehenden Daten innerhalb von B?men abgebildet werden. Ein Baum besteht
		aus Bl?tern, die den jeweils zusammengefa?en Daten entsprechen (Artikel,
		Warengruppen usw). Diese werden innerhalb von Knoten gesammelt. In diesen
		Knoten werden auch die Daten der Bl?ter kumuliert.\n
		Den Stamm dieses Baums bilden die Kellner bzw Kostenstellen, je nach
		angefordertem Report. Der Eintrag 0 in diesen Listen bildet die Gesamtsumme
		aller Kellner/Kostenstellen.\n
		Die Aufsummierung der Daten erfolgt in Abh?gigkeit der ausgewerteten Aktion.
		Je nach Datentyp werden die Bestellungen, die Stornos oder die Bezahlungen
		ausgewertet.\n
		Folgende Datentypen k?nen ausgewertet werden:
		- Aktion \link PosLib::TRepOrderNode Bestellungen \endlink
		-# \link PosLib::TRepOrders Bestellungen \endlink
		-# \link PosLib::TRepFamilies Warengruppen \endlink
		-# \link PosLib::TRepFamGroups Oberwarengruppen \endlink
		-# \link PosLib::TRepDiscounts Rabatte \endlink
		-# \link PosLib::TRepArticles Artikel \endlink
		-# \link PosLib::TRepHours Stunden \endlink
		-# \link PosLib::TRepVatRates Mwst-S?ze \endlink
		- Aktion \link PosLib::TRepVoidNode Stornos \endlink
		-# \link PosLib::TRepVoids Stornos \endlink
		-# \link PosLib::TRepVArticles Storno-Artikel \endlink
		-# \link PosLib::TRepAllVoids Storno-?ersicht \endlink
		- Aktion \link PosLib::TRepPayNode Zahlvorg?ge \endlink
		-# \link PosLib::TRepPays Zahlungen \endlink
		-# \link PosLib::TRepTables Tischbersicht \endlink
		-# \link PosLib::TRepPayforms Abrechnungsarten \endlink
		\n\n

		Mit den folgenden Iteratoren kann ber die entsprechenden Knoten iteriert werden:
		Iterator fr \link PosLib::TReportNodeIt Knoten \endlink
		- Bestellungen haben keinen Bl?ter, sie akkumulieren nur die entsprechende Aktion.
		- \link PosLib::TRepFamiliesIt Warengruppen \endlink
		- \link PosLib::TRepArticlesIt Artikel \endlink
		- \link PosLib::TRepHoursIt Stunden \endlink
		- \link PosLib::TRepVatRatesIt Mwst-S?ze \endlink
		- Stornos haben keinen Bl?ter, sie akkumulieren nur die entsprechende Aktion.
		- \link PosLib::TRepVArticlesIt Storno-Artikel \endlink
		- \link PosLib::TRepAllVoidsIt Storno-?ersicht \endlink
	*/
	class			TRepData
	: public TReportLeaf
	{
	public:
		static const char	repOrders[];
		static const char	repNegOrders[];
		static const char	repFamilies[];
		static const char	repFamGroups[];
		static const char	repDiscounts[];
		static const char	repArticles[];
		static const char	repTables[];
		static const char	repPayforms[];
		static const char	repCurrencies[];
		static const char	repCurrReturns[];
		static const char	repSubvents[];
		static const char	repVatRates[];
		static const char	repHours[];
		static const char	repVoids[];
		static const char	repVArticles[];
		static const char	repAllVoids[];
		static const char	repPays[];
		static const char	repArchs[];
		static const char	repDepartments[];
		static const char	repHotelRooms[];
		static const char	entryTip[];
		static const char	entryVoids[];
		static const char	entryCredits[];
		static const char	entryCashAmount[];
		static const char	entryTableCount[];
		static const char	entryTableAmount[];
		static const char	repCashIns[];
		static const char	repCashOuts[];
	public:
		/*!	Erzeuge eine Instanz eines Reportblattes.
			\brief ctor
		*/
		TRepData(TTableAction* act, const char* path);
		~TRepData();
		void		walkTable(TTable* table);
		void		walkOrders(TTableOrder* order, TTable* table);
		void		walkVoids(TTableVoid* _void, TTable* table);
		void		walkPays(TTablePay* pay, TTable* table);
		void		walkCashing(TTableCashing* cash, TTable* table);
		void		walkTaxes(TTable* table, TTaxGroups& list);
		virtual void	exportCSV(const QString& path, TValue& value, QTextStream &st, QStringList& tags);
	#if QT_VERSION>=300
		virtual void	exportXml(QDomDocument& doc, QDomElement& root);
	#endif
		TReportNode*	getOrderNode(const char* name)
		{
			return m_Orders.find(name);
		}
		TReportNode*	getVoidNode(const char* name)
		{
			return m_Voids.find(name);
		}
		TReportNode*	getPayNode(const char* name)
		{
			return m_Pays.find(name);
		}
		TReportNode*	getCashNode(const char* name)
		{
			return m_Cashs.find(name);
		}
		int			getWaiter() const
		{
			return getValue(TTableAction::entryWaiter, 0);
		}
		QString		getWaiterName() const
		{
			return getString(TTableAction::entryWaiterName);
		}
		QString		getWaiterDescr() const
		{
			return getString(TWaiter::entryDescr);
		}
		void		setWaiterDescr(const QString& desc)
		{
			if( !desc.isEmpty() )
				setValue(TWaiter::entryDescr, desc);
		}
		int			getCenter() const
		{
			return getValue(TTableAction::entryCenter, 0);
		}
		QString		getCenterName() const
		{
			return getString(TTableAction::entryCenterName);
		}
		long		getCredits()
		{
			return getValue(entryCredits, 0L);
		}
		long		getTip() const
		{
			return getValue(entryTip, 0L);
		}
		long		getVoids() const
		{
			return getValue(entryVoids, 0L);
		}
		long		getCashAmount() const
		{
			return getValue(entryCashAmount, 0L);
		}
		int			getTableCount() const
		{
			return getValue(entryTableCount, 0);
		}
		long		getTableAmount() const
		{
			return getValue(entryTableAmount, 0L);
		}
/*		QStringList	getCashInReasons()
		{
			return m_CashInReasons;
		}
		QStringList	getCashOutReasons()
		{
			return m_CashOutReasons;
		}*/
	protected:
		void		setWaiter(int waiter)
		{
			setValue(TTableAction::entryWaiter, waiter);
		}
		void		setWaiterName(const QString& name)
		{
			setValue(TTableAction::entryWaiterName, name);
		}
		void		setCenter(int center)
		{
			setValue(TTableAction::entryCenter, center);
		}
		void		setCenterName(const QString& name)
		{
			setValue(TTableAction::entryCenterName, name);
		}
		void		addOrder(TTableOrder* order)
		{
			if( order->isCredit() )
				setValue(entryCredits, getCredits()+order->getAmount());
		}
		void		addPay(TTablePay* pay)
		{
			setValue(entryTip, getTip()+pay->getTip());
			if( pay->inCashSummary() )
				setValue(entryCashAmount, getCashAmount()+pay->getAmount());
		}
		void		addVoid(TTableVoid* _void)
		{
			setValue(entryVoids, getVoids()+_void->getAmount());
		}
/*		void		addCash(TTableCashing* cash)
		{
			if( cash->getAmount()<0 )
			{
				setValue(entryCashOut, getCashOut()+cash->getAmount());
				m_CashOutReasons << QString("%1:%2").arg(cash->getReason()).arg(cash->getAmount());
			}
			else
			{
				setValue(entryCashIn, getCashIn()+cash->getAmount());
				m_CashInReasons << QString("%1:%2").arg(cash->getReason()).arg(cash->getAmount());
			}
		}*/
	protected:
		QDict<TRepOrderNode>	m_Orders;
		QDict<TRepVoidNode>		m_Voids;
		QDict<TRepPayNode>		m_Pays;
		QDict<TRepCashNode>		m_Cashs;
//		QStringList				m_CashInReasons;
//		QStringList				m_CashOutReasons;
	private:
		void		init(TTableAction* act);
	};

	class			TRepDatas
	: public TReportNode
	{
	public:
		TRepDatas(TWaiter* waiter, const QString& name, const char* path);
		~TRepDatas();
		int			getMaximum()
		{
			return m_Maximum;
		}
		void		walkTable(TTable* table);
		virtual void	walk(TTableAction* entry, TTable* table) = 0;
		virtual void	walkTax(TTable* table, TTaxGroups& taxes) = 0;
		void		exportCSV(const QString& path);
	#if QT_VERSION>=300
		virtual QDomElement	exportXml(QDomDocument& doc, QDomElement& root);
	#endif
	protected:
		TRepData*	createLeaf(TTableAction* act)
		{
			return new TRepData(act, m_Path);
		}
		virtual QString		getKey(TTableAction* entry, TTable*) = 0;
	protected:
		TWaiter*	m_Waiter;
		int			m_Maximum;
		const char*	m_Path;
	};

	class			TRepDatasIt
	: public TReportNodeIt
	{
	public:
		TRepDatasIt(const TRepDatas& waits)
		: TReportNodeIt(waits)
		{
		}
		TRepData*	operator () ()
		{
			return (TRepData*) TReportNodeIt::operator()();
		}
		TRepData*	toFirst()
		{
			return (TRepData*) TReportNodeIt::toFirst();
		}
		TRepData*	current()
		{
			return (TRepData*) TReportNodeIt::current();
		}
		TRepData*	operator ++ ()
		{
			return (TRepData*) TReportNodeIt::operator ++();
		}
	};

	class			TRepWaiters
	: public TRepDatas
	{
	public:
		TRepWaiters(TWaiter* waiter, const char* path);
		virtual void	walk(TTableAction* entry, TTable* table);
		virtual void	walkTax(TTable* table, TTaxGroups& taxes);
		virtual QString		getKey(TTableAction* entry, TTable*);
	#if QT_VERSION>=300
//		virtual QDomElement	exportXml(QDomDocument& doc, QDomElement& root)
//		{
//			TRepDatas::exportXml(doc, node, "Waiter");
//		}
	#endif
	protected:
		bool		m_CWaiter;
		bool		m_VWaiter;
	};

	class			TRepCenters
	: public TRepDatas
	{
	public:
		TRepCenters(TWaiter* waiter, const char* path);
		virtual void	walk(TTableAction* entry, TTable* table);
		virtual void	walkTax(TTable* table, TTaxGroups& taxes);
		virtual QString		getKey(TTableAction* entry, TTable*);
	protected:
		bool		m_CCenter;
	};

	class			TReport
	: public QObject
	{
		Q_OBJECT
	public:
		TReport(TWaiter* waiter, bool allowed, const char* path);
		TReport(const QDate& date, TWaiter* waiter, const char* path="");
		TRepWaiters*	runWaiters(/*bool waiterfilter*/);
		TRepCenters*	runCenters();
		TRepWaiters*	runWaiters(const QDate& from, const QDate& to);
		TRepCenters*	runCenters(const QDate& from, const QDate& to);
		QDate		getDate() const
		{
			return m_Date;
		}
		void		run(TRepWaiters* waiters, TRepCenters* centers)
		{
			run(waiters, centers, m_Date);
		}
		void		run(TRepWaiters* waiters, TRepCenters* centers, const QDate& from, const QDate& to);
		void		setFilter(TSumFilter* filter)
		{
			m_Filter = filter;
/* |Python
			if( m_Filter )
				m_Filter->initParser();
*/
		}
		TSumFilter*	getFilter()
		{
			return m_Filter;
		}
		QString		getOutletName() const
		{
			return m_OutName;
		}
		void		setOutlets(const QString& name, const QStringList& paths)
		{
			m_OutName = name;
			m_Outlets = paths;
		}
		TWaiter*	getWaiter()
		{
			return m_Waiter;
		}
		void		exportCSV(TRepWaiters* waiters, TRepCenters* centers, const char* path);
		bool		wasStopped() const
		{
			return m_Stopped;
		}
		void		doOpenTabs(bool flag=TRUE)
		{
			m_WithTabs = flag;
		}
		void		doWithoutCredits(bool flag=TRUE)
		{
			m_WithoutCredits = flag;
		}
	public slots:
		void		setStopped()
		{
			m_Stopped = TRUE;
		}
	protected:
		bool		checkTable(TTable* table)
		{
			if( !m_Filter )
				return TRUE;
			return m_Filter->checkTable(table);
		}
		bool		checkTableEntry(TTable* table, TTableEntry* entry)
		{
			if( !m_Filter )
				return TRUE;
			return m_Filter->checkEntry(table, entry);
		}
		bool		run(TRepWaiters* waiters, TRepCenters* centers, const QDate& date);
		bool		runArchInfos(TRepWaiters* waiters, TRepCenters* centers, TArchTables& infos);
		bool		runTableInfos(TRepWaiters* waiters, TRepCenters* centers, TTableList* tables, const QString& path, bool arch);
		void		runTable(TRepWaiters* waiters, TRepCenters* centers, TTable* table);
	signals:
		void		sigDataCount(int);
		void		sigProgress(int);
		void		sigDate(const QDate&);
	protected:
		TWaiter*	m_Waiter;
		QString		m_Path;
		QDate		m_Date;
		TSumFilter*	m_Filter;
		bool		m_Allowed;
		bool		m_WithTabs;							// Flag, ob offene Tische auch rein
		bool		m_WithoutCredits;					// Flag, ob Auslagen nicht rein
		QString		m_OutName;
		QStringList	m_Outlets;
		bool		m_Stopped;
	};
}

#endif

