﻿using System.IO;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using ProjectCleaner.Parsing;
using ProjectCleaner.ViewModels;

namespace ProjectCleaner
{
    public class CleanupProcessor
    {
        readonly IEventAggregator _eventAggregator;
        ICollectProjects ProjectCollector { get; set; }
        
        public IProcessProjects[] ProjectProcessors { get; set; }

        public CleanupProcessor(IEventAggregator eventAggregator, ICollectProjects collector, IProcessProjects[] processors)
        {
            _eventAggregator = eventAggregator;
            ProjectCollector = collector;
            ProjectProcessors = processors;
        }

        public void Run(string directoryName)
        {
            //var removeFiles = new List<string>();

            var projectFiles = ProjectCollector.CollectFiles(directoryName);
            foreach (var projectFile in projectFiles)
            {
                var changed = false;
                var project = new ProjectParser(projectFile);
                if (!project.ParseProject()) continue;

                _eventAggregator.GetEvent<ProjectVisitedEvent>().Publish(projectFile);

                foreach (var processor in ProjectProcessors.Where(x=>x.IsEnabled).OrderBy(x=>x.Priority))
                {
                    changed = changed | processor.Handle(project);
                }

                if (changed)
                {
                    var tmpFileName = projectFile + ".tmp";
                    var tmpFileInfo = new FileInfo(tmpFileName);
                    if(tmpFileInfo.Exists) tmpFileInfo.IsReadOnly = false;
                    if(File.Exists(tmpFileName)) File.Delete(tmpFileName);

                    File.Copy(projectFile, tmpFileName);
                    
                    project.RootElement.Save(projectFile);

                    //removeFiles.Add(tmpFileName);
                }
            }

            //foreach (var file in removeFiles) File.Delete(file);
        }
    }
}