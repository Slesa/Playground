#ifndef				POSLIB_TTABLECREATE_H
#define				POSLIB_TTABLECREATE_H

namespace PosLib
{
	/*!	Diese Klasse umfa�t alle ben�tigten Informationen f�r die Tischaktion "Tisch anlegen". Diese Aktion
		kommt nur einmal pro Tisch vor und beinhaltet die vorkonfigurierten Werte f�r diesen Tisch.
		\brief POS-Klassen: Tischaktionen, Tisch neu angelegt.
	*/
	class			TTableCreate
	: public TTableAction
	{
	public:
		static const char	defElementName[];
		static const char	entryTableName[];
		static const char	entryOrgWaiter[];
		static const char	entryOrgWaiterName[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Create.
			\brief ctor
		*/
		TTableCreate()
		: TTableAction(Create)
		{
		}
		void		setOrgWaiter(TWaiter* waiter)
		{
			if( !waiter )
				return;
			setValue(entryOrgWaiter, waiter->getID());
			setValue(entryOrgWaiterName, waiter->getName());
		}
		void		setWaiterDescr(const QString& d)
		{
			if( d.isEmpty() )
				clrValue(TWaiter::entryDescr);
			else
				setValue(TWaiter::entryDescr, d);
		}
		QString		getWaiterDescr() const 
		{
			return getString(TWaiter::entryDescr);
		}
		/*!	Extrahiert aus table die f�r die Tischaktion ben�tigten Tisch-Werte und tr�gt sie in die Aktion
			ein.
			\param table	Tischkonfiguration des Tisches
			\brief Ben�tigte Tischdaten in die Tischaktion �bernehmen.
		*/
		void		setTable(TTableCfg* table)
		{
			if( !table )
				return;
			setTableName(table->getName());
		}
		/*!	\return den Namen des Tisches, auf den sich diese Tischaktion bezieht.
			\brief Tischnamen der Tischaktion ermitteln.
		*/
		QString		getTableName() const
		{
			return getString(entryTableName);
		}
		/*!	�ndert den Namen des Tisches, auf den sich diese Tischaktion bezieht.
			\param name		Neuer Name des Tisches
			\brief Tischnamen der Tischaktion �ndern.
		*/
		void		setTableName(const QString& name)
		{
			if( name.isEmpty() )
				clrValue(entryTableName);
			else
				setValue(entryTableName, name);
		}
		/*!	\return Liefert TRUE wenn der Tisch Rabattierungen erlaubt. Default
		sind immer Rabatte erlaubt.
			\brief Flag Darf der Tisch rabattieren
		*/
		bool		canDiscounts()
		{
			return getValue(TTableCfg::entryDoDiscount,TRUE);
		}
		/*!	Aendert das Flag, ob es der tisch rabatte erlaubt
			\param flag		TRUE, wenn der Tisch rabattieren darf
			\breif Flag Rabatte erlauben verbieten
		*/
		void		setCanDiscounts(bool flag)
		{
			setValue(TTableCfg::entryDoDiscount,flag);
		}
		/*!	\return das Ablaufdatum des Vertrages (Loga Import)
			\brief Vertragsendedatum ermitteln.
		*/
		QDate		getContractExpire() const
		{
			return TValue::getDate(TTableCfg::entryContractExpire);
		}
		/*!	Aendert das Ablaufdatum des Vertrages (Loga Import)
			\param date		Neues Datum
			\brief Vertragsendedatum (Loga import) der Tischaktion aendern
		*/
		void		setContractExpire(const QDate& date)
		{
			if( !date.isValid() )
				clrValue(TTableCfg::entryContractExpire);
			else
				setValue(TTableCfg::entryContractExpire,date);
		}
		/*!	\return den Abrechnungskreis (Loga Import)
			\brief Abrechnungskreis (Loga import) ermitteln.
		*/
		int			getPayrollSubunit() const
		{
			return getValue(TTableCfg::entryPayrollSubunit,0);
		}
		/*!	Aendert den Abrechnugskreis (Loga import)
			\param prs		Neuer Abrechnuhnungskreis
			\brief Abrechnungskreis (Loga import) der Tischaktion aendern
		*/
		void		setPayrollSubunit(int prs)
		{
			if(!prs)
				clrValue(TTableCfg::entryPayrollSubunit);
			else
				setValue(TTableCfg::entryPayrollSubunit,prs);
		}
		/*!	\return den Mandanten (Loga Import)
			\brief Mandanten (Loga import) ermitteln.
		*/
		int			getMandant() const
		{
			return getValue(TTableCfg::entryMandant,0);
		}
		/*!	Aendert den Mandanten (Loga import)
			\param date		Neuer Mandant
			\brief Mandaten (Loga import) der Tischaktion aendern
		*/
		void		setMandant(int mandant)
		{
			if(!mandant)
				clrValue(TTableCfg::entryMandant);
			else
				setValue(TTableCfg::entryMandant,mandant);
		}
		/*!	\return Liefert TRUE, wenn es sich um einen Trainingstisch handelt, der nicht in den Umsatz
			einflie�en soll.
			\brief Flag Trainingstisch ermitteln.
		*/
		bool		isTrainee()
		{
			return getValue(TWaiter::entryTrainee, FALSE);
		}
		/*!	�ndert das Flag, ob es sich um einen Trainingstisch handelt und somit der Tisch nicht in den
			Umsatz einflie�en soll.
			\param flag		TRUE, wenn es sich um einen Trainingstisch handelt.
			\breif Flag Trainingstisch �ndern.
		*/
		void		setTrainee(bool flag)
		{
			if( !flag )
				clrValue(TWaiter::entryTrainee);
			else
				setValue(TWaiter::entryTrainee, flag);
		}
		/*!	Setzt alle Werte, die in cr auch gesetzt sind. Bestehende Werte in dieser Instanz werden dabei
			nicht zur�ckgesetzt.
			\return Liefert eine Referenz auf diese Instanz.
			\param cr		Die zu kopierenden Werte
			\brief Copy-Operator.
		*/
		TTableCreate&	operator = (const TTableCreate& cr);
	};

}

#endif
