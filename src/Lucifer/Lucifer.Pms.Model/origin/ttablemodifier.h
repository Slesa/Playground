#ifndef				POSLIB_TTABLEMODIFIER_H
#define				POSLIB_TTABLEMODIFIER_H

namespace PosLib
{
	class			TTableModifier
	: public TTableAction
	{
		static const char	entryNumber[];
		static const char	entryName[];
		static const char	entryPrice[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Modifier.
			\brief ctor
		*/
		TTableModifier()
		: TTableAction(Modifier)
		{
		}
		TTableModifier& operator = (const TTableModifier& mod);
		/*!	\return Liefert den Artikelpreis dieser Aktion.
			\brief Artikelpreis ermitteln.
		*/
		long		getPrice() const
		{
			return getValue(entryPrice, 0);
		}
		/*!	Ändert den Artikelpreis dieser Aktion.
			\param pr		der neue Preis
			\brief ArtikelPreis ändern.
		*/
		void		setPrice(long pr)
		{
			setValue(entryPrice, pr);
		}
		/*!	\return Liefert den Index des bestellten Modifiers.
			\brief Modifier ermitteln,
		*/
		int			getNumber()
		{
			return getValue(entryNumber, 0);
		}
		/*!	Ändert den Index des bestellten Modifiers.
			\param num	Die Nummer des Modifiers
			\brief Modifiers ändern,
		*/
		void		setNumber(int num)
		{
			setValue(entryNumber, num);
		}
		/*!	\return Liefert den Namen des bestellten Modifierss.
			\brief Namen des Modifierss ermitteln,
		*/
		QString		getName() const
		{
			return getString(entryName);
		}
		/*!	Ändert den Namen des bestellten Modifierss.
			\param name		Der Name des Modifierss
			\brief Modifiers-Text ändern,
		*/
		void		setName(const QString& name)
		{
			setValue(entryName, name);
		}
		/*!	Übernimmt die Werte des Modifiers mod in die Datenstruktur.
			\param mod	Zu übernehmde Werte
			\brief Werte übernehmen.
		*/
		void		setData(TModifier* mod)
		{
			setNumber(mod->getID());
			setName(mod->getName());
			setPrice(mod->getPrice());
		}
	};

}

#endif

