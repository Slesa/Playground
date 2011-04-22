#ifndef				POSLIB_TLAYGROUP_H
#define				POSLIB_TLAYGROUP_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Layout-Gruppen.
		Alle verfügbaren Layout-Gruppen werden in einer Instanz von TLayGroups
		zur Verfügung gestellt.
		Layout-Gruppen dienen dazu, daß unter Artikel/Warengruppe/Ober-WG eingestellte
		Layout während des Bestellvorgangs zu überschreiben.
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
			als Index in die entsprechenden Tabellen  für den Bondruck.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert bildet den Default für alle Warengruppe dieser Oberwarengruppe.
			\code
			QStringList list = QStringList::split(";", grp->getPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Oberwaren für Bons ermitteln
			\sa setPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPrinters() const
		{
			return getString(entryPrinters);
		}
		/*!	Ändert die Drucker/Layout-Kombinationen, mit denen Artikel dieser Oberwarengruppe beim
			Bestellen ausgedruckt werden soll. Siehe getPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelöscht.
			\note Dieser Wert bildet den Default für alle Warengruppe dieser Oberwarengruppe.
			\brief Ausgabedrucker und -layout der Oberwarengruppe für Bons ändern.
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


