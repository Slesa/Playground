#ifndef				POSLIB_TTABLEVOID_H
#define				POSLIB_TTABLEVOID_H

namespace PosLib
{
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r die Tischaktion "Artikelstorno".
		\brief POS-Klassen: Tischaktionen, Artikel storniert.
	*/
	class			TTableVoid
	: public TTableArtAction
	{
	public:
		static const char	entryAmount[];
		static const char	entryVWaiter[];
		static const char	entryVWaiterName[];
		static const char	entryReason[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Void.
			\brief ctor
		*/
		TTableVoid()
		: TTableArtAction(Void)
		{
		}
		double		getDAmount() const
		{
			return getValue(entryAmount, 0.0);
		}
		/*!	\return Liefert den Stornobetrag dieses Artikelstornos.
			\brief Stornobetrag ermitteln.
		*/
		long		getAmount() const
		{
			return lRound(getValue(entryAmount, 0.0));
		}
		/*!	�ndert den Stornobetrag dieses Artikelstornos.
			\param amount	der neue Stornobetrag
			\brief Stornobetrag �ndern.
		*/
		void		setAmount(double amount)
		{
			setValue(entryAmount, amount);
		}
		/*!	\return Liefert den Kellner, der dieses Storno durchgef�hrt hat.
			\brief Stornokellner ermitteln.
		*/
		int			getVWaiter() const
		{
			return getValue(entryVWaiter, 0);
		}
		/*!	�ndert den Kellner, der dieses Storno durchgef�hrt hat.
			\param waiter	der neue Stornokellner.
			\brief Stornokellner �ndern.
		*/
		void		setVWaiter(int waiter)
		{
			setValue(entryVWaiter, waiter);
		}
		/*!	\return Liefert den Namen des Kellners, der dieses Storno durchgef�hrt hat.
			\brief Name des Stornokellners ermitteln.
		*/
		QString		getVWaiterName() const
		{
			return getString(entryVWaiterName);
		}
		/*!	�ndert den Namen des Kellners, der dieses Storno durchgef�hrt hat.
			\param name		der neue Name.
			\brief Name des Stornokellners �ndern.
		*/
		void		setVWaiterName(const QString& name)
		{
			setValue(entryVWaiterName, name);
		}
		/*!	�bernimmt ale relevanten Stornokellner-Informationen aus waiter.
			\param waiter	der Kellner, der das Storno durchgef�hrt hat
			\brief Stornokellner-Daten �bernehmen.
		*/
		void		setVWaiter(TWaiter* waiter)
		{
			if( !waiter )
				return;
			setVWaiter(waiter->getID());
			setVWaiterName(waiter->getName());
		}
		/*!	\return Liefert den Stornogrund dieses Stornos.
			\brief Stornogrund ermitteln.
		*/
		QString		getReason() const
		{
			return getString(entryReason);
		}
		/*!	�ndert den Stornogrund dieses Stornos.
			\param str		Text mit dem Stornogrund
			\brief Stornogrund �ndern.
		*/
		void		setReason(const QString& str)
		{
			if( !str.isEmpty() )
				setValue(entryReason, str);
			else
				clrValue(entryReason);
		}
	};
}
 
#endif
