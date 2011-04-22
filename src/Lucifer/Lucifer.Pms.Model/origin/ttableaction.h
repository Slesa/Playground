#ifndef				POSLIB_TTABLEACTION_H
#define				POSLIB_TTABLEACTION_H
#include			"poslib/ttableentry.h"
#include			"poslib/tprncontrol.h"
#include			"poslib/ttablecfg.h"
#include			"poslib/tterminal.h"
#include			"poslib/tcurrency.h"
#include			"poslib/tarticle.h"
#include			"poslib/twaiter.h"
#include			"poslib/tfamily.h"
#include			"poslib/tmodifier.h"
#include			"poslib/tfamgroup.h"
#include			"poslib/tpayform.h"
#include			"poslib/tsubvention.h"
#include			"poslib/tdiscount.h"
#include			"poslib/tvatrate.h"
#include			"poslib/tcostcenter.h"

namespace PosLib
{
	class TTable;

	/*!	Diese Klasse umfaßt alle benötigten Informationen für Tischaktionen. Zu
		jeder Tischaktion muß im Unterschied zu einem Tischeintrag ein Kellner
		angegeben werden.
		\brief POS-Klassen: Tischaktionen.
	*/
	class			TTableAction
	: public TTableEntry
	{
	public:
		static const char	entryTerminal[];
		static const char	entryCenter[];
		static const char	entryCenterName[];
		static const char	entryCenterAcc[];
		static const char	entryTermName[];
		static const char	entryWaiter[];
		static const char	entryWaiterName[];
		static const char	entryWaiterAcc[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ type.
			\param type		Der Typ der Aktion laut TTableEntry::Types
			\brief ctor
		*/
		TTableAction(int type)
		: TTableEntry(type)
		{
		}
		/*!	Extrahiert aus term die für die Tischaktion benötigten Terminal-Werte und trägt sie in die Aktion
			ein.
			\param term		Terminal das die Tischaktion einträgt
			\brief Benötigte Terminaldaten in die Tischaktion übernehmen.
		*/
		void		setTerminal(TTerminal* term)
		{
			if( !term )
				return;
			setTerminal(term->getID());
			setTermName(term->getName());
		}
		/*!	\return das Terminal, der diese Tischaktion in den Tisch eingetragen hat.
			\brief Terminal der Tischaktion ermitteln.
		*/
		int			getTerminal() const
		{
			return getValue(entryTerminal, 0);
		}
		/*!	Ändert das Terminal, das diese Tischaktion in den Tisch eingetragen hat.
			\param term		Neuer Terminalnummer der Tischaktion
			\brief Terminal der Tischaktion ändern.
		*/
		void		setTerminal(int term)
		{
			if( !term )
				clrValue(entryTerminal);
			else
				setValue(entryTerminal, term);
		}
		/*!	\return den Namen des Terminals, der diese Tischaktion in den Tisch eingetragen hat.
			\brief Terminalnamen der Tischaktion ermitteln.
		*/
		QString		getTermName() const
		{
			return getString(entryTermName);
		}
		/*!	Ändert den Namen des Terminals, der diese Tischaktion in den Tisch eingetragen hat.
			\param name		Neuer Name des Terminals
			\brief Terminalnamen der Tischaktion ändern.
		*/
		void		setTermName(const QString& name)
		{
			if( name.isEmpty() )
				clrValue(entryTermName);
			else
				setValue(entryTermName, name);
		}
		/*!	Extrahiert aus center die für die Tischaktion benötigten Kostenstellen-Werte und trägt sie in
			die Aktion ein.
			\param center	Kostenstelle, an der die Tischaktion eintragen wird
			\brief Benötigte Kostenstellendaten in die Tischaktion übernehmen.
		*/
		void		setCostCenter(TCostCenter* center)
		{
			if( !center )
				return;
			setCenter(center->getID());
			setCenterName(center->getName());
			setCenterAccount(center->getAccount());
		}
		/*!	\return die Kostenstelle, die diese Tischaktion in den Tisch eingetragen hat.
			\brief Kostenstelle der Tischaktion ermitteln.
		*/
		int			getCostCenter() const
		{
			return getValue(entryCenter, 0);
		}
		/*!	Ändert die Kostenstelle, an der diese Tischaktion in den Tisch eingetragen wurde.
			\param center	Neuer Kostenstelle der Tischaktion
			\brief Kostenstelle der Tischaktion ändern.
		*/
		void		setCenter(int center)
		{
			if( !center )
				clrValue(entryCenter);
			else
				setValue(entryCenter, center);
		}
		/*!	\return den Namen der Kostenstelle, die diese Tischaktion in den Tisch eingetragen hat.
			\brief Kostenstellenname der Tischaktion ermitteln.
		*/
		QString		getCenterName() const
		{
			return getString(entryCenterName);
		}
		/*!	Ändert den Namen der Kostenstelle, an der diese Tischaktion in den Tisch eingetragen wurde.
			\param name		Neuer Name der Kostenstelle
			\brief Kostenstellennamen der Tischaktion ändern.
		*/
		void		setCenterName(const QString& name)
		{
			if( name.isEmpty() )
				clrValue(entryCenterName);
			else
				setValue(entryCenterName, name);
		}
		QString		getCenterAccount() const
		{
			return getString(entryCenterAcc);
		}
		void		setCenterAccount(const QString& acc)
		{
			if( acc.isEmpty() )
				clrValue(entryCenterAcc);
			else
				setValue(entryCenterAcc, acc);
		}
		/*!	Extrahiert aus waiter die für die Tischaktion benötigten Kellner-Werte und trägt sie in die Aktion
			ein.
			\param waiter	Kellner der die Tischaktion einträgt
			\brief Benötigte Kellnerdaten in die Tischaktion übernehmen.
		*/
		void		setWaiter(TWaiter* waiter)
		{
			if( !waiter )
				return;
			setWaiter(waiter->getID());
			setWaiterName(waiter->getName());
			setWaiterAccount(waiter->getAccount());
			setWaiterTeams(waiter->getTeams());
		}
		/*!	\return den Kellner, der diese Tischaktion in den Tisch eingetragen hat.
			\brief Kellner der Tischaktion ermitteln.
		*/
		int			getWaiter() const
		{
			return getValue(entryWaiter, 0);
		}
		/*!	Ändert den Kellner, der diese Tischaktion in den Tisch eingetragen hat.
			\param waiter	Neuer Kellner der Tischaktion
			\brief Kellner der Tischaktion ändern.
		*/
		void		setWaiter(int waiter)
		{
			if( !waiter )
				clrValue(entryWaiter);
			else
				setValue(entryWaiter, waiter);
		}
		/*!	\return den Namen des Kellners, der diese Tischaktion in den Tisch eingetragen hat.
			\brief Kellnernamen der Tischaktion ermitteln.
		*/
		QString		getWaiterName() const
		{
			return getString(entryWaiterName);
		}
		/*!	Ändert den Namen des Kellners, der diese Tischaktion in den Tisch eingetragen hat.
			\param name		Neuer Name des Kellners
			\brief Kellnernamen der Tischaktion ändern.
		*/
		void		setWaiterName(const QString& name)
		{
			if( name.isEmpty() )
				clrValue(entryWaiterName);
			else
				setValue(entryWaiterName, name);
		}
		QString		getWaiterAccount() const
		{
			return getString(entryWaiterAcc);
		}
		void		setWaiterAccount(const QString& acc)
		{
			if( acc.isEmpty() )
				clrValue(entryWaiterAcc);
			else
				setValue(entryWaiterAcc, acc);
		}
		/*!	\return die Kellnerteams des Kellners, der diese Tischaktion in den Tisch eingetragen hat.
			\brief Kellnerteams der Tischaktion ermitteln.
		*/
		QString		getWaiterTeams() const
		{
			return getString(TWaiter::entryTeams);
		}
		/*!	Ändert die Kellnerteams des Kellners, der diese Tischaktion in den Tisch eingetragen hat.
			\param teams		die Teams des Kellners
			\brief Kellnerteams der Tischaktion ändern.
		*/
		void		setWaiterTeams(const QString& teams)
		{
			if( teams.isEmpty() )
				clrValue(TWaiter::entryTeams);
			else
				setValue(TWaiter::entryTeams, teams);
		}
	};

	class			TTableRestore
	: public TTableAction
	{
	public:
		TTableRestore()
		: TTableAction(Restore)
		{
		}
	};

}

#include			"poslib/ttablechange.h"
#include			"poslib/ttablecreate.h"
#include			"poslib/ttablecontrol.h"
#include			"poslib/ttablepay.h"
#include			"poslib/ttablearchived.h"
#include			"poslib/ttableartaction.h"
#include			"poslib/ttableorder.h"
#include			"poslib/ttablevoid.h"
#include			"poslib/ttablecashing.h"
#include			"poslib/ttablesplit.h"
#include			"poslib/ttablecombi.h"
#include			"poslib/ttablemodifier.h"

using namespace PosLib;

#endif
