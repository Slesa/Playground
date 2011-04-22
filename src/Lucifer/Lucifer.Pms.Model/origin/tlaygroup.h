#ifndef				POSLIB_TLAYGROUP_H
#define				POSLIB_TLAYGROUP_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Layout-Gruppen.
		Alle verf�gbaren Layout-Gruppen werden in einer Instanz von TLayGroups
		zur Verf�gung gestellt.
		Layout-Gruppen dienen dazu, da� unter Artikel/Warengruppe/Ober-WG eingestellte
		Layout w�hrend des Bestellvorgangs zu �berschreiben.
		\brief POS-Klassen: Layout-Gruppen.
	*/
	class			TLayGroup
	: public TNValue
	{
		static const char	entryPrinters[];
	public:
		/*!	Erzeuge eine leere Instanz einer Layout-Gruppe.
		*/
		TLayGroup()
		: TNValue()
		{
		}
		~TLayGroup()
		{
		}
		/*!	\return Liefert als Stringliste die Kombinationen von Drucker und Layout
			als Index in die entsprechenden Tabellen  f�r den Bondruck.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert bildet den Default f�r alle Warengruppe dieser Oberwarengruppe.
			\code
			QStringList list = QStringList::split(";", grp->getPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Oberwaren f�r Bons ermitteln
			\sa setPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPrinters() const
		{
			return getString(entryPrinters);
		}
		/*!	�ndert die Drucker/Layout-Kombinationen, mit denen Artikel dieser Oberwarengruppe beim
			Bestellen ausgedruckt werden soll. Siehe getPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gel�scht.
			\note Dieser Wert bildet den Default f�r alle Warengruppe dieser Oberwarengruppe.
			\brief Ausgabedrucker und -layout der Oberwarengruppe f�r Bons �ndern.
			\sa getPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		void		setPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entryPrinters);
			else
				setValue(entryPrinters, prns);
		}
	};

	class			TLayGroups
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TLayGroups(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TLayGroups()
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
		TLayGroup*	operator [] (int index)
		{
			return (TLayGroup*) TValueList::operator [](index);
		}
	};

	class			TLayGroupIt
	: public TValueListIt
	{
	public:
		TLayGroupIt(TLayGroups& list)
		: TValueListIt(list)
		{
		}
		TLayGroup*	operator () ()
		{
			return (TLayGroup*) TValueListIt::operator()();
		}
		TLayGroup*	toFirst()
		{
			return (TLayGroup*) TValueListIt::toFirst();
		}
		TLayGroup*	current()
		{
			return (TLayGroup*) TValueListIt::current();
		}
		TLayGroup*	operator ++ ()
		{
			return (TLayGroup*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


