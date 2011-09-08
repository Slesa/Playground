using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Prolog;
using Prolog.Code;

namespace PrologNet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            buttonExecute.Enabled = !string.IsNullOrWhiteSpace(textCode.Text) && !string.IsNullOrWhiteSpace(textQuery.Text);
        }

        void OnExecute(object sender, EventArgs e)
        {
            var oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            ExecuteCode();
            Cursor.Current = oldCursor;
        }


        void ExecuteCode()
        {
            var codeSentences = GetCodeSentences();
            if (codeSentences == null)
                return;

            var query = GetQuery();
            if (query == null)
                return;

            var program = new Prolog.Program();
            foreach (var sentence in codeSentences)
                program.Add(sentence);

            textResult.Text = "";
            try
            {
                var machine = PrologMachine.Create(program, query);
                machine.ExecutionComplete += CodeExecuted;
                var result = machine.RunToSuccess();
                textStatus.Text = Enum.GetName(typeof(ExecutionResults), result);
            }
            catch (Exception ex)
            {
                textStatus.Text = "Error";
                textResult.Text += string.Format("Got exception: {0}", ex.Message);
                return;
            }
        }

        void CodeExecuted(object sender, PrologQueryEventArgs e)
        {
            var machine = sender as PrologMachine;
            machine.ExecutionComplete -= CodeExecuted;

            if (e.Results == null)
            {
                textResult.Text = "No result available";
                return;
            }

            var sb = new StringBuilder();

            string prefix = null;
            foreach (var variable in e.Results.Variables)
            {
                sb.Append(prefix); prefix = Environment.NewLine;
                sb.AppendFormat("{0} = {1}", variable.Name, variable.Text);
            }

            textResult.Text = sb.ToString();
        }

        Query GetQuery()
        {
            var codeSentences = Parser.Parse(textQuery.Text);
            if (codeSentences == null)
            {
                textStatus.Text = "Query sentences were empty";
                return null;
            }
            return new Query(codeSentences[0]);
        }

        IEnumerable<CodeSentence> GetCodeSentences()
        {
            var codeSentences = Parser.Parse(textCode.Text);
            if (codeSentences == null)
            {
                textStatus.Text = "Code sentences were empty";
                return null;
            }
            return codeSentences;
        }
    }

}
