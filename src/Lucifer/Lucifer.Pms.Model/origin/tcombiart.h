#ifndef				POSLIB_TCOMBIART_H
#define				POSLIB_TCOMBIART_H
#include			"basics/tvalue.h"
#include			"qstringlist.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse erleichtert den Zugriff auf die einzelnen Kombiartikel-
		Elemente. Im wesentlichen wird ein String in seine Elemente zerlegt
		und der Zugriff auf die darin enthaltenen Werte vereinfacht.
		\warning Die Zusammensetzung des Strings mu�der Definition in
		TCombiArt::getCombis entsprechen.
		\brief POS-Klassen: Kombiartikel-Eintr�e.
	*/
	class			TCombiEntry
	{
	public:
		/*!	Erzeuge eine leere Instanz eines Kombiartikel-Eintrags.
			\brief ctor.
		*/
		TCombiEntry()
		{
		}
		/*!	Erzeuge eine Instanz eines Kombiartikel-Eintrags mit den Werten aus str.
			\param str	Der String eines Iterator-Schrittes der Stringliste von TCombiArt::getCombis()
			\brief ctor.
		*/
		TCombiEntry(const QString& str)
		{
			m_Entries = QStringList::split(":", str, TRUE);
		}
		int			getPlu() const
		{
			return m_Entries[0].toInt();
		}
		long		getPrice() const
		{
			return (long) m_Entries[1].toInt();
		}
		int			getCostcenter() const
		{
			if( m_Entries.count()<3 )
				return 0;
			return m_Entries[2].toInt();
		}
		QString		setValues(int plu, long price, int center)
		{
			m_Entries.clear();
			m_Entries << QString::number(plu) << QString::number(price) << QString::number(center);
			return m_Entries.join(":");
		}
	protected:
		QStringList	m_Entries;
	};

	/*!	\ingroup PosLib
		Diese Klasse umfa� alle ben�igten Informationen fr Kombiartikel.
		Rein technisch gesehen geh�en Kombiartikel zu den Artikeln selbst,
		der �ersichtlichkeit halber wurden sie aber ausgelagert.
		Alle verfgbaren Kombiartikel werden in einer Instanz von TCombiArts
		zur Verfgung gestellt.
		\warning Die ID des Kombiartikels mu�der des zugeh�igen Artikels entsprechen.
		\brief POS-Klassen: Kombiartikel.
	*/
	class			TCombiArt
	: public TValue
	{
		static const char	entryCombis[];
		static const char	entryMainVat[];
	public:
		/*!	Erzeuge eine leere Instanz eines Kombiartikels.
			\brief ctor.
		*/
		TCombiArt()
		: TValue()
		{
		}
		/*!	\return Liefert die Liste der Kombiartikel. Die Elemente der Liste
			selbst sind durch Semikolon getrennt, die einzelnen Werte durch
			Doppelpunkt.
			\brief Kombiartikel ermitteln.
		*/
		QStringList	getCombis() const
		{
			return QStringList::split(";", getString(entryCombis), TRUE);
		}
		/*!	�dert die Liste der Kombiartikel. Die einzelnen Werte eines jeden
			Artikels mssen durch Doppelpunkt getrennt sein. Die Liste selbst
			wird in einem String durch Semikolon getrennt abgespeichert.
			\param combis	Die neue Liste der Kombiartikel.
			\brief Kombiartikel �dern.
		*/
		void		setCombis(const QStringList& combis)
		{
			if( !combis.count() )
				clrValue(entryCombis);
			else
				setValue(entryCombis, combis.join(";"));
		}
		/*!	\return Liefert TRUE, wenn die Mehrwertsteuer vom Hauptartikel
			bernommen werden soll, FALSE, wenn die vom jeweiligen Kombiartikel
			genommen werden soll.
			\brief Mwst-Art ermitteln.
		*/
		bool		isMainVat() const
		{
			return getValue(entryMainVat, TRUE);
		}
		/*!	�dert das Flag, ob die Mehrwertsteuer vom Hauptartikel bernommen
			werden soll.
			\param flag		Wenn TRUE, die Mwst vom Hauptartikel nehmen, sonst
							die vom Kombiartikel.
			\brief Mwst-Art �dern.
		*/
		void		setMainVat(bool flag)
		{
			if( flag )
				clrValue(entryMainVat);
			else
				setValue(entryMainVat, flag);
		}
	};

	class			TCombiArts
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];
	public:
		static const char	listName[];
		static const char	elementName[];
	public:
		TCombiArts(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		~TCombiArts()
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
		TCombiArt*	operator [] (int index)
		{
			return (TCombiArt*) TValueList::operator [](index);
		}
	};

	class			TCombiArtIt
	: public TValueListIt
	{
	public:
		TCombiArtIt(TCombiArts& list)
		: TValueListIt(list)
		{
		}
		TCombiArt*	operator () ()
		{
			return (TCombiArt*) TValueListIt::operator()();
		}
		TCombiArt*	toFirst()
		{
			return (TCombiArt*) TValueListIt::toFirst();
		}
		TCombiArt*	current()
		{
			return (TCombiArt*) TValueListIt::current();
		}
		TCombiArt*	operator ++ ()
		{
			return (TCombiArt*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif


