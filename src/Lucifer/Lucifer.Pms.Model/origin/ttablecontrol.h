#ifndef				POSLIB_TTABLECONTROL_H
#define				POSLIB_TTABLECONTROL_H

namespace PosLib
{
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r die Tischaktion "Drucker-Kontrollartikel".
		Tischeintr�ge dieser Art beeinflussen das Erzeugen und Verarbeiten von Druckjobs, da normalerweise
		nachfolgende Bestellungen diesen Kontrolleintr�gen untergeordnet werden.
		\brief POS-Klassen: Tischaktionen, Druckerkontrollet.
	*/
	class			TTableControl
	: public TTableAction
	{
		static const char	entryControl[];
		static const char	entryName[];
		static const char	entryPrint[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Control.
			\brief ctor
		*/
		TTableControl()
		: TTableAction(Control)
		{
		}
		/*!	\return Liefert den Index des bestellten Drucker-Kontrollartikels.
			\brief Kontrollartikel ermitteln,
		*/
		int			getControl()
		{
			return getValue(entryControl, 0);
		}
		/*!	�ndert den Index des bestellten Drucker-Kontrollartikels.
			\param contr	Die Nummer des Kontrollartikels
			\brief Kontrollartikel �ndern,
		*/
		void		setControl(int contr)
		{
			setValue(entryControl, contr);
		}
		/*!	\return Liefert den Namen des bestellten Drucker-Kontrollartikels.
			\brief Namen des Kontrollartikels ermitteln,
		*/
		QString		getName() const
		{
			return getString(entryName);
		}
		/*!	�ndert den Namen des bestellten Drucker-Kontrollartikels.
			\param name		Der Name des Kontrollartikels
			\brief Kontrollartikel-Text �ndern,
		*/
		void		setName(const QString& name)
		{
			setValue(entryName, name);
		}
		/*!	\return Liefert den Druckertext des bestellten Drucker-Kontrollartikels.
			\brief Druckertext des Kontrollartikels ermitteln,
		*/
		QString		getPrint() const
		{
			return getString(entryPrint);
		}
		/*!	�ndert den Druckertext des bestellten Drucker-Kontrollartikels.
			\param name		Der Text des Kontrollartikels
			\brief Kontrollartikel-Druckertext �ndern,
		*/
		void		setPrint(const QString& print)
		{
			setValue(entryPrint, print);
		}
		/*!	�bernimmt die Werte des Drucker-Kontrollartikels contr in die Datenstruktur.
			\param contr	Zu �bernehmde Werte
			\brief Werte �bernehmen.
		*/
		void		setData(TPrnControl* contr)
		{
			setControl(contr->getID());
			setName(contr->getName());
			setPrint(contr->getPrint());
		}
	};

}

#endif
 
