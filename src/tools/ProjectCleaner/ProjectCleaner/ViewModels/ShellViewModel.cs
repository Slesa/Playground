using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace ProjectCleaner.ViewModels
{
    public class ShellViewModel : NotificationObject
    {
        public CleanupProcessor CleanupProcessor { get; set; }

        public ShellViewModel(IEventAggregator eventAggregator, CleanupProcessor cleanupProcessor)
        {
            CleanupProcessor = cleanupProcessor;
            VisitedProjects = new ObservableCollection<string>();
            eventAggregator.GetEvent<ProjectVisitedEvent>().Subscribe(x =>VisitedProjects.Add(x));
        }

        public string DropTargetText
        {
            get { return _dropTarget == null ? "Drop here" : DropTarget.DirectoryName; }
        }

        public ObservableCollection<string> VisitedProjects { get; private set; }

        FileInfo _dropTarget;
        public FileInfo DropTarget
        {
            get { return _dropTarget; }
            set
            {
                _dropTarget = value;
                RaisePropertyChanged(() => DropTargetText);
            }
        }

        public ShellViewModel()
        {
            HandleDragLeave();
        }

        public bool HandleDragOver(DragEventArgs dragEventArgs)
        {
            var fileNames = (string[])dragEventArgs.Data.GetData(DataFormats.FileDrop, false);
            var fileName = fileNames.FirstOrDefault();
            if (fileName == null) return false;

            var fileInfo = new FileInfo(fileName);
            DropTarget = fileInfo;
            return true;
        }


        public void HandleDragLeave()
        {
            DropTarget = null;
        }

        public void HandleDrop(DragEventArgs dragEventArgs)
        {
            if (DropTarget == null) return;

            VisitedProjects.Clear();
            CleanupProcessor.Run(DropTarget.DirectoryName);
        }
    }
}