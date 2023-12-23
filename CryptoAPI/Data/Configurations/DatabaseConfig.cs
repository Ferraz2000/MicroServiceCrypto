namespace CryptoAPI.Data.Configurations
{
    public class DatabaseConfig : IDataBaseConfig
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
