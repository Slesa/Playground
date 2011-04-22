#ifndef				POSLIB_TERMDRV_H
#define				POSLIB_TERMDRV_H
#include			"basics/tinifile.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	Diese Klasse implementiert die Kombination aus Gerätetreiber/Konfiguration innerhalb der Sektion
		Devices in der possystem.ini. Ein Laden der Konfigurationen finidet nicht statt.
		\brief POS-Klassen: Liste von Terminal-Konfigurationen, Eintrag in Devices.
	*/
	class			TTermDrv
	: public TValue
	{
	public:
		static const char	entryDriver[];						//!< Eintrag Gerätetreiber
		static const char	entryConfig[];						//!< Eintrag Gerätekonfiguration
		static const char	entryPath[];						//!< Pfad zu den entsprechenden Dateien
	public:
		/*!	Erzeugt eine neue Instanz einer Treiber/Konfiguration-Kombination.
			\brief ctor.
		*/
		TTermDrv();
		/*!	\return Liefert den Namen des für dieses Gerät zuständigen Treibers. Innerhalb der Sektion
			Devices ist dies das Attribut.
			\brief Namen des Treibers errmitteln.
		*/
		QString		getDriver() const
		{
			return getString(entryDriver);
		}
		/*!	Ändert den für dieses Gerät zuständigen Treiber, also das Attribut des zugehörigen Eintrags
			in der Sektion Device.
			\param drv		Der neue Gerätetreiber.
			\brief Treiber des Gerätes ändern.
		*/
		void		setDriver(const QString& drv)
		{
			setValue(entryDriver, drv);
		}
		/*!	\return Liefert den Namen der für dieses Gerät zuständigen Konfiguration. Innerhalb der Sektion
			Devices ist dies der Wert.
			\brief Konfiguration des Treibers errmitteln.
		*/
		QString		getConfig() const
		{
			return getString(entryConfig);
		}
		/*!	Ändert die für dieses Gerät zuständige Konfigurationsdatei, also den Wert des zugehörigen Eintrags
			in der Sektion Device.
			\param cfg		Die neue Konfigurationsdatei.
			\brief Konfiguration des Gerätes ändern.
		*/
		void		setConfig(const QString& cfg)
		{
			setValue(entryConfig, cfg);
		}
		/*!	\return Liefert den Pfad zum Finden der Konfigurationsdateien für dieses Gerät. Der Pfad wird
			beim Lesen der Devices-Sektion der entsprechenden Konfigurationsdatei ermittelt und dem Gerät
			entsprechenden übergeben.
			\brief Pfad zu den Dateien ermitteln.
		*/
		QString		getPath() const
		{
			return getString(entryPath);
		}
		/*!	Ändert den Pfad zum Finden der Konfigurationsdateien für dieses Gerät. Der Pfad wird
			beim Lesen der Devices-Sektion der entsprechenden Konfigurationsdatei ermittelt und dem Gerät
			entsprechenden übergeben.
			\param path		Der neue Pfad zu den Dateien.
			\brief Pfad zu den Dateien ändern.
		*/
		void		setPath(const QString& path)
		{
			setValue(entryPath, path);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse faßt die Sektion Devices innerhalb der possystem zu einer Liste aus Geräte-Treibern
		zusammen. Jeder Eintrag der Sektion besteht aus der Kombination Geräte-Treiber/Konfiguration und
		wird entsprechend in einer Instanz von TTermDrv zusammengefaßt. Die Konfiguration des Treibers
		selbst wird nicht geladen. Hierfür ist entweder der Geräte-Loader (Benutzung der Geräte) oder
		die Template-Verwaltung (Bearbeiten der Konfiguration) zuständig.
		\brief POS-Klassen: Liste von Terminal-Konfigurationen, Sektion Devices.
	*/
	class			TTermDrvs
	: public QDict<TTermDrv>
	{
	public:
		static const char	sectDevices[];						// Sektion [Devices] in der possystem
	public:
		/*!	Erzeugt eine neue Instanz einer Terminal-Treiberliste.
			\brief ctor.
		*/
		TTermDrvs()
		: QDict<TTermDrv>(17, FALSE)
		{
			setAutoDelete(TRUE);
		}
		/*!	Lädt alle in der Sektion Devices der Inidatei ini angegebenen Geräte.
			\param ini		Konfigurationsdatei des Terminals (possystem.ini)
			\param path		Pfad zu den Daten (etc-Verzeichnis)
			\brief Gerätekonfiguration laden.
		*/
		void		load(TInifile& ini, const QString& path);
		/*!	Speichert alle Geräte der Liste in die Sektion Devices Der Inidatei ini.
			\param ini		Konfigurationsdatei des Terminals (possystem.ini)
			\param path		Pfad zu den Daten (etc-Verzeichnis)
			\brief Gerätekonfiguration speichern.
		*/
		void		save(TInifile& ini, const QString& path);
		void		save(const QString& from, const char* to="");
	};

	class			TTermDrvIt
	: public QDictIterator<TTermDrv>
	{
	public:
		TTermDrvIt(TTermDrvs& list)
		: QDictIterator<TTermDrv>(list)
		{
		}
		/*
		TTermDrv*	operator () ()
		{
			return (TTermDrv*) TValueListIt::operator()();
		}
		TTermDrv*	toFirst()
		{
			return (TTermDrv*) TValueListIt::toFirst();
		}
		TTermDrv*	current()
		{
			return (TTermDrv*) TValueListIt::current();
		}
		TTermDrv*	operator ++ ()
		{
			return (TTermDrv*) TValueListIt:: operator ++();
		}
		*/
	};
}

using namespace PosLib;

#endif

