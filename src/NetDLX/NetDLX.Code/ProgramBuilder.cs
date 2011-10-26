using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetDLX.Code
{
    public class ProgramBuilder
    {
        Program program;
        IEnumerable<IBuilderHandler> _handlers;

        public ProgramBuilder()
        {
            var handlers = new List<IBuilderHandler>();
            handlers.Add(new LabelBuilder());
            _handlers = handlers;
        }

        public Program Generate(string sourceCode)
        {
            var lines = Regex.Split(BuilderHandlerBase.ReplaceSpacesRegex.Replace(sourceCode.Trim(), @" "), Environment.NewLine);
            program = new Program();

            foreach (var line in lines)
            {
                if (line.StartsWith(";")) continue;
                var restOfLine = line;
                foreach (var handler in _handlers)
                {
                    if (!handler.CanHandle(restOfLine)) continue;
                    restOfLine = handler.Handle(program, restOfLine);
                }
            }
            
            return program;
        }
        
        string HandleLabel(string line)
        {
            var labelBuilder = new LabelBuilder();
            return labelBuilder.CanHandle(line) ? labelBuilder.Handle(program, line) : line;
        }


    }
}