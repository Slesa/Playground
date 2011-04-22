#ifndef			POSLIB_TOMPROFILE_H
#define			POSLIB_TOMPROFILE_H
#include		"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
	Diese Klasse umfasst alle benoetigten Informationen fuer ein DON/MAX Terminal Profil.
	\brief POS Klasse: Orderman Profile
	*/
	class		TOMProfile
	: public TNValue
	{
	public:
		static const char	entryWaiterSelectMode[];
		static const char	entryTableSelectMode[];
		static const char	entryFaxEnabled[];
		static const char	entrySaveFaxBitmap[];
		static const char	entryVatHandling[];
		static const char	entryBillprintHandling[];
		static const char	entryPayformHandling[];
		static const char	entrySoftkeys[];
		static const char	entryOrderColumn[];
		static const char	entryControlColumn[];
		static const char	entryManualElpay[];
		static const char	entryManualEnter[];
		static const char	entryArticleSort[];
		static const char	entryWaiterSort[];
		static const char	entryPayformSort[];
		static const char	entryDepartmentSort[];
		static const char	entryFamilySort[];
		static const char	entryFamgroupSort[];
		static const char	entryDiscountSort[];
		static const char	entryCurrencySort[];
		static const char	entryPrinterSort[];
		static const char	entryModifierSort[];
		static const char	entryArchiveSort[];
		static const char	entryHotelclientSort[];
		static const char	entryControlSort[];
		static const char	entryBatchreportSort[];
		static const char	entryClientSort[];
		static const char	entryTareSort[];
		static const char	entryConstraintSort[];
		static const char	entryOpentableSort[];
		static const char	entryModCardreaderData[];
		static const char	entryCRStartPos[];
		static const char	entryCRDataLength[];
		static const char	entryNumericTransponder[];
		static const char	entryLeaveOrdermode[];
		static const char	entryMsrTrack[];
		static const char	entryHideClientSeekButton[];

	public:
		/*!	\ingroup Poslib
		The following table shows the available buttons in the main orderscreen
		\brief Button for the main orderscreen
		*/
		enum OrderscreenButton
		{
			SearchArticles				= 'A'			//!< Artikel suchen
	//		,PrevTable					= 'B'			//!< Tischnummer um 1 vermindern und îffnen
			,PayMode					= 'C'			//!< In den Paymode wechseln
			,PageUp						= 'D'			//!< Liste 1 Seite nach oben
			,PageDown					= 'E'			//!< Liste 1 Seite nach unten
			,OrderArticle				= 'F'			//!< Direkt via 10er Block (PLU) mehrere ordern
			,FastSplit					= 'G'			//!< ÷ffnet das schnellsplitten
			,ControlsList				= 'H'			//!< Gangsteuerung
			,CommitOrders				= 'I'			//!< Die Bontaste
			,LogoutWaiter				= 'J'			//!< Meldet den kellner aus
			,SetPluCount				= 'K'			//!< Setzt die Anzahl der nÑchsten Bestellung
			,Split						= 'L'			//!< ErfrÑgt den splittisch vor dem splitten
			,ChangeTable				= 'M'			//!< Tischwechsel
			,DiscountTable				= 'N'			//!< Tischrabatt
			,BatchReport				= 'O'			//!< Generiert einen Batch Bericht im Verzeichnis
			,OrderOneArticle			= 'P'			//!< Siehe OrderOneArticle aber nur 1x
			,RestoreTable				= 'Q'			//!< Tisch Retten
			,SetSeat					= 'R'			//!< Sitznummer am terminal einstellen
			,BonMemo					= 'S'			//!< Bonmemo erfassen
			,DeliverTables				= 'T'			//!< Tische ¸bergeben an anderen kellner
			,TakeoverTables				= 'U'			//!< Tische ¸bernehemn von anderem kellner
			,ReceiptCake				= 'V'			//!< Kuchenbon
			,OpenTableTools				= 'W'			//!< ÷ffnet das tisch tools menu
			,SelectTable				= 18			//!< Tischauswahl Special Enterkey.!!!
			,SelectParty				= 19			//!< Parteiauswahl Special Enterkey.!!!
		};
		/*!	\ingroup Poslib
		The following table shows the types of sorting a list
		\brief Sorting modes for lists
		*/
		enum ListSorting
		{
			NoSorting					= 0				//!< liste so wie eingef¸gt
			,SortedById					= 1				//!< Liste nach id sortiert
			,SortedByName				= 2				//!< Liste alphabetisch ortiert
			,SortedByPrio				= 3				//!< Liste nach der Touchprio sortiert
			,SortedByTable				= 20
			,SortedByParty				= 21
			,SortedByAmount				= 40
			,SortedByPrice				= 41
			,SortedByBill				= 50
			,SortedByDate				= 60
			,SortedByTime				= 61
		};
		/*!	\ingroup Poslib
		The following table shows the types of the pay handling
		\brief Pay Handling modes
		*/
		enum PayHandling
		{
			Fix							= 0				//!< payform vat und billprint werden nach bezahlen auf terminalsetting resettiert
			,Changeable					= 1				//!< payform vat und billprint bleiben so wie beim bezahlen eingestellt
		};
		/*!	\ingroup Poslib
		The following table shows the types of data selection
		\brief Data Selection modes
		*/
		enum DataSelectionModes
		{
			ListSelect					= 0				//!< Daten wurden aus einer liste ausgewÑhlt
			,NumericSelect				= 1				//!< Daten wurden mit einem 10er Block ausgewÑhlt
			,RFIDSelect					= 2				//!< Daten werden via Transponder ermittelt
			,ButtonSelect				= 3				//!< Daten werden via buttons ausgewÑhlt
		};
	public:
		/*!	Erzeugt eine leere Instanz eines Profiles.
			\brief ctor
		*/
			TOMProfile()
			: TNValue()
			{
			}
		/*!	\return TRUE Wenn im hotel und kundendatenbank screen kein button zum suchen angezeigt werden soll
			\brief Suchenbutton hiden
			\sa setHideClientSeekButton
		*/
		bool			hideClientSeekButton() const
		{
			return getValue(entryHideClientSeekButton,FALSE);
		}
		/*!	Seek button hiden
			\param hide	TRUE wenn im hotel und kundendatenbank screen kein button zum suchen angezeigt werden soll
			\brief Suchebutton hiden
			\sa hideClientSeekButton
		*/
		void			setHideClientSeekButton(bool hide)
		{
			setValue(entryHideClientSeekButton,hide);
		}
		//	\return Die Nummer des tracks bei msr karten die uebermittelt werden soll
		int				getMsrTrack()
		{
			return getValue(entryMsrTrack,0);
		}
		//	\return Setzt die nummer des tracks bei msr karten die uebermittel werden soll
		void			setMsrTrack(int track)
		{
			setValue(entryMsrTrack,track);
		}
		/*!	\return Die Startposition in den cardreaderdaten ab der die daten gelesen werden sollen
			\brief Startposition in den Cardreaderdaten ermitteln
			\sa setCRStartPos
		*/
		int				getCRStartPos()
		{
			return getValue(entryCRStartPos,0);
		}
		/*!	Setzt die Startposition in den cardeaderdaten ab der diese gelesen werden
			\brief Startposition in den Cardreaderdaten setzen
			\sa getCRStartPos
		*/
		void			setCRStartPos(int pos)
		{
			setValue(entryCRStartPos,pos);
		}
		/*!	\return Die laenge der cardreaderdaten
			\brief Laenge der Cardreaderdaten ermitteln
			\sa setCRDataLength
		*/
		int				getCRDataLength()
		{
			return getValue(entryCRDataLength,1);
		}
		/*!	Setzt die laenge der cardeaderdaten
			\brief Laenge der Cardreaderdaten setzen
			\sa getCRDataLength
		*/
		void			setCRDataLength(int len)
		{
			setValue(entryCRDataLength,len);
		}
		//	TRUE Wenn nach abbruch des transponderlesevorgangs eine numerische eingabe
		//	erfolgen soll
		bool			numericTransponder() const
		{
			return getValue(entryNumericTransponder,FALSE);
		}
		//	TRUE wenn nach abbruch des transponderlesevorgans eine numerisch eingabe
		//	erfolgen soll
		void			setNumericTransponder(bool b)
		{
			setValue(entryNumericTransponder,b);
		}
		/*!	\return TRUE Wenn die Daten des Cardreaders modifiziert gelesen werden sollen
			\brief Cardreaderdaten modifizieren
			\sa setModifyCardreaderData
		*/
		bool			modifyCardreaderData() const
		{
			return getValue(entryModCardreaderData,FALSE);
		}
		/*!	Cardreaderdaten modifiziert lesen an/aus
			\param enable	TRUE wenn die Daten des Cardreaders modifiziert gelesen werden sollen
			\brief cardreaderdaten modifizeiren
			\sa modifyCardreaderData
		*/
		void			setModifyCardreaderData(bool enabled)
		{
			setValue(entryModCardreaderData,enabled);
		}
		/*!	\return Die Sortierung f¸r die Artikel Liste
		\brief Artikelliste Sortierung ermitteln
		\sa setArticleSort
		*/
		int				getArticleSort()
		{
			return getValue(entryArticleSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Artikel Liste
			\brief Artikelliste Sortierung setzen
			\sa getArticleSort
		*/
		void			setArticleSort(int mode)
		{
			setValue(entryArticleSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Kellner Liste
			\brief Kellnerliste Sortierung ermitteln
			\sa setWaiterSort
		*/
		int				getWaiterSort()
		{
			return getValue(entryWaiterSort,SortedById);
		}
		/*!	Setzt die Sortierung f¸r die Kellner Liste
		\brief Kellnerliste Sortierung setzen
		\sa getWaiterSort
		*/
		void			setWaiterSort(int mode)
		{
			setValue(entryWaiterSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Abrechnungsarten Liste
			\brief Abrechnungsartenliste Sortierung ermitteln
			\sa setPayformSort
		*/
		int				getPayformSort()
		{
			return getValue(entryPayformSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Abrechnungsarten Liste
		\brief Abrechnungsarten Liste Sortierung setzen
		\sa getPayformSort
		*/
		void			setPayformSort(int mode)
		{
			setValue(entryPayformSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Sparten Liste
			\brief Spartenliste Sortierung ermitteln
			\sa setDepartmentSort
		*/
		int				getDepartmentSort()
		{
			return getValue(entryDepartmentSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Sparten Liste
		\brief Spartenliste Sortierung setzen
		\sa getDepartmentSort
		*/
		void			setDepartmentSort(int mode)
		{
			setValue(entryDepartmentSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Warengruppen Liste
			\brief Warengruppenliste Sortierung ermitteln
			\sa setFamilySort
		*/
		int				getFamilySort()
		{
			return getValue(entryFamilySort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Warengruppen Liste
		\brief Warengruppenliste Sortierung setzen
		\sa getFamilySort
		*/
		void			setFamilySort(int mode)
		{
			setValue(entryFamilySort,mode);
		}
		/*!	\return Die Sortierung f¸r die Warenobergruppe Liste
			\brief Warenobergruppeliste Sortierung ermitteln
			\sa setFamgroupSort
		*/
		int				getFamgroupSort()
		{
			return getValue(entryFamgroupSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Warenobergruppe Liste
		\brief Warenobergruppeliste Sortierung setzen
		\sa getFamgroupSort
		*/
		void			setFamgroupSort(int mode)
		{
			setValue(entryFamgroupSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Rabatt Liste
			\brief Rabattliste Sortierung ermitteln
			\sa setDiscountSort
		*/
		int				getDiscountSort()
		{
			return getValue(entryDiscountSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Rabatt Liste
		\brief Rabattliste Sortierung setzen
		\sa getDiscountSort
		*/
		void			setDiscountSort(int mode)
		{
			setValue(entryDiscountSort,mode);
		}
		/*!	\return Die Sortierung f¸r die W‰hrungs Liste
			\brief W‰hrungsliste Sortierung ermitteln
			\sa setCurrencySort
		*/
		int				getCurrencySort()
		{
			return getValue(entryCurrencySort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die W‰hrungs Liste
		\brief W‰hrungsliste Sortierung setzen
		\sa getCurrencySort
		*/
		void			setCurrencySort(int mode)
		{
			setValue(entryCurrencySort,mode);
		}
		/*!	\return Die Sortierung f¸r die Drucker Liste
			\brief Druckerliste Sortierung ermitteln
			\sa setPrinterSort
		*/
		int				getPrinterSort()
		{
			return getValue(entryPrinterSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Drucker Liste
		\brief Druckerliste Sortierung setzen
		\sa getPrinterArticleSort
		*/
		void			setPrinterSort(int mode)
		{
			setValue(entryPrinterSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Modifier Liste
			\brief Modifierliste Sortierung ermitteln
			\sa setModifierSort
		*/
		int				getModifierSort()
		{
			return getValue(entryModifierSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Modifier Liste
		\brief Modifierliste Sortierung setzen
		\sa getModifierSort
		*/
		void			setModifierSort(int mode)
		{
			setValue(entryModifierSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Archives Liste
			\brief Archivesliste Sortierung ermitteln
			\sa setArchiveSort
		*/
		int				getArchiveSort()
		{
			return getValue(entryArchiveSort,SortedByBill);
		}
		/*!	Setzt die Sortierung f¸r die Archives Liste
		\brief Archivesliste Sortierung setzen
		\sa getArchiveSort
		*/
		void			setArchiveSort(int mode)
		{
			setValue(entryArchiveSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Hotelg‰ste Liste
			\brief Hotelg‰steliste Sortierung ermitteln
			\sa setHotelclientSort
		*/
		int				getHotelclientSort()
		{
			return getValue(entryHotelclientSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Hotelg‰ste Liste
		\brief Hotelg‰steliste Sortierung setzen
		\sa getHotelclientSort
		*/
		void			setHotelclientSort(int mode)
		{
			setValue(entryHotelclientSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Gangartikel Liste
			\brief Gangartikelliste Sortierung ermitteln
			\sa setControlSort
		*/
		int				getControlSort()
		{
			return getValue(entryControlSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Gangartikel Liste
		\brief Gangartikelliste Sortierung setzen
		\sa getControlSort
		*/
		void			setControlSort(int mode)
		{
			setValue(entryControlSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Batchreport Liste
			\brief Batchreportliste Sortierung ermitteln
			\sa setBatchreportSort
		*/
		int				getBatchreportSort()
		{
			return getValue(entryBatchreportSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Batchreport Liste
		\brief Batchreportliste Sortierung setzen
		\sa getBatchreportSort
		*/
		void			setBatchreportSort(int mode)
		{
			setValue(entryBatchreportSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Kunden Liste
			\brief Kundenliste Sortierung ermitteln
			\sa setClientSort
		*/
		int				getClientSort()
		{
			return getValue(entryClientSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Kunden Liste
		\brief Kundenliste Sortierung setzen
		\sa getClientSort
		*/
		void			setClientSort(int mode)
		{
			setValue(entryClientSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Tara Liste
			\brief Taraliste Sortierung ermitteln
			\sa setTareSort
		*/
		int				getTareSort()
		{
			return getValue(entryTareSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Tara Liste
		\brief Taraliste Sortierung setzen
		\sa getTareSort
		*/
		void			setTareSort(int mode)
		{
			setValue(entryTareSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Zwangartikel Liste
			\brief Zwangartikelliste Sortierung ermitteln
			\sa setConstraintSort
		*/
		int				getConstraintSort()
		{
			return getValue(entryConstraintSort,SortedByName);
		}
		/*!	Setzt die Sortierung f¸r die Zwangartikel Liste
		\brief Zwangartikelliste Sortierung setzen
		\sa getConstraintSort
		*/
		void			setConstraintSort(int mode)
		{
			setValue(entryConstraintSort,mode);
		}
		/*!	\return Die Sortierung f¸r die Offenstands Liste
			\brief Offenstandsliste Sortierung ermitteln
			\sa setOpentableSort
		*/
		int				getOpentableSort()
		{
			return getValue(entryOpentableSort,SortedByTable);
		}
		/*!	Setzt die Sortierung f¸r die Offenstands Liste
		\brief Offenstandsliste Sortierung setzen
		\sa getOpentableSort
		*/
		void			setOpentableSort(int mode)
		{
			setValue(entryOpentableSort,mode);
		}
		/*!	\return Die Softkeys des Ordermodes in einer stringliste
			\brief Softkeys des Ordermodes ermitteln
			\sa setSoftkeys
		*/
		QStringList		getSoftkeys() const
		{
			return QStringList::split(";",getValue(entrySoftkeys));
		}
		/*!	Fuellt die softkeyliste mit den softkeys des ordermodes ab. Es werden immer
			KeyId Funktion und Beschriftung zusammengefasst und mit einem '-' getrennt. Diese Pakete werden
			untereinander mit einem ';' getrennt.
			\param keys			Die Softkeys als stringliste im format <keyid>-<funktion>-<caption>;<keyid...
			\brief Softkeys des Ordermodes setzen
			\sa getSoftkeys
		*/
		void			setSoftkeys(const QStringList& keys)
		{
			setValue(entrySoftkeys,keys.join(";"));
		}
		/*!	Baut eine Softkeydefinition des Ordermodes zusammen und liefert diese als String mit '-' getrennt zurÅck.
			\return softkeydata	Die Daten eines Softkeys im format <keyid>-<funktion>-<beschriftung>
			\param keyid		Die ID des Softkeys z.zt. 1-8
			\param function		Die Funktion des Keys (Bon, split, Gang...)
			\param caption		Die Beschriftung max. 3 zeichenas
			\brief Softkeydata setzen
			\sa getSoftkeyId getSoftkeyFunction getSoftkeyCap
		*/
		QString			buildSoftkey(int keyid,int function,const QString caption);
		/*!	Liefert die Id des Softkeys als integer zurÅck. Er wird aus dem Åbergebenen Datenstring
			gelesen. Der String muss das format <keyid>-<funktion>-<caption> besitzen.
			\return keyid		Die id des Softkeys 1-8
			\param data			Der datenstring des softkeys
			\brief Key ID eines Softkeys ermittlen
			\sa buildSoftkey getSoftkeyFunction getSoftkeyCap
		*/
		int				getSoftkeyId(const QString& data)
		{
			return data.section("-",0,0).toInt();
		}
		/*!	Liefert den Funktionscode eines Softkeys zurÅck. Der String muss das format
			<keyid>-<funktion>-<caption> besitzen.
			\return funktion id		Die Funktion des Softkeys
			\param data				Der datenstring des Softkeys
			\brief Funktion eines Softkeys ermittlen
			\sa buildSoftkey getSoftkeyId getSoftkeyCap
		*/
		int				getSoftkeyFunction(const QString& data)
		{
			return data.section("-",1,1).toInt();
		}
		/*!	Liefert die Beschriftung eines Softkeys zurÅck. Der String muss das format
			<keyid>-<funktion>-<caption> besitzen.
			\return caption		Die Beschriftung
			\param data		Der datenstring des Softkeys
			\brief Beschriftung eines Softkeys ermittlen
			\sa buildSoftkey getSoftkeyFunction getSoftkeyId
		*/
		QString			getSoftkeyCap(const QString& data)
		{
			QString cap = data.section("-",2,2);
			return cap.leftJustify(3,' ');
		}
		//	TRue wenn man nicht wieder im ordermode landen soll nach der
		//	Tischuebersicht sondern dort bleiben will
		bool			leaveOrdermode() const
		{
			return getValue(entryLeaveOrdermode,FALSE);
		}
		//	setzt das flag ob der ordermode eng¸eltig verlassen werden soll
		//	beim wechsel in die tisch¸bersicht
		void			setLeaveOrdermode(bool leave)
		{
			setValue(entryLeaveOrdermode,leave);
		}
		/*!	\return TRUE Wenn das FAX Feature erlaubt ist
			\brief FAX Feature Status ermitteln
			\sa setFaxEnabled
		*/
		bool			isFaxEnabled() const
		{
			return getValue(entryFaxEnabled,FALSE);
		}
		/*!	Schaltet das FAX Feature an oder aus
			\param enable	TRUE wenn FAX erlaubt sein soll
			\brief FAX Feature erlauben / sperren
			\sa isFaxEnabled
		*/
		void			setFaxEnabled(bool enabled)
		{
			setValue(entryFaxEnabled,enabled);
		}
		/*!	\return TRUE Wenn das die FAX funktion das FAX als bitmap
			sichern soll
			\brief FAX als bitmap sichern ermittlen
			\sa setSaveFaxBitmap
		*/
		bool			saveFaxBitmap() const
		{
			return getValue(entrySaveFaxBitmap,FALSE);
		}
		/*!	Sichern der faxe als bitmap an/aus
			\param enable	TRUE wenn FAX als bitmap gesichert werden soll
			\brief FAX bitmap sichern erlauben / sperren
			\sa saveFaxBitmap
		*/
		void			setSaveFaxBitmap(bool enabled)
		{
			setValue(entrySaveFaxBitmap,enabled);
		}
		/*!	\return TRUE Wenn Elpaydaten manuell erfasst werden kˆnnen
			\brief ELPAY daten manuell erfassen
			\sa setManualElpay
		*/
		bool			isManualElpay() const
		{
			return getValue(entryManualElpay,FALSE);
		}
		/*!	Kˆnnen Elpay Daten manuell erfasst werden
			\param b	TRUE wenn Elpay Daten manuell erfasst werden kˆnnen
			\brief Elpay datenerfsassung manuell erlauben / sperren
			\sa isManualElpay
		*/
		void			setManualElpay(bool b)
		{
			setValue(entryManualElpay,b);
		}
		/*!	\return TRUE Wenn in der Listenansicht der offenst‰nde der 1. Eintrag
			zur manuellen eingabe der Tischnummer dienen soll -> numpad ˆffnen
			\brief Manuelle Tischnummereingabe aus der Offenstandsliste
			\sa setManualEnterAvailable
		*/
		bool			isManualEnterAvailable() const
		{
			return getValue(entryManualEnter,TRUE);
		}
		/*!	Manuelle eingabe in einer liste ermˆglichen
			\param b	TRUE wenn die manuelle eingabe via numpad erlaubt ist
			\brief Mauelle eingabe anstatt auswahl aus der liste
			\sa isManualEnterAvailable
		*/
		void			setManualEnterAvailable(bool b)
		{
			setValue(entryManualEnter,b);
		}
		/*!	\return Die Art wie ein Kellner ausgew‰hlt wird
			\brief Art der Kellnerauswahl
			\sa setWaiterSelectMode DataSelectionModes
		*/
		int			getWaiterSelectMode() const
		{
			return getValue(entryWaiterSelectMode,ListSelect);
		}
		/*!	ƒndert die Art der Kellnerauswahl
			\param mode		Die neue Auswahlart
			\brief Art der Kellnerauswahl
			\sa getWaiterSelectMode DataSelectionModes
		*/
		void			setWaiterSelectMode(int mode)
		{
			setValue(entryWaiterSelectMode,mode);
		}
		/*!	\return Die Art wie ein Tisch ausgew‰hlt wird.
			\brief Art der Tischauswahl
			\sa setTableSelectMode DataSelectionModes
		*/
		int			getTableSelectMode() const
		{
			return getValue(entryTableSelectMode,ListSelect);
		}
		/*!	éndert die Art der Tischauswahl
			\param mode		Die neue auswahlart
			\brief Art der Tischauswahl
			\sa getTableSelectMode DataSelectionModes
		*/
		void			setTableSelectMode(int mode)
		{
			setValue(entryTableSelectMode,mode);
		}
		/*!	\return Die Art wie nach dem Bezahlen eines Tisches mit der
			Abrechnungsarteinstellung verfahren werden soll.
			\brief Art des Abrechnungsart Handlings nach dem bezahlen
			\sa setVatHandling PayHandling setBillprintHandling setPayformHandling
		*/
		int			getPayformHandling() const
		{
			return getValue(entryPayformHandling,Fix);
		}
		/*!	éndert die Art des Abrechnungsat Handlings beim bezahlen eines Tisches
			\param mode		Fix oder Changeable
			\brief Art des Abrechnungsart Handlings
			\sa getVatHandling getBillprintHandling PayHandling getPayformHandling
		*/
		void			setPayformHandling(int mode)
		{
			setValue(entryPayformHandling,mode);
		}
		/*!	\return Die Art wie nach dem Bezahlen eines Tisches mit der Mwst
			einstelung verfahren werden soll.
			\brief Art des MWST Handlings nach dem bezahlen
			\sa setVatHandling PayHandling setBillprintHandling
		*/
		int			getVatHandling() const
		{
			return getValue(entryVatHandling,Fix);
		}
		/*!	éndert die Art des MWST Handlings beim bezahlen eines Tisches
			\param mode		Fix oder Changeable
			\brief Art des MWST Handlings
			\sa getVatHandling getBillprintHandling PayHandling
		*/
		void			setVatHandling(int mode)
		{
			setValue(entryVatHandling,mode);
		}
		/*!	\return Die Art wie nach dem Bezahlen eines Tisches mit der Rechnungsdruck
			einstelung verfahren werden soll.
			\brief Art des Rechnungsdrucks Handlings nach dem bezahlen
			\sa setBillprintHandling PayHandling setVatHandling
		*/
		int			getBillprintHandling() const
		{
			return getValue(entryBillprintHandling,Fix);
		}
		/*!	éndert die Art des Rechnungsdruck Handlings beim bezahlen eines Tisches
			\param mode		Fix oder Changeable
			\brief Art des Rechnungsdruck Handlings
			\sa getVatHandling getBillprintHandling PayHandling
		*/
		void		setBillprintHandling(int mode)
		{
			setValue(entryBillprintHandling,mode);
		}
		/*!	\return Der Formatstring eines Ordereintrags f¸r den parser
			\brief Orderformatstring f¸r den parser
			\sa setOrderColumn
		*/
		QString		getOrderColumn() const
		{
			return getValue(entryOrderColumn,"");
		}
		/*!	ƒndert den Formatstring des Parsers f¸r einen Ordereintrag
			\param fs		Neuer Formatstring
			\brief Formatstring eines Ordereintrags ‰ndern
			\sa getOrderColumn
		*/
		void		setOrderColumn(const QString& fs)
		{
			setValue(entryOrderColumn,fs);
		}
		/*!	\return Der Formatstring eines Controleintrags f¸r den parser
			\brief Controlformatstring f¸r den parser
			\sa setControlColumn
		*/
		QString		getControlColumn() const
		{
			return getValue(entryControlColumn,"");
		}
		/*!	ƒndert den Formatstring des Parsers f¸r einen Controleintrag
			\param fs		Neuer Formatstring
			\brief Formatstring eines Controleintrags ‰ndern
			\sa getControlColumn
		*/
		void		setControlColumn(const QString& fs)
		{
			setValue(entryControlColumn,fs);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse fasst mehrere TOMProfile-Elemente zu einer Liste zusammen. Die fuer die
		XML-Funktionen noetigen TValueList-Funktionen wurden ueberschrieben.
		\brief POS-Klassen: Liste von Orderman Profile Zusammenstellungen.
	*/
	class		TOMProfiles
	: public TValueList
	{
		Q_OBJECT
		static const char	fileName[];					//!< Default-Dateiname
	public:
		static const char	listName[];					//!< Name der Liste (omanprofiles)
		static const char	elementName[];				//!< Name eines Elements der Liste (omanprofile)
	public:
		/*!	Erzeugt eine neue Instanz einer Orderman Profile Liste.
			\param autodel	Wenn TRUE, werden die Elemente beim entfernen aus der Liste geloescht.
			\brief ctor.
		*/
		TOMProfiles(bool autodel=TRUE)
		: TValueList(autodel)
		{
		}
		/*!	Zerstoert die Instanz der Profiles liste.
			\brief dtor.
		*/
		~TOMProfiles()
		{
		}
		virtual TValue*	createValue()
		{
			return new TOMProfile();
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
		TOMProfile*	operator [] (int index)
		{
			return (TOMProfile*) TValueList::operator [](index);
		}
	};

	/*!	\ingroup PosLib
		Diese Klasse definiert einen Iterator ueber eine TOMProfile Liste.
		\brief TOMProfile Iterator.
	*/
	class			TOMProfileIt
	: public TValueListIt
	{
	public:
		/*!	Erzeugt eine Instanz eines TOMProfile-Iterators.
			\param list		Liste mit Profiles, ueber die iteriert werden soll.
			\brief ctor.
		*/
		TOMProfileIt(TOMProfiles& list)
		: TValueListIt(list)
		{
		}
		TOMProfile*	operator () ()
		{
			return (TOMProfile*) TValueListIt::operator()();
		}
		TOMProfile*	toFirst()
		{
			return (TOMProfile*) TValueListIt::toFirst();
		}
		TOMProfile*	current()
		{
			return (TOMProfile*) TValueListIt::current();
		}
		TOMProfile*	operator ++ ()
		{
			return (TOMProfile*) TValueListIt:: operator ++();
		}
	};
}

using namespace PosLib;

#endif //POSLIB_TOMPROFILE_H

