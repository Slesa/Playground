namespace Infrastructure.Configuration
{
    public interface IConfigurationReader
    {
        string ValueOf(string key);
    }
}