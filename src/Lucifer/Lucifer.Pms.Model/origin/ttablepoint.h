#ifndef				POSLIB_TTABLEPOINT_H
#define				POSLIB_TTABLEPOINT_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\brief POS-Klassen: Tisch-Übergabepunkte.
	*/
	class			TTablePoint
	: public TNValue
	{
		static const char	entryPath[];
		static const char	entryWaiters[];
	public:
		/*!	Erzeuge eine leere Instanz eines Tischübergabe-Punktes.
		*/
		TTablePoint()
		: TNValue()
		{
		}
		QString		getPath() const
		{
			return getString(entryPath);
		}
		void		setPath(const QString& path)
		{
			if( path.isEmpty() )
				clrValue(entryPath);
			else
				setValue(entryPath, path);
		}
		QString		getWaiters() const
		{
			return getString(entryWaiters);
		}
		void		setWaiters(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryWaiters);
			else
				setValue(entryWaiters, str);
		}
	};

	class			TTablePoints
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TTablePoints(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TTablePoints()
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
		TTablePoint*	operator [] (int index)
		{
			return (TTablePoint*) TValueList::operator [](index);
		}
	};

	class			TTablePointIt
	: public TValueListIt
	{
	public:
		TTablePointIt(TTablePoints& list)
		: TValueListIt(list)
		{
		}
		TTablePoint*	operator () ()
		{
			return (TTablePoint*) TValueListIt::operator()();
		}
		TTablePoint*	toFirst()
		{
			return (TTablePoint*) TValueListIt::toFirst();
		}
		TTablePoint*	current()
		{
			return (TTablePoint*) TValueListIt::current();
		}
		TTablePoint*	operator ++ ()
		{
			return (TTablePoint*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


