#ifndef				POSLIB_TFAMILY_H
#define				POSLIB_TFAMILY_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa? alle bentigten Informationen fr Warengruppen.
		Alle verfgbaren Warengruppen werden in einer Instanz von TFamilyList
		zur Verfgung gestellt.
		\brief POS-Klassen: Warengruppen.
	*/
	class			TFamily
	: public TNSValue
	{
		static const char	entryGroup[];
		static const char	entryPrio[];
		static const char	entryIH[];
		static const char	entryOH[];
		static const char	entryTaxIH[];
		static const char	entryTaxOH[];
		static const char	entryButton[];
		static const char	entryBitmap[];
		static const char	entryModifiers[];
		static const char	entryPrinters[];
		static const char	entryVPrinters[];
		static const char	entrySPrinters[];
		static const char	entryFoodBev[];
		static const char	entryAccount[];
	public:
		/*!	Erzeuge eine leere Instanz einer Warengruppe.
		*/
		TFamily()
		: TNSValue()
		{
		}
		/*!	\return Die Priorit?fr den Touch innerhalb einer Warengruppe. Warengruppen mit
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
			Falls Der String leer ist, werden die Voreinstellungen von TFamGroup genommen.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert berschreibt den Oberwaren-Drucker.
			\code
			QStringList list = QStringList::split(";", fam->getPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Warengruppe fr Bons ermitteln
			\sa setPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getPrinters() const
		{
			return getString(entryPrinters);
		}
		/*!	Idert die Drucker/Layout-Kombinationen, mit denen Artikel dieser Warengruppe beim
			Bestellen ausgedruckt werden soll. Siehe getPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelscht.
			\note Dieser Wert berschreibt den Oberwaren-Drucker.
			\brief Ausgabedrucker und -layout der Warengruppe fr Bons ?ern.
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
			Falls Der String leer ist, werden die Voreinstellungen von TFamGroup genommen.
			Die Kombinationen Drucker/Layout sind mit , getrennt, die Liste davon mit ;
			\note Dieser Wert berschreibt den Oberwaren-Drucker.
			\code
			QStringList list = QStringList::split(";", fam->getVPrinters());
			for( QStringList::Iterator it=list.begin(); it!=list.end(); ++it)
			{
				QStringList prns = QStringList::split(",", *it);
				int iPrn = prns[0].toInt();
				int iLay = prns[1].toInt();
			}
			\endcode
			\brief Ausgabedrucker und -layout der Warengruppe fr Stornos ermitteln.
			\sa setVPrinters, TPrnConfig, TPrnConfigList, TLayConfig, TLayConfigList
		*/
		QString		getVPrinters() const
		{
			return getString(entryVPrinters);
		}
		/*!	Idert die Drucker/Layout-Kombinationen, mit denen Artikel dieser Warengruppe beim
			Stornieren ausgedruckt werden soll. Siehe getVPrinters.
			\param prns		Die neuen Ausgabedrucker als Index auf die Druckerliste
							und das Ausgabelayout als Index auf die Layoutliste.
							Ist prns leer, wird das Attribut gelscht.
			\note Dieser Wert berschreibt den Warengruppen-Drucker.
			\brief Ausgabedrucker und -layout der Warengruppe fr Storno-Bons ?ern.
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
		/*!	\return die Oberwarengruppe der Warengruppe. Ist der Wert gleich 0, hat
			die Warengruppe keine Obergruppe. Der zurckgegebene Wert ist ein Index auf die
			Oberwarengruppenliste.
			\brief Oberwarengruppe abfragen.
			\sa setGroup, TFamilyGroup, TFamilyGroupList
		*/
		int			getGroup() const
		{
			return getValue(entryGroup, 0);
		}
		/*!	Idert die Oberwarenguppe der Warengruppe.
			\param group	Die neue Oberwarengruppe als Index auf die Oberwarengruppenliste.
							Ist der Wert 0, wird das Attribut gelscht.
			\brief Oberwarengruppe ?ern.
			\sa getGroup, TFamilyGroup, TFamilyGroupList
		*/
		void		setGroup(int group)
		{
			if( !group )
				clrValue(entryGroup);
			else
				setValue(entryGroup, group);
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
		/*!	\return den Ausgabetext fr einen Touch-Button. Wenn der Text selbst leer ist,
			wir der Name des Artikels zurckgegeben.
			\brief Button-Text abfragen.
			\sa setButton
		*/
		QString		getButton() const
		{
			QString ret = getValue(entryButton);
			if( ret.isEmpty() )
				return getName();
			return ret;
		}
		/*!	Idert den Button-Text des Artikels. Wenn txt leer ist, wird bei einem Aufruf von
			getButton() der Name des Artikels zurckgegeben.
			\param txt		der neue Buttontext des Artikels.
			\brief Button-Text ?ern.
		*/
		void		setButton(const QString& txt)
		{
			if( txt.isEmpty() )
				clrValue(entryButton);
			else
				setValue(entryButton, txt);
		}
		QString		getBitmap() const
		{
			return getString(entryBitmap);
		}
		void		setBitmap(const QString& str)
		{
			if( str.isEmpty() )
				clrValue(entryBitmap);
			else
				setValue(entryBitmap, str);
		}
		QStringList	getModifiers() const
		{
			return QStringList::split(";", getValue(entryModifiers));
		}
		QString		strModifiers() const
		{
			return getString(entryModifiers);
		}
		void		setModifiers(const QStringList& mods)
		{
			if( !mods.count() )
				clrValue(entryModifiers);
			else
				setValue(entryModifiers, mods.join(";"));
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


}

using namespace PosLib;

#endif


