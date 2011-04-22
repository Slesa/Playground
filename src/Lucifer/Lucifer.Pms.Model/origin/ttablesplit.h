#ifndef				POSLIB_TTABLESPLIT_H
#define				POSLIB_TTABLESPLIT_H

namespace PosLib
{
	/*!	Diese Klasse umfa? alle bentigten Informationen fr die Tischaktion "Tisch splitten".
		Gespeichert werden beim Splitten die Informationen: Anzahl, Plu, Betrag, Quell- und Zieltisch
		sowie die brigen Werte von TTableAction.
		\brief POS-Klassen: Tischaktionen, Tisch splitten.
	*/
	class			TTableSplit
	: public TTableAction
	{
		static const char	entryFromTable[];
		static const char	entryFromParty[];
		static const char	entryToTable[];
		static const char	entryToParty[];
	public:
		/*!	Erzeugt eine neue Instanz einer Tischaktion mit dem Typ TTableEntry::Split.
			\brief ctor
		*/
		TTableSplit()
		: TTableAction(Split)
		{
		}
		/*!	\return Liefert die PLU des gesplitten Artikels.
			\brief PLU ermitteln.
		*/
		int			getPlu() const
		{
			return getValue(TTableOrder::entryPlu, 0);
		}
		/*!	Idert die PLU des gesplitteten Artikels auf plu.
			\param plu		Die PLU des Split-Artikels.
			\brief PLU ?ern.
		*/
		void		setPlu(int plu)
		{
			setValue(TTableOrder::entryPlu, plu);
		}
		QString		getArticle()
		{
			return getString(TTableArtAction::entryArticle);
		}
		void		setArticle(const QString& art)
		{
			setValue(TTableArtAction::entryArticle, art);
		}
		void		setArtPrint(const QString& art)
		{
			setValue(TTableArtAction::entryArtPrint, art);
		}
		/*!	\return Liefert die Anzahl der gesplitteten Artikel.
			\brief Anzahl ermitteln.
		*/
		double		getCount() const
		{
			return getValue(TTableOrder::entryCount, 0.0);
		}
		/*!	Ider die Anzahl der gesplitteten Artikel auf count.
			\param count	Die neue Anzahl.
			\brief Anzahl ?ern.
		*/
		void		setCount(double count)
		{
			setValue(TTableOrder::entryCount, count);
		}
		/*!	\return Liefert den Betrag der gesplitteten Artikel.
			\brief Splitbetrag ermitteln.
		*/
		long		getAmount() const
		{
			return getValue(TTableVoid::entryAmount, 0);
		}
		/*!	Idert den Betrag der gesplitteten Artikel auf amount.
			\param amount	Der neue Split-Betrag.
			\brief Splitbetrag ?ern.
		*/
		void		setAmount(long amount)
		{
			setValue(TTableVoid::entryAmount, amount);
		}
		/*!	\return Liefert den Quelltisch des Splitvorgangs.
			\brief Quelltisch ermitteln.
		*/
		long		getFromTable() const
		{
			return getValue(entryFromTable, 0L);
		}
		/*!	Idert den Quelltisch des Splitvorgangs auf table.
			\param table	Neuer Quelltisch
			\brief Quelltisch ?ern.
		*/
		void		setFromTable(long table)
		{
			setValue(entryFromTable, table);
		}
		/*!	\return Liefert die Quellpartei des Splitvorgangs.
			\brief Quellpartei ermitteln.
		*/
		int			getFromParty() const
		{
			return getValue(entryFromParty, 0);
		}
		/*!	Idert die Quellpartei des Splitvorgangs auf party.
			\param party	Neue Quellpartei
			\brief Quellpartei ?ern.
		*/
		void		setFromParty(int party)
		{
			setValue(entryFromParty, party);
		}
		/*!	Idert den Quelltisch und die Quellpartei des Splitvorgangs anhand der Werte von from.
			\param from		Tisch mit den Werten Quelltisch und Quelldatei
			\brief Quelltisch und -partei ?ern.
		*/
		void		setFromTable(TTable* from);
		/*!	\return Liefert den Zieltisch des Splitvorgangs.
			\brief Zieltisch ermitteln.
		*/
		long		getToTable() const
		{
			return getValue(entryToTable, 0L);
		}
		/*!	Idert den Zieltisch des Splitvorgangs auf table.
			\param table	Neuer Zieltisch
			\brief Zieltisch ?ern.
		*/
		void		setToTable(long table)
		{
			setValue(entryToTable, table);
		}
		/*!	\return Liefert die Zielpartei des Splitvorgangs.
			\brief Zielpartei ermitteln.
		*/
		int			getToParty() const
		{
			return getValue(entryToParty, 0);
		}
		/*!	Idert die Zielpartei des Splitvorgangs auf party.
			\param party	Neue Zielpartei
			\brief Zielpartei ?ern.
		*/
		void		setToParty(int party)
		{
			setValue(entryToParty, party);
		}
		/*!	Idert den Zieltisch und die Zielpartei des Splitvorgangs anhand der Werte von to.
			\param from		Tisch mit den Werten Zieltisch und Zieldatei
			\brief Zieltisch und -partei ?ern.
		*/
		void		setToTable(TTable* to);
	};

}

#endif
