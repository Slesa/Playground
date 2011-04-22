#ifndef				POSLIB_TJOURNAL_H
#define				POSLIB_TJOURNAL_H
#include			"poslib/twaiter.h"
#include			"basics/tvalue.h"
#include			"basics/tfile.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r die Journaleinstellungen. Das Journal kann
		fortlaufend sein, pro Tag eine Datei erzeugen, pro Woche oder pro Monat. Einzelne Aktionen k�nnen
		unterdr�ckt werden und die Maximalgr��e der Journal-Datei kann eingeschr�nkt werden.
		\brief POS-Klassen: Journal.
	*/
	class			TJournal
	: public TValue
	{
		static const char	fileConfig[];
		static const char	pathConfig[];
		static const char	sectSettings[];
		static const char	sectActions[];
		static const char	entrySize[];
		static const char	entryFile[];
		static const char	entryPath[];
		static const char	entryPrinter[];
		static const char	entryType[];
		static const char	entryDoCashing[];
		static const char	entryDoOrder[];
		static const char	entryDoOrderArts[];
		static const char	entryDoUnorder[];
		static const char	entryDoVoid[];
		static const char	entryDoPay[];
		static const char	entryDoChange[];
		static const char	entryDoSplit[];
		static const char	entryDoChangePay[];
		static const char	entryDoRestore[];
		static const char	entryDoSummary[];
		static const char	entryDoShChange[];
		static const char	entryDoPrChange[];
		static const char	entryDoTxtChange[];
		static const char	entryDoChangeTip[];
		static const char	entryDoChangeVat[];
		static const char	entryDoChangeOwn[];
		static const char	entryDoCloaking[];
		static const char	entryDoTakeover[];
		static const char	entryJrnHost[];					//!< Host = Journal-Server
		static const char	entryJrnPort[];					//!< Port = Journal-Server-Port
		static const char	strFile[];
		static const char	strDaily[];
		static const char	strWeekly[];
		static const char	strMonthly[];
	public: // ---- Kommando über Socket ------------------------------------------------------------------------------
		static const char	cmdJournal[];					//!< Socket-Kommando journal()
		static const char	paramDate[];					//!< Socket-Parameter Lokales Datum
		static const char	paramTime[];					//!< Socket-Parameter Lokale Uhrzeit
		static const char	paramAction[];					//!< Socket-Parameter Aktion
		static const char	paramParams[];					//!< Socket-Parameter Journal-Parameter
	public:
		static const char	cmdAppStarted[];
		static const char	cmdAppEnded[];
		static const char	cmdTermLogin[];
		static const char	cmdTermLogout[];
		static const char	cmdCashing[];
		static const char	cmdOrder[];
		static const char	cmdOrderDetail[];
		static const char	cmdUnorder[];
		static const char	cmdVoid[];
		static const char	cmdVoidDetail[];
		static const char	cmdSplit[];
		static const char	cmdSplitDetail[];
		static const char	cmdPay[];
		static const char	cmdRestore[];
		static const char	cmdChangeTable[];
		static const char	cmdChangePrice[];
		static const char	cmdChangeShift[];
		static const char	cmdChangeDiscount[];
		static const char	cmdChangeText[];
		static const char	cmdChangeTip[];
		static const char	cmdChangeOwner[];
		static const char	cmdChangeVat[];
		static const char	cmdCloak[];
		static const char	cmdTakeOver[];
		static const char	cmdTurnOver[];
	public:
		static const char	pathName[];
		static const char	fileName[];
	public:
		enum		Types
		{
			tFile		= 0				//!< Einzelne Datei schreiben
		,	tDaily		= 1				//!< Tagesgenaues Journal
		,	tWeekly		= 2				//!< Wochengenaues Journal
		,	tMonthly	= 3				//!< Monatsgenaues Journal
		};
	public: // ---- ctor/dtor ----------------------------------------------------------------
		/*!	Erzeuge eine Instanz eines Journals.
			\brief C'tor
		*/
		TJournal();
		// -------- Datenausgabe -------------------------------------------------------------
		QString		writeAction(const char* action, const QStringList& params, int waiter=0, int term=0);
		QString		writeAction(const char* action, const QString& param=QString::null, int waiter=0, int term=0);
		// -------- Dateizugriff -------------------------------------------------------------
		QStringList	getContent(const char* path=NULL, TWaiter* waiter=0, int term=0);
		void		load(const char* path="");
		void		save(const char* path="");
		long		getSize() const
		{
			return m_Size;
		}
		void		setSize(long size)
		{
			m_Size = size;
		}
		/*!	\return der Drucker, auf den alle Artikel dieser Warengruppe beim Bestellen
			ausgedruckt werden sollen.
			Der zur�ckgegebene Wert ist ein Index auf die Druckerinfo-Liste.
			\brief Ausgabedrucker abfragen.
			\sa setPrinter, TPrinterCfg, TPrinterCfgList
		*/
		int			getPrinter() const
		{
			return m_Printer;
		}
		/*!	�ndert den Drucker, auf den alle Artikel dieser Warengruppe beim Bestellen ausgedruckt
			werden sollen.
			\param prn		Der neue Ausgabedrucker als Index auf die Druckerliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief Ausgabedrucker der Warengruppe �ndern.
			\sa getPrinter, TPrinterCfg, TPrinterCfgList
		*/
		void		setPrinter(int prn)
		{
			m_Printer = prn;
		}
		int				getType() const
		{
			return m_Type;
		}
		void		setType(int type)
		{
			m_Type = type;
		}
		QString		getFile() const
		{
			return m_File;
		}
		void		setFile(const QString& file)
		{
			m_File = file;
		}
		QString		getPath() const
		{
			return m_Path;
		}
		void		setPath(const QString& path)
		{
			m_Path = path;
		}
		bool		doCashing() const
		{
			return getValue(entryDoCashing, TRUE);
		}
		void		setDoCashing(bool flag)
		{
			setValue(entryDoCashing, flag);
		}
		bool		doOrder() const
		{
			return getValue(entryDoOrder, TRUE);
		}
		void		setDoOrder(bool flag)
		{
			setValue(entryDoOrder, flag);
		}
		bool		doOrderArts() const
		{
			return getValue(entryDoOrderArts, TRUE);
		}
		void		setDoOrderArts(bool flag)
		{
			setValue(entryDoOrderArts, flag);
		}
		bool		doUnorder() const
		{
			return getValue(entryDoUnorder, FALSE);
		}
		void		setDoUnorder(bool flag)
		{
			setValue(entryDoUnorder, flag);
		}
		bool		doVoid() const
		{
			return getValue(entryDoVoid, TRUE);
		}
		void		setDoVoid(bool flag)
		{
			setValue(entryDoVoid, flag);
		}
		bool		doPay() const
		{
			return getValue(entryDoPay, TRUE);
		}
		void		setDoPay(bool flag)
		{
			setValue(entryDoPay, flag);
		}
		bool		doChange() const
		{
			return getValue(entryDoChange, TRUE);
		}
		void		setDoChange(bool flag)
		{
			setValue(entryDoChange, flag);
		}
		bool		doSplit() const
		{
			return getValue(entryDoSplit, TRUE);
		}
		void		setDoSplit(bool flag)
		{
			setValue(entryDoSplit, flag);
		}
		bool		doChangePay() const
		{
			return getValue(entryDoChangePay, TRUE);
		}
		void		setDoChangePay(bool flag)
		{
			setValue(entryDoChangePay, flag);
		}
		bool		doRestore() const
		{
			return getValue(entryDoRestore, TRUE);
		}
		void		setDoRestore(bool flag)
		{
			setValue(entryDoRestore, flag);
		}
		bool		doSummary() const
		{
			return getValue(entryDoSummary, TRUE);
		}
		void		setDoSummary(bool flag)
		{
			setValue(entryDoSummary, flag);
		}
		bool		doChangePrice() const
		{
			return getValue(entryDoPrChange, TRUE);
		}
		void		setDoChangePrice(bool flag)
		{
			setValue(entryDoPrChange, flag);
		}
		bool		doChangeShift() const
		{
			return getValue(entryDoShChange, TRUE);
		}
		void		setDoChangeShift(bool flag)
		{
			setValue(entryDoShChange, flag);
		}
		bool		doChangeText() const
		{
			return getValue(entryDoTxtChange, TRUE);
		}
		void		setDoChangeText(bool flag)
		{
			setValue(entryDoTxtChange, flag);
		}
		bool		doChangeTip() const
		{
			return getValue(entryDoChangeTip, TRUE);
		}
		void		setDoChangeTip(bool flag)
		{
			setValue(entryDoChangeTip, flag);
		}
		bool		doChangeVat() const
		{
			return getValue(entryDoChangeVat, TRUE);
		}
		void		setDoChangeVat(bool flag)
		{
			setValue(entryDoChangeVat, flag);
		}
		bool		doChangeOwn() const
		{
			return getValue(entryDoChangeOwn, TRUE);
		}
		void		setDoChangeOwn(bool flag)
		{
			setValue(entryDoChangeOwn, flag);
		}
		bool		doCloaking() const
		{
			return getValue(entryDoCloaking, TRUE);
		}
		void		setDoCloaking(bool flag)
		{
			setValue(entryDoCloaking, flag);
		}
		bool		doTakeover() const
		{
			return getValue(entryDoTakeover, TRUE);
		}
		void		setDoTakeover(bool flag)
		{
			setValue(entryDoTakeover, flag);
		}
		QString		getFilename(const char* path="");
		QString		getPathname(const char* path="")
		{
		if( m_Path.isEmpty() )
			return TDir::checkPath(TDir::checkPath(path)+pathName);
		else
			return TDir::checkPath(TDir::checkPath(path)+m_Path);
		}
		static int	str2type(const QString& str);
		static QString	type2str(int type);
	protected:
		void		checkSize(TFile& fh);
		void		doAppend(const char* action, const QStringList& params=QStringList());
	protected:
		long		m_Size;								//!< Maximale Gr��e des Journals
		int			m_Printer;							//!< Ausgabedrucker f�r das Journal
		int			m_Type;								//!< Art des Journals
		QString		m_File;
		QString		m_Path;
		QString		m_Host;									//!< Journal-Server
		int			m_Port;									//!< Server-Port
	};

}

using namespace PosLib;

#endif


