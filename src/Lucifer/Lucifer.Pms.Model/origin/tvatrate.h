#ifndef				POSLIB_TVATRATE_H
#define				POSLIB_TVATRATE_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa? alle bentigten Informationen fr Mehrwertsteuers?e.
		Alle verfgbaren Steuers?e werden in einer Instanz von TVatRateList
		zur Verfgung gestellt.
		\brief POS-Klassen: Mehrwertsteuersatz.
	*/
	class			TVatRate
	: public TNValue
	{
	public:
		enum		Types
		{
			typeBrutto		= 0							// Default
		,	typeNetto		= 1							// Steuer auf Netto-Betrag
		,	typeAmount		= 2							// Fixbetrag
		};
	public:
		static const char	entryRate[];
		static const char	entryGroup[];
		static const char	entryType[];
		static const char	entryGuestIH[];
		static const char	entryGuestTA[];
		static const char	entryDiscount[];
		static const char	entryComposite[];
	public:
		/*!	Erzeuge eine leere Instanz einer Mehrwertsteuer.
		*/
		TVatRate()
		: TNValue()
		{
		}
		/*!	\return Steuergruppe des Steuersatzes 
			\brief Wert des Steuersatzes abfragen.
			\sa setRate
		*/
		int			getGroup() const
		{
			return getValue(entryGroup, 0);
		}
		/*!	Idert die Steuergruppe des Steuersatz auf grp.
			\param grp		die neue Gruppe fr den Steuersatz
			\brief Steuergruppe des Steuersatzes ?ern.
			\sa getGroup
		*/
		void		setGroup(int grp)
		{
			if( !grp )
				clrValue(entryGroup);
			else
				setValue(entryGroup, grp);
		}
		int			getType() const
		{
			return getValue(entryType, typeBrutto);
		}
		void		setType(int type)
		{
			if( type==typeBrutto )
				clrValue(entryType);
			else
				setValue(entryType, type);
		}
		bool		isNetto() const
		{
			return getType()==typeNetto;
		}
		bool		isAmount() const
		{
			return getType()==typeAmount;
		}
		bool		isGuestIH() const
		{
			return getValue(entryGuestIH, FALSE);
		}
		void		setGuestIH(bool on)
		{
			if( !on )
				clrValue(entryGuestIH);
			else
				setValue(entryGuestIH, TRUE);
		}
		bool		isGuestTA() const
		{
			return getValue(entryGuestTA, FALSE);
		}
		void		setGuestTA(bool on)
		{
			if( !on )
				clrValue(entryGuestTA);
			else
				setValue(entryGuestTA, TRUE);
		}
		bool		doDiscount() const
		{
			return getValue(entryDiscount, FALSE);
		}
		void		setDiscount(bool on)
		{
			if( !on )
				clrValue(entryDiscount);
			else
				setValue(entryDiscount, TRUE);
		}
		/*!	\return Den Wert des Steuersatzes im Format
					[Vorkomma]*100 + [Nachkomma]
			\brief Wert des Steuersatzes abfragen.
			\sa setRate
		*/
		int			getRate() const
		{
			return getValue(entryRate, 0);
		}
		/*!	Idert den Steuersatz auf rate.
			\param rate		der neue Wert fr den Steuersatz im Format
							[Vorkomma]*100 + [Nachkomma]
			\brief Wert des Steuersatzes ?ern.
			\sa getRate
		*/
		void		setRate(int rate)
		{
			if( !rate )
				clrValue(entryRate);
			else
				setValue(entryRate, rate);
		}
		QStringList	getComposites();
		void		setComposites(const QStringList& list);
		QString		asString() const
		{
			QString tmp;
			tmp.sprintf("%3d.%02d", getRate()/100, getRate()%100);
			return tmp;
		}
	};

	class			TVatRates
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TVatRates(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TVatRates()
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
		TVatRate*	operator [] (int index)
		{
			return (TVatRate*) TValueList::operator [](index);
		}
	};

	class			TVatRateIt
	: public TValueListIt
	{
	public:
		TVatRateIt(TVatRates& list)
		: TValueListIt(list)
		{
		}
		TVatRate*	operator () ()
		{
			return (TVatRate*) TValueListIt::operator()();
		}
		TVatRate*	toFirst()
		{
			return (TVatRate*) TValueListIt::toFirst();
		}
		TVatRate*	current()
		{
			return (TVatRate*) TValueListIt::current();
		}
		TVatRate*	operator ++ ()
		{
			return (TVatRate*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


