#ifndef				POSLIB_TTABLECHANGE_H
#define				POSLIB_TTABLECHANGE_H

namespace PosLib
{
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r die Tischaktion "Tisch wechseln".
		Wenn diese Aktion in einen Tisch eingef�gt wird, �ndert sich der Dateiname des Tisches.
		Falls sich die Applikation gerade in der Tisch�bersicht befindet, sollte das entpsrechende
		Signal abgefangen werden, um die Ansicht zu aktualisieren.
		\brief POS-Klassen: Tischaktionen, Tisch gewechselt.
	*/
	class			TTableChange
	: public TTableAction
	{
	public:
		static const char	entryFromTable[];
		static const char	entryFromParty[];
		static const char	entryFromWaiter[];
		static const char	entryToTable[];
		static const char	entryToParty[];
		static const char	entryToWaiter[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Change.
			\brief ctor
		*/
		TTableChange()
		: TTableAction(Change)
		{
		}
		/*!	\return der Tisch, von dem gewechselt wurde.
			\brief Quelltisch ermitteln.
		*/
		long		getFromTable() const
		{
			return getValue(entryFromTable, 0L);
		}
		/*!	�ndert den Quelltisch des Tischwechsel-Vorgangs.
			\param table		Alte Tischnummer
			\brief Tisch des Quelltisches �ndern.
		*/
		void		setFromTable(long table)
		{
			setValue(entryFromTable, table);
		}
		/*!	\return die Partei, von der gewechselt wurde.
			\brief Quellpartei ermitteln.
		*/
		int			getFromParty() const
		{
			return getValue(entryFromParty, 0);
		}
		/*!	�ndert die Parteinummer des  Quelltisches des Tischwechsel-Vorgangs.
			\param party		Alte Parteinummer
			\brief Partei des Quelltisches �ndern.
		*/
		void		setFromParty(int party)
		{
			setValue(entryFromParty, party);
		}
		/*!	\return der Tischeigent�mer des Tisches auf den gewechselt wurde.
			\brief Eigent�mer des Quelltisches ermitteln.
		*/
		int			getFromWaiter() const
		{
			return getValue(entryFromWaiter, 0);
		}
		/*!	�ndert den Kellner, dem der Tisch vor der Wechsel-Aktion geh�rte.
			\param waiter		Alter Eigent�mer des Tisches
			\brief Eigent�mer des Quelltisches �ndern.
		*/
		void		setFromWaiter(int waiter)
		{
			setValue(entryFromWaiter, waiter);
		}
		/*!	\return der Tisch, auf den gewechselt wurde.
			\brief Zieltisch ermitteln.
		*/
		long		getToTable() const
		{
			return getValue(entryToTable, 0L);
		}
		/*!	�ndert den Zieltisch des Tischwechsel-Vorgangs.
			\param table		Neue Tischnummer
			\brief Tisch des Zieltisches �ndern.
		*/
		void		setToTable(long table)
		{
			setValue(entryToTable, table);
		}
		/*!	\return die Partei, auf die gewechselt wurde.
			\brief Zielpartei ermitteln.
		*/
		int			getToParty() const
		{
			return getValue(entryToParty, 0);
		}
		/*!	�ndert die Zielpartei des Tischwechsel-Vorgangs.
			\param party		Neue Parteinummer
			\brief Partei des Zieltisches �ndern.
		*/
		void		setToParty(int party)
		{
			setValue(entryToParty, party);
		}
		/*!	\return den (neuen) Tischeigent�mer des Zieltisches.
			\brief Eigent�mer des Zieltisches ermitteln.
		*/
		int			getToWaiter() const
		{
			return getValue(entryToWaiter, 0);
		}
		/*!	�ndert den Kellner, dem der Tisch nach der Wechsel-Aktion geh�rt.
			\param waiter		Neuer Eigent�mer des Tisches
			\brief Eigent�mer des Zieltisches �ndern.
		*/
		void		setToWaiter(int waiter)
		{
			setValue(entryToWaiter, waiter);
		}
	};
}

#endif
