using System;
using Godot.IcsModel.Entities;

namespace Godot.IcsModel
{
    public class InvalidUnitException : Exception { }
    public class UnitNotConvertableException : Exception { }

    public interface IUnitConverter
    {
        decimal Convert(decimal value, Unit from, Unit to);
    }
}