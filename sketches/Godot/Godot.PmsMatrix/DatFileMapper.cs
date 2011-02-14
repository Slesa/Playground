using System;
using System.IO;

namespace Godot.PmsMatrix
{
    public interface IDatFileMapper
    {
        string GetFileNameFor<TEntityType>();
        string GetFullFileNameFor<TEntityType>();
        string DataPath { get; }
    }

    public class DatFileMapper : IDatFileMapper
    {
        public string GetFileNameFor<TEntityType>()
        {
            var name = typeof (TEntityType).Name;
            if (name == "SalesItem")
                return "articles.dat";
            if (name == "Costcenter")
                return "centers.dat";
            return name + "s.dat";
        }

        public string GetFullFileNameFor<TEntityType>()
        {
            return Path.Combine(DataPath, GetFileNameFor<TEntityType>());
        }

        public string DataPath
        {
            get { return @"usr\data"; }
        }
    }
}