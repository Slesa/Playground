using CommandLine;
using CommandLine.Text;

namespace ConWord
{
    public class Arguments
    {
        [Option('i', "input", DefaultValue = @"Files\CentigradeTemplate-Muster.dotx", HelpText = "A Microsoft Word template document as input file")]
        public string InputFile { get; set; }

        [Option('a', "author", DefaultValue = "Florian Moritz", HelpText = "The author of the Word Document")]
        public string Author { get; set; }

        [Option('v', "version", DefaultValue = "1.0", HelpText = "The version shown on the first page")]
        public string Version { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}