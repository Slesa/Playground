#ifndef				_POSLIB_TTIMECONTROL_

#define				_POSLIB_TTIMECONTROL_
#include			"basics/tvalue.h"
#include			"basics/tdir.h"
#include			"basics/tfile.h"

namespace PosLib
{
	/*!
		\brief POS-Klassen: Zeiterfassung.
	*/
	class			TTimeControl
	: public TValue
	{
		static const char	entryTime[];
//		static const char	entryDate[];
	public:
		TTimeControl()
		: TValue()
		{
		}
/*
		QDate		getDate() const
		{
			return TValue::getDate(entryDate);
		}
		void		setDate(const QDate& date)
		{
			setValue(entryDate, date);
		}
*/
		QStringList	getTimes() const
		{
			return QStringList::split(";", TValue::getValue(entryTime));
		}
		void		setTimes(const QString& time)
		{
			setValue(entryTime, time);
		}
		void		addTime(const QTime& time, bool out)
		{
			QString tmp = getValue(entryTime);
			if( out )
				tmp += "#";
			else
			if( !tmp.isEmpty() )
				tmp += ";";
			setValue(entryTime, tmp+time.toString());
		}
	};

	class			TTimeControls
	: public TValueList
	{
		Q_OBJECT
		static const char	pathName[];
	public:
		TTimeControls(const QDate& date, bool autodel=TRUE)
		: TValueList(autodel)
		, m_Date(date)
		{
		}
		TTimeControls(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*
		TPriceList(const TPriceList& list)
		: TValueList(list)
		, m_Article(list.m_Article)
		{
		}
		*/
		TTimeControl*	operator [] (int index)
		{
			return (TTimeControl*) TValueList::operator [](index);
		}
		virtual int		load(const char* path=NULL);
		virtual void	save(const char* path=NULL)
		{
			if( count()==0 )
			{
				if( exists(path) )
					TFile::remove(TDir::checkPath(TDir::checkPath(path)+pathName)+makeFilename());
				return;
			}
			TValueList::save(makeFilename(), TDir::checkPath(TDir::checkPath(path)+QString(pathName)));
		}
		bool			exists(const char* path="")
		{
			QString file = makeFilename();
			return QFile::exists(TDir::checkPath(path+QString(pathName))+file);
		}
		static bool		exists(const QDate& date, const char* path="")
		{
			QString file = makeFilename(date);
			return QFile::exists(TDir::checkPath(path+QString(pathName))+file);
		}
	/*
		virtual void	load(const QString& file=fileName, const char* path=NULL);
		virtual void	save(const QString& file=fileName, const char* path=NULL);
	private:
		void		import(const char* path);
		void		saveCompat(const char* path);
	*/
		QDate		getDate()
		{
			return m_Date;
		}
	protected:
		QString		makeFilename()
		{
		#if QT_VERSION>=300
			return m_Date.toString("yyyyMMdd");
		#else
			QString tmp;
			tmp.sprintf("%04d%02d%02d", m_Date.year(), m_Date.month(), m_Date.day());
			return tmp;
		#endif
		}
		static QString	makeFilename(const QDate& date)
		{
		#if QT_VERSION>=300
			return date.toString("yyyyMMdd");
		#else
			QString tmp;
			tmp.sprintf("%04d%02d%02d", date.year(), date.month(), date.day());
			return tmp;
		#endif
		}
	protected:
		QDate		m_Date;
		friend class TTimeControlList;
	};

	class			TTimeControlIt
	: public TValueListIt
	{
	public:
		TTimeControlIt(TTimeControls& list)
		: TValueListIt(list)
		{
		}
		TTimeControl*	operator () ()
		{
			return (TTimeControl*) TValueListIt::operator()();
		}
		TTimeControl*	toFirst()
		{
			return (TTimeControl*) TValueListIt::toFirst();
		}
		TTimeControl*	current()
		{
			return (TTimeControl*) TValueListIt::current();
		}
		TTimeControl*	operator ++ ()
		{
			return (TTimeControl*) TValueListIt:: operator ++();
		}
	};

	class			TTimeControlList
	: public QIntDict<TTimeControls>
	{
	public:
		TTimeControlList();
		int			load(const QDate& from, const QDate& to, const char* path="");
	};
/*
	class			TTimeControlsIt
	: public QIntDictIterator<TTimeControls>
	{
	public:
		TTimeControlsIt(TTimeControlList& list)
		: QIntDictIterator<TTimeControls>(list)
		{
		}
	};
*/
}

using namespace PosLib;

#endif


