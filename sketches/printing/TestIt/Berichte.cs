using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TestIt
{
    [XmlRoot("Berichte"), Serializable]
    public class Berichte : List<Bericht>
    {

        public static Berichte LoadFrom(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
                return new Berichte();

            Berichte newData = new Berichte();
            XmlSerializer s = new XmlSerializer(typeof(Berichte));
            TextReader r = new StreamReader(filepath);
            newData = (Berichte)s.Deserialize(r);
            r.Close();
            return newData;
        }
    }
}