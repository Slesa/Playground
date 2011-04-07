namespace Lucifer.Ics.Editor
{
    public interface IIcsModule 
    {
        string ModuleName { get; }
        string IconFileName { get; }
        string ToolTip { get; }
        
    }
}