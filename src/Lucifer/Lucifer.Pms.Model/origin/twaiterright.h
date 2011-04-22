#ifndef				POSLIB_TWAITERRIGHT_H
#define				POSLIB_TWAITERRIGHT_H
#include			"poslib/twaiter.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\brief POS-Klassen: Kellner-Rechte.
	*/
	class			TWaiterRight
	: public TNValue
	{
	public:
		/*!	Erzeuge eine leere Instanz einer Kellnerrechte-Gruppe.
		*/
		TWaiterRight()
		: TNValue()
		{
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe das Bestellen erlaubt ist.
			\brief Bestellen erlaubt?
		*/
		bool		canOrders() const
		{
			return getValue(TWaiter::entryOrders, TRUE);
		}
		void		setOrders(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryOrders);
			else
				setValue(TWaiter::entryOrders, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe das Stornieren erlaubt ist.
			\brief Stornieren erlaubt?
		*/
		bool		canVoids() const
		{
			return getValue(TWaiter::entryVoids, TRUE);
		}
		void		setVoids(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryVoids);
			else
				setValue(TWaiter::entryVoids, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe das Bezahlen erlaubt ist.
			\brief Bezahlen erlaubt?
		*/
		bool		canPays() const
		{
			return getValue(TWaiter::entryPays, TRUE);
		}
		void		setPays(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryPays);
			else
				setValue(TWaiter::entryPays, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe das Splitten erlaubt ist.
			\brief Splitten erlaubt?
		*/
		bool		canSplits() const
		{
			return getValue(TWaiter::entrySplits, TRUE);
		}
		void		setSplits(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entrySplits);
			else
				setValue(TWaiter::entrySplits, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe Tische neu anlegen werden drfen.
			Der Default ist TRUE.
			\brief Tische neu anlegen?.
		*/
		bool		canCreateTable() const
		{
			return getValue(TWaiter::entryCreateTable, TRUE);
		}
		/*!	ndert das Flag, ob in dieser Rechtegruppe Tische neu angelegt werden drfen.
			\param flag		TRUE, wenn Tisch angelegt werden drfen.
			\brief Tische anlegen ndern.
		*/
		void		setCreateTable(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryCreateTable);
			else
				setValue(TWaiter::entryCreateTable, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe der Zugriff auf alle Tische besteht.
			\brief Zugriff auf alle Tische?
		*/
		bool		canAllTables() const
		{
			return getValue(TWaiter::entryAllTables, FALSE);
		}
		void		setAllTables(bool flag)
		{
			if( !flag )
				clrValue(TWaiter::entryAllTables);
			else
				setValue(TWaiter::entryAllTables, flag);
		}
		/*!	\return
			* 0, wenn keine
			* 1, wenn immer
			* 2, wenn nur ohne Offenstnde
			* 3, wenn nur ohne eigenen Offenstnde
			Reporte in dieser Rechtegruppe gefahren werden knnen.
			\brief Reports erlaubt?
		*/
		int			canReports() const
		{
			int ret = getValue(TWaiter::entryReports, 1);
			if( !ret )
			{
				bool yes = getValue(TWaiter::entryReports, FALSE);
				if( yes )
					ret = 1;
			}
			return ret;
		}
		void		setReports(int flag)
		{
			setValue(TWaiter::entryReports, flag);
		}
		bool		canSelRepWaiter() const
		{
			return getValue(TWaiter::entrySelRepWaiter, FALSE);
		}
		void		setSelRepWaiter(bool flag)
		{
			if( !flag )
				clrValue(TWaiter::entrySelRepWaiter);
			else
				setValue(TWaiter::entrySelRepWaiter, flag);
		}
		/*!	\return
			* 0, wenn nie
			* 1, wenn immer
			* 2, wenn nur ohne Offenstnde
			* 3, wenn nur ohne eigenen Offenstnde
			der Tagesabschlu in dieser Rechtegruppe  durchgefhrt werden kann.
			\brief Tagesbertrag durchfhrbar?
		*/
		int			canTurnOver() const
		{
			int ret = getValue(TWaiter::entryTurnOver, 1);
			if( !ret )
			{
				bool yes = getValue(TWaiter::entryTurnOver, FALSE);
				if( yes )
					ret = 1;
			}
			return ret;
		}
		void		setTurnOver(int flag)
		{
			setValue(TWaiter::entryTurnOver, flag);
		}
		/*!	\return TRUE, wenn in Mitglieder dieser Rechtegruppe Chefkellner sind.
			\note Chefkellner beinhalten das Recht, die Kasse zu beenden. Weiterhin drfen sie
			im Office (bei Login) Kellner ndern.
			\brief Chef-Kellner?
		*/
		bool		isMaster() const
		{
			return getValue(TWaiter::entryMaster, FALSE);
		}
		void		setMaster(bool flag)
		{
			if( !flag )
				clrValue(TWaiter::entryMaster);
			else
				setValue(TWaiter::entryMaster, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe Preise gendert werden knnen.
			\brief Preisnderungen erlaubt?
		*/
		bool		canPrices() const
		{
			return getValue(TWaiter::entryPrices, TRUE);
		}
		void		setPrices(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryPrices);
			else
				setValue(TWaiter::entryPrices, flag);
		}
		bool		canConnects() const
		{
			return getValue(TWaiter::entryConnects, TRUE);
		}
		void		setConnects(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryConnects);
			else
				setValue(TWaiter::entryConnects, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe Kellner sich als jemand anders ausgeben knnen.
			\brief Fremde Kellner vorgeben?
		*/
		bool		canCloak() const
		{
			return getValue(TWaiter::entryCloak, TRUE);
		}
		void		setCloak(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryCloak);
			else
				setValue(TWaiter::entryCloak, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe der Tischeigentmer gendert werden kann.
			\brief Tischeigentmer wechseln erlaubt?
		*/
		bool		canChangeOwner() const
		{
			return getValue(TWaiter::entryChOwner, FALSE);
		}
		void		setChangeOwner(bool flag)
		{
			if( !flag )
				clrValue(TWaiter::entryChOwner);
			else
				setValue(TWaiter::entryChOwner, flag);
		}
		/*!	\return Liefert TRUE, wenn bei einem Tischwechsel dieses Kellners
			dessen Auslagen weitergegeben werden an die nchste Schicht.
			\brief Auslagen verrerben?
		*/
		bool		doChangeCredits() const
		{
			return getValue(TWaiter::entryChCredits, FALSE);
		}
		/*!	ndert das Flag, ob bei einem Tischwechsel dieses Kellners
			dessen Auslagen weitergegeben werden an die nchste Schicht.
			\param flag		Wenn TRUE, werden die Auslagen weitergegeben.
			\brief Flag Auslagen verrerben ndern
		*/
		void		setChangeCredits(bool flag)
		{
			if( !flag )
				clrValue(TWaiter::entryChCredits);
			else
				setValue(TWaiter::entryChCredits, flag);
		}
		/*!	\return TRUE, wenn in dieser Rechtegruppe Rabatte gegeben werden knnen.
			\brief Rabattierung erlaubt?
		*/
		bool		canDiscounts() const
		{
			return getValue(TWaiter::entryDiscounts, TRUE);
		}
		void		setDiscounts(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryDiscounts);
			else
				setValue(TWaiter::entryDiscounts, flag);
		}
		bool		canRestores() const
		{
			return getValue(TWaiter::entryRestores, TRUE);
		}
		void		setRestores(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryRestores);
			else
				setValue(TWaiter::entryRestores, flag);
		}
		bool		canRescues() const
		{
			return getValue(TWaiter::entryRescues, TRUE);
		}
		void		setRescues(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryRescues);
			else
				setValue(TWaiter::entryRescues, flag);
		}
		bool		canChangePayform() const
		{
			return getValue(TWaiter::entryChPayform, TRUE);
		}
		void		setChangePayform(bool flag)
		{
			if( flag )
				clrValue(TWaiter::entryChPayform);
			else
				setValue(TWaiter::entryChPayform, flag);
		}
		bool		canNegative() const	
		{
			return getValue(TWaiter::entryNegative, TRUE);
		}
		void		setNegative(bool flag)	
		{
			if( flag )	
				clrValue(TWaiter::entryNegative);
			else
				setValue(TWaiter::entryNegative, flag);
		}
		bool		canDivide() const	
		{
			return getValue(TWaiter::entryDivide, TRUE);
		}
		void		setDivide(bool flag)	
		{
			if( flag )	
				clrValue(TWaiter::entryDivide);
			else
				setValue(TWaiter::entryDivide, flag);
		}
	};

	class			TWaiterRights
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TWaiterRights(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TWaiterRights()
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
		TWaiterRight*	operator [] (int index)
		{
			return (TWaiterRight*) TValueList::operator [](index);
		}
	};

	class			TWaiterRightIt
	: public TValueListIt
	{
	public:
		TWaiterRightIt(TWaiterRights& list)
		: TValueListIt(list)
		{
		}
		TWaiterRight*	operator () ()
		{
			return (TWaiterRight*) TValueListIt::operator()();
		}
		TWaiterRight*	toFirst()
		{
			return (TWaiterRight*) TValueListIt::toFirst();
		}
		TWaiterRight*	current()
		{
			return (TWaiterRight*) TValueListIt::current();
		}
		TWaiterRight*	operator ++ ()
		{
			return (TWaiterRight*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


