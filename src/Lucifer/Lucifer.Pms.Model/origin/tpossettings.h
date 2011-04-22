#ifndef				POSLIB_TPOSSETTINGS_H
#define				POSLIB_TPOSSETTINGS_H
#include			"basics/tdir.h"

namespace PosLib
{
	class			TPosSettings
	{
	public:
		static const char	pathEtc[];
		static const char	fileConfig[];
		static const char	sectPos[];
		static const char	entryTipBase[];
		static const char	entryTipMax[];
		static const char	entryOrderGuestCount[];
	public:
		TPosSettings(const char* path="");
		void		load(const char* path="");
		void		save(const char* path="");
	public:
		long		getTipBase() const
		{
			return m_TipBase;
		}
		int			getTipMax() const
		{
			return m_TipMax;
		}
	protected:
		long		m_TipBase;				//!< Sockelbetrag, der immer buchbar ist
		int			m_TipMax;				//!< Maximale Prozentzahl des Tischbetrages
		bool		m_OrderGuestCount;		// true: Anzahl der Gäste beim Bestellen der ersten
											//		order erfragen
											// false: Anzahl der Gäste erst beim Bezahlen erfragen
	};
}

using namespace PosLib;

#endif


