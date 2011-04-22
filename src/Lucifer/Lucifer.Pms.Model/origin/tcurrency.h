#ifndef				POSLIB_TCURRENCY_H
#define				POSLIB_TCURRENCY_H
#include			"basics/tvalue.h"
#include			<math.h>
#include			<stdlib.h>

namespace PosLib
{
	inline int		iRound( double d )
	{
    	return d >= 0.0 ? int(d + 0.5) : int( d - ((int)d-1) + 0.5 ) + ((int)d-1);
	}
	inline long		lRound( double d )
	{
    	return d >= 0.0 ? long(d + 0.5) : long( d - ((long)d-1) + 0.5 ) + ((long)d-1);
	}

	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r W�hrungen.
		Alle verf�gbaren Kostenstellen-Informationen werden in einer Instanz von TCurrencyList
		zur Verf�gung gestellt.
		\brief POS-Klassen: W�hrungen.
	*/
	class			TCurrency
	: public TNSValue
	{
	public:
		static const char	entryRate[];
		static const char	entryDecPos[];
		static const char	entryDecChar[];
		static const char	entryThsChar[];
		static const char	entryReverse[];
		static const char	entryReturn[];					// R�ckgeldf�hig?
		static const char	entryIgnRounding[];				// Rundung der Aart ignorieren f�r R�ckgeld?
	public:
		/*!	Erzeuge eine leere Instanz einer W�hrung.
		*/
		TCurrency()
		: TNSValue()
		{
		}
		/*!	Hilfsfunktion f�rs Office.
			return den wirklichen Umrechnungskurs der W�hrung, unabh#�ngig ob sie umgekehrt eingegeben wurde.
			brief Eingegebenen Umrechnungskurs abfragen.
			sa setRate
		*/
		double		getRealRate() const
		{
			return getValue(entryRate, 1.0);
		}
		/*!	\return den Umrechnungskurs der W�hrung.
			\brief Umrechnungskurs abfragen.
			\sa setRate
		*/
		double		getRate() const
		{
			double ret = getRealRate();
			if( isReverse() )
			{
				if( ret==0.0 )
					return 1.0;
				return 1/ret;
			}
			return ret;
		}
		/*!	�ndert den Umrechnungskurs dieser W�hrung.
			\param rate		Der neue Umrechnungskurs
			\brief Umrechnungskurs �ndern.
			\sa getRate
		*/
		void		setRate(double rate)
		{
			setValue(entryRate, rate);
		}
		bool		isReverse() const
		{
			return getValue(entryReverse, FALSE);
		}
		void		setReverse(bool flag)
		{
			if( !flag )
				clrValue(entryReverse);
			else
				setValue(entryReverse, flag);
		}
		bool		isReturn() const
		{
			return getValue(entryReturn, FALSE);
		}
		void		setReturn(bool flag)
		{
			if( !flag )
				clrValue(entryReturn);
			else
				setValue(entryReturn, flag);
		}
		bool		isIgnRounding() const
		{
			return getValue(entryIgnRounding, FALSE);
		}
		void		setIgnRounding(bool flag)
		{
			if( !flag )
				clrValue(entryIgnRounding);
			else
				setValue(entryIgnRounding, flag);
		}
	};

	class			TCurrencies
	: public TValueList
	, public TValue
	{
		Q_OBJECT
		static const char	fileName[];
		static const char	pathName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		static const char	sectSettings[];
		static const char	entryCount[];
		static const char	entrySystemCurr[];
		static const char	sectCurrency[];
		static const char	entryNum[];
		static const char	entryName[];
		static const char	entryShortname[];
		static const char	entryRate[];
		static const char	entryDecPos[];
		static const char	entryFromEuro[];
		static const char	entrySysCurrency[];
		static const char	strSysCurr[];
	public:
		TCurrencies(bool autodel=TRUE)
		: TValueList(autodel)
		, TValue()
		{
		}
		~TCurrencies()
		{
		}
		virtual int		load(const char* path="")
		{
			return TValueList::load(path);
		}
		virtual void	save(const char* path="")
		{
			TValueList::save(path);
		}
		virtual int		load(const QString& file, const char* path);
		virtual void	save(const QString& file, const char* path);
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
		TCurrency*	operator [] (int index)
		{
			return (TCurrency*) TValueList::operator [](index);
		}
		virtual TValue*	find(int index)
		{
			return TValueList::find(index);
		}
		virtual uint	count() const
		{
			return TValueList::count();
		}
		virtual void	insert(TCurrency* item)
		{
			TValueList::insert(item);
		}
		long		convert(long amount, TCurrency* to, TCurrency* from=NULL);
		QString		formatb(long amount, int cur=0, bool showThs=FALSE)
		{
			if( cur )
				return formatb(amount, (TCurrency*)TValueList::find(cur),showThs);
			return formatb(amount, (TCurrency*)NULL, showThs);
		}
		QString		format(long amount, int cur=0)
		{
			if( cur )
				return format(amount, (TCurrency*)TValueList::find(cur));
			return format(amount, (TCurrency*)NULL);
		}
		QString		format(long amount, int len, int cur)
		{
			if( cur )
				return format(amount, len, (TCurrency*)TValueList::find(cur));
			return format(amount, len, (TCurrency*)NULL);
		}
		long		scan(const QString& str, int curr=0)
		{
			if( curr )
				return scan(str, (TCurrency*)NULL);
			return scan(str, (TCurrency*)TValueList::find(curr));
		}
		bool		isValid(const QString& str, int curr=0)
		{
			if( curr )
				return isValid(str, (TCurrency*)NULL);
			return isValid(str, (TCurrency*)TValueList::find(curr));
		}
		QString		format(long amount, TCurrency* curr);
		QString		formatb(long amount, TCurrency* curr, bool showThs);
		QString		format(long amount, int len, TCurrency* curr);
		QString		formatOrg(long amount, TCurrency* curr);
		QString		formatOrg(long amount, int len, TCurrency* curr);
		long		scanOrig(const QString& str, TCurrency* curr);
		long		scan(const QString& str, TCurrency* curr);
		bool		isValid(const QString& str, TCurrency* curr);
		int			getSysCurrency() const
		{
			return getValue(entrySysCurrency, 1);
		}
		void		setSysCurrency(int curr)
		{
			setValue(entrySysCurrency, curr);
		}
		QDate		getExpire() const
		{
			return m_Expire.date();
		}
		void		clrExpire()
		{
			m_Expire = QDateTime();
		}
		void		setExpire(const QDate& date)
		{
			m_Expire = QDateTime(date, QTime::currentTime());
		}
	protected:
		QString		doFormat(long amount, TCurrency* curr, bool conv, bool showThs);
		int			multiplier(TCurrency* curr)
		{
			int mul = 1;
			for( int i=0; i<abs(curr->getDecPos()); i++ )
				mul = mul*10;
			return mul;
		}
	protected:
		QDateTime	m_Expire;
	};


}

using namespace PosLib;

#endif


