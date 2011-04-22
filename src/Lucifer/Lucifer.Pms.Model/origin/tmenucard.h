#ifndef				POSLIB_TMENUCARD_H
#define				POSLIB_TMENUCARD_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\brief POS-Klassen: Men-Zusammenstellungen.
	*/
	class			TMenuCard
	: public TNValue
	{
//		static const char	entryValidFrom[];
//		static const char	entryValidTo[];
		static const char	entryExcludes[];
	public:
		/*!	Erzeuge eine leere Instanz einer Kostenstelle.
			\brief ctor.
		*/
		TMenuCard()
		: TNValue()
		{
		}
		/*
		QDate		getValidFrom() const
		{
			return getDate(entryValidFrom);
		}
		void		setValidFrom(const QDate& from)
		{
			if( !from.isValid() )
				clrValue(entryValidFrom);
			else
				setValue(entryValidFrom, from);
		}
		QDate		getValidTo() const
		{
			return getDate(entryValidTo);
		}
		void		setValidTo(const QDate& to)
		{
			if( !to.isValid() )
				clrValue(entryValidTo);
			else
				setValue(entryValidTo, to);
		}
		*/
		bool		contains(int plu)
		{
			return m_Cache.contains(plu);
		}
		QStringList	getExcludes() const
		{
			return QStringList::split(":", getString(entryExcludes));
		}
		void		setExcludes(const QStringList& arts)
		{
			if( !arts.count() )
				clrValue(entryExcludes);
			else
				setValue(entryExcludes, arts.join(":"));
			resort();
		}
	public:
		void		resort();
	protected:
		QMap<int,bool>	m_Cache;
	};

	/*!	\ingroup PosLib
		Diese Klasse fa� mehrere TMenuCard-Elemente zu einer Liste zusammen. Die fr die
		XML-Funktionen n�igen TValueList-Funktionen wurden berschrieben.
		\brief POS-Klassen: Liste von Men-Zusammenstellungen.
	*/
	class			TMenuCards
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste (costcenters)
		static const char	elementName[];				//!< Name eines Elements der Liste (costcenter)
	public:
		/*!	Erzeugt eine neue Instanz einer Men-Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste gel�cht.
			\brief ctor.
		*/
		TMenuCards(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zerst�t die Instanz der Menliste.
			\brief dtor.
		*/
		~TMenuCards()
		{
		}
		virtual TValue*	createValue()
		{
			return new TMenuCard();
		}
		virtual int		load(const char* path="")
		{
			return TValueList::load(path);
		}
		virtual int		load(const QString& file, const char* path);
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
		TMenuCard*	operator [] (int index)
		{
			return (TMenuCard*) TValueList::operator [](index);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse definiert einen Iterator ber eine TCostCenter-Liste.
		\brief TCosCenters-Iterator.
	*/
	class			TMenuCardIt
	: public TValueListIt
	{
	public:
		/*!	Erzeugt eine Instanz eines TMenuCards-Iterators.
			\param list		Liste mit Men-Karten, ber die iteriert werden soll.
			\brief ctor.
		*/
		TMenuCardIt(TMenuCards& list)
		: TValueListIt(list)
		{
		}
		TMenuCard*	operator () ()
		{
			return (TMenuCard*) TValueListIt::operator()();
		}
		TMenuCard*	toFirst()
		{
			return (TMenuCard*) TValueListIt::toFirst();
		}
		TMenuCard*	current()
		{
			return (TMenuCard*) TValueListIt::current();
		}
		TMenuCard*	operator ++ ()
		{
			return (TMenuCard*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


