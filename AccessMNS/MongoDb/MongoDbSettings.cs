namespace AccessMNS.MongoDb
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string MessagesCollection { get; set; } = string.Empty;
    }
}
