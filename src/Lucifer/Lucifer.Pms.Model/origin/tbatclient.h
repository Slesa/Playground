#ifndef				_POSLIB_TBATCLIENT_
#define				_POSLIB_TBATCLIENT_
#include			"basics/tvalue.h"

namespace			PosLib
{
	class			TBatClient
	: public TValue
	{
	public:
		static const char	entryFirstName[];
		static const char	entryLastName[];
		static const char	entryCompany[];
		static const char	entryPhone[];
		static const char	entryPhoneBusiness[];
		static const char	entryPhoneMobile[];
		static const char	entryEMail[];
	public:
		TBatClient()
		: TValue()
		{
		}
		QString		getFirstName() const
		{
			return getString(entryFirstName);
		}
		QString		getLastName() const
		{
			return getString(entryLastName);
		}
		QString		getCompany() const
		{
			return getString(entryCompany);
		}
		QString		getPhone() const
		{
			return getString(entryPhone);
		}
		QString		getPhoneBusiness() const
		{
			return getString(entryPhoneBusiness);
		}
		QString		getPhoneMobile() const
		{
			return getString(entryPhoneMobile);
		}
		QString		getEMail() const
		{
			return getString(entryEMail);
		}
	};
	
	class			TBatClients
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TBatClients(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TBatClients()
		{
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		virtual QString	getFileName() const
		{
			return fileName;
		}
		virtual QString	getElementName()
		{
			return elementName;
		}
		TBatClient*	operator [] (int index)
		{
			return (TBatClient*) TValueList::operator [](index);
		}
	};

	class			TBatClientIt
	: public TValueListIt
	{
	public:
		TBatClientIt(TBatClients& list)
		: TValueListIt(list)
		{
		}
		TBatClient*	operator () ()
		{
			return (TBatClient*) TValueListIt::operator()();
		}
		TBatClient*	toFirst()
		{
			return (TBatClient*) TValueListIt::toFirst();
		}
		TBatClient*	current()
		{
			return (TBatClient*) TValueListIt::current();
		}
		TBatClient*	operator ++ ()
		{
			return (TBatClient*) TValueListIt:: operator ++();
		}
	};
}

#endif

