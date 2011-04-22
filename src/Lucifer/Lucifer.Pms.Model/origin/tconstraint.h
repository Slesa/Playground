#ifndef				POSLIB_TCONSTRAINT_H
#define				POSLIB_TCONSTRAINT_H
#include			"poslib/tpricelevel.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	class	TConstraintList;

	/*!	\ingroup PosLib
		Diese Klasse umfat alle bentigten Informationen fr Artikelzwnge.
		Alle verfgbaren Zwnge werden in einer Instanz von TConstraintList
		zur Verfgung gestellt.
		\brief POS-Klassen: Artikelzwnge.
	*/
	class			TConstraint
	: public TValue
	{
		static const char	entryPlu[];
	public:
		/*!	Erzeuge eine leere Instanz eines Zwangs.
		*/
		TConstraint()
		: TValue()
		{
		}
		/*!	\return die Artikelnummer des Zwangartikels
			\brief PLU abfragen.
			\sa setPlu
		*/
		int			getPlu() const
		{
			return getValue(entryPlu, 0);
		}
		/*!	ndert die Artikelnummer des Zwangartikels.
			\param plu		Der neue Wert
			\brief Artikelnummer des Zwangartikels ndern.
			\sa getPlu
		*/
		void		setPlu(int plu)
		{
			setValue(entryPlu, plu);
		}
	};

	class			TConstraints
	: public TValueList
	, public TValue
	{
		Q_OBJECT
	public:
		static const char	entryActive[];
		static const char	entryCloseable[];
		static const char	entryType[];
		static const char	entryRemove[];
		static const char	entryTakeCount[];
		static const char	entryRecursive[];
		static const char	entryRepeat[];
		static const char	entryPricelevel[];
		static const char	entryPrnControl[];
		static const char	listName[];
		static const char	elementName[];
		static const char	pathName[];
	public:
		TConstraints(int article, int level, bool autodel=TRUE)
		: TValueList(autodel)
		, m_Article(article)
		, m_Level(level)
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
			QString tmp = "C"+QString::number(m_Article)+"."+QString::number(m_Level);
			return tmp;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		void		clear()
		{
			TValueList::clear();
			TValue::clear();
		}
		void		setParams(int article, int level)
		{
			clear();
			m_Article = article;
			m_Level = level;
		}
		bool		hasParams()
		{
			return m_Level!=0 ? TRUE : FALSE;
		}
		int			getArticle() const
		{
			return m_Article;
		}
		int			getLevel() const
		{
			return m_Level;
		}
		int			getPrnControl() const
		{
			return getValue(entryPrnControl, 0);
		}
		void		setPrnControl(int c)
		{
			if( !c )
				clrValue(entryPrnControl);
			else
				setValue(entryPrnControl, c);
		}
		bool		isActive(TPricelevels* levels=NULL);
		void		setActive(bool flag)
		{
			if( flag )
				clrValue(entryActive);
			else
				setValue(entryActive, flag);
		}
		bool		isCloseable() const
		{
			return getValue(entryCloseable, FALSE);
		}
		void		setCloseable(bool flag)
		{
			if( !flag )
				clrValue(entryCloseable);
			else
				setValue(entryCloseable, flag);
		}
		int			getType() const
		{
			return getValue(entryType, 0);
		}
		void		setType(int type)
		{
			if( !type )
				clrValue(entryType);
			else
				setValue(entryType, type);
		}
		bool		doRemove() const
		{
			return getValue(entryRemove, FALSE);
		}
		void		setRemove(bool flag)
		{
			if( !flag )
				clrValue(entryRemove);
			else
				setValue(entryRemove, flag);
		}
		bool		isTakeCount() const
		{
			return getValue(entryTakeCount, FALSE);
		}
		void		setTakeCount(bool flag)
		{
			if( !flag )
				clrValue(entryTakeCount);
			else
				setValue(entryTakeCount, flag);
		}
		bool		doRecurse() const
		{
			return getValue(entryRecursive, FALSE);
		}
		void		setRecurse(bool flag)
		{
			if( !flag )
				clrValue(entryRecursive);
			else
				setValue(entryRecursive, flag);
		}
		int			getRepeat() const
		{
			return getValue(entryRepeat, 1);
		}
		void		setRepeat(int rep)
		{
			if( rep==1 )
				clrValue(entryRepeat);
			else
				setValue(entryRepeat, rep);
		}
		int			getPricelevel() const
		{
			return getValue(entryPricelevel, 0);
		}
		void		setPricelevel(int lev)
		{
			setValue(entryPricelevel, lev);
		}
		TConstraint*	operator [] (int index)
		{
			return (TConstraint*) TValueList::operator [](index);
		}
		TValue*		find(int index)
		{
			return TValueList::find(index);
		}
		static bool		exists(int art, int lev, const char* path="")
		{
			QString file = makeFilename(art, lev);
			return QFile::exists(TDir::checkPath(TDir::checkPath(path)+QString(pathName))+file);
		}
		static void		copyto(int newart, int oldart, const char* path="");
		static void		moveto(int newart, int oldart, const char* path="");
		static void		remove(int art, const char* path="");
		bool		exists(const char* path="")
		{
			QString file = getFileName();
			return QFile::exists(TDir::checkPath(TDir::checkPath(path)+pathName)+file);
		}
		int			load(const char* path=NULL)
		{
			return load(getFileName(), TDir::checkPath(TDir::checkPath(path)+pathName));
		}
		void		save(const char* path=NULL)
		{
			if( TValueList::count()==0 )
			{
				if( exists(path) )
					TFile::remove(TDir::checkPath(TDir::checkPath(path)+pathName)+getFileName());
				return;
			}
			save(getFileName(), TDir::checkPath(TDir::checkPath(path)+QString(pathName)));
		}
		virtual void	insert(TValue* item)
		{
			TValueList::insert(item);
		}
		virtual bool	remove(TValue* item)
		{
			return TValueList::remove(item);
		}
		virtual void	change(TValue* item)
		{
			TValueList::change(item);
		}
		int				count()
		{
			return TValueList::count();
		}
		virtual void	importXml(const QDomElement& root)
		{
			TValue::importXml(root);
		}
		static TConstraintList*	getAllConstraints(const char* path="");
	protected:
		virtual int		load(const QString& file, const char* path);
		virtual void	save(const QString& file, const char* path);
		static QString	makeFilename(int art, int level)
		{
			QString tmp = "C"+QString::number(art)+"."+QString::number(level);
			return tmp;
		}
	protected:
		int			m_Article;
		int			m_Level;
	};

	QDataStream&	operator << (QDataStream& st, const TConstraints& list);
	QDataStream&	operator >> (QDataStream& st, TConstraints& list);

	class			TConstraintIt
	: public TValueListIt
	{
	public:
		TConstraintIt(TConstraints& list)
		: TValueListIt(list)
		{
		}
		TConstraint*	operator () ()
		{
			return (TConstraint*) TValueListIt::operator()();
		}
		TConstraint*	toFirst()
		{
			return (TConstraint*) TValueListIt::toFirst();
		}
		TConstraint*	current()
		{
			return (TConstraint*) TValueListIt::current();
		}
		TConstraint*	operator ++ ()
		{
			return (TConstraint*) TValueListIt:: operator ++();
		}
	};
	
	class		TConstraintList
	: public TValueList
	{
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TConstraintList(bool autodel=TRUE)
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
		virtual QString	getElementName()
		{
			return elementName;
		}
	};
}

using namespace PosLib;

#endif


