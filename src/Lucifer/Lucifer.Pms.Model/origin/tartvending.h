#ifndef				POSLIB_TARTVENDING_H
#define				POSLIB_TARTVENDING_H
#include			"basics/tinifile.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa�t alle ben�tigten Informationen f�r Z�hlinformationen
		von Artikeln.
		Die Z�hlinformation zu einem Artikel wird in einer Ini-Datei gehalten, um mehreren
		Terminals den Zugriff zu erm�glichen.
		\brief POS-Klassen: Artikel, Z�hlinformationen.
	*/
	class			TArtVending
	{
		static const char	sectSettings[];
		static const char	sectArt[];
//		static const char	entryActive[];
//		static const char	entryQuant[];
//		static const char	entryZero[];
//		static const char	entryIncr[];
		static const char	pathName[];
		static const char	fileName[];
	public:
		TArtVending(int waiter, const char* path)
		: m_Waiter(waiter)
		, m_Path(path)
		{
		}
		~TArtVending()
		{
		}
		static bool	delQuantities(const char* path);
//		bool		isActive(int art) const;
//		void		delQuantity(int art);
//		void		zeroQuantity(int art);
//		void		setQuantity(int art, long count);
		double		getQuantity(int art) const;
		double		bookQuantity(int art, double count);
//		void		setData(int art, long count, long zero, bool incr);
//		void		getData(int art, long& count, long& zero, bool& incr);
//		void		setZero(int art, long zero);
//		void		setIncrease(int art, bool incr);
		QStringList	getExisting();
	protected:
		QString		getFileName() const;
		int			m_Waiter;
		QString		m_Path;
	};
}

using namespace PosLib;

#endif


