using System.IO;
using Microsoft.Office.Interop.Word;
using MsSystem = Microsoft.Office.Interop.Word.System;

namespace ConWord
{
    public class TemplateConverter
    {
        readonly Arguments _arguments;
        Application _applicationWord;

        public TemplateConverter(Arguments arguments)
        {
            _arguments = arguments;

            _applicationWord = new Microsoft.Office.Interop.Word.Application();
            _applicationWord.Visible = true;
        }

        public bool Run()
        {
            // var fileName = @"d:\work\svn\Templates\Documentation\CentigradeTemplate-Muster.dotx";
            if (!File.Exists(_arguments.InputFile))
            {
                System.Console.WriteLine("Input file {0} not found", _arguments.InputFile);
                return false;
            }
            var document = _applicationWord.Documents.Open(_arguments.InputFile);
            document.Activate();


            ReplaceFields(document, _applicationWord);
            return true;
        }

        static void ReplaceFields(Document document, Application applicationWord)
        {
            foreach (Field myMergeField in document.Fields)
            {
                Range rngFieldCode = myMergeField.Code;
                var text = rngFieldCode.Text.Trim();

                if (text.StartsWith("ASK")) text = text.Substring(4);
                var buffers = text.Split(new char[] { '\\', '\\' });
                var field = buffers[0].Trim();
                var fieldname = field.Split(' ')[0].ToLower();

                if (fieldname == "doc_author")
                {
                    myMergeField.Select();
                    applicationWord.Selection.TypeText("Florian Moritz");
                }
                if (fieldname == "doc_version")
                {
                    myMergeField.Select();
                    applicationWord.Selection.TypeText("1.0");
                }
            }

        }
    }
}