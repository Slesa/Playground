#ifndef				POSLIB_TFAMGROUP_H
#define				POSLIB_TFAMGROUP_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa? alle bentigten Informationen fr Ober-Warengruppen.
		Alle verfgbaren Oberwarengruppen werden in einer Instanz von TFamGroupList
		zur Verfgung gestellt.
		\brief POS-Klassen: Oberwarengruppen.
	*/
	class			TFamGroup
	: public TNValue
	{
	public:
		static const char	entryPrio[];
		static const char	entryPrinters[];
		static const char	entryVPrinters[];
		static const char	entrySPrinters[];
		static const char	entryIH[];
		static const char	entryOH[];
		static const char	entryTaxIH[];
		static const char	entryTaxOH[];
		static const char	entryFoodBev[];
		static const char	entryAccount[];
	public:
		/*!	Erzeuge eine leere Instanz einer Oberwarengruppe.
		*/
		TFamGroup()
		: TNValue()
		{
		}
		/*!	\return Die Priorit?fr den Touch innerhalb einer Oberwarengruppe. OWGs mit
			hherer Prio sollten weiter vorne stehen.
			\brief Touch, Gruppen-Priorit?ermitteln.
		*/
		int			getPrio() const
		{
			return getValue(entryPrio, 0);
		}
		void		setPrio(int prio)
		{
			if( !prio )
				clrValue(entryPrio);
			else
				setValue(entryPrio, prio);
		}
		/*!	\return Liefert als Stringliste die Kombinationen von Drucker und Layout
			als Index in die entsprechenden Tabellen  fr den Bondruck.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert bildet den Default fr alle Warengruppe dieser Oberwarengruppe.
			\code
			QStringList list = QStringList::split(";", grp->getPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Oberwaren fr Bons ermitteln
			\sa setPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPrinters() const
		{
			return getString(entryPrinters);
		}
		/*!	Idert die Drucker/Layout-Kombinationen, mit denen Artikel dieser Oberwarengruppe beim
			Bestellen ausgedruckt werden soll. Siehe getPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelscht.
			\note Dieser Wert bildet den Default fr alle Warengruppe dieser Oberwarengruppe.
			\brief Ausgabedrucker und -layout der Oberwarengruppe fr Bons ?ern.
			\sa getPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		void		setPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entryPrinters);
			else
				setValue(entryPrinters, prns);
		}
		/*!	\return Liefert als Stringliste die Kombinationen von Drucker und Layout
			als Index in die entsprechenden Tabellen fr den Stornodruck.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert bildet den Default fr alle Warengruppe dieser Oberwarengruppe.
			\code
			QStringList list = QStringList::split(";", grp->getVPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Oberwarengruppe fr Stornos ermitteln.
			\sa setVPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getVPrinters() const
		{
			return getString(entryVPrinters);
		}
		/*!	Idert die Drucker/Layout-Kombinationen, mit denen Artikel dieser Oberwarengruppe beim
			Stornieren ausgedruckt werden soll. Siehe getVPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelscht.
			\note Dieser Wert bildet den Default fr alle Warengruppe dieser Oberwarengruppe.
			\brief Ausgabedrucker und -layout der Oberwarengruppe fr Storno-Bons ?ern.
			\sa getVPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		void		setVPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entryVPrinters);
			else
				setValue(entryVPrinters, prns);
		}
		QString		getSPrinters() const
		{
			return getString(entrySPrinters);
		}
		void		setSPrinters(const QString& prns)
		{
			if( prns.isEmpty() )
				clrValue(entrySPrinters);
			else
				setValue(entrySPrinters, prns);
		}
		int			getIH() const
		{
			return getValue(entryIH, 0);
		}
		void		setIH(int ih)
		{
			if( !ih )
				clrValue(entryIH);
			else
				setValue(entryIH, ih);
		}
		int			getOH() const
		{
			return getValue(entryOH, 0);
		}
		void		setOH(int oh)
		{
			if( !oh )
				clrValue(entryOH);
			else
				setValue(entryOH, oh);
		}
		int			getTaxIH() const
		{
			return getValue(entryTaxIH, 0);
		}
		void		setTaxIH(int ih)
		{
			if( !ih )
				clrValue(entryTaxIH);
			else
				setValue(entryTaxIH, ih);
		}
		int			getTaxOH() const
		{
			return getValue(entryTaxOH, 0);
		}
		void		setTaxOH(int oh)
		{
			if( !oh )
				clrValue(entryTaxOH);
			else
				setValue(entryTaxOH, oh);
		}
		int			getFoodBev() const
		{
			return getValue(entryFoodBev, 0);
		}
		void		setFoodBev(int fb)
		{
			if( !fb )
				clrValue(entryFoodBev);
			else
				setValue(entryFoodBev, fb);
		}
		QString		getAccount() const
		{
			return getString(entryAccount);
		}
		void		setAccount(const QString& acc)
		{
			if( acc.isEmpty() )
				clrValue(entryAccount);
			else
				setValue(entryAccount, acc);
		}
	};

	class			TFamGroups
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TFamGroups(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TFamGroups()
		{
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString	getFileName() const
		{
			return fileName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		TFamGroup*	operator [] (int index)
		{
			return (TFamGroup*) TValueList::operator [](index);
		}
	};

	class			TFamGroupIt
	: public TValueListIt
	{
	public:
		TFamGroupIt(TFamGroups& list)
		: TValueListIt(list)
		{
		}
		TFamGroup*	operator () ()
		{
			return (TFamGroup*) TValueListIt::operator()();
		}
		TFamGroup*	toFirst()
		{
			return (TFamGroup*) TValueListIt::toFirst();
		}
		TFamGroup*	current()
		{
			return (TFamGroup*) TValueListIt::current();
		}
		TFamGroup*	operator ++ ()
		{
			return (TFamGroup*) TValueListIt:: operator ++();
		}
    };
}

using namespace PosLib;

#endif


