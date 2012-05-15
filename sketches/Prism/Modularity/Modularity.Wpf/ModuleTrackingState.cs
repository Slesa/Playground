using System.ComponentModel;
using Microsoft.Practices.Prism.Modularity;
using Modularity.Wpf.Values;

namespace Modularity.Wpf
{
    public class ModuleTrackingState : INotifyPropertyChanged
    {
        string _moduleName;
        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                if (_moduleName == value) return;
                _moduleName = value;
                RaisePropertyChanged("ModuleName");
            }
        }

        ModuleInitializationStatus _moduleInitializationStatus;
        public ModuleInitializationStatus ModuleInitializationStatus
        {
            get { return _moduleInitializationStatus; }
            set
            {
                if (_moduleInitializationStatus == value) return;
                _moduleInitializationStatus = value;
                RaisePropertyChanged("ModuleInitializationStatus");
            }
        }

        DiscoveryMethod _expectedDiscoveryMethod;
        public DiscoveryMethod ExpectedDiscoveryMethod
        {
            get { return _expectedDiscoveryMethod; }
            set
            {
                if (_expectedDiscoveryMethod == value) return;
                _expectedDiscoveryMethod = value;
                RaisePropertyChanged("ExpectedDiscoveryMethod");
            }
        }

        InitializationMode _expectedInitializationMode;
        public InitializationMode ExpectedInitializationMode
        {
            get { return _expectedInitializationMode; }
            set
            {
                if (_expectedInitializationMode == value) return;
                _expectedInitializationMode = value;
                RaisePropertyChanged("ExpectedInitializationMode");
            }
        }

        DownloadTiming _expectedDownloadTiming;
        public DownloadTiming ExpectedDownloadTiming
        {
            get { return _expectedDownloadTiming; }
            set
            {
                if (_expectedDownloadTiming == value) return;
                _expectedDownloadTiming = value;
                RaisePropertyChanged("ExpectedDownloadTiming");
            }
        }

        string _configuredDependencies;
        public string ConfiguredDependencies
        {
            get { return _configuredDependencies; }
            set
            {
                if (_configuredDependencies == value) return;
                _configuredDependencies = value;
                RaisePropertyChanged("ConfiguredDependencies");
            }
        }

        long _bytesReceived;
        public long BytesReceived
        {
            get { return _bytesReceived; }
            set
            {
                if (_bytesReceived == value) return;
                _bytesReceived = value;
                RaisePropertyChanged("BytesReceived");
                RaisePropertyChanged("DownloadProgressPercentage");
            }
        }

        long _totalBytesToReceive;
        public long TotalBytesToRecevice
        {
            get { return _totalBytesToReceive; }
            set
            {
                if (_totalBytesToReceive == value) return;
                _totalBytesToReceive = value;
                RaisePropertyChanged("TotalBytesToRecevice");
                RaisePropertyChanged("DownloadProgressPercentage");
            }
        }

        public int DownloadProgressPercentage
        {
            get
            {
                if (_bytesReceived < _totalBytesToReceive)
                    return (int) (_bytesReceived*100.0/_totalBytesToReceive);
                return 100;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            if( PropertyChanged!=null )
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}