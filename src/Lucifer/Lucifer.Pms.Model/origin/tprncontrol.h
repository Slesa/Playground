#ifndef				POSLIB_TPRNCONTROL_H
#define				POSLIB_TPRNCONTROL_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\PosLib
		Diese Klasse umfaßt alle benötigten Informationen für die Bonsteuerung.
		Alle verfügbaren Bonsteuerungs-Einträge werden in einer Instanz von
		TPrnControlList
		zur Verfügung gestellt.
		\brief POS-Klassen: Bonsteuerung.
	*/
	class			TPrnControl
	: public TNValue
	{
		static const char	entryPrint[];
	public:
		/*!	Erzeuge eine leere Instanz einer Bonsteuerung.
		*/
		TPrnControl()
		: TNValue()
		{
		}
		~TPrnControl()
		{
		}
		QString		getPrint() const
		{
			return getString(entryPrint);
		}
		void		setPrint(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryPrint);
			else
				setValue(entryPrint, str);
		}
	};

	class			TPrnControls
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TPrnControls(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TPrnControls()
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
		TPrnControl*		operator [] (int index)
		{
			return (TPrnControl*) TValueList::operator [](index);
		}
	};

	class			TPrnControlIt
	: public TValueListIt
	{
	public:
		TPrnControlIt(TPrnControls& list)
		: TValueListIt(list)
		{
		}
		TPrnControl*	operator () ()
		{
			return (TPrnControl*) TValueListIt::operator()();
		}
		TPrnControl*	toFirst()
		{
			return (TPrnControl*) TValueListIt::toFirst();
		}
		TPrnControl*	current()
		{
			return (TPrnControl*) TValueListIt::current();
		}
		TPrnControl*	operator ++ ()
		{
			return (TPrnControl*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


