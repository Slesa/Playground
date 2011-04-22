#ifndef				POSLIB_TPRNCONFIG_H
#define				POSLIB_TPRNCONFIG_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Druckerinformationen.
		Alle verf�gbaren Drucker werden in einer Instanz von TPrinterCfgList
		zur Verf�gung gestellt.
		\brief POS-Klassen: Druckerinformationen.
	*/
	class			TPrnConfig
	: public TNValue
	{
	public:
		static const char	entryPath[];		//!< Spoolpfad zum Druckertreiber
		static const char	entryV5[];			//!< Druckertreiber GastroFix?
		static const char	entryUnicode[];		//!< Utf16 benutzen?
		static const char	entryOrderman[];	//!< Drucker im Orderman-Funknetz?
		static const char	entryCfgPath[];		//!< Sonderpfad f�r die Layouts
		static const char	entryTermPath[];	//!< Spoolpfad zum Druckertreiber pro Terminal
		static const char	entryWaiterPath[];	//!< Spoolpfad zum Druckertreiber pro Kellner
		static const char	entryTablePath[];	//!< Spoolpfad zum Druckertreiber pro Tisch(bereich)

		static const char	entryDoBill[];
		static const char	entryDoSummary[];
	public:
		/*!	Erzeuge eine leere Instanz einer Druckerinformation.
		*/
		TPrnConfig()
		: TNValue()
		{
		}
		/*!	Erzeuge eine Instanz einer Oberarengruppe als Kopie von fam.
			\param fam		die zu kopierende Oberwarengruppe.
		TFamGroup(const TFamGroup& fam)
		: TNValue(fam)
		{
		}
		*/
		~TPrnConfig()
		{
		}
		QString		getPath(int term, int waiter=0, int table=0) const;
		void		setPath(const QString& path)
		{
			if( !path.isEmpty() )
				setValue(entryPath, path);
			else
				clrValue(entryPath);
		}
		void		setTablePath(int table, const QString& path, const QString& cfg)
		{
			QString attr = QString(entryTablePath)+QString::number(table);
			if( !path.isEmpty() || !cfg.isEmpty() )
				setValue(attr, path+";"+cfg);
			else
				clrValue(attr);
		}
		void		setWaiterPath(int waiter, const QString& path, const QString& cfg)
		{
			QString attr = QString(entryWaiterPath)+QString::number(waiter);
			if( !path.isEmpty() || !cfg.isEmpty() )
				setValue(attr, path+";"+cfg);
			else
				clrValue(attr);
		}
		void		setTermPath(int term, const QString& path, const QString& cfg)
		{
			QString attr = QString(entryTermPath)+QString::number(term);
			if( !path.isEmpty() || !cfg.isEmpty() )
				setValue(attr, path+";"+cfg);
			else
				clrValue(attr);
		}
		bool		isV5() const
		{
			return getValue(entryV5, FALSE);
		}
		void		setV5(bool flag)
		{
			if( !flag )
				clrValue(entryV5);
			else
				setValue(entryV5, flag);
		}
		bool		isUnicode() const
		{
			return getValue(entryUnicode, FALSE);
		}
		void		setUnicode(bool flag)
		{
			if( !flag )
				clrValue(entryUnicode);
			else
				setValue(entryUnicode, flag);
		}
		bool		isOrderman() const
		{
			return getValue(entryOrderman, FALSE);
		}
		void		setOrderman(bool flag)
		{
			if( !flag )
				clrValue(entryOrderman);
			else
				setValue(entryOrderman, flag);
		}
		QString		getCfgPath(int term, int waiter=0, int table=0) const;
		void		setCfgPath(const QString& path)
		{
			if( !path.isEmpty() )
				setValue(entryCfgPath, path);
			else
				clrValue(entryCfgPath);
		}
		bool		doSummary() const
		{
			return getValue(entryDoSummary, FALSE);
		}
		void		setDoSummary(bool flag)
		{
			if( !flag )
				clrValue(entryDoSummary);
			else
				setValue(entryDoSummary, flag);
		}
	};

	class			TPrnConfigs
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TPrnConfigs(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TPrnConfigs()
		{
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString	getFileName() const
		{
			return fileName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		TPrnConfig*	operator [] (int index)
		{
			return (TPrnConfig*) TValueList::operator [](index);
		}
	};

	class			TPrnConfigIt
	: public TValueListIt
	{
	public:
		TPrnConfigIt(TPrnConfigs& list)
		: TValueListIt(list)
		{
		}
		TPrnConfig*	operator () ()
		{
			return (TPrnConfig*) TValueListIt::operator()();
		}
		TPrnConfig*	toFirst()
		{
			return (TPrnConfig*) TValueListIt::toFirst();
		}
		TPrnConfig*	current()
		{
			return (TPrnConfig*) TValueListIt::current();
		}
		TPrnConfig*	operator ++ ()
		{
			return (TPrnConfig*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


