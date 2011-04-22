#ifndef				POSLIB_TTABLECOMBI_H
#define				POSLIB_TTABLECOMBI_H

namespace PosLib
{
	/*!	Diese Klasse umfa� alle ben�igten Informationen fr die Tischaktion "Kombiartikel". Kombiartikel
		werden im Gegensatz zu den brigen Tischaktionen unterhalb der zugeh�igen Order einsortiert, so
		da�eine Art von Baumstruktur entsteht.
		\brief POS-Klassen: Tischaktionen, Kombiartikel.
	*/
	class			TTableCombi
	: public TTableOrder
	{
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::CombiArt.
			\brief ctor
		*/
		TTableCombi()
		: TTableOrder(CombiArt)
		{
		}
		TTableCombi& operator = (const TTableCombi& mod);
	};
}

#endif
