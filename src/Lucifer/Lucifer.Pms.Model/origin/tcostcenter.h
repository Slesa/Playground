#ifndef				POSLIB_TCOSTCENTER_H
#define				POSLIB_TCOSTCENTER_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Kostenstellen.
		Alle verf�gbaren Kostenstellen-Informationen werden in einer Instanz von TCostCenterList
		zur Verf�gung gestellt.
		\note Die Liste der Kostenstellen mu� nicht alle verf�gbaren Kostellen
		umfassen.
		\brief POS-Klassen: Kostenstellen.
	*/
	class			TCostCenter
	: public TNValue
	{
		static const char	entrySerial[];
		static const char	entryPath[];
		static const char	entryAccount[];
	public:
		/*!	Erzeuge eine leere Instanz einer Kostenstelle.
			\brief ctor.
		*/
		TCostCenter()
		: TNValue()
		{
		}
		/*!	Erzeuge eine Instanz einer Kostenstelle als Kopie von center.
			\param center	die zu kopierende Kostenstelle.
		TCostCenter(const TCostCenter& center)
		: TNValue(center)
		{
		}
		*/
		/*!	Zerst�rt die Instanz der Kostenstelle.
			\brief dtor.
		*/
		~TCostCenter()
		{
		}
		/*!	\return die Seriennummer des Dongles dieser Kostenstelle.
			\brief Seriennummer des Dongles abfragen.
			\sa setSerial
		*/
		long		getSerial() const
		{
			return getValue(entrySerial, 0L);
		}
		/*!	�ndert die Seriennummer des Dongles dieser Kostenstelle.
			\param serial	Die neue Donglenummer
			\brief Seriennummer des Dongles �ndern.
			\sa getSerial
		*/
		void		setSerial(long serial)
		{
			if( !serial )
				clrValue(entrySerial);
			else
				setValue(entrySerial, serial);
		}
		/*!	\return der Pfad zu den Daten dieser Kostenstelle.
			\brief Pfad zu den Daten abfragen.
			\sa setPath
		*/
		QString		getPath() const
		{
			return getString(entryPath, "");
		}
		/*!	�ndert den Pfad zu den Daten dieser Kostenstelle.
			\param path		Der neue Pfad
			\brief Pfad zu den Daten �ndern.
			\sa getPath
		*/
		void		setPath(const QString& path)
		{
			if( path.isEmpty() )
				clrValue(entryPath);
			else
				setValue(entryPath, path);
		}
		QString		getAccount() const
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
		Diese Klasse fa�t mehrere TCostCenter-Elemente zu einer Liste zusammen. Die f�r die
		XML-Funktionen n�tigen TValueList-Funktionen wurden �berschrieben.
		\brief POS-Klassen: Liste von Kostenstellen.
	*/
	class			TCostCenters
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste (costcenters)
		static const char	elementName[];				//!< Name eines Elements der Liste (costcenter)
	public:
		/*!	Erzeugt eine neue Instanz einer Kostenstellen-Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste gel�scht.
			\brief ctor.
		*/
		TCostCenters(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zerst�rt die Instanz der Kostenstellenliste.
			\brief dtor.
		*/
		~TCostCenters()
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
		/*!	\return Liefert die Kostenstelle mit ID index oder NULL, falls kein Element mit
			dieser ID existiert.
			\param index		ID der zu suchenden Kostenstelle.
			\brief Kostenstelle suchen.
		*/
		TCostCenter*	operator [] (int index)
		{
			return (TCostCenter*) TValueList::operator [](index);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse definiert einen Iterator �ber eine TCostCenter-Liste.
		\brief TCosCenters-Iterator.
	*/
	class			TCostCenterIt
	: public TValueListIt
	{
	public:
		/*!	Erzeugt eine Instanz eines TCostCenters-Iterators.
			\param list		Liste mit Kostenstellen, �ber die iteriert werden soll.
			\brief ctor.
		*/
		TCostCenterIt(TCostCenters& list)
		: TValueListIt(list)
		{
		}
		TCostCenter*	operator () ()
		{
			return (TCostCenter*) TValueListIt::operator()();
		}
		TCostCenter*	toFirst()
		{
			return (TCostCenter*) TValueListIt::toFirst();
		}
		TCostCenter*	current()
		{
			return (TCostCenter*) TValueListIt::current();
		}
		TCostCenter*	operator ++ ()
		{
			return (TCostCenter*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


