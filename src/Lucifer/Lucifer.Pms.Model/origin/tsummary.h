#ifndef				_POSLIB_TSUMMARY_
#define				_POSLIB_TSUMMARY_
#include			"poslib/treport.h"
#include			"poslib/tcurrency.h"
#include			"basics/tinifile.h"

namespace PosLib
{
	class	TSumBatch;

	class			TSummary
	: QObject
	{
		static const char	sectReports[];
		static const char	sectNoPrint[];
		static const char	entryBatchReport[];
		static const char	entryHeader[];
		static const char	entryHeader2[];
		static const char	entryHeaderFrom[];
		static const char	entryHeaderTo[];
		static const char	entryHeaderFilter[];
		static const char	entryHeaderOutlet[];
		static const char	entryWaiters[];
		static const char	entryWaiters2[];
		static const char	entryWaiterDescr[];
		static const char	entryCenters[];
		static const char	entryCenter0[];
		static const char	entryPaidTables[];
		static const char	entryVoids[];
		static const char	entryVoidAmount[];
		static const char	entryOpenTables[];
		static const char	entryOpenTableSum[];
		static const char	entryInSummary[];
		static const char	entrySumPayforms1[];
		static const char	entrySumPayforms2[];
		static const char	entrySumPayformsA[];
		static const char	entryListOrders[];
		static const char	entrySumInHouse[];
		static const char	entrySumOutHouse[];
		static const char	entryVat[];
		static const char	entryNet[];
		static const char	entryBrut[];
		static const char	entryNetSum[];
		static const char	entryNsReturn[];
		static const char	entryTip[];
		static const char	entryCredits[];
		static const char	entryNonRefunds[];
		static const char	entryAllAmountAb[];
		static const char	entryAllAmount[];
		static const char	entryNetAllAmount[];
		static const char	entryNotSummary[];
		static const char	entryAllPayforms[];
		static const char	entryCashAmount[];
		static const char	entrySumArticles[];
		static const char	entrySumFamilies[];
		static const char	entrySumFamGroups[];
		static const char	entrySumVoids[];
		static const char	entrySumVoidEntries[];
		static const char	entrySumPayments[];
		static const char	entrySumCurrencies[];
		static const char	entryCurrencies[];
		static const char	entrySumHours[];
		static const char	entrySumOrders[];
		static const char	entrySumDepartments[];
		static const char	entryControlArts[];
		static const char	entryArticleSum[];
		static const char	entryHeaderHistory[];
		static const char	entryHeaderFiscal[];
		static const char	entryHeaderActual[];
		static const char	entryHeaderTurn[];
		static const char	entryArticleCount[];
		static const char	entrySumGuests[];
		static const char	entrySumPerGuest[];
		static const char	entryNetSumPerGuest[];
		static const char	entryDiscOrderAmount[];
		static const char	entryDiscounted[];
		static const char	entryLostMoney[];
		static const char	entrySubUndiscount[];
		static const char	entrySubAmount[];
		static const char	entrySubDiscount[];
		static const char	entryCashIn[];
		static const char	entryCashOut[];
		static const char	entryUseShortText[];
		static const char	entryStatists[];
		static const char	entryForeignCurrs[];
		static const char	entryNegCounts[];
		static const char	entryReturnDiff[];
		static const char	entryNotSummarySum[];

	public:
		typedef QMap<int,QString>	TTitleMap;
	public:
		/*!	Die folgenden Auswertungen sind verfgbar.
			\brief Die verfgbaren Auswertungen.
		*/
		enum		Types
		{
			Articles		= 0				//!< Artikel-Report
		,	Families		= 1				//!< Warengruppen-Report
		,	Waiters			= 2				//!< Kellner-Report
		,	Centers			= 3				//!< Kostenstellen-Report
		,	Voids			= 4				//!< Storno-Report
		,	Tables			= 5				//!< Tisch-Report (Zahlungen)
		,	AllVoids		= 6				//!< Storno-Report (ausfhrlich)
		,	Hours			= 7				//!< Zeit/Umsatz
		,	Orders			= 8				//!< Bestellung/Aart
		,	FamGroups		= 9				//!< Oberwarengruppen-Report
		,	Departments		= 10			//!< Sparten-Report
		,	Minimal			= 11			//!< Minimal-Report
		,	Discounts		= 12			//!< Rabatt-Report
		,	Subvents		= 13			//!< Preisfindungs-Report
		,	HotelRooms		= 14			//!< Hotelzimmer-Report
		};
		enum		Reports
		{
			repPaidTables		= 1			//!< Abgerechnete Tische
		,	repJournal			= 2			//!< Aktuelles Journal
		,	repPayments			= 3			//!< Zahlvorg?ge
		,	repAllWaiters		= 4			//!< Kellnerreport, alle Kellner
		,	repWaiters			= 5			//!< Kellnerreport pro Kellner
		,	repAllCenters		= 6			//!< Kostenstellenreport, alle KStellen
		,	repAllArticles		= 7			//!< Artikelreport
		,	repWaiterArticles	= 8			//!< Artikelreport pro Kellner
		,	repCenterArticles	= 9			//!< Artikelreport pro Kostenstelle
		,	repAllFamilies		= 10		//!< Warengruppenreport
		,	repWaiterFamilies	= 11		//!< Warengruppenreport pro Kellner
		,	repCenterFamilies	= 12		//!< Warengruppenreport pro Kostenstelle
		,	repAllVoids			= 13		//!< Stornobericht
		,	repWaiterVoids		= 14		//!< Stornobericht pro Kellner
		,	repCenterVoids		= 15		//!< Stornobericht pro Kostenstelle
		,	repAllVEntries		= 16		//!< Stornoeinzelbericht
		,	repWaiterVEntries	= 17		//!< Stornoeinzelbericht pro Kellner
		,	repCenterVEntries	= 18		//!< Stornoeinzelbericht pro Kostenstelle
		,	repAllHours			= 19		//!< Zeit/Umsatz gesamt
		,	repWaiterHours		= 20		//!< Zeit/Umsatz pro Kellner
		,	repCenterHours		= 21		//!< Zeit/Umsatz pro Kostenstelle
		,	repAllOrders		= 22
		,	repWaiterOrders		= 23
		,	repCenterOrders		= 24
		,	repAllFamGroups		= 25		//!< Oberwarengruppenreport
		,	repWaiterFamGroups	= 26		//!< Oberwarengruppenreport pro Kellner
		,	repCenterFamGroups	= 27		//!< Oberwarengruppenreport pro Kostenstelle
		,	repAllDeps			= 28		//!< Spartenreport
		,	repWaiterDeps		= 29		//!< Spartenreport pro Kellner
		,	repCenterDeps		= 30		//!< Spartenreport pro Kostenstelle
		,	repMinimal			= 31		//!< Minimaler Umsatzbericht fr Sharp
		,	repAllDiscounts		= 32		//!< Rabattreport
		,	repWaiterDiscounts	= 33		//!< Rabattreport pro Kellner
		,	repCenterDiscounts	= 34		//!< Rabattreport pro Kostenstelle
		,	repAllSubventions	= 35		//!< Preisfindungen
		,	repWaiterSubvents	= 36		//!< Preisfindungen pro Kellner
		,	repCenterSubvents	= 37		//!< Preisfindungen pro Kostenstelle
		,	repAllPayments		= 38		//!< Zahlvorg?ge aller Kellner
		,	repCenterPayments	= 39		//!< Zahlvorg?ge pro Kostenstelle
		,	repAllHotelRooms	= 40		//!< Bericht über Hotelzimmer
		,	repWaiterHotelRooms	= 41		//!< Hotelzimmer pro Kllner
		,	repCenterHotelRooms	= 42		//!< Hotelzimmer pro Kostenstelle
		};

	public:
		TSummary(TReport* rep, TCurrencies* currs, TWaiters* waiters, TWaiter* waiter=NULL, int z=0, const char* path=NULL);
		QStringList	batchReport(TSumBatch* batch, const TTitleMap& titles);
		QStringList	waiterReport(int type, const QString& title, bool all);
		QStringList	centerReport(int type, const QString& title);
		void		setFromTo(const QDate& from, const QDate& to, int step)
		{
			m_From = from;
			m_To = to;
			m_Step = step;
		}
	protected:
		QStringList	getHeader(const QDate& from, const QDate& to, const QString& title, TSumBatch* batch=NULL);
		QStringList	getData(int type, TRepData* data);
		// ------------
		QStringList	getWaiterData(int type, TRepDatas* data, bool all);
		QStringList	getCenterData(int type, TRepDatas* data);
		// ------------
		QStringList	getMinimal(TRepData* data);
		QStringList	getWaiter(TRepData* data);
		QStringList	getArticles(TRepData* data);
		QStringList	getFamilies(TRepData* data);
		QStringList	getFamGroups(TRepData* data);
		QStringList	getDiscounts(TRepData* data);
		QStringList	getSubvents(TRepData* data);
		QStringList	getVoids(TRepData* data);
		QStringList	getVoidEntries(TRepData* data);
		QStringList	getTables(TRepData* data);
//		QStringList	getNPayforms(TRepPayforms* forms, TRepData* data);
		QStringList	getHours(TRepData* data);
		QStringList	getOrders(TRepData* data);
		QStringList	getDepartments(TRepData* data);
		QStringList	getHotelRooms(TRepData* data);
		QString		getCurrBez();
		QString		formatCurr(long amount, TCurrency* org=NULL);

		bool		nosectprint(const QString& str)
		{
			QString hilf="no"+str;		// Name des Eintrages aus altem Eintrag herleiten
			bool flag = m_Ini.getValue(sectNoPrint, hilf, FALSE); // Schauen ob ein entsprechender Eintrag in der reports.ini exisitiert
			return(flag);				// Wenn True dann diesen Eintrag nicht drucken.
		}
		QString		trans(const QString& str, const QString& def)
		{
//qDebug("Searching "+str+" in "+sectReports);
			return m_Ini.getString(sectReports, str, def);
		}
		QString		formatLine(int length, const QString& str, char ch='.')
		{
			return str + formatFill(ch, length-str.length());
		}
		QString		formatLine(int length, const QString& str, const QString& def, char ch='.')
		{
			QString ret = trans(str, def).left(length);
			return ret + formatFill(ch, length-ret.length());
		}
		QString		formatFill(char ch, int length)
		{
			QString filler;
			filler.fill(ch, length);
			return filler;
		}
		QString		getFiscalName(TSumBatch* batch);
	public:
		static QString	getFiscalName(TSumBatch* batch, const QDate& date, const char* path=0);
		static bool		hasFiscalNames(const QDate& date, const char* path=0);
	protected:
		TReport*	m_Report;
		TWaiter*	m_Waiter;
		TCurrencies*	m_Currs;
		TWaiters*	m_Waiters;
		QDate		m_From;
		QDate		m_To;
		int			m_Step;
		TInifile	m_Ini;
		int			m_RepLength;
		int			m_PageWidth;
		QString		m_Path;
		int			m_zCounter;
		bool		m_MasterOwnTabs;
		bool		m_ShortTexts;
		TSumBatch*	m_Batch;
//		bool		m_WaiterFilter;
	};
}

using namespace PosLib;

#endif

