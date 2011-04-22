#ifndef				POSLIB_TERMDRV_H
#define				POSLIB_TERMDRV_H
#include			"basics/tinifile.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	Diese Klasse implementiert die Kombination aus Ger�tetreiber/Konfiguration innerhalb der Sektion
		Devices in der possystem.ini. Ein Laden der Konfigurationen finidet nicht statt.
		\brief POS-Klassen: Liste von Terminal-Konfigurationen, Eintrag in Devices.
	*/
	class			TTermDrv
	: public TValue
	{
	public:
		static const char	entryDriver[];						//!< Eintrag Ger�tetreiber
		static const char	entryConfig[];						//!< Eintrag Ger�tekonfiguration
		static const char	entryPath[];						//!< Pfad zu den entsprechenden Dateien
	public:
		/*!	Erzeugt eine neue Instanz einer Treiber/Konfiguration-Kombination.
			\brief ctor.
		*/
		TTermDrv();
		/*!	\return Liefert den Namen des f�r dieses Ger�t zust�ndigen Treibers. Innerhalb der Sektion
			Devices ist dies das Attribut.
			\brief Namen des Treibers errmitteln.
		*/
		QString		getDriver() const
		{
			return getString(entryDriver);
		}
		/*!	�ndert den f�r dieses Ger�t zust�ndigen Treiber, also das Attribut des zugeh�rigen Eintrags
			in der Sektion Device.
			\param drv		Der neue Ger�tetreiber.
			\brief Treiber des Ger�tes �ndern.
		*/
		void		setDriver(const QString& drv)
		{
			setValue(entryDriver, drv);
		}
		/*!	\return Liefert den Namen der f�r dieses Ger�t zust�ndigen Konfiguration. Innerhalb der Sektion
			Devices ist dies der Wert.
			\brief Konfiguration des Treibers errmitteln.
		*/
		QString		getConfig() const
		{
			return getString(entryConfig);
		}
		/*!	�ndert die f�r dieses Ger�t zust�ndige Konfigurationsdatei, also den Wert des zugeh�rigen Eintrags
			in der Sektion Device.
			\param cfg		Die neue Konfigurationsdatei.
			\brief Konfiguration des Ger�tes �ndern.
		*/
		void		setConfig(const QString& cfg)
		{
			setValue(entryConfig, cfg);
		}
		/*!	\return Liefert den Pfad zum Finden der Konfigurationsdateien f�r dieses Ger�t. Der Pfad wird
			beim Lesen der Devices-Sektion der entsprechenden Konfigurationsdatei ermittelt und dem Ger�t
			entsprechenden �bergeben.
			\brief Pfad zu den Dateien ermitteln.
		*/
		QString		getPath() const
		{
			return getString(entryPath);
		}
		/*!	�ndert den Pfad zum Finden der Konfigurationsdateien f�r dieses Ger�t. Der Pfad wird
			beim Lesen der Devices-Sektion der entsprechenden Konfigurationsdatei ermittelt und dem Ger�t
			entsprechenden �bergeben.
			\param path		Der neue Pfad zu den Dateien.
			\brief Pfad zu den Dateien �ndern.
		*/
		void		setPath(const QString& path)
		{
			setValue(entryPath, path);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse fa�t die Sektion Devices innerhalb der possystem zu einer Liste aus Ger�te-Treibern
		zusammen. Jeder Eintrag der Sektion besteht aus der Kombination Ger�te-Treiber/Konfiguration und
		wird entsprechend in einer Instanz von TTermDrv zusammengefa�t. Die Konfiguration des Treibers
		selbst wird nicht geladen. Hierf�r ist entweder der Ger�te-Loader (Benutzung der Ger�te) oder
		die Template-Verwaltung (Bearbeiten der Konfiguration) zust�ndig.
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
		/*!	L�dt alle in der Sektion Devices der Inidatei ini angegebenen Ger�te.
			\param ini		Konfigurationsdatei des Terminals (possystem.ini)
			\param path		Pfad zu den Daten (etc-Verzeichnis)
			\brief Ger�tekonfiguration laden.
		*/
		void		load(TInifile& ini, const QString& path);
		/*!	Speichert alle Ger�te der Liste in die Sektion Devices Der Inidatei ini.
			\param ini		Konfigurationsdatei des Terminals (possystem.ini)
			\param path		Pfad zu den Daten (etc-Verzeichnis)
			\brief Ger�tekonfiguration speichern.
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

