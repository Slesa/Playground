using System;
using System.Collections.Generic;
using System.IO;
using DataAccess;
using Infrastructure.Configuration;

namespace MatrixAccess
{
    public class MatrixFileLoader<TEntity> : IMatrixFileLoader<TEntity> where TEntity : DomainEntity, new()
    {
        readonly IDatFileNameMapper _datFileMapper;
        readonly string _dataPath;
        Dictionary<int, TEntity> _elements;

        public MatrixFileLoader(IConfigurationReader configReader, IDatFileNameMapper datFileMapper)
        {
            _datFileMapper = datFileMapper;
            _dataPath = configReader.ValueOf("MatrixPath");
        }

        public object GetById(object id)
        {
            return Elements[(int)id];
        }

        public Dictionary<int, TEntity> Elements
        {
            get
            {
                if (_elements != null) return _elements;
                _elements = LoadElements();
                return _elements;
            }
        }

        public string FullFileName
        {
            get
            {
                var fileName = _datFileMapper.GetFullFileNameFor<TEntity>();
                if (!string.IsNullOrEmpty(_dataPath))
                    fileName = Path.Combine(_dataPath, fileName);
                return fileName;
            }
        }

        public string FullPathName
        {
            get
            {
                var usrpath = _datFileMapper.DataPath;
                if (!string.IsNullOrEmpty(_dataPath))
                    usrpath = Path.Combine(_dataPath, usrpath);
                return usrpath;
            }
        }

        Dictionary<int, TEntity> LoadElements()
        {
            if (!CreateDataPathIfNeccessary())
                return new Dictionary<int, TEntity>();

            var fileName = FullFileName;
            if (!File.Exists(fileName))
                return new Dictionary<int, TEntity>();

            System.Diagnostics.Debug.WriteLine(String.Format("Importing file {0} ...", fileName));

            using (var fh = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var st = new BinaryReader(fh, System.Text.Encoding.BigEndianUnicode))
                {
                    return ReadElements(st);
                }
            }
        }

        bool CreateDataPathIfNeccessary()
        {
            try
            {
                if (!Directory.Exists(FullPathName))
                    Directory.CreateDirectory(FullPathName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static Dictionary<int, TEntity> ReadElements(BinaryReader st)
        {
            var result = new Dictionary<int, TEntity>();
            while (true)
            {
                try
                {
                    var size = FlipWord((UInt32)st.ReadInt32());
                    if (size == 0)
                        continue;
                    var element = ReadElement(st, size);
                    if (element != null)
                    {
                        if (element.Id == 0)
                            continue;
                        //RemapId(element);
                        result.Add(element.Id, element);
                    }
                }
                catch (EndOfStreamException)
                {
                    break;
                }
            }
            return result;
        }

        static TEntity ReadElement(BinaryReader st, uint size)
        {
            var bag = new Dictionary<string, string>();
            //System.Diagnostics.Debug.WriteLine("Reading bag with {0} items...", size);
            for (var i = 0; i < size; i++)
            {
                var lenAttrib = FlipWord((UInt32)st.ReadInt32());
                var attrib = new String(st.ReadChars((int)lenAttrib / 2));
                var lenValue = FlipWord((UInt32)st.ReadInt32());
                var value = new String(st.ReadChars((int)lenValue / 2));
                //System.Diagnostics.Debug.WriteLine("{0} = {1}", attrib, value);
                bag.Add(attrib, value);
            }
            return BagToElement(bag);
        }

        static TEntity BagToElement(IDictionary<string, string> bag)
        {
            var entity = new TEntity(); //CreateEntity();
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                string value;
                if (bag.TryGetValue(property.Name.ToLower(), out value))
                    property.SetValue(entity, Convert.ChangeType(value, property.PropertyType), null);
            }
            return entity;
        }

        public void SaveElements(string dataname, string datapath, IDictionary<int, TEntity> values)
        {
            if (!CreateDataPathIfNeccessary())
                throw new InvalidOperationException();

            var fileName = FullFileName;
            using (var fh = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var st = new BinaryWriter(fh, System.Text.Encoding.BigEndianUnicode))
                {
                    WriteElements(st, values);
                }
            }
        }

        static void WriteElements(BinaryWriter st, IDictionary<int, TEntity> values)
        {
            foreach (var value in values.Values)
                WriteElement(st, value);
        }

        static void WriteElement(BinaryWriter st, TEntity entity)
        {
            var bag = ElementToBag(entity);
            var count = bag.Count;
            st.Write(FlipWord((UInt32)count));
            foreach (var keyValue in bag)
            {
                var lenAttrib = keyValue.Key.Length * 2;
                st.Write(FlipWord((UInt32)lenAttrib));
                st.Write(keyValue.Key.ToCharArray());
                var lenValue = keyValue.Value.Length * 2;
                st.Write(FlipWord((UInt32)lenValue));
                st.Write(keyValue.Value.ToCharArray());
            }
        }

        static Dictionary<string, string> ElementToBag(TEntity entity)
        {
            var bag = new Dictionary<string, string>();
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity, null);
                bag[property.Name] = value.ToString();
            }
            return bag;
        }

        static uint FlipWord(uint inDWord)
        {
            uint ret = ((inDWord >> 24) & 0xFF) | ((inDWord & 0xFF0000) >> 16) & 0xFF00 | ((inDWord & 0xFF00) * 0x100) | ((inDWord & 0x7F) * 0x1000000);
            if ((inDWord & 0x80) != 0)
                ret = ret | 0x80000000;
            return ret;
        }
    }
}
