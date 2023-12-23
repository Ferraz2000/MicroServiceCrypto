namespace CryptoAPI.Data.Configurations
{
    public interface IDataBaseConfig
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
