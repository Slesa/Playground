using System;
using System.Collections.Generic;
using System.Linq;
using Prolog;
using Prolog.Code;

namespace SudokuNet
{
    public class PrologExecutor
    {
        readonly Prolog.Program _program;
        public delegate void ExecutionEventHandler(object sender, ExecutionEventArgs args);
        public event ExecutionEventHandler Executed;

        public PrologExecutor(string sentences)
        {
            _program = new Prolog.Program();
            InitializeSentences(sentences);
        }

        public bool Execute(string queryText)
        {
            var query = GetQuery(queryText);
            try
            {
                var machine = PrologMachine.Create(_program, query);
                machine.ExecutionComplete += CodeExecuted;
                return machine.RunToSuccess()==ExecutionResults.Success;
            }
            catch (Exception ex)
            {
                throw new PrologExecutionException(ex.Message);
            }
        }

        void CodeExecuted(object sender, PrologQueryEventArgs e)
        {
            var machine = sender as PrologMachine;
            machine.ExecutionComplete -= CodeExecuted;

            Dictionary<string, string> values = null;
            if (e.Results != null)
                values = e.Results.Variables.ToDictionary(value => value.Name, value => value.Text);
            if( Executed!=null )
                Executed(this, new ExecutionEventArgs(values));
        }

        void InitializeSentences(string sentences)
        {
            var codeSentences = GetCodeSentences(sentences);
            foreach (var codeSentence in codeSentences)
                _program.Add(codeSentence);
        }

        static Query GetQuery(string queryText)
        {
            var codeSentence = Parser.Parse(queryText);
            if (codeSentence == null || codeSentence.Length<1)
                throw new NoQueryFoundException();
            return new Query(codeSentence[0]);
        }

        static IEnumerable<CodeSentence> GetCodeSentences(string sentences)
        {
            var codeSentences = Parser.Parse(sentences);
            if (codeSentences == null)
                throw new NoSentenceFoundException();
            return codeSentences;
        }
    }

    public class ExecutionEventArgs
    {
        public ExecutionEventArgs(Dictionary<string,string> values)
        {
            Values = values;
        }
        protected Dictionary<string, string> Values { get; set; }
    }

    public class PrologExecutionException : Exception
    {
        public PrologExecutionException(string message)
            : base(message)
        {
        }
    }

    class NoQueryFoundException : Exception
    {
    }

    class NoSentenceFoundException : Exception
    {
    }
}