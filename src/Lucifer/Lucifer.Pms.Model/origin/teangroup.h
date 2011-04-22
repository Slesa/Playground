#ifndef				POSLIB_TEANGROUP_H
#define				POSLIB_TEANGROUP_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa� alle ben�igten Informationen fr EAN-Codes.
		Alle verfgbaren EAN-Codes werden in einer Instanz von TEanCodeList
		zur Verfgung gestellt.
		\brief POS-Klassen: EAN-Codes.
	*/
	class			TEanGroup
	: public TValue
	{
		static const char	entryPrefix[];
		static const char	entryForcePlu[];
		static const char	entryPluPos[];
		static const char	entryPluLen[];
		static const char	entryPricePos[];
		static const char	entryPriceLen[];
		static const char	entryWeightPos[];
		static const char	entryWeightLen[];
	public:
		/*!	Erzeuge eine leere Instanz eines EAN-Codes.
		*/
		TEanGroup()
		: TValue()
		{
		}
		QString		getPrefix() const
		{
			return getString(entryPrefix);
		}
		void		setPrefix(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryPrefix);
			else
				setValue(entryPrefix, str);
		}
		int			getForcePlu() const
		{
			return getValue(entryForcePlu, 0);
		}
		void		setForcePlu(int plu)
		{
			if( !plu )
				clrValue(entryForcePlu);
			else
				setValue(entryForcePlu, plu);
		}
		int			getPluPos() const
		{
			return getValue(entryPluPos, 0);
		}
		void		setPluPos(int pos)
		{
			if( !pos )
				clrValue(entryPluPos);
			else
				setValue(entryPluPos, pos);
		}
		int			getPluLen() const
		{
			return getValue(entryPluLen, 1);
		}
		void		setPluLen(int len)
		{
			if( len==1 )
				clrValue(entryPluLen);
			else
				setValue(entryPluLen, len);
		}
		int			getPricePos() const
		{
			return getValue(entryPricePos, 0);
		}
		void		setPricePos(int pos)
		{
			if( !pos )
				clrValue(entryPricePos);
			else
				setValue(entryPricePos, pos);
		}
		int			getPriceLen() const
		{
			return getValue(entryPriceLen, 1);
		}
		void		setPriceLen(int len)
		{
			if( len==1 )
				clrValue(entryPriceLen);
			else
				setValue(entryPriceLen, len);
		}
		int			getWeightPos() const
		{
			return getValue(entryWeightPos, 0);
		}
		void		setWeightPos(int pos)
		{
			if( !pos )
				clrValue(entryWeightPos);
			else
				setValue(entryWeightPos, pos);
		}
		int			getWeightLen() const
		{
			return getValue(entryWeightLen, 1);
		}
		void		setWeightLen(int len)
		{
			if( len==1 )
				clrValue(entryWeightLen);
			else
				setValue(entryWeightLen, len);
		}
	};

	class			TEanGroups
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TEanGroups(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TEanGroups()
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
		TEanGroup*	operator [] (int index)
		{
			return (TEanGroup*) TValueList::operator [](index);
		}
		TEanGroup*	byEan(const QString& ean);
	};

	class			TEanGroupIt
	: public TValueListIt
	{
	public:
		TEanGroupIt(TEanGroups& list)
		: TValueListIt(list)
		{
		}
		TEanGroup*	operator () ()
		{
			return (TEanGroup*) TValueListIt::operator()();
		}
		TEanGroup*	toFirst()
		{
			return (TEanGroup*) TValueListIt::toFirst();
		}
		TEanGroup*	current()
		{
			return (TEanGroup*) TValueListIt::current();
		}
		TEanGroup*	operator ++ ()
		{
			return (TEanGroup*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


