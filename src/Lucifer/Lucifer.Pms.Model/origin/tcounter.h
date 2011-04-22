#ifndef				POSLIB_COUNTER_H
#define				POSLIB_COUNTER_H
#include			"basics/tdir.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	class			TCounter
	: public TValue
	{
		static const char	defaultFile[];
	public:
		static const char	fileZCount[];
	public:
		TCounter(const char* path="", const char* file=defaultFile);
		long		getCounter(const char* idx);
		void		setCounter(const char* idx, long c);
		long		operator[] (const char* idx);
	protected:
		QString		m_Path;
		QString		m_File;
	};
}

using namespace PosLib;

#endif


