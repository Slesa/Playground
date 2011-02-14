using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Godot.IcsRunner.Core;
using Godot.Infrastructure;

namespace Godot.IcsRunner.Console
{
    class Program
    {
        static string _spoolPath;
        static IRecipeExecutor _recipeExecutor;

        static void Main(/*string[] args*/)
        {
            if (!ReadConfiguration())
                return;

            if (!CheckDirectory())
                return;

            DoTheStuff();
        }

        static void DoTheStuff()
        {
            using (var container = Bootstrapper.CreateBootstrapper())
            {
                if (!ResolveExecutor(container))
                    return;

                System.Console.CancelKeyPress += BreakProgram;
                System.Console.WriteLine("Looking for spool jobs in {0}, press Ctrl+C to abort", _spoolPath);
                MainLoop();
            }
        }

        static void MainLoop()
        {
            for (; ; )
            {
                if (Cancelled)
                    return;
                System.Threading.Thread.Sleep(500);
                var foundJobs = GetSpoolJobs();
                foreach (var foundJob in foundJobs)
                {
                    var jobs = JobReader.ResolveJobs(foundJob.FullName);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                            _recipeExecutor.Execute(job);
                    }
                    File.Delete(foundJob.FullName);
                }
            }
        }

        static bool ResolveExecutor(Bootstrapper container)
        {
            _recipeExecutor = container.Container.Resolve<IRecipeExecutor>();
            if (_recipeExecutor == null)
            {
                System.Console.WriteLine("Fatal error, could not find valid recipe executor. Aborting.");
                return false;
            }
            return true;
        }

        static IEnumerable<FileInfo> GetSpoolJobs()
        {
            return new DirectoryInfo(_spoolPath).GetFiles("*.job").ToList();

        }

        static bool CheckDirectory()
        {
            try
            {
                if (!Directory.Exists(_spoolPath))
                    Directory.CreateDirectory(_spoolPath);
                return true;
            }
            catch
            {
                System.Console.WriteLine("Could not create spool path, aborting...");
                return false;
            }
        }

        static bool ReadConfiguration()
        {
            _spoolPath = ConfigurationManager.AppSettings["SpoolPath"];
            if (string.IsNullOrEmpty(_spoolPath))
            {
                System.Console.WriteLine("Spool path is not configured yet, aborting...");
                return false;
            }
            return true;
            
        }

        static void BreakProgram(object sender, ConsoleCancelEventArgs e)
        {
            Cancelled = true;
        }

        static readonly Object Locker = new Object();
        static bool _cancelled;

        static bool Cancelled
        {
            get { lock (Locker) return _cancelled; }
            set { lock (Locker) _cancelled = value; }
        }
    }
}
