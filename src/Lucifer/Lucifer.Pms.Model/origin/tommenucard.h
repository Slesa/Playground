#ifndef			POSLIB_TOMMENUCARD_H
#define			POSLIB_TOMMENUCARD_H
#include		"basics/tvalue.h"
#include		"qfont.h"

namespace PosLib
{
	/*!	\ingroup PosLib
	Diese Klasse umfasst alle benoetigten Informationen fuer eine DON/MAX Menucard.
	\brief POS Klasse: Orderman Menucards
	*/
	class		TOMMenuCard
	: public TNValue
	{
	public:
		static const char	entryTemplateFile[];
		static const char	entryCardColor[];
		static const char	entryCardBitmap[];
		static const char	entryScanCodes[];
		static const char	entryGeometries[];
		static const char	entryColors[];
		static const char	entryBitmaps[];
		static const char	entryTexts[];
		static const char	entryFonts[];
		static const char	entryFrames[];
		static const char	entryTextColors[];
		static const char	entryAlignments[];
	public:
		/*!	\ingroup Poslib
		The following table shows the specialfunction in a orderman menucard layout.
		\brief Special Orderman Menucardfunctions
		*/
		enum Functions
		{
			FamiliesList		= 1			//!< Zeigt alle Warengruppen an
			,FamgroupsList		= 2			//!< Zeigt alle Oberwarengruppen an
			,DepartmentsList	= 3			//!< Zeigt alle Sparten an
			,ControlsList		= 4			//!< Zeigt die gangsteuerung an
		};
		/*!	\ingroup Poslib
		The following table shows types of a orderman menucard button
		\brief Orderman Menucard Buttontypes
		*/
		enum ButtonTypes
		{
			Unknown			= 0
		,	Article			= 1
		,	Family			= 2
		,	Famgroup		= 3
		,	Department		= 4
		,	Function		= 5
		,	Bitmap			= 6
		,	SearchArticle	= 7
		,	Control			= 8
		};
	public:
		/*!	Erzeugt eine leere Instanz einer Menucard.
			\brief ctor
		*/
			TOMMenuCard()
			: TNValue()
			{
			}
		/*!	Ermittelt die Funktion und deren Daten eines Buttons einer Menukarte.
		Die Funktion liefert FALSE wenn kein button gefunden wurde, keine Funktion in der
		Menukarte hinterlegt wurde oder keine Menukarte gefunden wurde.
		\return	TRUE	Kein Fehler FALSE Fehler Den Funktionstyp als int in der �bergebenen
						Funktionsvariablen. Die Daten als int in der �bergebenen Daten
						variablen.
		\param	scancode		Der Scancodes des buttons
		\param	function		Der funktionscode des buttons
		\param	data			Die Daten der Funktion des Buttons
		\brief Button Funktion und deren Daten ermittlen
		*/
		bool				getButtonData(int scancode,int& function,int& data);
		/*!	\return Den Namen der Template Datei (.bly) aus dem Orderman Menucard Designer.
			\brief Namen der Template Datei ermitteln
			\sa setTemplateFile
		*/
		QString			getTemplateFile() const
		{
			return getValue(entryTemplateFile,"");
		}
		/*!	Aendert den Namen der Template Datei.
			\param temp		Der neue Dateiname
			\brief Namen der Template Datei aendern
			\sa getTemplateFile
		*/
		void			setTemplateFile(const QString& temp)
		{
			setValue(entryTemplateFile,temp);
		}
		/*!	\return Die hintergrundfarbe der Menukarte im long format.
			\brief Hintergrundfarbe ermitteln
			\sa setCardColor
		*/
		long			getCardColor() const
		{
			return getValue(entryCardColor,0);
		}
		/*!	Aendert die hintergrundfarbe
			\param c	Die neue hintergrundfarbe als long wert.
			\brief Hintergrundfarbe der Menukarte aendern
			\sa getCardColor
		*/
		void			setCardColor(long c)
		{
			setValue(entryCardColor,c);
		}
		/*!	\return Den Namen der Hintergrundbitmap Datei
			\brief Namen der bitmapdatei ermitteln
			\sa setCardBitmap
		*/
		QString			getCardBitmap() const
		{
			return getValue(entryCardBitmap,"");
		}
		/*!	Aendert den Namen der Hintergrundbitmapdatei
			\param temp		Der neue Dateiname
			\brief Namen der bitmap Datei aendern
			\sa getCardBitmap
		*/
		void			setCardBitmap(const QString& s)
		{
			if(s.isEmpty())
				clrValue(entryCardBitmap);
			setValue(entryCardBitmap,s);
		}
		/*!	\return Die Checksumme der scancodes
			\brief Checksumme der scancodes ermitteln
		*/
		int				getCheckSum();
		/*!	\return Die scancodes,functions und data der buttons als stringliste als
			Datentriple mit ';' getrennt. Bsp: 1;1,3-2;1;2-... Scancode 1,Artikeltaste, PLU = 3
			Scancode 2,Artikeltaste, PLU = 2
			\brief Scancodes, Funcktion und data ermitteln
			\sa setScanCodes
		*/
		QStringList		getScanCodes() const
		{
			return QStringList::split(";",getValue(entryScanCodes));
		}
		/*!	Fuellt die scancodeliste mit den scancodes,functionen und data der angelegten buttons. Es werden
			immer Scancode Funktion und data zusammengefasst und mit einem '-' getrennt. Diese Pakete werden
			untereinander mit einem ';' getrennt.
			\param codes		Die scancodes als stringliste im format <scancode>-<funktion>-<data>;<scancode...
			\brief Scancodes, Funktion und data setzen
			\sa getScanCodes
		*/
		void			setScanCodes(const QStringList& codes)
		{
			setValue(entryScanCodes,codes.join(";"));
		}
		/*!	Baut eine Buttondefinition eines Scancodes zusammen und liefert diese als String mit '-' getrennt zur�ck-
			\return scancodedata	Die Daten eines Buttons im format <scancode>-<funktion>-<data>
			\param scancode		Der scancodes des Buttons
			\param function		Die Fucnktion des Buttons (artikel wg, department ...)
			\param data			Das Datum des Buttons PLUnr WgNr Funcktions Nr...
			\brief Scancodes, Funktion und data eines Buttons setzen
			\sa getScanCodes setScanCodes getScanCode getFunction getData
		*/
		QString			buildScanCode(int scancode,int function,int data)
		{
			return QString("%1-%2-%3").arg(scancode).arg(function).arg(data);
		}
		/*!	Liefert den ScanCode eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <scancode>-<funktion>-<data> besitzen.
			\return scancode		Der scancodes des Buttons
			\param button		Der datenstring eines Buttons
			\brief Scancode eines Buttons ermittlen
			\sa getScanCodes setScanCodes getScanCode getFunction getData
		*/
		int				getScanCode(const QString& button)
		{
			return button.section("-",0,0).toInt();
		}
		/*!	Liefert die Function eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <scancode>-<funktion>-<data> besitzen.
			\return fucntion		Die Function des Buttons
			\param button		Der datenstring eines Buttons
			\brief Function eines Buttons ermittlen
			\sa getScanCodes setScanCodes getScanCode getFunction getData
		*/
		int				getFunction(const QString& button)
		{
			return button.section("-",1,1).toInt();
		}
		/*!	Liefert das Datum eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <scancode>-<funktion>-<data> besitzen.
			\return data			Das Datum des Buttons
			\param button		Der datenstring eines Buttons
			\brief Datum eines Buttons ermittlen
			\sa getScanCodes setScanCodes getScanCode getFunction getData
		*/
		int				getData(const QString& button)
		{
			return button.section("-",2,2).toInt();
		}
		/*!	\return Die geometrie daten der buttons in einer stringliste ermitteln.
			Es stehen immer 4 werte fuer einen Button direkt in der liste hintereinander.
			<Links>-<Oben>-<breite>-<h�he> diese Daten sind vom n�chsten Datum
			durch ein ';' getrennt
			\brief Geometriedaten aller buttons ermitteln
			\sa setGeometries buildGeometry
		*/
		QStringList		getGeometries() const
		{
			return QStringList::split(";",getValue(entryGeometries));
		}
		/*!	Fuellt die geometrieliste mit den koordinaten der angelegten buttons.
			\param geo		Die Geometriedaten als string mit semicolon getrennt.
			Es werden immer StartX;StartY;Breite;Hoehe fuer einen Button gepeichert.
			\brief Geometriedaten setzen
			\sa getGeometries buildGeometry
		*/
		void			setGeometries(const QStringList& geo)
		{
			setValue(entryGeometries,geo.join(";"));
		}
		/*!	Baut die Geometrie Information eines Buttons zusammen und liefert diese als String mit '-' getrennt zur�ck-
			\return geometriedata	Die geometrieDaten eines Buttons im format <links>-<oben>-<breite>-<h�he>
			\param r			QRect eines Buttons
			\brief Geometrie eines Buttons setzen
			\sa getGeometries setGeometries
		*/
		QString		buildGeometry(const QRect& r)
		{
			return QString("%1-%2-%3-%4").arg(r.left()).arg(r.top()).arg(r.width()).arg(r.height());
		}
		/*!	Liefert die X Position (Links) eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <links>-<oben>-<breite>-<h�he> besitzen.
			\return xpos			Die XPosition des Buttons
			\param button		Der geometriestring eines Buttons
			\brief XPossition eines Buttons ermittlen
			\sa setGeometries getGeometries buildGemometrie getTop getWidth getHeight
		*/
		int			getLeft(const QString& button)
		{
			return button.section("-",0,0).toInt();
		}
		/*!	Liefert die Y Position (Oben) eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <links>-<oben>-<breite>-<h�he> besitzen.
			\return ypos			Die YPosition des Buttons
			\param button		Der geometriestring eines Buttons
			\brief YPosition eines Buttons ermittlen
			\sa setGeometries getGeometries buildGemometrie getLeft getWidth getHeight
		*/
		int			getTop(const QString& button)
		{
			return button.section("-",1,1).toInt();
		}
		/*!	Liefert die Breite eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <links>-<oben>-<breite>-<h�he> besitzen.
			\return breite			Die Breite des Buttons
			\param button		Der geometriestring eines Buttons
			\brief Breite eines Buttons ermittlen
			\sa setGeometries getGeometries buildGemometrie getTop getLeft getHeight
		*/
		int			getWidth(const QString& button)
		{
			return button.section("-",2,2).toInt();
		}
		/*!	Liefert die H�he eines Buttons als integer zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <links>-<oben>-<breite>-<h�he> besitzen.
			\return h�he			Die H�he des Buttons
			\param button		Der geometriestring eines Buttons
			\brief H�he eines Buttons ermittlen
			\sa setGeometries getGeometries buildGemometrie getTop getWidth getLeft
		*/
		int			getHeight(const QString& button)
		{
			return button.section("-",3,3).toInt();
		}
		/*!	\return Die RGB Werte der buttons in einer stringliste ermitteln.
			Es steht 1 long wert fuer den rgb wert. Er muss mittels QRgb in einen QColor
			gerechnet werden
			\brief RGB Werte aller buttons ermitteln
			\sa setColors
		*/
		QStringList		getColors() const
		{
			return QStringList::split(";",getValue(entryColors));
		}
		/*!	Fuellt die colorsliste mit den RGB Werten der angelegten buttons.
			\param colors		Die RGB Werte im uint format als string mit semicolon getrennt.
			Es werden immer RGB als uint gesichert.
			\brief RGB Werte setzen
			\sa getColors
		*/
		void			setColors(const QStringList& colors)
		{
			setValue(entryColors,colors.join(";"));
		}
		/*!	\return Die Bitmapdateinamen der buttons in einer stringliste ermitteln.
			Hat ein Button keine bitmap, ist der string in der liste leer.
			\brief Bitmapdateinamen aller buttons ermitteln
			\sa setBitmaps
		*/
		QStringList		getBitmaps() const
		{
			return QStringList::split(";",getValue(entryBitmaps),TRUE);
		}
		/*!	Fuellt die bitmapliste mit den Dateinamen der Bitmaps der angelegten buttons.
			\param bitmaps		Die Dateinamen als string mit semicolon getrennt.
			\brief Bitmapdateinamen setzen
			\sa getBitmaps
		*/
		void			setBitmaps(const QStringList& bitmaps)
		{
			setValue(entryBitmaps,bitmaps.join(";"));
		}
		/*!	\return Die Beschriftungen der buttons in einer stringliste ermitteln.
			\brief Beschriftung aller buttons ermitteln
			\sa setTexts
		*/
		QStringList		getTexts() const
		{
			return QStringList::split(";",getValue(entryTexts),TRUE);
		}
		/*!	Fuellt die Textliste mit den Beschriftungen der angelegten buttons.
			\param texts		Die Beschriftungen als string mit semicolon getrennt.
			\brief Beschriftungen setzen
			\sa getTexts
		*/
		void			setTexts(const QStringList& texts)
		{
			setValue(entryTexts,texts.join(";"));
		}
		QStringList		getFonts() const
		{
			return QStringList::split(";",getValue(entryFonts));
		}
		/*!	Fuellt die Fontliste mit den Fonts der angelegten buttons.
			\param fonts		Die Fonts als string im format <family>-<size>
			\brief Fonts setzen
			\sa getFonts buildFonts getFontFamily getFontSize
		*/
		void			setFonts(const QStringList& fonts)
		{
			setValue(entryFonts,fonts.join(";"));
		}
		/*!	Baut die Font Information eines Buttons zusammen und liefert diese als String mit '-' getrennt zur�ck-
			\return fontdata	Die fontDaten eines Buttons im format <Familie>-<Gr�sse>
			\param font		Font des Buttons (QFont)
			\brief Font eines Buttons setzen
			\sa getFonts setFonts getFontFamily getFontSize
		*/
		QString		buildFont(const QFont& font)
		{
			return QString("%1-%2").arg(font.family()).arg(font.pointSize());
		}
		/*!	Liefert die Font Familie eines Buttons als string zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <familie>-<size>
			\return familie			Die Fontfamilie des Buttons
			\param button		Der fontstring eines Buttons
			\brief Fontfamilie eines Buttons ermittlen
			\sa setFonts getFonts buildFonts getFontSize
		*/
		QString			getFontFamily(const QString& button)
		{
			return button.section("-",0,0);
		}
		/*!	Liefert die Font gr��e eines Buttons als int zur�ck. Er wird aus dem �bergebenen Datenstring
			gelesen. Der String muss das format <familie>-<size>
			\return size			Die Fontgr��e des Buttons
			\param button		Der fontstring eines Buttons
			\brief Fontgr��e eines Buttons ermittlen
			\sa setFonts getFonts buildFonts getFontFamily
		*/
		int			getFontSize(const QString& button)
		{
			return button.section("-",1,1).toInt();
		}
		/*!	\return Die Frametypen der buttons in einer stringliste ermitteln.
			\brief Frametypen aller buttons ermitteln
			\sa setFrames
		*/
		QStringList		getFrames() const
		{
			return QStringList::split(";",getValue(entryFrames));
		}
		/*!	Fuellt die Frametypliste mit den Frametypen der angelegten buttons.
			\param frames		Die Frametypen als string mit semicolon getrennt.
			\brief Frametypen setzen
			\sa getFrames
		*/
		void			setFrames(const QStringList& frames)
		{
			setValue(entryFrames,frames.join(";"));
		}
		/*!	\return Die Textfarben der buttons in einer stringliste ermitteln.
			Es steht 1 long wert fuer den rgb wert. Er muss mittels QRgb in einen QColor
			gerechnet werden
			\brief RGB Werte der Textfarbe aller buttons ermitteln
			\sa setTextColors setColors getColors
		*/
		QStringList		getTextColors() const
		{
			return QStringList::split(";",getValue(entryTextColors));
		}
		/*!	Fuellt die Colorsliste mit den RGB Werten der angelegten buttons.
			\param colors		Die RGB Werte im uint format als string mit semicolon getrennt.
			Es werden immer RGB als uint gesichert.
			\brief RGB Werte der Textfarben setzen
			\sa getTextColors setColors getColors
		*/
		void			setTextColors(const QStringList& colors)
		{
			setValue(entryTextColors,colors.join(";"));
		}
		/*!	\return Die Textausrichtung der buttons in einer stringliste ermitteln.
			Es gibt links, rechts und zentriert als ausrichtung eines textes
			\brief Textausrichtung aller buttons ermitteln
			\sa setAlignments
		*/
		QStringList		getAlignments() const
		{
			return QStringList::split(";",getValue(entryAlignments));
		}
		/*!	Fuellt die Alignmentliste mit den Textausrichtungen der angelegten buttons.
			\param alignments		Die Textausrichtungen als string mit semicolon getrennt.
			\brief Textausrichtungen der Buttons setzen
			\sa getAlignments
		*/
		void			setAlignments(const QStringList& alignments)
		{
			setValue(entryAlignments,alignments.join(";"));
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse fasst mehrere TOMMenuCard-Elemente zu einer Liste zusammen. Die fuer die
		XML-Funktionen noetigen TValueList-Funktionen wurden ueberschrieben.
		\brief POS-Klassen: Liste von Orderman Menucards-Zusammenstellungen.
	*/
	class		TOMMenuCards
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste (omanmenucards)
		static const char	elementName[];				//!< Name eines Elements der Liste (omanmenucard)
	public:
		/*!	Erzeugt eine neue Instanz einer Orderman Menucards Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste geloescht.
			\brief ctor.
		*/
		TOMMenuCards(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zerstoert die Instanz der Menuliste.
			\brief dtor.
		*/
		~TOMMenuCards()
		{
		}
		virtual TValue*	createValue()
		{
			return new TOMMenuCard();
		}
		virtual int		load(const char* path="")
		{
			return TValueList::load(path);
		}
		virtual int		load(const QString& file, const char* path);
		/*!	\return Liefert den Default-Dateinamen.
			\brief Default-Dateinamen ermitteln.
		*/
		virtual QString	getFileName() const
		{
			return fileName;
		}
		/*!	\return Liefert den Namen der Liste innerhalb des XML-Baums.
			\brief Listennamen ermitteln.
		*/
		virtual QString	getListName() const
		{
			return listName;
		}
		/*!	\return Liefert den Namen eines Elements der Liste innerhalb des XML-Baums.
			\brief Elementnamen ermitteln.
		*/
		virtual QString	getElementName()
		{
			return elementName;
		}
		/*!	\return Liefert die Menucard mit ID index oder NULL, falls kein Element mit
			dieser ID existiert.
			\param index		ID der zu suchenden Menucard
			\brief Menucard suchen.
		*/
		TOMMenuCard*	operator [] (int index)
		{
			return (TOMMenuCard*) TValueList::operator [](index);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse definiert einen Iterator ueber eine TOMMenuCard Liste.
		\brief TOMMenuCard Iterator.
	*/
	class			TOMMenuCardIt
	: public TValueListIt
	{
	public:
		/*!	Erzeugt eine Instanz eines TOMMenuCards-Iterators.
			\param list		Liste mit Menucards, ueber die iteriert werden soll.
			\brief ctor.
		*/
		TOMMenuCardIt(TOMMenuCards& list)
		: TValueListIt(list)
		{
		}
		TOMMenuCard*	operator () ()
		{
			return (TOMMenuCard*) TValueListIt::operator()();
		}
		TOMMenuCard*	toFirst()
		{
			return (TOMMenuCard*) TValueListIt::toFirst();
		}
		TOMMenuCard*	current()
		{
			return (TOMMenuCard*) TValueListIt::current();
		}
		TOMMenuCard*	operator ++ ()
		{
			return (TOMMenuCard*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif //POSLIB_TOMMENUCARD_H
