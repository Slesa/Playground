using System;
using System.IO;
using log4net;

// http://www.beefycode.com/post/Log4Net-Tutorial-pt-3-Appenders.aspx
namespace HelloLog
{
    static class Base
    {
        internal static void LogMessages(ILog log)
        {
            log.Info("This is an info");
            log.InfoFormat("Base called at {0}", DateTime.Now);
            log.Debug("This is a debug");
            log.Warn("This is a warning");
            log.Error("This is an error");
        }
    }
    class Foo
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(Foo));

        public Foo()
        {
            User = "Bart Simpson";
            Log.Info("In ctor of Foo");
            Base.LogMessages(Log);
        }

        public string User { get; private set; }
    }

    class Bar
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(Bar));

        public Bar()
        {
            Log.Info("In ctor of Bar");
            Base.LogMessages(Log);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment the next line to enable log4net internal debugging
            // log4net.helpers.LogLog.InternalDebugging = true;

            // This will instruct log4net to look for a configuration file
            // called ConsoleApp.exe.config in the application base
            // directory (i.e. the directory containing ConsoleApp.exe)
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));


            ILog log = LogManager.GetLogger(typeof(Program));
            log.Info("This is an info");
            log.Debug("This is a debug");
            log.Warn("This is a warning");
            log.Error("This is an error");

            var foo = new Foo();
            var bar = new Bar();

            LogManager.Shutdown();

            Console.Write("Press <Enter> to continue...");
            Console.ReadLine();
        }
    }
}
