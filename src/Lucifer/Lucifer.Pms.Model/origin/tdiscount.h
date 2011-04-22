#ifndef				POSLIB_TDISCOUNT_H
#define				POSLIB_TDISCOUNT_H
#include			"basics/tvalue.h"

namespace PosLib
{
	/*!	\ingroup PosLib
		Diese Klasse umfa� alle ben�igten Informationen fr Rabatts�ze.
		Alle verfgbaren Rabatts�ze werden in einer Instanz von TDiscounts
		zur Verfgung gestellt.
		\brief POS-Klassen: Rabatts�ze.
	*/
	class			TDiscount
	: public TNValue
	{
		static const char	entryRate[];
		static const char	entryIsOrder[];
		static const char	entryIsAbsolut[];
		static const char	entryArticle[];
		static const char       entryRounding[];
	public:
		/*!	Erzeuge eine leere Instanz eines Rabattsatzes.
			\brief ctor.
		*/
		TDiscount()
		: TNValue()
		{
		}
		/*!	\return der Wert des Rabattsatzes.
			\brief Rabattsatz abfragen.
			\sa setRate
		*/
		double		getDiscRate() const
		{
			return getValue(entryRate, 0.0);
		}
		/*!	�dert den Wert des Rabattsatzes.
			\param rate		Der neue Wert
			\brief Wert des Rabattsatzes �dern.
			\sa getRate
		*/
		void		setDiscRate(double rate)
		{
			if( rate==0.0 )
				clrValue(entryRate);
			else
				setValue(entryRate, rate);
		}
		/*!	\return Flag, ob es sich bei dem Rabattsatz um einen Bestellrabatt handelt.
			\brief Bestellrabatt?.
			\sa setIsOrder
		*/
		int		getRounding() const
		{
			return getValue(entryRounding, 0);
		}

		void		setRounding(int rounding)
		{
			if(rounding==0)
				clrValue(entryRounding);
			else
				setValue(entryRounding, rounding);
		}
		bool		isOrder() const
		{
			return getValue(entryIsOrder, FALSE);
		}
		/*!	�dert das Flag, ob es sich bei dem Rabattsatz um einen Bestellrabatt handelt.
			\param flag		true, wenn es sich um einen Bestellrabatt handelt.
			\brief �dere Bestellrabatt-Flag.
			\sa isOrder
		*/
		void		setIsOrder(bool flag)
		{
			if( !flag )
				clrValue(entryIsOrder);
			else
				setValue(entryIsOrder, flag);
		}
		/*!	\return Flag, ob es sich bei dem Rabattsatz um einen absoluten Wert handelt
			(TRUE) oder prozentual.
			\brief Absoluter Rabatt?.
			\sa setIsAbsolut
		*/
		bool		isAbsolut() const
		{
			return getValue(entryIsAbsolut, FALSE);
		}
		/*!	�dert das Flag, ob es sich bei dem Rabattsatz um einen absoluten oder einen
			prozentualen Rabatt handelt.
			\param flag		TRUE, wenn es der Rabatt absolut ist.
			\brief �dere Absolutrabatt-Flag.
			\sa isAbsolut
		*/
		void		setIsAbsolut(bool flag)
		{
			if( !flag )
				clrValue(entryIsAbsolut);
			else
				setValue(entryIsAbsolut, flag);
		}
		int			getArticle() const
		{
			return getValue(entryArticle, 0);
		}
		void		setArticle(int art)
		{
			if( !art)
				clrValue(entryArticle);
			else
				setValue(entryArticle, art);
		}
	};


}

using namespace PosLib;

#endif


