namespace Lucifer.Pms.Editor
{
    public interface IPmsModule 
    {
        string ModuleName { get; }
        string IconFileName { get; }
        string ToolTip { get; }
        
    }
}