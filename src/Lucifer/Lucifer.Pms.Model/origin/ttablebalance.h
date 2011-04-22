#ifndef				POSLIB_TTABLEBALANCE_H
#define				POSLIB_TTABLEBALANCE_H

namespace PosLib
{
	/*!	Diese Klasse umfaßt alle benötigten Informationen für die Tischaktion "Kellnerselbstabrechnung"
		Diese Aktionen werden nur auf bestimmten Tischen durchgeführt.
		\brief POS-Klassen: Tischaktionen, Kellnerselbstabrechnung.
	*/
	class			TTableBalance
	: public TTableAction
	{
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Balance.
			\brief ctor
		*/
		TTableBalance()
		: TTableAction(Balance)
		{
		}
	};

}
#endif
