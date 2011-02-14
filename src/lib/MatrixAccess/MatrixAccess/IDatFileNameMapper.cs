namespace MatrixAccess
{
    public interface IDatFileNameMapper
    {
        string GetFileNameFor<TEntityType>();
        string GetFullFileNameFor<TEntityType>();
        string DataPath { get; }
    }
}