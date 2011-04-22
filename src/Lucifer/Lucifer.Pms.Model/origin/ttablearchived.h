#ifndef				POSLIB_TTABLEARCHIVED_H
#define				POSLIB_TTABLEARCHIVED_H

namespace PosLib
{
	/*!	Diese Klasse umfaßt alle benötigten Informationen für die Tischaktion "Tisch archiviert".
		Diese Tischaktion kommt nur einmal pro Tisch vor, es sei denn, der Tisch wurde gerettet. In diesem
		Fall wird die ursprüngliche Aktion als storniert gekennzeichnet und eine neue Archivierungsaktion
		eingetragen.
		\brief POS-Klassen: Tischaktionen, Tisch archiviert.
	*/
	class			TTableArchived
	: public TTableAction
	{
		static const char	entryVoided[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Archived.
			\brief ctor
		*/
		TTableArchived()
		: TTableAction(Archived)
		{
		}
		/*!	\return Liefert die Zahlart, mit der dieser Tisch archiviert, d.h. komplett bezahlt wurde. Im
			Normalfall entspricht dieser Wert dem Zahlvorgang des Tisches. Bei Teilzahlungen entspricht
			dieser Wert dem letzten Zahlvorgang.
			\brief Archivierungszahlart ermitteln.
		*/
		int			getPayform() const
		{
			return getValue(TTablePay::entryPayform, 0);
		}
		/*!	Ändert die Zahlart, mit der dieser Tisch archiviert, d.h. komplett bezahlt wurde.
			\param form		Neue Archivierungszahlart
			\nrief Archivierungszahlart ändern.
		*/
		void		setPayform(int form)
		{
			setValue(TTablePay::entryPayform, form);
		}
		/*!	\return Liefert TRUE, wenn dieser Archivierungsvorgang storniert wurde, d.h. der Tisch wurde
			gerettet und die Archivierung ist ungültig.
			\brief Flag Archivierung storniert ermitteln.
		*/
		bool		wasVoided() const
		{
			return getValue(entryVoided, FALSE);
		}
		/*!	Ändert das Flag, ob die Archivierung widerrufen, d.h. der Tisch gerettet wurde.
			\param flag		TRUE, wenn diese Archivierung ungültig ist.
			\brief Flag Archivierung storniert ändern.
		*/
		void		setVoided(bool flag)
		{
			setValue(entryVoided, flag);
		}
	};

}

#endif
 
