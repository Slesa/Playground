#ifndef				POSLIB_TEANCODE_H
#define				POSLIB_TEANCODE_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r EAN-Codes.
		Alle verf�gbaren EAN-Codes werden in einer Instanz von TEanCodeList
		zur Verf�gung gestellt.
		\brief POS-Klassen: EAN-Codes.
	*/
	class			TEanCode
	: public TValue
	{
		static const char	entryEan[];
		static const char	entryPlu[];
	public:
		/*!	Erzeuge eine leere Instanz eines EAN-Codes.
		*/
		TEanCode()
		: TValue()
		{
		}
		/*!	\return der EAN-Code des Artikels.
			\brief EAN-Code abfragen.
			\sa setEan
		*/
		QString			getEan() const
		{
			return getString(entryEan);
		}
		/*!	�ndert den EAN-Code des Artikels
			\param ean		Der neue EAN-Code.
							Ist der Wert leer, wird das Attribut gel�scht.
			\brief EAN-Code des Artikels �ndern.
			\sa getEan
		*/
		void		setEan(const QString& ean)
		{
			if( ean.isEmpty() )
				clrValue(entryEan);
			else
				setValue(entryEan, ean);
		}
		/*!	\return den zu diesem EAN-Code geh�rigen Artikel.
			Der zur�ckgegebene Wert ist ein Index auf die Artikel-Liste.
			\brief PLU zur EAN abfragen.
			\sa setPlu, TArticle, TArticleList
		*/
		int			getPlu() const
		{
			return getValue(entryPlu, 0);
		}
		/*!	�ndert den zu dieser EAN geh�rigen Artikel.
			\param plu		Der neue Artikel als Index auf die Artikelliste.
							Ist der Wert 0, wird das Attribut gel�scht.
			\brief PLU �ndern.
			\sa getPlu, TArticle, TArticleList
		*/
		void		setPlu(int plu)
		{
			if( !plu )
				clrValue(entryPlu);
			else
				setValue(entryPlu, plu);
		}
	};

	class			TEanCodes
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TEanCodes(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TEanCodes()
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
		TEanCode*	byEan(const QString& ean);
		TEanCode*	byPlu(int plu);
		TEanCode*	operator [] (int index)
		{
			return (TEanCode*) TValueList::operator [](index);
		}
	};

	class			TEanCodeIt
	: public TValueListIt
	{
	public:
		TEanCodeIt(TEanCodes& list)
		: TValueListIt(list)
		{
		}
		TEanCode*	operator () ()
		{
			return (TEanCode*) TValueListIt::operator()();
		}
		TEanCode*	toFirst()
		{
			return (TEanCode*) TValueListIt::toFirst();
		}
		TEanCode*	current()
		{
			return (TEanCode*) TValueListIt::current();
		}
		TEanCode*	operator ++ ()
		{
			return (TEanCode*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


