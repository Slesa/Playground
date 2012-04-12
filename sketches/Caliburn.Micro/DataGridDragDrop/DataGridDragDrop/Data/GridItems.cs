using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace DataGridDragDrop.Data
{
    public class GridItems
    {
        const string FileName = "DataGridDragDrop.xml";

        List<GridItem> _items;
        public List<GridItem> Items
        {
            get
            {
                if (_items==null ) Load();
                var query = from item in _items orderby item.Id select item;
                return query.ToList();
            }
        }

        public void Load()
        {
            if (File.Exists(FileName))
            {
                try
                {
                    var file = new StreamReader(FileName, Encoding.UTF8);
                    Deserialize(file);
                    return;
                }
                catch(Exception)
                {}
            }

            CreateDefaultList();
        }

        void Deserialize(TextReader file)
        {
            var dc = new DataContractSerializer(typeof (List<GridItem>));
            using(var xmlReader = XmlReader.Create(file))
            {
                _items = (List<GridItem>) dc.ReadObject(xmlReader);
            }
        }

        void CreateDefaultList()
        {
            _items = new List<GridItem>();
            for (var i = 1; i < 10; i++)
                _items.Add(new GridItem { Id = i, Checked = i % 2 != 0, Name = "Item " + i, Visible = true, IntValue = i, TextValue = i+". Item"});
            
        }

        public void Save()
        {
            var text = Serialize();
            var file = new StreamWriter(FileName, false, Encoding.UTF8);
            file.Write(text);
            file.Flush();
        }

        string Serialize()
        {
            var sb = new StringBuilder();
            var dc = new DataContractSerializer(typeof(List<GridItem>));
            using (var xmlWriter = XmlWriter.Create(sb))
            {
                dc.WriteObject(xmlWriter, _items);
                xmlWriter.Flush();
                return sb.ToString();
            }
        }
    }
}