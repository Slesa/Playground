#ifndef				POSLIB_TTABLEBALANCE_H
#define				POSLIB_TTABLEBALANCE_H

namespace PosLib
{
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r die Tischaktion "Kellnerselbstabrechnung"
		Diese Aktionen werden nur auf bestimmten Tischen durchgef�hrt.
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
