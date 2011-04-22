#ifndef				_POSLIB_DRIVER_

#define				_POSLIB_DRIVER_
#include			"poslib/tdevice.h"
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	class			TDriver
	: public TValue
	{
	public:
		TDriver()
		: TValue()
		, m_Active(false)
		{
		}
		void		load(const char* path="")
		{
		debug("load device "+ TDir::checkPath(path)+m_Config);
			m_Devices.load(TDir::checkPath(path)+m_Config);
		}
		void		save(const char* path="")
		{
			m_Devices.save(TDir::checkPath(path)+m_Config);
		}
		QString		getDriverName() const
		{
			return m_Driver;
		}
		void		setDriverName(const QString& name)
		{
			m_Driver = name;
		}
		QString		getDriver() const
		{
			if( m_Active )
				return m_Driver;
			return "; " + m_Driver;
		}
		void		setDriver(const QString& driver)
		{
			if( driver.left(1)==";" )
			{
				m_Active = false;
				m_Driver = driver.mid(1).stripWhiteSpace();
			}
			else
			{
				m_Driver = driver;
				m_Active = true;
			}
		}
		QString		getConfig() const
		{
			return m_Config;
		}
		void		setConfig(const QString& config)
		{
			m_Config = config;
		}
		bool		isActive() const
		{
			return m_Active;
		}
		void		setActive(bool flag)
		{
			m_Active = flag;
		}
		TDeviceList&	getDevices()
		{
			return m_Devices;
		}
		void		insert(TDevice* dev)
		{
			m_Devices.insert(dev);
		}
	protected:
		QString		m_Driver;
		QString		m_Config;
		bool		m_Active;
		TDeviceList	m_Devices;
	};

	class			TDriverList
	: public TValueList
	{
		static const char	fileKernel[];
		static const char	fileSystem[];
		static const char	sectDevices[];
	public:
		static const char	pathConfig[];
	public:
		TDriverList(bool kernel, bool autodel=TRUE)
		: TValueList(autodel)
		, m_Kernel(kernel)
		{
		}
		/*
		TDriverList(const TDriverList& list, bool autodel=false)
		: TGf5List(list, autodel)
		, m_Kernel(list.m_Kernel)
		{
		}
		*/
		QString		getFile(const char* path="")
		{
			if( m_Kernel )
				return TDir::checkPath(path+QString(pathConfig)) + fileKernel;
			return TDir::checkPath(path+QString(pathConfig)) + fileSystem;
		}
		virtual int		load(const char* path="");
		virtual void	save(const char* path="");
	protected:
		bool		m_Kernel;
	};

	class			TDriverIt
	: public TValueListIt
	{
	public:
		TDriverIt(TDriverList& list)
		: TValueListIt(list)
		{
		}
		TDriver*	operator () ()
		{
			return (TDriver*) TValueListIt::operator()();
		}
		TDriver*	toFirst()
		{
			return (TDriver*) TValueListIt::toFirst();
		}
		TDriver*	current()
		{
			return (TDriver*) TValueListIt::current();
		}
		TDriver*	operator ++ ()
		{
			return (TDriver*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif

