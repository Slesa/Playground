#ifndef				POSLIB_TPOSPARSER_H
#define				POSLIB_TPOSPARSER_H
#include			"poslib/tcurrency.h"
#include			"poslib/tposspace.h"
#include			"poslib/ttable.h"
#include			"basics/tparser.h"

namespace PosLib
{
	class			TPosParser
	: public TParser
	{
	public:
		static const char	layCurrency[];			//!< Umschalten der Druckw�hrung
		static const char	layCurrName[];			//!< Der Name der aktuellen W�hrung
		static const char	layCurrShort[];			//!< Die Kurzbezeichnung der aktuellen W�hrung
		static const char	grpTable[];				//!< Die Gruppe "Tisch"
		static const char	grpCreate[];			//!< Die Gruppe Create-Borgang
		static const char	grpControl[];			//!< Die Gruppe Kontroll-Artikel
	public:
		TPosParser(TInifile& ini, TCurrencies* currs, TPosSpace* space=NULL, TTable* table=NULL);
	protected:
		/*!	Ermittelt entsprechend grp die Gruppe, zu der die aufzul�sende Variable geh�rt, und
			liefert den Wert der Variable var.
			\param out		Ausgabe-Medium f�r das Ergebnis.
			\param grp		Die Gruppe der aufzul�senden Variable. M�gliche Werte sind.
							* terminal:	Terminal-Wert ermitteln
							* table:	Tisch-Wert ermitteln
							* entry:	Eintrags-Wert ermitteln
							* [leer]	Laufzeit-Wert ermitteln
			\param var		Aufzul�sende Variable
			\param param	Formatierungs-Parameter
			\return Wenn es sich um eine Formatier-Anweisung wie LF handelt, liefert
					die Funktion einen leeren String, ansonsten einen entsprechend
					param formatierter Text.
			\brief Einen Eintrag untersuchen.
		*/
		virtual bool	parseEntry(QString& ret, const QString& grp, const QString& var, const QString& param);
		virtual bool	formatEntry(QString& ret, const QString& str, QString& param);
		QString		formatPrice(long price, bool showThs=FALSE);
	protected:
		TCurrencies*	m_Currs;
		TPosSpace*		m_Space;
		int				m_Curr;
		TTable*			m_Table;				//!< Zu bearbeitendesr Tisch
//		TTableEntry*	m_Entry;				//!< Aktuell zu bearbeitender Eintrag w�hrend des Parsens
		TTableControl*	m_Contr;				//!< Eventuelle Drucker-Kontrolle (Vor/Haupt/Nachspeise)
	};
}

using namespace PosLib;

#endif

