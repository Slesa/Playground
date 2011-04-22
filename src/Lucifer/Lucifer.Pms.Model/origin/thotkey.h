#ifndef				POSLIB_THOTKEY_H
#define				POSLIB_THOTKEY_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Schnelltasten.
		Alle verfügbaren Schnelltasten werden in einer Instanz von THotkeyList
		zur Verfügung gestellt.
		\brief POS-Klassen: Schnelltasten.
	*/
	class			THotkey
	: public TValue
	{
		static const char	entryPlu[];
		static const char	entryBitmap[];
	public:
		/*!	Erzeuge eine leere Instanz einer Schnelltaste.
		*/
		THotkey()
		: TValue()
		{
		}
		/*!	\return die mit dieser Schnelltaste verknüpfte PLU als Index
			in die Artikelliste.
			\brief PLU abfragen.
			\sa setPlu, TArticle, TArticleList
		*/
		int			getPlu() const
		{
			return getValue(entryPlu, 0);
		}
		/*!	Ändert die mit dieser Schnelltaste verknüpfte PLU.
			\param plu		Die neue PLU.
			\brief PLU der Schnelltaste ändern.
			\sa getPlu, TArticle, TArticleList
		*/
		void		setPlu(int plu)
		{
			if( !plu )
				clrValue(entryPlu);
			else
				setValue(entryPlu, plu);
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
	};

	class			THotkeys
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		THotkeys(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~THotkeys()
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
		THotkey*	operator [] (int index)
		{
			return (THotkey*) TValueList::operator [](index);
		}
		THotkey*	find(int key, int state)
		{
			return (THotkey*) TValueList::find(getID(key,state));
		}
		static int	getID(int key, int state=0);
	};

	class			THotkeyIt
	: public TValueListIt
	{
	public:
		THotkeyIt(THotkeys& list)
		: TValueListIt(list)
		{
		}
		THotkey*	operator () ()
		{
			return (THotkey*) TValueListIt::operator()();
		}
		THotkey*	toFirst()
		{
			return (THotkey*) TValueListIt::toFirst();
		}
		THotkey*	current()
		{
			return (THotkey*) TValueListIt::current();
		}
		THotkey*	operator ++ ()
		{
			return (THotkey*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif

