#ifndef				POSLIB_TTAXGROUP_H
#define				POSLIB_TTAXGROUP_H
#include			"poslib/tvatrate.h"
#include			"poslib/ttaxgroup.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	class			TTaxGroupInfo
	: public QStringList
	{
	public:
		TTaxGroupInfo(const QString& str)
		: QStringList(QStringList::split(",", str, TRUE))
		{
		}
		TTaxGroupInfo(TVatRate* vat)
		{
//			*this << QString::number(vat->getID()) << vat->getName() << QString::number(vat->getRate())
//				<< QString::number(vat->getType()) << QString::number(vat->doDiscount());
			*this << QString::number(vat->getID()) << vat->getName() << QString::number(vat->getRate())
				<< QString::number(vat->getType()) << QString::number(vat->doDiscount()?1:0);
		}
		/*
		TTaxGroupInfo(int id, const QString& name, int rate, int type, bool disc)
		{
			*this << QString::number(id) << name << QString::number(rate) << QString::number(type) << QString::number(disc);
		}
		*/
		int			getId() const
		{
			return ((*this)[0]).toInt();
		}
		QString		getName() const
		{
			return (*this)[1];
		}
		int			getRate() const
		{
			return ((*this)[2]).toInt();
		}
		int			getType() const
		{
			return ((*this)[3]).toInt();
		}
		bool		doBrutto() const
		{
			return ((*this)[3]).toInt()==TVatRate::typeBrutto?TRUE:FALSE;
		}
		bool		doNet() const
		{
			return ((*this)[3]).toInt()==TVatRate::typeNetto?TRUE:FALSE;
		}
		bool		doAmount() const
		{
			return ((*this)[3]).toInt()==TVatRate::typeAmount?TRUE:FALSE;
		}
		bool		doWithDiscount() const
		{
			if( count()<4 || !(*this)[4].toInt() )
				return FALSE;
			return TRUE;
		}
		QString		asString()
		{
			return join(",");
		}
		static QString	asString(const QStringList& list)
		{
			return list.join(",");
		}
	};
	
	/*!	\ingroup PosLib
		Diese Klasse umfa? alle bentigten Informationen fr Steuer-Gruppierungen.
		Alle verfgbaren Gruppen werden in einer Instanz von TTaxGroupList
		zur Verfgung gestellt.
		\brief POS-Klassen: Steuergruppierungen.
	*/
	class			TTaxGroup
	: public TNValue
	{
		static const char	entryRates[];
//		static const char	entryIH[];
//		static const char	entryOH[];
	public:
		TTaxGroup()
		: TNValue()
		{
		}
		QStringList		getRates() const
		{
			return QStringList::split(":", getString(entryRates));
		}
		void		setRates(const QStringList& rates)
		{
			if( !rates.count() )
				clrValue(entryRates);
			else
				setValue(entryRates, rates.join(":"));
		}
/*
		int			getIH() const
		{
			return getValue(entryIH, 0);
		}
		void		setIH(int ih)
		{
			if( !ih )
				clrValue(entryIH);
			else
				setValue(entryIH, ih);
		}
		int			getOH() const
		{
			return getValue(entryOH, 0);
		}
		void		setOH(int oh)
		{
			if( !oh )
				clrValue(entryOH);
			else
				setValue(entryOH, oh);
		}
*/
	};

	class			TTaxGroups
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TTaxGroups(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TTaxGroups()
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
		TTaxGroup*	operator [] (int index)
		{
			return (TTaxGroup*) TValueList::operator [](index);
		}
	};

	class			TTaxGroupIt
	: public TValueListIt
	{
	public:
		TTaxGroupIt(TTaxGroups& list)
		: TValueListIt(list)
		{
		}
		TTaxGroup*	operator () ()
		{
			return (TTaxGroup*) TValueListIt::operator()();
		}
		TTaxGroup*	toFirst()
		{
			return (TTaxGroup*) TValueListIt::toFirst();
		}
		TTaxGroup*	current()
		{
			return (TTaxGroup*) TValueListIt::current();
		}
		TTaxGroup*	operator ++ ()
		{
			return (TTaxGroup*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


