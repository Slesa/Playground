#ifndef				POSLIB_TTABLEARTACTION_H
#define				POSLIB_TTABLEARTACTION_H
#include			"qregexp.h"

namespace PosLib
{
	/*!	Diese Klasse bildet die Basisklasse fr alle artikelgenauen Tischaktionen. Pro Aktion werden sich
		gemerkt: Anzahl, Plu, Preis, Warengruppe und Oberwarengruppe.
		\brief POS-Klassen: Tischaktionen, Artikelaktionen.
	*/
	class			TTableArtAction
	: public TTableAction
	{
	public:
		static const char	entryCount[];
		static const char	entryPlu[];
		static const char	entryArticle[];
		static const char	entryArtShort[];
		static const char	entryArtPrint[];
		static const char	entryPrice[];
		static const char	entryFamily[];
		static const char	entryFamilyName[];
		static const char	entryFamGroup[];
		static const char	entryFamGroupName[];
	public:
		/*!	Erzeugt eine neue Instanz einer Artikel-Tischaktion mit dem Typ type.
			\param type		Der Typ der Aktion laut TTableEntry::Types
			\brief ctor
		*/
		TTableArtAction(int type)
		: TTableAction(type)
		{
		}
		bool		hasCount() const
		{
			double anz=getCount();
			return 0.0!=anz;
		}
		bool		isRealCount() const
		{
			double anz = getCount();
			return 0.0 != anz-(int)anz;
		}
		/*!	\return Liefert die Anzahl dieser Tischaktion.
			\brief Anzahl ermitteln.
		*/
		double		getCount() const
		{
			double c = getValue(entryCount, 0.0);
			return c;
		}
		/*!	Idert die Anzahl dieser Tischaktion.
			\param count	die neue Anzahl
			\brief Anzahl ?ern.
		*/
		void		setCount(double count)
		{
			setValue(entryCount, count);
		}
		/*!	\return Liefert die PLU-Nummer dieser Aktion.
			\brief PLU ermitteln.
		*/
		int			getPlu() const
		{
			return getValue(entryPlu, 0);
		}
		/*!	Idert die PLU-Nummer dieser Aktion.
			\param plu		die neue PLU
			\brief PLU ?ern.
		*/
		void		setPlu(int plu)
		{
			setValue(entryPlu, plu);
		}
		/*!	\return Liefert den Artikelnamen dieser Aktion.
			\brief Artikelnamen ermitteln.
		*/
		QString		getArticle() const
		{
			return getString(entryArticle).replace(QRegExp("[\r\n]"), "|");
		}
		/*!	Idert den Artikelnamen dieser Aktion.
			\param art		der neue Name
			\brief Artikelnamen ?ern.
		*/
		virtual void	setArticle(const QString& art)
		{
			setValue(entryArticle, art);
		}
		/*!	\return Liefert den Artikelkurznamen dieser Aktion.
			\brief Artikelkurznamen ermitteln.
		*/
		QString		getArtShort() const
		{
			return getString(entryArtShort);
		}
		/*!	Idert den Artikelkurznamen dieser Aktion.
			\param art		der neue Kurzname
			\brief Artikelkurznamen ?ern.
		*/
		void		setArtShort(const QString& art)
		{
			setValue(entryArtShort, art);
		}
		/*!	\return Liefert den Artikel-Druckertext dieser Aktion.
			\brief Artikel-Druckertext ermitteln.
		*/
		QString		getArtPrint() const
		{
			return getString(entryArtPrint);
		}
		/*!	Idert den Artikel-Druckertext dieser Aktion.
			\param art		der neue Druckertext
			\brief Artikel-Druckertext ?ern.
		*/
		void		setArtPrint(const QString& art)
		{
			setValue(entryArtPrint, art);
		}
		/*!	\return Liefert TRUE, wenn es sich bei dem Artikel um einen Hinweisartikel handelt.
			\brief Flag Hinweisartikel ermitteln.
		*/
		bool		isHint() const
		{
			return getValue(TArticle::entryIsHint, FALSE);
		}
		/*!	Idert das Flag, da?es sich bei diesem Artikel um einen Hinweisartikel handelt.
			\param hint		TRUE, wenn es ein inweis ist.
			\brief Flag Hinweisartikel ?ern.
		*/
		void		setHint(bool hint)
		{
			setValue(TArticle::entryIsHint, hint);
		}
		/*!	?ernimmt alle relevanten Artikeldaten aus art.
			\param art		Der Artikel dieser Aktion.
			\brief Artikeldaten bernehmen.
		*/
		virtual void	setArticle(TArticle* art);
		/*!	\return Liefert den Artikelpreis dieser Aktion.
			\brief Artikelpreis ermitteln.
		*/
		long		getPrice(bool real=FALSE);
		/*
		{
			return getValue(entryPrice, 0);
		}*/
		/*!	Idert den Artikelpreis dieser Aktion.
			\param pr		der neue Preis
			\brief ArtikelPreis ?ern.
		*/
		void		setPrice(long pr)
		{
			setValue(entryPrice, pr);
		}
		/*!	\return Liefert die Warengrupe des Artikels dieser Aktion.
			\brief Warengruppe ermitteln.
		*/
		int			getFamily() const
		{
			return getValue(entryFamily, 0);
		}
		/*!	Idert die Warengruppe des Artikels dieser Aktion.
			\param fam		die neue Warengruppe
			\brief Warengruppe ?ern.
		*/
		void		setFamily(int fam)
		{
			setValue(entryFamily, fam);
		}
		/*!	\return Liefert den Namen der Warengrupe dieser Aktion.
			\brief Namen der Warengruppe ermitteln.
		*/
		QString		getFamilyName() const
		{
			return getString(entryFamilyName);
		}
		/*!	Idert den Namen der Warengruppe dieser Aktion.
			\param name		der neue Name
			\brief Namen der Warengruppe ?ern.
		*/
		void		setFamilyName(const QString name)
		{
			setValue(entryFamilyName, name);
		}
		/*!	?ernimmt aus fam alle relevanten Warengruppen-Informationen fr die Tischaktion.
			\param fam		Die Warengruppe der Tischaktion
			\brief Warengruppen-Informationen bernehmen.
		*/
		void		setFamily(TFamily* fam)
		{
			if( !fam )
				return;
			setFamily(fam->getID());
			setFamilyName(fam->getName());
			setFamGroup(fam->getGroup());
		}
		/*!	\return Liefert die Oberwarengrupe des Artikels dieser Aktion.
			\brief Oberwarengruppe ermitteln.
		*/
		int			getFamGroup() const
		{
			return getValue(entryFamGroup, 0);
		}
		/*!	Idert die Oberwarengruppe des Artikels dieser Aktion.
			\param grp		die neue Oberwarengruppe
			\brief Oberwarengruppe ?ern.
		*/
		void		setFamGroup(int grp)
		{
			setValue(entryFamGroup, grp);
		}
		/*!	\return Liefert den Namen der Oberwarengrupe dieser Aktion.
			\brief Namen der Oberwarengruppe ermitteln.
		*/
		QString		getFamGroupName() const
		{
			return getString(entryFamGroupName);
		}
		/*!	Idert den Namen der Oberwarengruppe dieser Aktion.
			\param name		der neue Name
			\brief Namen der Oberwarengruppe ?ern.
		*/
		void		setFamGroupName(const QString& name)
		{
			setValue(entryFamGroupName, name);
		}
		/*!	?ernimmt aus grp alle relevanten Oberwarengruppen-Informationen fr die Tischaktion.
			\param grp		Die Oberwarengruppe der Tischaktion
			\brief Oberwarengruppen-Informationen bernehmen.
		*/
		void		setFamGroup(TFamGroup* grp)
		{
			if( !grp )
				return;
			setFamGroupName(grp->getName());
		}
	};
}

#endif

