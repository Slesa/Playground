#ifndef				POSLIB_TERMDRVTEMPL_H
#define				POSLIB_TERMDRVTEMPL_H
#include			"poslib/ttermdrv.h"
#include			"basics/tinifile.h"
#include			"qtwidgets/tprogram.h"

namespace PosLib
{
	/*!	Diese Klasse implementiert die Templates der Ger�etreiber unter etc/hardware.
		Zu jedem Ger� sollte eine Template-Beschreibung in der Sektion Template existieren, die den Hersteller,
		den Namen des Ger�es und eine kurze Beschreibung beinhaltet.
		\brief POS-Klassen: Liste von Terminal-Konfigurationen, Ger�e-Templates.
	*/
	class			TTermDrvTempl
	: public TInifile
	{
	public:
		/*!	Die verfgbaren Arten der Eingabe einer Options-Seite.
			\brief Eingabe-M�lichkeiten.
		*/
	public:
		static const char	pathName[];							//!< Pfad zu den Templates
		static const char	sectTempl[];						//!< Sektion [Template]
		static const char	entryName[];						//!< [Template], Name
		static const char	entryManufact[];					//!< [Template], Manufacturer
		static const char	entryDescr[];						//!< [Template], Description
		static const char	entryWin32[];						//!< [Template], Win32
		static const char	entryDriver[];						//!< [Template], Driver
		static const char	entryPages[];						//!< [Template], Anzahl der Einstellungsseiten
		static const char	entryPageAttr[];					//!< [TemplPage<n>], Zu setzender Wert
		static const char	entryPageSect[];					//!< [TemplPage<n>], Sektion des Wertes
		static const char	entryPageCapt[];					//!< [TemplPage<n>], Titel der Seite
		static const char	entryPageHint[];					//!< [TemplPage<n>], Hinweis zum Wert
		static const char	entryPageBmp[];						//!< [TemplPage<n>], Optionale Bitmap zum Wert
		static const char	entryPageType[];					//!< [TemplPage<n>], Art der Eingabe
		static const char	entryPageLabel[];					//!< [TemplPage<n>], Label-Text
		static const char	entryPageValue[];					//!< [TemplPage<n>], Vorgabe-Wert
		static const char	entryPageMin[];						//!< [TemplPage<n>], Spin-Minimum
		static const char	entryPageMax[];						//!< [TemplPage<n>], Spin-Maximum
		static const char	entryPageStep[];					//!< [TemplPage<n>], Spin-Linestep
		static const char	entryPageValues[];					//!< [TemplPage<n>], Kombobox-Eintr�e
		static const char	entryPageMeans[];					//!< [TemplPage<n>], Kombobox-Eintr�e (�ersetzung)
		static const char	entryPageDatas[];					//!< [TemplPage<n>], Kombobox-Eintr�e (Liste)
		static const char	entryPageIfOp[];					//!< [TemplPage<n>], If Abfrage, Vergleichsoperation(en)
		static const char	entryPageIfNext[];					//!< [TemplPage<n>], If Abfrage, Next disablen
		static const char	entryPageIfFinish[];				//!< [TemplPage<n>], If Abfrage, Finish disablen
		static const char	entryPageIfPages[];					//!< [TemplPage<n>], If Abfrage, Abh�gige Seiten
		static const char	sectPage[];							//!< Sektion [TempPage<n>]
		static const char	entryPort[];						//!< [Settings], Port (<b>nur posserial</b>)
		static const char	sectLog[];							//!< Sektion [Log]
		static const char	entryLogFile[];						//!< [Log], Logdate
		static const char	pathDevices[];						//!< Pfad zu den Ger�ekonfigurationen
		static const char	pathPrinters[];						//!< Pfad zu den Druckern
		static const char	pathDisplays[];						//!< Pfad zu den Kundendisplays
		static const char	pathDrawers[];						//!< Pfad zu den Schubladen
		static const char	pathSystems[];						//!< Pfad zu den kompakt Systemen
		static const char	pathKeylocks[];						//!< Pfad zu den Schlsseln
		static const char	pathLog[];							//!< Pfad zu den Logdateien
		static const char	drvSerial[];						//!< Treiber posserial
		static const char	drvFile[];							//!< Treiber posfile
		static const char	drvOpos[];							//!< Treiber posopos
		static const char	drvSocket[];						//!< Treiber possocket
		static const char	drvSharp[];							//!< Treiber possharp
		static const char	inputNone[];						//!< Gar keine Eingabe
		static const char	inputLine[];						//!< Text-Eingabe
		static const char	inputValue[];						//!< Von-Bis-Eingabe
		static const char	inputCombo[];						//!< Combo-Box-Auswahl
		static const char	inputYesNo[];						//!< Ja/Nein-Checkbox
	public:
		/*!	Erzeugt eine neue Instanz einer Treiber/Konfiguration-Kombination.
			\brief ctor.
		*/
		TTermDrvTempl(const QString& name);
		TTermDrvTempl(TTermDrv& drv, const QString& path);
		void		save(const QString& file)
		{
			TInifile::save(file);
		}
		void		save(TTermDrv& drv, const QString& path);
		QString		getName()
		{
			return getString(sectTempl, entryName);
		}
		QString		getManufacturer()
		{
			return getString(sectTempl, entryManufact);
		}
		QString		getDescription()
		{
			return getString(sectTempl, entryDescr);
		}
		QString		getDriver()
		{
			return getString(sectTempl, entryDriver);
		}
		bool		isWin32()
		{
			return !getString(sectTempl, entryWin32).isEmpty();
		}
		int			getPages()
		{
			return getValue(sectTempl, entryPages);
		}
		int			getPort()
		{
			return getValue(TProgram::sectSettings, entryPort);
		}
		QString		getConfig();
		QString		getLogFile();
		/*!	\return Options-Seite, liefert das zu setzende Attribut der Seite.
			\param page		Nummer der Seite.
			\brief Attribut ermitteln.
		*/
		QString		getPageAttr(int page)
		{
			return getString(getPageSect(page), entryPageAttr);
		}
		/*!	\return Options-Seite, liefert die Sektion des zu setzenden Attributs der Seite.
			\param page		Nummer der Seite.
			\brief Sektion des Attributs ermitteln.
		*/
		QString		getPageSection(int page)
		{
			return getString(getPageSect(page), entryPageSect);
		}
		/*!	\return Options-Seite, liefert den Titel der Options-Seite page.
			\param page		Nummer der Seite.
			\brief Titel der Optionsseite ermitteln.
		*/
		QString		getPageCaption(int page)
		{
			return getString(getPageSect(page), entryPageCapt);
		}
		/*!	\return Options-Seite, liefert den Hinweis der Options-Seite page.
			\param page		Nummer der Seite.
			\brief Hinweis der Optionsseite ermitteln.
		*/
		QString		getPageHint(int page)
		{
			return getString(getPageSect(page), entryPageHint);
		}
		/*!	\return Options-Seite, liefert das Bitmap der Options-Seite page.
			\param page		Nummer der Seite.
			\brief Bitmap der Optionsseite ermitteln.
		*/
		QString		getPageBitmap(int page)
		{
			return getString(getPageSect(page), entryPageBmp);
		}
		/*!	\return Options-Seite, liefert die Eingabem�lichketi der Option selbst. Die Eingabem�lichkeiten
			sind in Inputs beschrieben.
			\param page		Nummer der Seite.
			\brief Eingabem�lichkeit der Optionsseite ermitteln.
		*/
		QString		getPageType(int page)
		{
			return getString(getPageSect(page), entryPageType);
		}
		QString		getPageLabel(int page)
		{
			return getString(getPageSect(page), entryPageLabel);
		}
		int			getPageMin(int page)
		{
			return getValue(getPageSect(page), entryPageMin);
		}
		int			getPageMax(int page)
		{
			return getValue(getPageSect(page), entryPageMax);
		}
		int			getPageStep(int page)
		{
			return getValue(getPageSect(page), entryPageStep, 1);
		}
		/*!	\return Options-Seite, liefert den Werte der Combobox.
			\param page		Nummer der Seite.
			\brief Optionsseite, Werte der Combobox ermitteln.
		*/
		QStringList	getPageValues(int page)
		{
			return QStringList::split(":", getString(getPageSect(page), entryPageValues));
		}
		QStringList	getPageMeans(int page)
		{
			return QStringList::split(":", getString(getPageSect(page), entryPageMeans));
		}
		QString		getPageDatas(int page)
		{
			return getString(getPageSect(page), entryPageDatas);
		}
		QString		getPageIfOp(int page)
		{
			return getString(getPageSect(page), entryPageIfOp);
		}
		bool		isPageIfNext(int page)
		{
			return getValue(getPageSect(page), entryPageIfNext, FALSE);
		}
		bool		isPageIfFinish(int page)
		{
			return getValue(getPageSect(page), entryPageIfFinish, FALSE);
		}
		QStringList	getPageIfPages(int page)
		{
			return QStringList::split(":", getString(getPageSect(page), entryPageIfPages));
		}
	protected:
		QString		getPageSect(int p)
		{
			return sectPage+QString::number(p);
		}
	};

}

using namespace PosLib;

#endif

