#ifndef				_POSLIB_TJOBPARSER_
#define				_POSLIB_TJOBPARSER_
#include			"poslib/tcurrency.h"
#include			"poslib/ttable.h"
#include			"basics/tvalue.h"
#include			"basics/tinifile.h"

namespace PosLib
{
	class			TJobParser
	: public TValue
	{
	public:
		static const char	pathConfig[];			//!< Pfad zur Konfigurationsdatei
		static const char	fileConfig[];			//!< Name der Konfigurationsdatei
		static const char	pathLayout[];			//!< Pfad zu den Layout-Dateien
		static const char	sectVars[];				//!< Variablen-Umdefinitionen
		static const char	layFile[];				//!< Include Textfile
		static const char	layLinefeed[]; 			//!< Zeilenvorschub
		static const char	layCurrency[];			//!< Umschalten der Druckw?ung
		static const char	layDate[];				//!< Das Erstellungsdatum des Drucks
		static const char	layJalaliDate[];		//!<
		static const char	layTime[];				//!< Die Erstellungszeit des Drucks
		static const char	layCurrName[];			//!< Der Name der aktuellen W?ung
		static const char	layCurrShort[];			//!< Die Kurzbezeichnung der aktuellen W?ung
		static const char	layCurrRate[];			//!< Der Umrechnungskurs der aktuellen W?ung
	public:
		TJobParser(TCurrencies* currs, TTable* table);
		/*!	Liest die angegebene Datei file ein und parst sie Zeilenweise. Die
			Ausgabe erfolgt in out.
			\param out		Ausgabe-Medium fr das Ergebnis
			\param file		Zu parsende Datei
			\param path		Pfad zu den Layouts
			\brief Layout-Datei parsen.
		*/
		void		parseLayout(QTextStream& out, const QString& file, const QString& path="");
		/*!	Parst den angegebene Textstream 'in'. Die Ausgabe erfolgt in out.
			\param in		Eingabe-Medium für das Ergebnis
			\param out		Ausgabe-Medium für das Ergebnis
			\brief Textstream parsen.
		*/
		void		parseStream(QTextStream& in,QTextStream& out);
	protected:
		/*!	Ermittelt entsprechend grp die Gruppe, zu der die aufzulsende Variable gehrt, und
			liefert den Wert der Variable var.
			\param out		Ausgabe-Medium fr das Ergebnis.
			\param grp		Die Gruppe der aufzulsenden Variable. Mgliche Werte sind.
							* terminal:	Terminal-Wert ermitteln
							* table:	Tisch-Wert ermitteln
							* entry:	Eintrags-Wert ermitteln
							* [leer]	Laufzeit-Wert ermitteln
			\param var		Aufzulsende Variable
			\param param	Formatierungs-Parameter
			\return Wenn es sich um eine Formatier-Anweisung wie LF handelt, liefert
					die Funktion einen leeren String, ansonsten einen entsprechend
					param formatierter Text.
			\brief Einen Eintrag untersuchen.
		*/
		virtual QString	parseEntry(QTextStream& out, const QString& grp, const QString& var, const QString& param);
		/*!	\return Formatiert den String str anhand der gegebenen Parameter param:
			* Ist param leer, wird der String selbst zurckgegeben
			* Ist param positiv, wird der String linksbndig mit der entsprechenden
				Anzahl von Leerzeichen aufgefllt
			* Ist param negativ, wird der String rechtsbndig mit der entsprechenden
				Anzahl von Leerzeichen aufgefllt.
			\param str		Zu formatierender String
			\param param	Formatierungs-Parameter
			\brief Text formatieren
		*/
		QString		formatLine(const QString& str, const QString& param);
		/*!	\return Einen anhand param formatierten String des Wertes des Eintrags
			var innerhalb von val, sofern val nicht NULL ist. Ansonsten wird ein leerer
			String zurckgegeben.
			\param val		Value, innerhalb dessen var gesucht wird
			\param var		Die zu suchende Variable innerhalb vo val
			\param param	Formatierungsanweisung
			\brief Hilfsfunktion um NULL-Zeiger abzufangen
		*/
		QString		parseValue(TValue* val, const QString& var, const QString& param)
		{
			if( !val )
				return QString();
			return formatLine(val->getValue(var), param);
		}
		/*!	\return Formatiert das Datum date.
			\param date		Zu formatierendes Datum
			\brief Datum formatieren
		*/
		QString		formatDate(const QDate& date);
		QString		formatJalaliDate(const QDate& date);
		QString		formatPrice(long price, bool showThs=FALSE)
		{
			if( m_Currs )
				return m_Currs->formatb(price, m_Curr, showThs);
			return QString::number(price);
		}
		/*!	\return Liefert den in cmd angegebenen Parameter oder einen leeren String.
			Parameter stehen mit @ getrennt hinter dem Variablennamen.
			\code entry@4 \endcode
			\param cmd	String mit der Kommando-Anweisung
			\brief Hilfsfunktion
		*/
		QString			getParam(QString& cmd)
		{
			QString ret;
			int pos = cmd.find('@');
			if( pos>=0 )
			{
				ret = cmd.mid(pos+1);
				cmd = cmd.left(pos);
			}
			return ret;
		}
		/*!	\return Liefert die in cmd angegebene Gruppe oder einen leeren String.
			Gruppen stehen mit :: getrennt vor dem Variablennamen.
			\code entry::plu \endcode
			\param cmd	String mit der Kommando-Anweisung
			\brief Hilfsfunktion
		*/
		QString		getGroup(QString& cmd)
		{
			QString ret;
			int pos = cmd.find("::");
			if( pos>=0 )
			{
				ret = cmd.left(pos);
				cmd = cmd.mid(pos+2);
			}
			return ret;
		}
		/*!	\return Liefert den in cmd angegebenen Vergleichsoperator oder einen leeren String.
			Operatoren stehen vor der Gruppe/vor der Variablen.
			Es stehen folgende Operatoren zur Verfgung:
			<>			Prfe ob ungleich
			<=			Prfe on kleiner/gleich
			>=			Prfe ob gr?r/gleich
			==			Prfe ob gleich
			<			Prfe ob kleiner
			>			Prfe ob gr?r
			Parameter stehen mit @ getrennt hinter dem Variablennamen.
			\code <>entry::plu \endcode
			\note Es knnen nur Integer verglichen werden.
			\param cmd	String mit der Kommando-Anweisung
			\brief Hilfsfunktion
		*/
		QString		getCompare(QString& cmd)
		{
			QString ret;
			// <> == <= >= < >
			if( cmd.left(1)=="!" || cmd.left(1)=="<" || cmd.left(1)==">" || cmd.left(1)=="=" )
			{
				ret = cmd.left(1);			// Operator gefunden
				cmd = cmd.mid(1);			// Kommando ist schon mal eins weniger
				if( cmd.left(1)==">" || cmd.left(1)=="=" )
				{
					ret += cmd.left(1);		// Operator mit 2 Zeichen
					cmd = cmd.mid(1);		// Kommando noch eins weniger
				}
			}
			return ret;
		}
		/*!	\return Liefert den Integer-Wert des Wertes grp::cmd, wenn es ein
			Variableninhalt gibt, ansonsten den Integer-Wert von cmd.
			\param out		Ausgabestream fr parseEntry
			\param grp		Grupppe der Variablen
			\param cmd		Die Variable oder ein direkter Integerwert
			\brief Hilfsfunktion zum Vergleichen fr If-Abfragen.
		*/
		int			getVarValue(QTextStream& out, const QString& grp, const QString& cmd)
		{
			QString tmp = parseEntry(out, grp.lower(), cmd.lower(), "");
			if( tmp.lower()=="no" )
				return 0;
			if( tmp.lower()=="yes" )
				return 1;
			if( tmp.isEmpty() )
				return cmd.toInt();
			return tmp.toInt();
		}
	protected:
		TCurrencies*	m_Currs;
		int				m_Curr;
		TInifile		m_Ini;					//!< ?ersetzungs-Datei fr Variablen
		TTable*			m_Table;				//!< Zu bearbeitendesr Tisch
		TValue*			m_Entry;				//!< Aktuell zu bearbeitender Eintrag w?end des Parsens
//		TTableEntry*	m_Entry;				//!< Aktuell zu bearbeitender Eintrag w?end des Parsens
		TTableControl*	m_Contr;				//!< Eventuelle Drucker-Kontrolle (Vor/Haupt/Nachspeise)
	};
}

using namespace PosLib;

#endif

