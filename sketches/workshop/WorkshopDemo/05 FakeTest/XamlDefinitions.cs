using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FakeTest
{
    public class XamlDefinitions
    {
        const string UserControlTag = "UserControl";
        const string ResourceDictionaryTag = "ResourceDictionary";
        const string ResourcesTag = "Resources";

        const string IgnorableTag = "mc:Ignorable=\"d\"";

        readonly List<string> _namespaces = new List<string>();
        readonly List<string> _lines = new List<string>();
        bool _userControl;
        bool _namespacesAdded;
        bool _noIgnorable;

        public XamlDefinitions DefaultNamespaces()
        {
            _namespacesAdded = true;
            _namespaces.Add("xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'");
            _namespaces.Add("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'");
            _namespaces.Add("xmlns:d='http://schemas.microsoft.com/expression/blend/2008'");
            _namespaces.Add("xmlns:mc='http://schemas.openxmlformats.org/markup-compatibility/2006'");
            return this;
        }

        public XamlDefinitions AddNamespace(string ns)
        {
            _namespaces.Add(ns);
            return this;
        }

        public XamlDefinitions AddLine(string line)
        {
            _lines.Add(line);
            return this;
        }

        public XamlDefinitions AddMore(string line, params object[] parameters)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(line, parameters);
            _lines.Add(sb.ToString());
            return this;
        }

        public string XamlCode
        {
            get
            {
                var sb = new StringBuilder();

                WriteStartTags(sb);

                foreach (var line in _lines) sb.AppendLine(line);

                WriteEndTags(sb);

                var str = sb.ToString();
                return str;
            }
        }

        public static Stream AsStream(string xamlCode)
        {
            return new MemoryStream(Encoding.Default.GetBytes(xamlCode));
        }

        public string CodeWithoutSpaceChars()
        {
            return XamlCode.Replace('\t', ' ').Replace("\r", "").Replace("\n", " ");
        }

        void WriteStartTags(StringBuilder sb)
        {
            // <ResourceDictionary ns:"Namespace"> oder <UserControl ns:"Namespace><UserControl.Resources><ResourceDictionary>
            sb.AppendFormat("<{0}", _userControl ? UserControlTag : ResourceDictionaryTag).AppendLine();

            foreach (var ns in _namespaces)
                sb.AppendLine("\t" + ns);

            if (_namespacesAdded && !_noIgnorable)
                sb.Append(IgnorableTag);
            sb.AppendLine(">");

            if (_userControl)
            {
                sb.AppendFormat("<{0}.{1}>", UserControlTag, ResourcesTag).AppendLine().AppendFormat("<{0}>", ResourceDictionaryTag);
            }
        }

        void WriteEndTags(StringBuilder sb)
        {
            // </ResourceDictionary> oder </ResourceDictionary></Resources></UserControl>
            sb.AppendFormat("</{0}>", ResourceDictionaryTag).AppendLine();
            if (_userControl)
            {
                sb.AppendFormat("</{0}.{1}>", UserControlTag, ResourcesTag).AppendLine().AppendFormat("</{0}>", UserControlTag);
            }
        }

        public XamlDefinitions CreateUserControl()
        {
            _userControl = true;
            return this;
        }

        public XamlDefinitions WithoutIgnorable()
        {
            _noIgnorable = true;
            return this;
        }}
}