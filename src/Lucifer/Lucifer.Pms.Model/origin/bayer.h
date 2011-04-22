#ifndef				POSLIB_BAYER_H
#define				POSLIB_BAYER_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfaßt alle benötigten Informationen für das Bayer-Interface.
		Es werden Funktionen bereitgestellt, um die Gültigkeit der Merk- bzw Werkziffer
		zu prüfen.
		\brief POS-Klassen: Bayer-Interface.
	*/
	class			TBayer
	: public TValue
	{
	public:
		static const char	fileWerk[];					//!< Dateiname werk.dat
		static const char	fileMerk[];					//!< Dateiname merk.dat
//		static const char	fileKost[];					//!< Dateiname kost.dat
		static const char	bayerMerk[];				//!< Tischeintrag bayermerk
		static const char	bayerWerk[];				//!< Tischeintrag bayerwerk
	public:
		/*!	Erzeuge eine Instanz für Bayer und liest dabei die Bayer-Dateien in die entsprechenden
			Puffer.
			\param path		Pfad zu den zu ladenden Bayer-Dateien.
			\brief Ctor.
		*/
		TBayer(const QString& path);
		/*!	\return Liefert TRUE, wenn nur eine Merkziffer angegeben wurde und somit keine andere
			Eingabemöglichkeit für die Merkziffer besteht.
			\brief Default-Merkziffer vorhanden?
		*/
		bool		hasDefMerk() const
		{
			return m_Merk.count()==1;
		}
		/*!	\return Liefert die erste eingetragene Merkziffer als Default oder 0, falls die
			Datei leer ist.
			\brief Default-Merkziffer ermitteln.
		*/
		int			getDefMerk() const
		{
			return m_Merk.first().toInt();
		}
		/*!	Prüft nach, ob die angegebene Merkziffer val in der Merkziffer-Datei angegeben wurde.
			\return TRUE, wenn der Wert gültig ist.
			\brief Merk-Ziffer gültig?
		*/
		bool		hasMerk(int val)
		{
			return m_Merk.contains(QString::number(val));
		}
		/*!	\return Liefert TRUE, wenn nur eine Werkziffer angegeben wurde und somit keine andere
			Eingabemöglichkeit für die Werkziffer besteht.
			\brief Default-Werkziffer vorhanden?
		*/
		bool		hasDefWerk() const
		{
			return m_Werk.count()==1;
		}
		/*!	\return Liefert die erste eingetragene Werkziffer als Default oder 0, falls die 
			Datei leer ist.
			\brief Default-Merkziffer ermitteln.
		*/
		int			getDefWerk() const
		{
			return m_Werk.first().toInt();
		}
		/*!	Prüft nach, ob die angegebene Werkziffer val in der Werkziffer-Datei angegeben wurde.
			\return TRUE, wenn der Wert gültig ist.
			\brief Werk-Ziffer gültig?
		*/
		bool		hasWerk(int val)
		{
			return m_Werk.contains(QString::number(val));
		}
	protected:
		void		loadWerk(const QString& path);
		void		loadMerk(const QString& path);
	protected:
		QString		m_Path;
		QStringList	m_Werk;
		QStringList	m_Merk;
	};
}

using namespace PosLib;

#endif


