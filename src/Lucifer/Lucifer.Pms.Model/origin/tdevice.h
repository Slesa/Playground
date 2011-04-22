#ifndef				_POSLIB_DEVICE_

#define				_POSLIB_DEVICE_
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	class			TDevice
	: public TValue
	{
	public:
		static const char	entryID[];
		static const char	entryType[];
		static const char	entryClass[];
	public:
		TDevice()
		: TValue()
		, m_Active(false)
		{
		}
		QString		getDevice() const
		{
			return m_Device;
		}
		void		setDevice(const QString& device)
		{
			m_Device = device;
		}
		QString		getType() const
		{
			return m_Type;
		}
		void		setType(const QString& type)
		{
			m_Type = type;
		}
		QString		getClass() const
		{
			return m_Class;
		}
		void		setClass(const QString& cl)
		{
			m_Class = cl;
		}
		bool		isActive() const
		{
			return m_Active;
		}
		void		setActive(bool flag)
		{
			m_Active = flag;
		}
	protected:
		QString		m_Device;
		QString		m_Class;
		QString		m_Type;
		bool		m_Active;
	};

	class			TDeviceList
	: public TValueList
	{
		static const char	sectDevices[];
		static const char	sectLog[];
		static const char	entryDevice[];
		static const char	entryLogLevel[];
		static const char	entryLogFile[];
	public:
		TDeviceList(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*
		TDeviceList(const TDeviceList& list)
		: TValueList(list)
		{
		}
		*/
		virtual int		load(const char* file);
		virtual void	save(const char* file);
	protected:
		int			m_LogLevel;
		QString		m_LogFile;
	};

	class			TDeviceIt
	: public TValueListIt
	{
	public:
		TDeviceIt(TDeviceList& list)
		: TValueListIt(list)
		{
		}
		TDevice*	operator () ()
		{
			return (TDevice*) TValueListIt::operator()();
		}
		TDevice*	toFirst()
		{
			return (TDevice*) TValueListIt::toFirst();
		}
		TDevice*	current()
		{
			return (TDevice*) TValueListIt::current();
		}
		TDevice*	operator ++ ()
		{
			return (TDevice*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif

