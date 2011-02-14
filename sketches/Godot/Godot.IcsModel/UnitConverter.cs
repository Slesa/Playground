using System;
using System.Linq;
using Godot.IcsModel.Entities;

namespace Godot.IcsModel
{
    public class UnitConverter : IUnitConverter
    {
        Unit _fromUnit;
        Unit _toUnit;
        decimal _value;

        public decimal Convert(decimal value, Unit from, Unit to)
        {
            if( from==to )
                return value;
            if( from==null || to==null )
                throw new InvalidUnitException();

            _fromUnit = from;
            _toUnit = to;
            _value = value;

            decimal result;
            if (ConvertToParent(out result))
                return result;
            if (ConvertToChild(out result))
                return result;

            throw new UnitNotConvertableException();
        }

        bool ConvertToParent(out decimal result)
        {
            result = _value;
            var iterator = _fromUnit;
            while (iterator != null)
            {
                result = iterator.FactorToParent != 0m ? result / iterator.FactorToParent : -1m;
                if (iterator.Parent == _toUnit)
                    return true;
                iterator = iterator.Parent;
            }
            return false;
        }

        bool ConvertToChild(out decimal result)
        {
            if (ConvertToSuitableChild(_fromUnit, _value, out result))
                return true;
            return false;
        }

        bool ConvertToSuitableChild(Unit iterator, decimal value, out decimal result)
        {
            result = value;
            var query = from child in iterator.Children where child == _toUnit select child;
            var unit = query.FirstOrDefault();
            if (unit != null)
            {
                result = result*unit.FactorToParent;
                return true;
            }

            foreach (var child in iterator.Children)
            {
                if (child.FactorToParent == 0m)
                {
                    result = -1m;
                    return true;
                }

                decimal subresult;
                if (ConvertToSuitableChild(child, result * child.FactorToParent, out subresult))
                {
                    result = subresult;
                    return true;
                }
            }
            return false;
        }
    }
}