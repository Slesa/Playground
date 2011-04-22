#ifndef				POSLIB_TDOTNET_H
#define				POSLIB_TDOTNET_H
#include			"basics/tinifile.h"
#include			"basics/tfile.h"
#include			"basics/tdir.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		\brief POS-Klassen: DotNet-Einstellungen.
	*/
	class			TDotNet
	: public TInifile
	{
		static const char	fileName[];
		static const char	pathName[];
		static const char	sectDotNet[];
		static const char	entryActive[];
		static const char	entryServer[];
		static const char	entryPort[];
		static const char	entryOutlet[];
		static const char	entryPass[];
	public:
		TDotNet(const QString& name);
		bool		isDotActive()
		{
			return getValue(sectDotNet, entryActive);
		}
		void		setDotActive(bool flag)
		{
			setValue(flag, sectDotNet, entryActive);
		}
		QString		getDotServer()
		{
			return getString(sectDotNet, entryServer);
		}
		void		setDotServer(const QString& server)
		{
			if( server.isEmpty() )
				clrValue(sectDotNet, entryServer);
			else
				setValue(server, sectDotNet, entryServer);
		}
		int			getDotPort()
		{
			return getValue(sectDotNet, entryPort, 1607);
		}
		void		setDotPort(int port)
		{
			if( port==1607 )
				clrValue(sectDotNet, entryPort);
			else
				setValue(port, sectDotNet, entryPort);
		}
		int			getDotOutlet()
		{
			return getValue(sectDotNet, entryOutlet);
		}
		void		setDotOutlet(int out)
		{
			if( !out )
				clrValue(sectDotNet, entryOutlet);
			else
				setValue(out, sectDotNet, entryOutlet);
		}
		QString		getDotPass()
		{
			return getString(sectDotNet, entryPass);
		}
		void		setDotPass(const QString& pwd)
		{
			if( pwd.isEmpty() )
				clrValue(sectDotNet, entryPass);
			else
				setValue(pwd, sectDotNet, entryPass);
		}
	};
}

using namespace PosLib;

#endif


