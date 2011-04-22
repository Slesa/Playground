#ifndef				POSLIB_TDEPARTMENT_H
#define				POSLIB_TDEPARTMENT_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Sparten.
		Alle verfügbaren Sparten werden in einer Instanz von TDepartmentList
		zur Verfügung gestellt.
		\brief POS-Klassen: Sparten.
	*/
	class			TDepartment
	: public TNValue
	{
		static const char	entryPrio[];
		static const char	entryBitmap[];
		static const char	entryChildren[];
		static const char	entryArticles[];
		static const char	entryIsLeaf[];
		static const char	entryIsSubvent[];
	public:
		/*!	Erzeuge eine leere Instanz einer Sparte.
		*/
		TDepartment()
		: TNValue()
		{
		}
		/*!	\return Die Priorität für den Touch innerhalb einer Sparte. Sparten mit
			höherer Prio sollten weiter vorne stehen.
			\brief Touch, Gruppen-Priorität ermitteln.
		*/
		int			getPrio() const
		{
			return getValue(entryPrio, 0);
		}
		void		setPrio(int prio)
		{
			if( !prio )
				clrValue(entryPrio);
			else
				setValue(entryPrio, prio);
		}
		QString		getBitmap() const
		{
			return getString(entryBitmap);
		}
		void		setBitmap(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryBitmap);
			else
				setValue(entryBitmap, str);
		}
		bool		isSubvention() const
		{
			return getValue(entryIsSubvent, FALSE);
		}
		void		setIsSubvention(bool is)
		{
			setValue(entryIsSubvent, is);
		}
		/*!	\return Liefert TRUE, wenn die Sparte ein Blatt ist, also keine weiteren Untersparten mehr
			besitzt, sondern nur noch PLUs.
			\brief Sparten-Blatt?
			\sa setIsLeaf
		*/
		bool		isLeaf() const
		{
			return getValue(entryIsLeaf, TRUE);
		}
		/*!	Ändert das Flag, ob diese Sparte noch weitere Untersparten besitzt oder nur noch PLUs beinhaltet.
			\param	is		TRUE = keine weiteren Untersparten.
			\brief Flag Sparten-Blatt ändern.
			\sa isLeaf
		*/
		void		setIsLeaf(bool is)
		{
			setValue(entryIsLeaf, is);
		}
		/*!	\return Liefert die untergeordneten Sparten oder eine leere Liste, falls es keine gibt.
			\brief Untersparten abfragen.
			\sa strChildren,setChildren
		*/
		QStringList	getChildren() const
		{
			return QStringList::split(";", getValue(entryChildren));
		}
		/*!	\return Liefert die untergeordneten Sparten oder einen leeren String, falls es keine gibt.
			\brief Untersparten abfragen.
			\sa getChildren,setChildren
		*/
		QString		strChildren() const
		{
			return getString(entryChildren);
		}
		/*!	Ändert die Untergeordneten Sparten dieser Sparte.
			\param children	Die neuen untergeordneten Sparten als Index, getrennt durch ;
			\brief Untersparten ändern.
			\sa getChildren, strChildren
		*/
		void		setChildren(const QString& children)
		{
			if( children.isEmpty() )
				clrValue(entryChildren);
			else
				setValue(entryChildren, children);
		}
		QStringList	getArticles() const
		{
			return QStringList::split(";", getValue(entryArticles));
		}
		QString		strArticles() const
		{
			return getString(entryArticles);
		}
		void		setArticles(const QString& arts)
		{
			if( arts.isEmpty() )
				clrValue(entryArticles);
			else
				setValue(entryArticles, arts);
		}
	};

	class			TDepartments
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TDepartments(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TDepartments()
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
		TDepartment*	operator [] (int index)
		{
			return (TDepartment*) TValueList::operator [](index);
		}
		int			hasPlu(int plu, int fam, TDepartment* dep);
	};

	class			TDepartmentIt
	: public TValueListIt
	{
	public:
		TDepartmentIt(TDepartments& list)
		: TValueListIt(list)
		{
		}
		TDepartment*	operator () ()
		{
			return (TDepartment*) TValueListIt::operator()();
		}
		TDepartment*	toFirst()
		{
			return (TDepartment*) TValueListIt::toFirst();
		}
		TDepartment*	current()
		{
			return (TDepartment*) TValueListIt::current();
		}
		TDepartment*	operator ++ ()
		{
			return (TDepartment*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


