#ifndef				POSLIB_PASSWORD_H
#define				POSLIB_PASSWORD_H
#include			"basics/tdir.h"
#include			"basics/tvalue.h"

namespace PosLib
{
	class			TPasswords
	: public TValue
	{
		static const char	defKey[];
		static const char	defaultFile[];
		static const char	defSupport[];
		static const char	entrySupport[];						// Supportmode Postouch
		static const char	defMaster[];
		static const char	entryMaster[];						// Office-Abfrage
		static const char	defBackdoor[];
		static const char	entryBackdoor[];					// Supportmode Office
	public:
		static const char	pw42[];
		static const char	pw43[];
		static const char	pwsharp[];
	public:
		TPasswords(const char* path="")
		{
			load(path);
		}
		void		save(const char*);
		QString		getSupport()
		{
			return getPw(entrySupport, defSupport);
		}
		void		setSupport(const QString& pw)
		{
			setPw(entrySupport, pw);
		}
		QString		getMaster()
		{
			return getPw(entryMaster, defMaster);
		}
		void		setMaster(const QString& pw)
		{
			setPw(entryMaster, pw);
		}
		QString		getBackdoor()
		{
			return getPw(entryBackdoor, defBackdoor);
		}
		void		setBackdoor(const QString& pw)
		{
			setPw(entryBackdoor, pw);
		}
		bool		checkPw(const QString& pw, const QString& check);
	protected:
		QString		getPw(const char* attr, const QString& def);
		void		setPw(const char* attr, const QString& pw);
		void		load(const char*);
	};
}

using namespace PosLib;

#endif



