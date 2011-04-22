#ifndef				POSLIB_TSUMBATCH_H
#define				POSLIB_TSUMBATCH_H
#include			"basics/tvalue.h"
#include			"basics/tdir.h"

namespace PosLib
{
	class			TSumBatch
	: public TNValue
	{
	public:
		static const char	entryDescription[];
		static const char	entryFilter[];
		static const char	entryFiscal[];
		static const char	entryReports[];
		static const char	entryKey[];
		static const char	entryDesc[];
	public:
		TSumBatch()
		: TNValue()
		{
		}
		~TSumBatch()
		{
		}
		/*!	\return die Beschreibung dieses Auswertungsbatches.
			\brief Beschreibung des Batches abfragen.
			\sa setDescription
		*/
		QString		getDescription() const
		{
			return getString(entryDescription);
		}
		/*!	�ndert die Beschreibung dieses Auswertungsbatches.
			\param descr	Die neue Beschreibung
			\brief Beschreibung des Batches �ndern.
			\sa getDescription
		*/
		void		setDescription(const QString& descr)
		{
			if( descr.isEmpty() )
				clrValue(entryDescription);
			else
				setValue(entryDescription, descr);
		}
		/*!	\return den Filter dieses Auswertungsbatches.
			\brief Filter des Batches abfragen.
			\sa setFilter
		*/
		int			getFilter() const
		{
			return getValue(entryFilter, 0);
		}
		/*!	�ndert den Filter dieses Auswertungsbatches.
			\param filter	Der neue Filter
			\brief Filter des Batches �ndern.
			\sa getFilter
		*/
		void		setFilter(int filter)
		{
			if( !filter )
				clrValue(entryFilter);
			else
				setValue(entryFilter, filter);
		}
		/*!	\return Liefert TRUE, wenn dieser Batchreport zur Fiskalisierung
			dieses Statists f�hrt.
			\brief Fiskal-Batch?
		*/
		bool		isFiscal() const
		{
			return getValue(entryFiscal, FALSE);
		}
		/*!	�ndert das Flag, dass dieser Batchreport zur Fiskalisierung des
			Statist-Ordners f�hrt.
			\param flag		TRUE bedeutet fiskalisieren
			\brief Flag Fiskal-Batch �ndern
		*/
		void		setFiscal(bool flag)
		{
			if( !flag )
				clrValue(entryFiscal);
			else
				setValue(entryFiscal, flag);
		}
		QString		getReport() const
		{
			return getString(entryReports);
		}
		void		setReport(const QString& rep)
		{
			if( rep.isEmpty() )
				clrValue(entryReports);
			else
				setValue(entryReports, rep);
		}
		QString		getKey() const
		{
			return getString(entryKey);
		}
		void		setKey(const QString& key)
		{
			if( key.isEmpty() )
				clrValue(entryKey);
			else
				setValue(entryKey, key);
		}
		bool		isDescending() const
		{
			return getValue(entryDesc, FALSE);
		}
		void		setDescending(bool flag)
		{
			if( !flag )
				clrValue(entryDesc);
			else
				setValue(entryDesc, flag);
		}
		QStringList	getReports() const
		{
			return QStringList::split(";", getReport());
		}
		void		setReports(const QStringList& reps)
		{
			setReport(reps.join(";"));
		}
	};

	class			TSumBatches
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TSumBatches(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TSumBatches()
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
		TSumBatch*	operator [] (int index)
		{
			return (TSumBatch*) TValueList::operator [](index);
		}
	};

	class			TSumBatchIt
	: public TValueListIt
	{
	public:
		TSumBatchIt(TSumBatches& list)
		: TValueListIt(list)
		{
		}
		TSumBatch*	operator () ()
		{
			return (TSumBatch*) TValueListIt::operator()();
		}
		TSumBatch*	toFirst()
		{
			return (TSumBatch*) TValueListIt::toFirst();
		}
		TSumBatch*	current()
		{
			return (TSumBatch*) TValueListIt::current();
		}
		TSumBatch*	operator ++ ()
		{
			return (TSumBatch*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif
