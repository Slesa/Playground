#ifndef				POSLIB_TLAYCONFIG_H
#define				POSLIB_TLAYCONFIG_H
#include			"basics/tvalue.h"

namespace PosLib
{
	class			TLayConfig
	: public TNValue
	{
		static const char	entryType[];		//!< Typ des Layouts nach Types
		static const char	entryDir[];			//!< Flag ob vorw�ts oder rckw�ts
		static const char	entryCounts[];		//!< Anzahl drucken oder iterieren?
		static const char	entrySingle[];		//!< Flag ob Einzelbon
		static const char	entryVatFirst[];	//!< Flag ob Mwst vor Endsumme
		static const char	entryHeader[];		//!< Bei allen Ausdrucken: der Header
		static const char	entryBody[];		//!< Body fr Liste der Eintr�e
		static const char	entryModifier[];	//!< Body fr Modifiers
		static const char	entryCombi[];		//!< Body fr Kombiartikel
		static const char	entryHint[];		//!< Body fr Hinweise
		static const char	entrySubtotal[];	//!< Zwischensumme nach den Orders
		static const char	entryBillBody[];	//!< Rechnung: Liste fr Zahlarten
		static const char	entryBillSum[];		//!< Rechnung: Aktuelle Summe
		static const char	entryBillVat[];		//!< Rechnung: Mehrwertsteuerliste
		static const char	entryCheckVoid[];	//!< Guestcheck: Liste der Storno-Buchungen
		static const char	entryCheckSplit[];	//!< Guestcheck: Liste der Split-Buchungen
		static const char	entryFooter[];		//!< Bei allen Ausdrucken: der Footer
		static const char	entryControls[];	//!< Bei Bons: Kontrollartikel trennen?
		static const char	entryTogether[];	//!< Bei Bons: Artikel zusammenfassen?
	public:
		enum		Types
		{
			Order		= 0
		,	Void		= 1
		,	Bill		= 2
		,	PartBill	= 3
		,	Split		= 4
		,	Guestcheck	= 10
		};
	public:
		TLayConfig()
		: TNValue()
		{
		}
		~TLayConfig()
		{
		}
		int			getType() const
		{
			return getValue(entryType, Order);
		}
		void		setType(int type)
		{
			if( type==Order )
				clrValue(entryType);
			else
				setValue(entryType, type);
		}
		int			getDirection() const
		{
			return getValue(entryDir, 0);
		}
		void		setDirection(int dir)
		{
			if( !dir )
				clrValue(entryDir);
			else
				setValue(entryDir, dir);
		}
		int			getCounts() const
		{
			return getValue(entryCounts, 0);
		}
		void		setCounts(int counts)
		{
			if( !counts )
				clrValue(entryCounts);
			else
				setValue(entryCounts, counts);
		}
		QString		getHeader() const
		{
			return getString(entryHeader);
		}
		void		setHeader(const QString& hdr)
		{
			if( hdr.isEmpty() )
				clrValue(entryHeader);
			else
				setValue(entryHeader, hdr);
		}
		QString		getBody() const
		{
			return getString(entryBody);
		}
		void		setBody(const QString& body)
		{
			if( body.isEmpty() )
				clrValue(entryBody);
			else
				setValue(entryBody, body);
		}
		QString		getModifier() const
		{
			return getString(entryModifier);
		}
		void		setModifier(const QString& mod)
		{
			if( mod.isEmpty() )
				clrValue(entryModifier);
			else
				setValue(entryModifier, mod);
		}
		QString		getCombi() const
		{
			return getString(entryCombi);
		}
		void		setCombi(const QString& combi)
		{
			if( combi.isEmpty() )
				clrValue(entryCombi);
			else
				setValue(entryCombi, combi);
		}
		QString		getHint() const
		{
			return getString(entryHint);
		}
		void		setHint(const QString& hint)
		{
			if( hint.isEmpty() )
				clrValue(entryHint);
			else
				setValue(entryHint, hint);
		}
		QString		getFooter() const
		{
			return getString(entryFooter);
		}
		void		setFooter(const QString& ftr)
		{
			if( ftr.isEmpty() )
				clrValue(entryFooter);
			else
				setValue(entryFooter, ftr);
		}
		QString		getSubtotal() const
		{
			return getString(entrySubtotal);
		}
		void		setSubtotal(const QString& sub)
		{
			if( sub.isEmpty() )
				clrValue(entrySubtotal);
			else
				setValue(entrySubtotal, sub);
		}
		QString		getBillOrder() const
		{
			return getString(entryBillBody);
		}
		void		setBillOrder(const QString& body)
		{
			if( body.isEmpty() )
				clrValue(entryBillBody);
			else
				setValue(entryBillBody, body);
		}
		QString		getBillBody() const
		{
			return getString(entryBillBody);
		}
		void		setBillBody(const QString& body)
		{
			if( body.isEmpty() )
				clrValue(entryBillBody);
			else
				setValue(entryBillBody, body);
		}
		QString		getBillSum() const
		{
			return getString(entryBillSum);
		}
		void		setBillSum(const QString& sum)
		{
			if( sum.isEmpty() )
				clrValue(entryBillSum);
			else
				setValue(entryBillSum, sum);
		}
		QString		getBillVat() const
		{
			return getString(entryBillVat);
		}
		void		setBillVat(const QString& txt)
		{
			if( txt.isEmpty() )
				clrValue(entryBillVat);
			else
				setValue(entryBillVat, txt);
		}
		QString		getCheckVoid() const
		{
			return getString(entryCheckVoid);
		}
		void		setCheckVoid(const QString& txt)
		{
			if( txt.isEmpty() )
				clrValue(entryCheckVoid);
			else
				setValue(entryCheckVoid, txt);
		}
		QString		getCheckSplit() const
		{
			return getString(entryCheckSplit);
		}
		void		setCheckSplit(const QString& txt)
		{
			if( txt.isEmpty() )
				clrValue(entryCheckSplit);
			else
				setValue(entryCheckSplit, txt);
		}
		bool		useControls() const
		{
			return getValue(entryControls, TRUE);
		}
		void		setUseControls(bool flag)
		{
			if( flag )
				clrValue(entryControls);
			else
				setValue(entryControls, flag);
		}
		bool		putTogether() const
		{
			return getValue(entryTogether, FALSE);
		}
		void		setTogether(bool flag)
		{
			if( !flag )
				clrValue(entryTogether);
			else
				setValue(entryTogether, flag);
		}
		bool		isSingle() const
		{
			return getValue(entrySingle, FALSE);
		}
		void		setSingle(bool flag)
		{
			if( !flag )
				clrValue(entrySingle);
			else
				setValue(entrySingle, flag);
		}
		bool		isVatFirst() const
		{
			return getValue(entryVatFirst, FALSE);
		}
		void		setVatFirst(bool flag)
		{
			if( !flag )
				clrValue(entryVatFirst);
			else
				setValue(entryVatFirst, flag);
		}
	};

	class			TLayConfigs
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TLayConfigs(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TLayConfigs()
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
		TLayConfig*	operator [] (int index)
		{
			return (TLayConfig*) TValueList::operator [](index);
		}
	};

	class			TLayConfigIt
	: public TValueListIt
	{
	public:
		TLayConfigIt(TLayConfigs& list)
		: TValueListIt(list)
		{
		}
		TLayConfig*	operator () ()
		{
			return (TLayConfig*) TValueListIt::operator()();
		}
		TLayConfig*	toFirst()
		{
			return (TLayConfig*) TValueListIt::toFirst();
		}
		TLayConfig*	current()
		{
			return (TLayConfig*) TValueListIt::current();
		}
		TLayConfig*	operator ++ ()
		{
			return (TLayConfig*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif
