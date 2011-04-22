#ifndef				POSLIB_TSHIFTGROUP_H
#define				POSLIB_TSHIFTGROUP_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\brief POS-Klassen: Schichtgruppen.
	*/
	class			TShiftGroup
	: public TNValue
	{
	public:
		TShiftGroup()
		: TNValue()
		{
		}
		~TShiftGroup()
		{
		}
	};

	class			TShiftGroups
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TShiftGroups(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TShiftGroups()
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
	/*
		virtual void	load(const char* path="")
		{
			load(getFileName(), TDir::checkPath(TDir::checkPath(path)+getDefPath()));
		}
		virtual void	save(const char* path="")
		{
			save(getFileName(), TDir::checkPath(TDir::checkPath(path)+getDefPath()));
		}
		virtual void	load(const QString& file, const char* path)
		{
			m_Path = path;
			TValueList::load(file, path);
		}
		virtual void	save(const QString& file, const char* path)
		{
			m_Path = path;
			TValueList::save(file, path);
		}
		virtual bool	remove(TValue* item);
	*/
		TShiftGroup*	operator [] (int index)
		{
			return (TShiftGroup*) TValueList::operator [](index);
		}
	};

	class			TShiftGroupIt
	: public TValueListIt
	{
	public:
		TShiftGroupIt(TShiftGroups& list)
		: TValueListIt(list)
		{
		}
		TShiftGroup*	operator () ()
		{
			return (TShiftGroup*) TValueListIt::operator()();
		}
		TShiftGroup*	toFirst()
		{
			return (TShiftGroup*) TValueListIt::toFirst();
		}
		TShiftGroup*	current()
		{
			return (TShiftGroup*) TValueListIt::current();
		}
		TShiftGroup*	operator ++ ()
		{
			return (TShiftGroup*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif

