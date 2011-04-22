#ifndef				POSLIB_TTABLECASHING_H
#define				POSLIB_TTABLECASHING_H
#include			"poslib/ttablepay.h"

namespace PosLib
{
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r die Tischaktion "Einzahlung" bzw
		"Auszahlung". Diese Aktionen werden nur auf bestimmten Archiven durchgef�hrt.
		\brief POS-Klassen: Tischaktionen, Ein- und Auszahlung.
	*/
	class			TTableCashing
	: public TTableAction
	{
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Cashing.
			\brief ctor
		*/
		TTableCashing()
		: TTableAction(Cashing)
		{
		}
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Cashing
			und dem Betrag amount.
			\param amount	Ein- bzw Auszahlungsbetrag.
			\brief ctor
		*/
		TTableCashing(long amount)
		: TTableAction(Cashing)
		{
			setAmount(amount);
		}
		/*!	\return den Betrag der Aus- bzw Einzahlung. Auszahlungen sind negativ.
			\brief Wert der Transaktion ermitteln.
		*/
		long		getAmount() const
		{
			return getValue(TTablePay::entryAmount);
		}
		/*!	�ndert den Betrag der Aus- bzw Einzahlung. Auszahlungen sind negativ.
			\brief Wert der Transaktion �ndern.
		*/
		void		setAmount(long amount)
		{
			setValue(TTablePay::entryAmount, amount);
		}
		QString		getReason() const
		{
			return getString(TTableVoid::entryReason);
		}
		void		setReason(const QString& reason)
		{
			setValue(TTableVoid::entryReason, reason);
		}
		long		getRetAmount() const
		{
			return getValue(TTablePay::entryRetAmount, 0L);
		}
		int			getRetCurrency() const
		{
			return getValue(TTablePay::entryRetCurrency, 0);
		}
		void		setRetAmount(long ret)
		{
			setValue(TTablePay::entryRetAmount, ret);
		}
		bool		getReturnCurrency(TCurrency& curr)
		{
			int id = getValue(TTablePay::entryRetCurrency);
			if( !id )
				return FALSE;
			curr.setID(id);
			curr.setName(getString(TTablePay::entryRetCurrName));
			curr.setShortname(getString(TTablePay::entryRetCurrShort));
			curr.setRate(getValue(TTablePay::entryRetCurrRate, 0.0));
			return TRUE;
		}
		void		setReturnCurrency(TCurrency* curr)
		{
			if( !curr ) {
				clrValue(TTablePay::entryRetCurrency);
				clrValue(TTablePay::entryRetCurrName);
				clrValue(TTablePay::entryRetCurrShort);
				clrValue(TTablePay::entryRetCurrRate);
			} else {
				setValue(TTablePay::entryRetCurrency, curr->getID());
				setValue(TTablePay::entryRetCurrName, curr->getName());
				setValue(TTablePay::entryRetCurrShort, curr->getShortname());
				setValue(TTablePay::entryRetCurrRate, curr->getRate());
			}
		}
	};

}
#endif
