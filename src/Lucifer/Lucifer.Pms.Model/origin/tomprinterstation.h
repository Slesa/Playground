#ifndef			POSLIB_TOMPRINTERSTATION_H
#define			POSLIB_TOMPRINTERSTATION_H
#include		"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
	Diese Klasse umfasst alle benoetigten Informationen fuer eine Orderman Printer Station Typ: OMPR
	\brief POS Klasse: Orderman Printerstation OMPR
	*/
	class		TOMPrinterstation
	: public TNValue
	{
	public:
		static const char	entrySerialNo[];
		static const char	entrySpoolPath[];

	public:
		/*!	Erzeugt eine leere Instanz einer Printerstation
			\brief ctor
		*/
			TOMPrinterstation()
			: TNValue()
			{
			}
		/*!	\return Die Serialnummer der Printerstation
			\brief Serialnummer der Printerstation ermitteln
			\sa setSerialNo
		*/
		int				getSerialNo()
		{
			return getValue(entrySerialNo,0);
		}
		/*!	Setzt die Serialnummer der Printerstation
			\brief Serialnummer der Printerstation setzen
			\sa getSerialNo
		*/
		void			setSerialNo(int number)
		{
			setValue(entrySerialNo,number);
		}

		/*!	\return Der Spoolpath der Printerstation
			\brief Spoolpath der Printerstation ermitteln
			\sa setSpoolpath
		*/
		QString			getSpoolPath()
		{
			return getString(entrySpoolPath,"");
		}
		/*!	Setzt den Spoolpath der Printerstation
			\brief Spoolpath der Printerstation setzen
			\sa getSpoolPath
		*/
		void			setSpoolPath(const QString& path)
		{
			setValue(entrySpoolPath,path);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse fasst mehrere TOMPrinterstations-Elemente zu einer Liste zusammen. Die fuer die
		XML-Funktionen noetigen TValueList-Funktionen wurden ueberschrieben.
		\brief POS-Klassen: Liste von Orderman Printerstations OMPR Zusammenstellungen.
	*/
	class		TOMPrinterstations
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste (omanprinterstations)
		static const char	elementName[];				//!< Name eines Elements der Liste (omanprinterstation)
	public:
		/*!	Erzeugt eine neue Instanz einer Orderman Printerstation Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste geloescht.
			\brief ctor.
		*/
		TOMPrinterstations(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zerstoert die Instanz der Printerstations liste.
			\brief dtor.
		*/
		~TOMPrinterstations()
		{
		}
		virtual TValue*	createValue()
		{
			return new TOMPrinterstation();
		}
		virtual int		load(const char* path="")
		{
			return TValueList::load(path);
		}
		virtual int		load(const QString& file, const char* path);
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
		/*!	\return Liefert die Menucard mit ID index oder NULL, falls kein Element mit
			dieser ID existiert.
			\param index		ID der zu suchenden Menucard
			\brief Menucard suchen.
		*/
		TOMPrinterstation*	operator [] (int index)
		{
			return (TOMPrinterstation*) TValueList::operator [](index);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse definiert einen Iterator ueber eine TOMPrinterstation Liste.
		\brief TOMPrinterstation Iterator.
	*/
	class			TOMPrinterstationIt
	: public TValueListIt
	{
	public:
		/*!	Erzeugt eine Instanz eines TOMPrinterstation-Iterators.
			\param list		Liste mit Printerstations, ueber die iteriert werden soll.
			\brief ctor.
		*/
		TOMPrinterstationIt(TOMPrinterstations& list)
		: TValueListIt(list)
		{
		}
		TOMPrinterstation*	operator () ()
		{
			return (TOMPrinterstation*) TValueListIt::operator()();
		}
		TOMPrinterstation*	toFirst()
		{
			return (TOMPrinterstation*) TValueListIt::toFirst();
		}
		TOMPrinterstation*	current()
		{
			return (TOMPrinterstation*) TValueListIt::current();
		}
		TOMPrinterstation*	operator ++ ()
		{
			return (TOMPrinterstation*) TValueListIt:: operator ++();
		}
	};
}
using namespace PosLib;
#endif //POSLIB_PRINTERSTATION
