#ifndef				POSLIB_TPRNLAYOUT_H
#define				POSLIB_TPRNLAYOUT_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Druckerlayouts.
		Alle verfügbaren Druckerlayouts werden in einer Instanz von TPrinterLayoutList
		zur Verfügung gestellt.
		\brief POS-Klassen: Druckerlayouts.
	*/
	class			TPrnLayout
	: public TNValue
	{
		static const char	entryText[];
	public:
		/*!	Erzeuge eine leere Instanz eines Druckerlaoyuts.
		*/
		TPrnLayout()
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
		~TPrnLayout()
		{
		}
		QString		getText() const
		{
			return getString(entryText);
		}
		void		setText(const QString& text)
		{
			if( text.isEmpty() )
				clrValue(entryText);
			else
				setValue(entryText, text);
		}
	};

	class			TPrnLayouts
	: public TValueList
	{
		Q_OBJECT
	public:
//		static const char	fileName[];
		static const char	pathName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TPrnLayouts(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TPrnLayouts()
		{
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		TPrnLayout*	operator [] (int index)
		{
			return (TPrnLayout*) TValueList::operator [](index);
		}
		int			find(const QString& str);
		virtual int	load(const char* path=NULL);
		void		save(const char* path=NULL);
	};

	class			TPrnLayoutIt
	: public TValueListIt
	{
	public:
		TPrnLayoutIt(TPrnLayouts& list)
		: TValueListIt(list)
		{
		}
		TPrnLayout*	operator () ()
		{
			return (TPrnLayout*) TValueListIt::operator()();
		}
		TPrnLayout*	toFirst()
		{
			return (TPrnLayout*) TValueListIt::toFirst();
		}
		TPrnLayout*	current()
		{
			return (TPrnLayout*) TValueListIt::current();
		}
		TPrnLayout*	operator ++ ()
		{
			return (TPrnLayout*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


