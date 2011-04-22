#ifndef				POSLIB_TMESSAGE_H
#define				POSLIB_TMESSAGE_H
#include			"poslib/tterminal.h"
#include			"poslib/twaiter.h"
#include			"basics/tvalue.h"

	/*!	Diese Klasse beinhaltet alle verfügbaren Informationen einer Nachricht.
		\brief Nachrichtensystem, Nachricht.
	*/
	class 			TMessage
	: public TValue
	{
		static const char	entryType[];						//!< Eintrag Art der Nachricht
		static const char	entryFromTerm[];					//!< Eintrag von welchem Terminal
		static const char	entryFromWait[];					//!< Eintrag von welchem Kellner
		static const char	entrySendTime[];					//!< Eintrag wann gesendet
		static const char	entryToWaiter[];					//!< Eintrag an welchen Kellner
		static const char	entryReadTime[];					//!< Eintrag wann gelesen
		static const char	entryMsg[];							//!< Eintrag Nachrichtentext
		static const char	entryIsNew[];						//!< Eintrag ob neue Nachricht
	public:
		/*!	Eine Nachricht kann unterschiedliche Typen haben, zum einen Nachrichten von Kellner zu Kellner,
			zum anderen aber auch Systemkommandos. Ale bekannten Typen werden hier aufgelistet.
			\brief Verfügbare Nachrichtentypen
		*/
		enum		Types
		{
			typeMsg		= 0										//!< Nachricht von Kellner zu Kellner
		,	typeSys		= 1										//!< Nachricht beinhaltet Systemkommando
		,	typeGui		= 2										//!< Nachricht beinhaltet Kommandos für die Oberfläche
		};
	public:
		TMessage()
		{
		}
		int		getType() const
		{
			return getValue(entryType, typeMsg);
		}
		void	setType(int type)
		{
			if( type==typeMsg )
				clrValue(entryType);
			else
				setValue(entryType, type);
		}
		int		getFromTerm() const
		{
			return getValue(entryFromTerm, 0);
		}
		void	setFromTerm(int term)
		{
			if( !term )
				clrValue(entryFromTerm);
			else
				setValue(entryFromTerm, term);
		}
		QString	getFromWaiter() const
		{
			return getString(entryFromWait);
		}
		void	setFromWaiter(const QString& wait)
		{
			if( wait.isEmpty() )
				clrValue(entryFromWait);
			else
				setValue(entryFromWait, wait);
		}
		QDateTime	getSendTime() const
		{
			return QDateTime::fromString(getString(entrySendTime), Qt::ISODate);
		}
		void		setSendTime(const QDateTime& time)
		{
			setValue(entrySendTime, time.toString(Qt::ISODate));
		}
		int		getToWaiter() const
		{
			return getValue(entryToWaiter, 0);
		}
		void	setToWaiter(int wait)
		{
			if( !wait )
				clrValue(entryToWaiter);
			else
				setValue(entryToWaiter, wait);
		}
		QDateTime	getReadTime() const
		{
			return QDateTime::fromString(getString(entryReadTime), Qt::ISODate);
		}
		void		setReadTime(const QDateTime& time)
		{
			setValue(entryReadTime, time.toString(Qt::ISODate));
		}
		QString		getMessage() const
		{
			return getString(entryMsg);
		}
		void		setMessage(const QString& msg)
		{
			if( msg.isEmpty() )
				clrValue(entryMsg);
			else
				setValue(entryMsg, msg);
		}
		bool	isNew() const
		{
			return getValue(entryIsNew, FALSE);
		}
		void	setNew(bool flag)
		{
			if( !flag )
				clrValue(entryIsNew);
			else
				setValue(entryIsNew, TRUE);
		}
	};

	class			TMessages
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	strSeperator;
		static const char	pathMsgs[];
		static const char	msgFiscalTurn[];
		static const char	msgKillGui[];
		static const char	msgFreezeGui[];
		static const char	msgUnfreezeGui[];
		static const char	msgPrinterError[];
		static const char	msgPrinterReady[];
	public:
		TMessages(bool autodel=TRUE);
		virtual QString	getFileName() const
		{
			return fileName;
		}
	public:
		/*!	Sendet die Nachricht msg an den Kellner towait an Terminal toterm. Die Nachricht wird erst dann
			ein Signal auslösen, wenn towait 0 ist oder sich der angegebene Kellner anmeldet.
			\param towait		Der Zielkellner für die Nachricht, 0 für beliebigen Kellner
			\param toterm		Das Zielterminal der Nachricht, 0 für alle (bekannten)
			\param msg			Der Nachrichtentext
			\param term			Das Terminal, das die Nachricht abschickt.
			\brief Nachricht verschicken.
		*/
		void		sendMessage(TTerminals* terms, int towait, int toterm, const QString& msg, TTerminal* term, TWaiter* waiter, const char* path=0);
		/*!	Sendet die Systemnachricht msg an das Terminal toterm. Die Nachricht wird direkt abgearbeitet.
			\param toterm		Das Zielterminal der Nachricht, 0 für alle (bekannten)
			\param msg			Das Systemkommando
			\param term			Das Terminal, das die Nachricht abschickt.
			\brief Systemnachricht verschicken.
		*/
		void		sendSysMessage(TTerminals* terms, int toterm, const QString& msg, TTerminal* term, TWaiter* waiter, const char* path=0);
		void		sendSysMessage(const QStringList& msg, TTerminals* terms, int toterm, TTerminal* term, TWaiter* waiter, const char* path=0);
		void		sendGuiMessage(TTerminals* terms, int toterm, const QString& msg, TTerminal* term, TWaiter* waiter, const char* path=0);
	public:
		int			load(const QString& file, const char* path)
		{
			return TValueList::load(file, TDir::checkPath(TDir::checkPath(path)+pathMsgs));
		}
		int			load(int term, const char* path)
		{
			return TValueList::load(getFileName()+QString::number(term), TDir::checkPath(TDir::checkPath(path)+pathMsgs));
		}
		void		save(int term, const char* path)
		{
			TValueList::save(getFileName()+QString::number(term), TDir::checkPath(TDir::checkPath(path)+QString(pathMsgs)));
		}
	protected: // - Thread-Funktionen ------------------------------------------------------------------------
		void		saveNew(int term, const char* path);
		void		saveMsg(TTerminals* terms, TMessages& list, int toterm, TTerminal* term, bool sys=FALSE, const char* path=0);
		void		setData(TMessage* msg, int type, const QString& str, TTerminal* term, TWaiter* waiter=NULL);
	};

	class				TMessageIt
	: public TValueListIt
	{
	public:
		TMessageIt(TMessages& list)
		: TValueListIt(list)
		{
		}
		TMessage*	operator () ()
		{
			return (TMessage*) TValueListIt::operator()();
		}
		TMessage*	toFirst()
		{
			return (TMessage*) TValueListIt::toFirst();
		}
		TMessage*	current()
		{
			return (TMessage*) TValueListIt::current();
		}
		TMessage*	operator ++ ()
		{
			return (TMessage*) TValueListIt:: operator ++();
		}
    };
#endif

