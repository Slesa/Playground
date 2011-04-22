#ifndef				POSLIB_TTABLECHANGE_H
#define				POSLIB_TTABLECHANGE_H

namespace PosLib
{
	/*!	Diese Klasse umfaßt alle benötigten Informationen für die Tischaktion "Tisch wechseln".
		Wenn diese Aktion in einen Tisch eingefügt wird, ändert sich der Dateiname des Tisches.
		Falls sich die Applikation gerade in der Tischübersicht befindet, sollte das entpsrechende
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
		/*!	Ändert den Quelltisch des Tischwechsel-Vorgangs.
			\param table		Alte Tischnummer
			\brief Tisch des Quelltisches ändern.
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
		/*!	Ändert die Parteinummer des  Quelltisches des Tischwechsel-Vorgangs.
			\param party		Alte Parteinummer
			\brief Partei des Quelltisches ändern.
		*/
		void		setFromParty(int party)
		{
			setValue(entryFromParty, party);
		}
		/*!	\return der Tischeigentümer des Tisches auf den gewechselt wurde.
			\brief Eigentümer des Quelltisches ermitteln.
		*/
		int			getFromWaiter() const
		{
			return getValue(entryFromWaiter, 0);
		}
		/*!	Ändert den Kellner, dem der Tisch vor der Wechsel-Aktion gehörte.
			\param waiter		Alter Eigentümer des Tisches
			\brief Eigentümer des Quelltisches ändern.
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
		/*!	Ändert den Zieltisch des Tischwechsel-Vorgangs.
			\param table		Neue Tischnummer
			\brief Tisch des Zieltisches ändern.
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
		/*!	Ändert die Zielpartei des Tischwechsel-Vorgangs.
			\param party		Neue Parteinummer
			\brief Partei des Zieltisches ändern.
		*/
		void		setToParty(int party)
		{
			setValue(entryToParty, party);
		}
		/*!	\return den (neuen) Tischeigentümer des Zieltisches.
			\brief Eigentümer des Zieltisches ermitteln.
		*/
		int			getToWaiter() const
		{
			return getValue(entryToWaiter, 0);
		}
		/*!	Ändert den Kellner, dem der Tisch nach der Wechsel-Aktion gehört.
			\param waiter		Neuer Eigentümer des Tisches
			\brief Eigentümer des Zieltisches ändern.
		*/
		void		setToWaiter(int waiter)
		{
			setValue(entryToWaiter, waiter);
		}
	};
}

#endif
