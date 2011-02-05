using System;
using System.IO;

namespace MatrixAccess
{
    public class DatFileNameMapper : IDatFileNameMapper
    {
        public string GetFileNameFor<TEntityType>()
        {
            var name = typeof(TEntityType).Name;
            if (name == "SalesItem")
                return "articles.dat";
            if (name == "Costcenter")
                return "centers.dat";
            if (name.EndsWith("y"))
                return name.Substring(0,name.Length-1).ToLower()+"ies.dat";
            if (name.Contains("DongleInfo"))
                return "dongles.dat";
            return name.ToLower() + "s.dat";
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