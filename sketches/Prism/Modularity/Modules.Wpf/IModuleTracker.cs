namespace Modules.Wpf
{
    public interface IModuleTracker
    {
        void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytes);
        void RecordModuleLoaded(string moduleName);
        void RecordModuleConstructed(string moduleName);
        void RecordModuleInitialized(string moduleName);
    }
}