#ifndef				POSLIB_TARTQUANTITY_H
#define				POSLIB_TARTQUANTITY_H
#include			"basics/tinifile.h"
#include			"basics/tlockfile.h"
#include			"qstringlist.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Mengeninformationen
		von Artikeln.
		Die Mengeninformation zu einem Artikel wird in einer Ini-Datei gehalten, um mehreren
		Terminals den Zugriff zu erm�glichen.
		\brief POS-Klassen: Artikel, Mengeninformationen.
	*/
	class			TArtQuantity
	{
	public:
		static const char	fileName[];
		static const char	sectArt[];
	protected:
		static const char	fileLock[];
		static const char	sectSettings[];
		static const char	entryActive[];
		static const char	entryQuant[];
		static const char	entryZero[];
		static const char	entryWarn[];
		static const char	entryShowWarning[];
		static const char	entryIncr[];
		static const char	entryUnderrun[];
		static const char	pathName[];
	public:
		TArtQuantity(const char* path);
		~TArtQuantity();
		bool				isActive(int art) const;
		bool				showWarning(int art) const;
		bool				doUnderrun(int art) const;
		void				delQuantity(int art);
		void				zeroQuantity(int art);
		void				setQuantity(int art, double count);
		double				getWarn(int art) const;
		double				getQuantity(int art) const;
		double				bookQuantity(int art, double count);
		void				setData(int art, double count, double zero, bool incr);
		void				getData(int art, double& count, double& zero, bool& incr, bool& under);
		void				setZero(int art, double zero);
		void				setWarn(int art, double warn);
		void				setShowWarning(int art,bool s);
		void				setIncrease(int art, bool incr);
		void				setUnderrun(int art, bool under);
		void				setActive(int art, bool active);
		QStringList			getArticles();
		//QStringList			getArticles(bool all=FALSE);
	protected:
		QString				m_Path;
		TLockfile			m_Lock;
	};
}

using namespace PosLib;

#endif


