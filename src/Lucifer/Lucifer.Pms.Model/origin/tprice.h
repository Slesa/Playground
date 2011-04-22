#ifndef				POSLIB_TPRICE_H
#define				POSLIB_TPRICE_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für Artikelpreise.
		Alle verfügbaren Preise werden in einer Instanz von TPriceList
		zur Verfügung gestellt.
		\brief POS-Klassen: Artikelpreise.
	*/
	class			TPrice
	: public TValue
	{
	public:
		static const char	entryPrice[];
		static const char	entryCurrency[];
		static const char	entryArticle[];
		static const char	entryLevel[];
	public:
		/*!	Erzeuge eine leere Instanz eines Preises.
		*/
		TPrice()
		: TValue()
		{
		}
		/*!	Erzeuge eine Instanz eines Preises als Kopie von pr.
			\param pr		zu kopierender Preis.
		TPrice(const TPrice& pr)
		: TValue(pr)
		{
		}
		*/
		~TPrice()
		{
		}
		/*!	\return den Artikelpreis.
			\brief Preis abfragen.
			\sa setPrice
		*/
		long			getPrice() const
		{
			return getValue(entryPrice, 0L);
		}
		/*!	Ändert den Artikelpreis.
			\param price		Der neue Preis
			\brief Preis ändern.
			\sa getPrice
		*/
		void			setPrice(long price)
		{
			setValue(entryPrice, price);
		}
		int			getLevel() const
		{
			return getValue(entryLevel, 0);
		}
		void			setLevel(int level)
		{
			setValue(entryLevel, level);
		}
		int			getCurrency() const
		{
			return getValue(entryCurrency, 0);
		}
		void			setCurrency(int curr)
		{
			setValue(entryCurrency, curr);
		}
	};

	class			TPrices
	: public TValueList
	{
		Q_OBJECT
	public:
		static const char	listName[];					//!< Name der Liste (articles)
		static const char	elementName[];				//!< Name eines Elements der Liste (article)
		static const char	pathName[];
	public:
		TPrices(int article, int curr, bool autodel=TRUE)
		: TValueList(autodel)
		, m_Article(article)
		, m_Currency(curr)
		{
		}
		~TPrices()
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
		TPrice*		operator [] (int index)
		{
			return (TPrice*) TValueList::operator [](index);
		}
		bool		exists(const char* path="")
		{
			QString file = makeFilename();
			return QFile::exists(TDir::checkPath(TDir::checkPath(path)+QString(pathName))+file);
		}
		int			load(const char* path=NULL)
		{
			return TValueList::load(makeFilename(), TDir::checkPath(TDir::checkPath(path)+QString(pathName)));
		}
		void		save(const char* path=NULL)
		{
			if( count()==0 )
			{
				if( exists(path) )
					TFile::remove(TDir::checkPath(TDir::checkPath(path)+pathName)+makeFilename());
				return;
			}
			TValueList::save(makeFilename(), TDir::checkPath(TDir::checkPath(path)+QString(pathName)));
		}
		/*!	Exportiert die Liste in eine XML-Datei unterhalb des Knotens root.
			\param root		Root-Knoten, in den die Liste eingefügt wird.
			\brief Liste nach XML exportieren.
			\note getListName() und getElementName() müssen hierfür überschrieben worden sein.
		*/
//		virtual void	importXml(const QDomElement& root);
		/*!	Importiert die Liste aus der XML-Datei doc aus dem Knoten root.
			\param doc		Die XML-Datei
			\param root		Root-Knoten, aus dem die Liste importiert wird.
			\brief Liste aus XML importieren.
			\note getListName() und getElementName() müssen hierfür überschrieben worden sein.
		*/
		static void	exportXml(QDomDocument& doc, QDomElement& root, const char* path="");
		static void	exportCSV(QTextStream& doc, QStringList& tags, const char* path="");
		static bool		exists(int art, int curr, const char* path="")
		{
			QString file = makeFilename(art, curr);
			return QFile::exists(TDir::checkPath(path+QString(pathName))+file);
		}
		static void		copyto(int newart, int oldart, const char* path="");
		static void		moveto(int newart, int oldart, const char* path="");
		static void		remove(int art, const char* path="");
		static void		copyLevel(int level, int oldlev, const char* path="");
		static void		moveLevel(int newlev, int oldlev, const char* path="");
		static void		removeLevel(int level, const char* path="");
		virtual bool	remove(TValue* item)
		{
			return TValueList::remove(item);
		}
		static TPrices*	getAllPrices(const char* path="");
	protected:
		QString		makeFilename()
		{
			QString tmp;
			tmp.sprintf("P%d.%d", m_Article, m_Currency);
			return tmp;
		}
		static QString	makeFilename(int art, int curr)
		{
			QString tmp;
			tmp.sprintf("P%d.%d", art, curr);
			return tmp;
		}
	protected:
		int			m_Article;
		int			m_Currency;
	};

	class			TPriceIt
	: public TValueListIt
	{
	public:
		TPriceIt(TPrices& list)
		: TValueListIt(list)
		{
		}
		TPrice*		operator () ()
		{
			return (TPrice*) TValueListIt::operator()();
		}
		TPrice*		toFirst()
		{
			return (TPrice*) TValueListIt::toFirst();
		}
		TPrice*		current()
		{
			return (TPrice*) TValueListIt::current();
		}
		TPrice*		operator ++ ()
		{
			return (TPrice*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


