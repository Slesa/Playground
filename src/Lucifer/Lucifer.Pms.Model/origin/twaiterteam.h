#ifndef				POSLIB_TWAITERTEAM_H
#define				POSLIB_TWAITERTEAM_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Kellnerteams.
		Alle verfügbaren Kellnertream-Informationen werden in einer Instanz von TWaiterTeams
		zur Verfügung gestellt.
		\brief POS-Klassen: Kellnerteams.
	*/
	class			TWaiterTeam
	: public TNValue
	{
	public:
		static const char	entryTables[];
		static const char	entryArts[];
	public:
		/*!	Erzeuge eine leere Instanz eines Kellnerteams.
		*/
		TWaiterTeam()
		: TNValue()
		{
		}
		~TWaiterTeam()
		{
		}
		QString		strTableRange() const
		{
			return getString(entryTables);
		}
		QStringList	getTableRange() const
		{
			return QStringList::split(";", getValue(entryTables));
		}
		void		setTableRange(const QString& range)
		{
			if( range.isEmpty() )
				clrValue(entryTables);
			else
				setValue(entryTables, range);
		}
		QString		strArtRange() const
		{
			return getString(entryArts);
		}
		QStringList	getArtRange() const
		{
			return QStringList::split(";", getValue(entryArts));
		}
		void		setArtRange(const QString& range)
		{
			if( range.isEmpty() )
				clrValue(entryArts);
			else
				setValue(entryArts, range);
		}
	};

	class			TWaiterTeams
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TWaiterTeams(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TWaiterTeams()
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
		TWaiterTeam*	operator [] (int index)
		{
			return (TWaiterTeam*) TValueList::operator [](index);
		}
	};

	class			TWaiterTeamIt
	: public TValueListIt
	{
	public:
		TWaiterTeamIt(TWaiterTeams& list)
		: TValueListIt(list)
		{
		}
		TWaiterTeam*	operator () ()
		{
			return (TWaiterTeam*) TValueListIt::operator()();
		}
		TWaiterTeam*	toFirst()
		{
			return (TWaiterTeam*) TValueListIt::toFirst();
		}
		TWaiterTeam*	current()
		{
			return (TWaiterTeam*) TValueListIt::current();
		}
		TWaiterTeam*	operator ++ ()
		{
			return (TWaiterTeam*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


