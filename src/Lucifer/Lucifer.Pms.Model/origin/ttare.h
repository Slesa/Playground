#ifndef				POSLIB_TTARE_H
#define				POSLIB_TTARE_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Tara-Gewichte.
		Alle verfügbaren Taras werden in einer Instanz von TTareList
		zur Verfügung gestellt.
		\brief POS-Klassen: Tara-Gewichte.
	*/
	class			TTare
	: public TNValue
	{
	public:
		static const char	entryTare[];
	public:
		/*!	Erzeuge eine leere Instanz eines Taras.
		*/
		TTare()
		: TNValue()
		{
		}
		/*!	\return das Taragewicht.
			\brief Tara abfragen.
			\sa setTare
		*/
		long			getTare() const
		{
			return getValue(entryTare, 0L);
		}
		/*!	Ändert das Gewicht des Taras.
			\param tare		Das neue Gewicht
			\brief Tara ändern.
			\sa getTare
		*/
		void			setTare(long tare)
		{
			setValue(entryTare, tare);
		}
	};

	class			TTares
	: public TValueList
	{
		Q_OBJECT
	public:
		static const char	listName[];					//!< Name der Liste (tares)
		static const char	elementName[];				//!< Name eines Elements der Liste (tare)
		static const char	fileName[];
	public:
		TTares(bool autodel=TRUE)
		: TValueList(autodel)
		{
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
		TTare*		operator [] (int index)
		{
			return (TTare*) TValueList::operator [](index);
		}
		virtual QString	getFileName() const
		{
			return fileName;
		}
	};

	class			TTareIt
	: public TValueListIt
	{
	public:
		TTareIt(TTares& list)
		: TValueListIt(list)
		{
		}
		TTare*		operator () ()
		{
			return (TTare*) TValueListIt::operator()();
		}
		TTare*		toFirst()
		{
			return (TTare*) TValueListIt::toFirst();
		}
		TTare*		current()
		{
			return (TTare*) TValueListIt::current();
		}
		TTare*		operator ++ ()
		{
			return (TTare*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


