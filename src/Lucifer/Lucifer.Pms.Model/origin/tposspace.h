#ifndef				POSLIB_TPOSSPACE_H
#define				POSLIB_TPOSSPACE_H
#include			"basics/tworkspace.h"

namespace PosLib
{
	class			TPosSpace
	: public  TWorkspace
	{
		Q_OBJECT
		static const char	spaceName[];
	public:
		TPosSpace()
		: TWorkspace(spaceName)
		{
		}
		TPosSpace(const char* name)
		: TWorkspace(name)
		{
		}
		virtual void		registerAll();
	};
}

using namespace PosLib;

#endif
